using System;

namespace ApiLayer.Models.CandidateModels
{
    public record CandidateUpdateRequest
    {
        public string FullName { get; init; }

        public DateTime DateOfBirth { get; init; }

        public string ContactNumber { get; init; }

        public string Email { get; init; }
    }
}
