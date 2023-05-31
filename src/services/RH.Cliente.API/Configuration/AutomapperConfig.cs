using AutoMapper;
using RH.Clientes.API.Application.DTO;
using RH.Clientes.API.Models;

namespace RH.Produtos.API.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
        }
    }
}
