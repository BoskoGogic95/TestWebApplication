using AutoMapper;
using Business_Access_Layer.Models;
using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Access_Layer.Mappings
{
    public static class AutoMapperBALProfiles
    {
        public class SkillModelProfile : Profile
        {
            public SkillModelProfile()
            {
                CreateMap<SkillModel, Skill>().ReverseMap();
            }
        }

        public class CandidateModelProfile : Profile
        {
            public CandidateModelProfile()
            {
                CreateMap<CandidateModel, Candidate>().ReverseMap();
            }
        }
    }
}
