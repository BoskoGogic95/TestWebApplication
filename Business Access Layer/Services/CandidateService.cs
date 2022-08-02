using AutoMapper;
using Business_Access_Layer.Extensions;
using Business_Access_Layer.Models;
using Business_Access_Layer.Services.IServices;
using Data_Access_Layer.Contracts;
using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business_Access_Layer.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CandidateService()
        {
        }

        public CandidateService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid?> Create(CandidateModel model)
        {

            var existis = await _unitOfWork.Candidates.Find(c => c.Email.Equals(model.FullName)); ;
            if (existis != null) return null;

            var candidateToCreate = _mapper.Map<Candidate>(model);

            var created = await _unitOfWork.Candidates.Create(candidateToCreate);

            if (model.SkillGuids.Count == 0)
            {
                await _unitOfWork.SaveChanges();
                return created.Id;
            }

            foreach (var skillGuid in model.SkillGuids)
            {
                await _unitOfWork.CandidatesAndSkills.Create(new CandidateAndSkill()
                {
                    Id = Guid.NewGuid(),
                    CandidateId = created.Id,
                    SkillId = skillGuid
                });
            }

            await _unitOfWork.SaveChanges();

            return created.Id;
        }

        public async Task<bool> Delete(Guid id)
        {
            var candidateToDelete = await _unitOfWork.Candidates.GetById(id);

            if (candidateToDelete == null) return false;

            var candidatesAndSkills = new List<CandidateAndSkill>(await _unitOfWork.CandidatesAndSkills.FindAll(cs => cs.CandidateId == id));

            foreach (var CandidateAndSkillSingle in candidatesAndSkills)
            {
                await _unitOfWork.CandidatesAndSkills.Delete(CandidateAndSkillSingle);
            }

            await _unitOfWork.Candidates.Delete(candidateToDelete);

            await _unitOfWork.SaveChanges();

            return true;
        }

        public async Task<IEnumerable<CandidateModel>> GetAll()
        {
            var candidates = await _unitOfWork.Candidates.GetAll();

            var result = new List<CandidateModel>();

            foreach (var candidate in candidates)
            {
                List<Guid> skillGuids = new(await GuidGetter.GetSkillGuids(candidate.Id, _unitOfWork));

                result.Add(_mapper.Map<CandidateModel>(candidate) with
                {
                    SkillGuids = skillGuids
                });
            }
            return result;

        }

        public async Task<CandidateModel> GetById(Guid id)
        {
            var candidate = await _unitOfWork.Candidates.GetById(id);

            if (candidate == null) return null;

            List<Guid> SkillGuids = new(await GuidGetter.GetSkillGuids(id, _unitOfWork));

            var model = _mapper.Map<CandidateModel>(candidate) with
            {
               SkillGuids = SkillGuids
            };

            return model;

        }

        public async Task<CandidateModel> GetByName(string name)
        {
            var candidate = await _unitOfWork.Candidates.Find(c => c.FullName.Equals(name));

            if (candidate == null) return null;

            List<Guid> SkillGuids = new(await GuidGetter.GetSkillGuids(candidate.Id, _unitOfWork));

            var model = _mapper.Map<CandidateModel>(candidate) with
            {
                SkillGuids = SkillGuids
            };

            return model;
        }

        public async Task<Candidate> Update(Guid id, CandidateModel model)
        {
            var candidate = await _unitOfWork.Candidates.GetById(id);

            if (candidate == null) return null;

            candidate = _mapper.Map<Candidate>(model) with
            {
                Id = id
            };

            if (model.SkillGuids != null)
            {
                var candidatesAndSkills = new List<CandidateAndSkill>(await _unitOfWork.CandidatesAndSkills.FindAll(cs => cs.CandidateId == id));

                int skillGuidCount = model.SkillGuids.Count - 1;

                foreach (var candidateAndSkill in candidatesAndSkills)
                {
                    if (skillGuidCount > -1)
                    {
                        CandidateAndSkill toUpdate = candidateAndSkill with
                        {
                            SkillId = model.SkillGuids[skillGuidCount--]
                        };

                        await _unitOfWork.CandidatesAndSkills.Update(toUpdate, toUpdate.Id);
                    }
                }

                if (skillGuidCount != -1)
                {
                    for (int i = skillGuidCount; i > -1; i--)
                    {
                        CandidateAndSkill candidateAndSkill = new()
                        {
                            Id = Guid.NewGuid(),
                            SkillId = model.SkillGuids[i],
                            CandidateId = id
                        };

                        await _unitOfWork.CandidatesAndSkills.Create(candidateAndSkill);
                    }
                }
            }

            await _unitOfWork.Candidates.Update(candidate, id);

            await _unitOfWork.SaveChanges();

            return candidate;
        }
    }
}
