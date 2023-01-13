using AutoMapper;

namespace nzwalks.Profiles
{
    public class RegionsProfiles: Profile
    {
        public RegionsProfiles()
        {
            CreateMap<Models.Domain.Region,Models.DTO.Region>().ReverseMap();
        }
    }
}
