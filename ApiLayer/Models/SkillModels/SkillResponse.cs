using System;
using System.Collections.Generic;

namespace ApiLayer.Models.SkillModels
{
    public record SkillResponse
    {
        public string Name { get; init; }

        public List<Guid>? CandidateGuids { get; init; }
    }
}
