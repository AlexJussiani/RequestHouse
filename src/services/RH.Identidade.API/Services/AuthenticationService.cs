﻿using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RH.Core.Identidade;
using RH.Identidade.API.Models;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RH.Identidade.API.Data;
using System.Linq;

namespace RH.Identidade.API.Services
{
    public class AuthenticationService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;


        public AuthenticationService(ApplicationDbContext context,
                              SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<AppSettings> appSettings
                                    )
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }
        public async Task<UsuarioRespostaLogin> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);

            var identityClaims = await ObterClaimsUsuario(claims, user);
            var encodedToken = CodificarToken(identityClaims);

            return ObterRespostaToken(encodedToken, user, claims);
        }
        private async Task<ClaimsIdentity> ObterClaimsUsuario(ICollection<Claim> claims, IdentityUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(),
                ClaimValueTypes.Integer64));
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);
            return identityClaims;
        }

        private string CodificarToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        private UsuarioRespostaLogin ObterRespostaToken(string encodedToken, IdentityUser user,
           IEnumerable<Claim> claims)
        {
            return new UsuarioRespostaLogin
            {
                AccessToken = encodedToken,
                //RefreshToken = refreshToken.Token,
                ExpiresIn = TimeSpan.FromHours(1).TotalSeconds,
                UsuarioToken = new UsuarioToken
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new UsuarioClaim { Type = c.Type, Value = c.Value })
                }
            };
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);

        //private async Task<RefreshToken> GerarRefreshToken(string email)
        //{
        //    var refreshToken = new RefreshToken
        //    {
        //        Username = email,
        //        ExpirationDate = DateTime.UtcNow.AddHours(_appTokenSettingsSettings.RefreshTokenExpiration)
        //    };

        //    _context.RefreshTokens.RemoveRange(_context.RefreshTokens.Where(u => u.Username == email));
        //    await _context.RefreshTokens.AddAsync(refreshToken);

        //    await _context.SaveChangesAsync();

        //    return refreshToken;
        //}

        //public async Task<RefreshToken> ObterRefreshToken(Guid refreshToken)
        //{
        //    var token = await _context.RefreshTokens.AsNoTracking()
        //        .FirstOrDefaultAsync(u => u.Token == refreshToken);

        //    return token != null && token.ExpirationDate.ToLocalTime() > DateTime.Now
        //        ? token
        //        : null;
        //}
    }
}

