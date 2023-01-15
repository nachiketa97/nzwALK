using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using nzwalks.Models.DTO;
using nzwalks.Repositories;

namespace nzwalks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultiesController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultiesController(IWalkDifficultyRepository walkDifficultyRepository,
            IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var walkdiffulty = await walkDifficultyRepository.GetAllAsync();
            return Ok(walkdiffulty);

        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetAsync")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var walkdifficultyDomain = await walkDifficultyRepository.GetAsync(id);
            if (walkdifficultyDomain == null)
            {
                return NotFound();

            }
            var walkdifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkdifficultyDomain);
            return Ok(walkdifficultyDTO);
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync(AddWalkDifficultReq addWalkDifficultReq)
        {
            var WalkDifficultDomain = new Models.Domain.WalkDifficulty()
            {
                Code = addWalkDifficultReq.Code,
            };
            WalkDifficultDomain = await walkDifficultyRepository.AddAsync(WalkDifficultDomain);
            var WalkDTO = mapper.Map<Models.DTO.WalkDifficulty>(WalkDifficultDomain);
            return CreatedAtAction(nameof(GetAsync),
                new { id = WalkDTO.Id }, WalkDTO);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id,
            Models.DTO.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            var difficultyDomain = new Models.Domain.WalkDifficulty()
            {
                Code = updateWalkDifficultyRequest.Code,
            };
            difficultyDomain=  await walkDifficultyRepository.UpdateAsync(id, difficultyDomain);
            if (difficultyDomain == null)
            {
                return NotFound();
            }
            var DifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(difficultyDomain);
            return Ok(DifficultyDTO);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var difficultyDomain=await walkDifficultyRepository.DeleteAsync(id);
            if (difficultyDomain == null)
            {
                return NotFound();
            }
            var DifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(difficultyDomain);
            return Ok(DifficultyDTO);

        }
    }
}
