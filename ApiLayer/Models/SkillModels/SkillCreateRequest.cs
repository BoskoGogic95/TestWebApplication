using System;
using System.Collections.Generic;

namespace ApiLayer.Models.SkillModels
{
    public record SkillCreateRequest
    {
        public string Name { get; init; }

        public List<Guid>? CandidateGuids { get; init; }
    }
}
