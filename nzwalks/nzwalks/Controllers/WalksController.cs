using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using nzwalks.Repositories;

namespace nzwalks.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository,IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalkAsync()
        {
            //fetch data from  database
            var WalksDomain = await walkRepository.GetAllAsync();
            // convert domain walks to dto walks
            var WalkDTO = mapper.Map<List<Models.DTO.Walk>>(WalksDomain);
            return Ok(WalkDTO);

        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
           //fetching data from database
           var WalkDomain= await walkRepository.GetAsync(id);
           var WalkDTO= mapper.Map<Models.DTO.Walk>(WalkDomain);
            return Ok(WalkDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync([FromBody] Models.DTO.AddWalkRequest addWalkRequest)
        {
            //convert dto to domain object
            var walkDomain = new Models.Domain.Walk
            {
                Length= addWalkRequest.Length,
                Name = addWalkRequest.Name,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId,
            };
            //Pass the domain to repository

            walkDomain=await walkRepository.AddAsync(walkDomain);
            //convert the domain object back to dto
            var walkDTO = new Models.DTO.Walk
            {
                Id= walkDomain.Id,
                Length=walkDomain.Length,
                Name = walkDomain.Name,
                RegionId = walkDomain.RegionId,
                WalkDifficultyId = walkDomain.WalkDifficultyId,

            };

            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDTO.Id }, walkDTO);



        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, 
            [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            //convert dto to domain object
            var walkDomain = new Models.Domain.Walk
            {
                Length = updateWalkRequest.Length,
                Name = updateWalkRequest.Name,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId,

            };
            // pass details to repositoruy
            walkDomain= await walkRepository.UpdateAsync(id,walkDomain);
            //handle null
            if (walkDomain==null)
            {
                return NotFound();

            }
            // convert back to dto 
           
                var WalkDTO = new Models.DTO.Walk
                {
                    Id = walkDomain.Id,
                    Length = walkDomain.Length,
                    Name = walkDomain.Name,
                    RegionId = walkDomain.RegionId,
                    WalkDifficultyId = walkDomain.WalkDifficultyId,
                };
           
            // return response
            return Ok(WalkDTO);
            
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            var walkdomain= await walkRepository.DeleteAsync(id);
            if (walkdomain==null)
            { 
                return NotFound(); 
            }
            var walkDTO =mapper.Map<Models.DTO.Walk>(walkdomain); 
            return Ok(walkDTO);

            
        }

    }
}
