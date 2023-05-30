using AutoMapper;
using RH.Produtos.API.Application.DTO;
using RH.Produtos.API.Models;

namespace RH.Produtos.API.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Produto, ProdutoDTO>().ReverseMap();
        }
    }
}
