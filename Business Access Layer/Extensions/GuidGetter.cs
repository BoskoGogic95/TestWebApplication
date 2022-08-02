using Data_Access_Layer.Contracts;
using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Access_Layer.Extensions
{
    internal static class GuidGetter
    {

        internal static async Task<IEnumerable<Guid>> GetCandidateGuids(Guid skillId, IUnitOfWork _unitOfWork)
        {
            var candidatesAndSkills = new List<CandidateAndSkill>(await _unitOfWork.CandidatesAndSkills.FindAll(cs => cs.SkillId == skillId));

            List<Guid> candidateGuids = new();

            foreach (var candidateAndSkill in candidatesAndSkills)
            {
                candidateGuids.Add(candidateAndSkill.CandidateId);
            }

            return candidateGuids;
        }

        internal static async Task<IEnumerable<Guid>> GetSkillGuids(Guid candidateId, IUnitOfWork _unitOfWork)
        {
            var candidatesAndSkills = new List<CandidateAndSkill>(await _unitOfWork.CandidatesAndSkills.FindAll(cs => cs.CandidateId == candidateId));

            List<Guid> skillGuids = new();

            foreach (var candidateAndSkill in candidatesAndSkills)
            {
                skillGuids.Add(candidateAndSkill.SkillId);
            }

            return skillGuids;
        }
    }
}
