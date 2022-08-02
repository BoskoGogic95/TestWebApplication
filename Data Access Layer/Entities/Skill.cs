using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entities
{
    public record Skill
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public List<Candidate> Candidates { get; init; }
    }
}
