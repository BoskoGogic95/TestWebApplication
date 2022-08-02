using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Access_Layer.Models
{
    public record SkillModel
    {
        public string Name { get; init; }

        public List<Guid>? CandidateGuids { get; init; }
    }
}
