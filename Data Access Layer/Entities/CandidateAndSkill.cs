using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entities
{
    public record CandidateAndSkill
    {
        public Guid Id { get; init; }

        public Guid CandidateId { get; init; }

        public Guid SkillId { get; init; }
    }
}
