using ApiLayer.Models.CandidateModels;
using ApiLayer.Models.SkillModels;
using AutoMapper;
using Business_Access_Layer.Models;

namespace ApiLayer.Mappings
{
    public static class AutoMapperAPIProfiles
    {
        public class CandidateCreateRequestProfile : Profile
        {
            public CandidateCreateRequestProfile()
            {
                CreateMap<CandidateCreateRequest, CandidateModel>();
            }
        }

        public class CandidateResponseProfile : Profile
        {
            public CandidateResponseProfile()
            {
                CreateMap<CandidateModel, CandidateResponse>();
            }
        }

        public class CandidateUpdateRequestProfile : Profile
        {
            public CandidateUpdateRequestProfile()
            {
                CreateMap<CandidateUpdateRequest, CandidateModel>();
            }
        }

        public class SkillCreateRequestProfile : Profile
        {
            public SkillCreateRequestProfile()
            {
                CreateMap<SkillCreateRequest,SkillModel>();
            }
        }

        public class SkillUpdateRequestProfile : Profile
        {
            public SkillUpdateRequestProfile()
            {
                CreateMap<SkillUpdateRequest, SkillModel>();
            }
        }

        public class SkillResponseProfile : Profile 
        {
            public SkillResponseProfile()
            {
                CreateMap<SkillModel, SkillResponse>();
            }    
        }
    }
}
