using AutoMapper;
using System.Runtime.ConstrainedExecution;

namespace NZWalks.API.Profiles
{
    public class RegionsProfile:Profile
    {
        public RegionsProfile()
        {
            CreateMap<Models.Domain.Region, Models.DTO.Region>()
            //Cuando el domain model y el dto model son diferentes algunos de sus properties o campos tienen distinto nombre se especifica de la sigiente manera
            //Por ejemplo si tengo que mapear el domain con el servicio api y alguno de sus  campos son distintos para que no se rompa todo en la api que consume el cliente 
            //Se crea la capa DTO para asi poder mapear y si alguno de los nombres de los campos son distinos lo especifico aca de la sienguiente manera 
            
            //Ejemplo de mapeo no necesario ya que los campos id se llaman igua
            .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id));

            //Por ejemplo si en el DTO el id se llamara RegionId tendria que mapearlo de esta manera
            //.ForMember(dest => dest.Id, options => options.MapFrom(src => src.RegionId));\

            //se peude hacer un reverse map pero no entiendo para que casos o para que se utilizaria
            //.ReverseMap(); 

            //Despues de crear el mapeo como le decimos a nuestro programa que creamos el mapeo, vamos a program.cs y inyectamos el auto mapper despues de haber agregado el repositorio

        }

    }
}
