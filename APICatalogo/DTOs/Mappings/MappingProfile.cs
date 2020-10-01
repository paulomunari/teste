using API_Crud.Models;
using AutoMapper;

namespace API_Crud.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Pessoa, PessoaDTO>().ReverseMap();
            CreateMap<Escolaridade, EscolaridadeDTO>().ReverseMap();
        }
    }
}
