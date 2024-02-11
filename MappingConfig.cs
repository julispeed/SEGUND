using AutoMapper;
using PersonaCompleta.Models;
using PersonaCompleta.Models.DTO;
namespace PersonaCompleta
{
    public class MappingConfig : Profile
    {

         public MappingConfig()
        {

            CreateMap<Persona, PersonaDTO>();
            CreateMap<Persona, PersonaUpdateDTO>();
            CreateMap<PersonaUpdateDTO, Persona>();
            CreateMap<PersonaDTO, Persona>();
            CreateMap<Persona, IEnumerable<PersonaDTO>>().ReverseMap();
            CreateMap<Secundario, SecundariosDTO>().ReverseMap();


        }




    }
}
