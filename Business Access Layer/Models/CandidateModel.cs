using System;
using System.Collections.Generic;

namespace Business_Access_Layer.Models
{
    public record CandidateModel
    {
        public string FullName { get; init; }

        public DateTime DateOfBirth { get; init; }

        public string ContactNumber { get; init; }

        public string Email { get; init; }

        public List<Guid>? SkillGuids { get; init; }


    }
}
