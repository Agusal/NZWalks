using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("Walks")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;
        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            //Fetch data from database - domain walks
            var walks = await walkRepository.GetAllAsync();
            //var walksDTO = new List<Models.DTO.Walk>();

            //walks.ToList().ForEach(walk =>
            // {
            //   var walkDTO = new Models.DTO.Walk()
            // {
            //     Id = walk.Id,
            //     Name = walk.Name,
            //     Length = walk.Length,
            //     RegionId = walk.RegionId,
            //     WalkDifficultyId = walk.WalkDifficultyId,
            // };
            // walksDTO.Add(walkDTO);

            //});

            var walksDTO = mapper.Map<List<Models.DTO.Walk>>(walks);
            return Ok(walksDTO);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            // Get Walk Domain object from database
            var walkDOmain= await walkRepository.GetAsync(id);

            //Convert Domain object to DTO
            var walkDTO = mapper.Map<Models.DTO.Walk>(walkDOmain);

            //Return Response
            return Ok(walkDTO);
        }
        [HttpPost]
        public async Task<IActionResult> AddWalkAsync([FromBody] Models.DTO.AddWalkRequest addWalkRequest)
        {
            //Convert DTO to Domain object 
            var walkDomain = new Models.Domain.Walk
            {
                Length = addWalkRequest.Length,
                Name = addWalkRequest.Name,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId
            };
            //Pass Domain Object to Repository to persist this
            walkDomain= await walkRepository.AddAsync(walkDomain);

            //Convert the Domain object back to DTO 
            var walkDTO = new Models.DTO.Walk
            {
                Id = walkDomain.Id,
                Length = walkDomain.Length,
                Name = walkDomain.Name,
                RegionId = walkDomain.RegionId,
                WalkDifficultyId = walkDomain.WalkDifficultyId
            };

            //Send DTO response back to Client
            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDTO.Id }, walkDTO);

        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync( [FromRoute] Guid id, [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest )
        {
            //DTO to Domain Object

            var walkDomain = new Models.Domain.Walk
            {
                Length = updateWalkRequest.Length,
                Name = updateWalkRequest.Name,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId

            };

            //Pass Details to Repository - Get Domain Object in response or null
            walkDomain=await walkRepository.UpdateAsync(id, walkDomain);

            //handle null (not found)
            if (walkDomain == null)
            {
                return NotFound("");
            }
            
                //Convert back domain to DTO
                var walkDTO = new Models.DTO.Walk
                {
                    Id = walkDomain.Id,
                    Length = walkDomain.Length,
                    Name = walkDomain.Name,
                    RegionId=walkDomain.RegionId,
                    WalkDifficultyId = walkDomain.WalkDifficultyId
                };
           

            //Return Response
            return Ok(walkDTO);

        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync([FromRoute] Guid id)
        {
            //call Repositry to delete walk 
            var walkDomain=await walkRepository.DeleteAsync(id);
            if (walkDomain==null)
            {
                return NotFound();
                    
            }
            var walkDTO=mapper.Map<Models.DTO.Walk>(walkDomain);
            return Ok(walkDTO);
        }
    }
}
