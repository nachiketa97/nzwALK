using AutoMapper;

namespace nzwalks.Profiles
{
    public class WalkProfiles:Profile
    {
        public WalkProfiles()
        {
            CreateMap<Models.Domain.Walk,Models.DTO.Walk>()
                .ReverseMap();
            CreateMap<Models.Domain.WalkDifficulty,Models.DTO.WalkDifficulty>()
                .ReverseMap();
        }
    }
}
