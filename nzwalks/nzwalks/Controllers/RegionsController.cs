﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using nzwalks.Models.Domain;
using nzwalks.Repositories;

namespace nzwalks.Controllers
{
    [ApiController]
    [Route("[Controller]")]
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
        public async Task<IActionResult> GetAllRegions()
        {
            var regions=await regionRepository.GetAllAsync();
            //return DTO Regions
            //var regionsDTO = new List<Models.DTO.Region>();
            ////regions.ToList().ForEach(region =>
            //{
            //    var regionDTO = new Models.DTO.Region()
            //    {
            //        Id = region.Id,
            //        Code=region.Code,
            //        Name=region.Name,
            //        Area=region.Area,
            //        Lat=region.Lat,
            //        Long=region.Long,
            //        Population=region.Population,

            //    };
            //    regionsDTO.Add(regionDTO);

            //});
            mapper.Map<List<Models.DTO.Region>>(regions);
            return Ok(regions);
        }
    }
}