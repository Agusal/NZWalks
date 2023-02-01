using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("Regions")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper; 
        public RegionsController(IRegionRepository regionRepository,IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        

        [HttpGet]
        public async  Task<IActionResult> GetAllRegionsAsync()
        {
            var regions= await regionRepository.GetAllAsync();

            //Return DTO Regions 
            //var regionsDTO=new  List<Models.DTO.Region>();

            //regions.ToList().ForEach(region => {
            //    var regionDTO = new Models.DTO.Region()
            //    {
            //        Id = region.Id,
            //        Code= region.Code,
            //        Name    = region.Name,
            //        Area = region.Area,
            //        Lat= region.Lat,    
            //        Long= region.Long,  
            //        Population= region.Population,
            //    };
            //    regionsDTO.Add(regionDTO);
            //});

            var regionsDTO=mapper.Map<List<Models.DTO.Region>>(regions);
            return Ok(regionsDTO);     
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region=await regionRepository.GetAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            var regionDTO=mapper.Map<Models.DTO.Region>(region);

            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            //Request to Domain Model
            var region = new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Name = addRegionRequest.Name,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Population = addRegionRequest.Population

            };

            //Pass Detail to de Repository
            region =await regionRepository.AddAsync(region);

            //Convert back to DTO
            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population
            };

            return CreatedAtAction(nameof(GetRegionAsync), new {id=regionDTO.Id},regionDTO);

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult>DeleteRegionAsync(Guid id)
        {
            //
            var region=await regionRepository.DeleteAsync(id);

            //
            if (region==null)
            {
                return NotFound();
            }
            //
            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population
            };
            //
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id,[FromBody] Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            //Convert DTO to Domain Model
             var region = new Models.Domain.Region()
            {
                Code = updateRegionRequest.Code,
                Name = updateRegionRequest.Name,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Population = updateRegionRequest.Population
            };

            //Update Region using repository
            region = await regionRepository.UpdateAsync(id, region);


            //If null then NotFound
            if (region == null)
            {
                return NotFound();
            }

            //Convert Domain Back to DTO
            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population
            };
            //Return Ok Response
            return Ok(regionDTO);

        }
    }
}
