using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class WalksDifficultiesController:Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalksDifficultiesController(Repositories.IWalkDifficultyRepository walkDifficultyRepository,IMapper mapper )
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficulties()
        {
            var walkDifficultiesDomain = await walkDifficultyRepository.GetAllAsync();
            
            var walkDifficultiesDTO=mapper.Map<List<Models.DTO.WalkDifficulty>>(walkDifficultiesDomain);

            return Ok(walkDifficultiesDTO);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyById")]
        public async Task<IActionResult> GetWalkDifficultyById(Guid id)
        {
            var walkDifficulty = await walkDifficultyRepository.GetAsync(id);
            
            if (walkDifficulty == null)
            {
                return NotFound(); 
            }

            //convert domain into DTO
            var walkdifficultyDTO =mapper.Map<Models.DTO.WalkDifficulty>(walkDifficulty);
            return Ok(walkDifficulty);


        }

        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultyAsync([FromBody] Models.DTO.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            //Convert DTO to Domain object 
            var walkDifficultyDomain = new Models.Domain.WalkDifficulty
            {
               Code= addWalkDifficultyRequest.Code,
            };
            //Pass Domain Object to Repository to persist this
            walkDifficultyDomain = await walkDifficultyRepository.AddAsync(walkDifficultyDomain);

            //Convert the Domain object back to DTO 
           /* var walkDifficultyDTO = new Models.DTO.WalkDifficulty
            {
               Code= addWalkDifficultyRequest.Code,
            };
           */
            var walkDifficultyDTO= mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);

            //Send DTO response back to Client
            return CreatedAtAction(nameof(GetWalkDifficultyById), new { id = walkDifficultyDTO.Id }, walkDifficultyDTO);

        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync(Guid id, Models.DTO.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            //Convert DTO to Domain Model
            var walkDIfficultyDomain = new Models.Domain.WalkDifficulty
            {
                Code = updateWalkDifficultyRequest.Code,
            };

            //call repository to update 
            walkDIfficultyDomain= await walkDifficultyRepository.UpdateAsync(id,walkDIfficultyDomain);
            if (walkDIfficultyDomain==null)
            {
                return NotFound();
            }
            //convert Domain to DTO
            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDIfficultyDomain);

            //return response
            return Ok(walkDifficultyDTO);

        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficulty(Guid id)
        {
            var walkDifficultyDomain=await walkDifficultyRepository.DeleteAsync(id);
            if (walkDifficultyDomain==null)
            {
                return NotFound();
            }
            //convert Domain to DTO
            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);

            //return response
            return Ok(walkDifficultyDTO);
        }

    }
}
