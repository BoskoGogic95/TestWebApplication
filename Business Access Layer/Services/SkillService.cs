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
    public class SkillService : ISkillService
    {
      
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SkillService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid?> Create(SkillModel model)
        {
        
            var existis = await _unitOfWork.Skills.Find(c => c.Name.Equals(model.Name));
            if (existis != null) return null;

            var skillToCreate = _mapper.Map<Skill>(model);

            var created = await _unitOfWork.Skills.Create(skillToCreate);

            if (model.CandidateGuids.Count == 0)
            {
                await _unitOfWork.SaveChanges();
                return created.Id;
            } 

            foreach(var candidateGuid in model.CandidateGuids)
            {
                await _unitOfWork.CandidatesAndSkills.Create(new CandidateAndSkill()
                {
                    Id = Guid.NewGuid(),
                    CandidateId = candidateGuid,
                    SkillId = created.Id
                });
            }

            await _unitOfWork.SaveChanges();

            return created.Id;
        }

        public async Task<bool> Delete(Guid id)
        {

            var skillToDelete = await _unitOfWork.Skills.GetById(id);

            if (skillToDelete == null) return false;

            var candidatesAndSkills = new List<CandidateAndSkill>(await _unitOfWork.CandidatesAndSkills.FindAll(cs => cs.SkillId == id));

            foreach(var CandidateAndSkillSingle in candidatesAndSkills)
            {
                await _unitOfWork.CandidatesAndSkills.Delete(CandidateAndSkillSingle);
            }

            await _unitOfWork.Skills.Delete(skillToDelete);

            await _unitOfWork.SaveChanges();

            return true;
        }

        public async Task<IEnumerable<SkillModel>> GetAll()
        {
            var skills = await _unitOfWork.Skills.GetAll();

            var result = new List<SkillModel>();

            foreach (var skill in skills)
            {
                List<Guid> candidateGuids = new(await GuidGetter.GetCandidateGuids(skill.Id, _unitOfWork));

                result.Add(_mapper.Map<SkillModel>(skill) with
                {
                    CandidateGuids = candidateGuids
                });
            }
            return result;
        }

        public async Task<SkillModel> GetById(Guid Id)
        {
            var skill = await _unitOfWork.Skills.GetById(Id);

            if (skill == null) return null;

            List<Guid> candidateGuids = new(await GuidGetter.GetCandidateGuids(Id, _unitOfWork));

            var model = _mapper.Map<SkillModel>(skill) with
            {
                CandidateGuids = candidateGuids
            };

            return model;

        }

        public async Task<SkillModel> GetByName(string name)
        {

            var skill = await _unitOfWork.Skills.Find(c => c.Name.Equals(name));

            if (skill == null) return null;

            List<Guid> candidateGuids = new(await GuidGetter.GetCandidateGuids(skill.Id, _unitOfWork));

            var model = _mapper.Map<SkillModel>(skill) with
            {
                CandidateGuids = candidateGuids
            };

            return model;

        }

        public async Task<Skill> Update(Guid id, SkillModel model)
        {
            var skill = await _unitOfWork.Skills.GetById(id);

            if (skill == null) return null;

            skill = _mapper.Map<Skill>(model) with
            {
                Id = id      
            };

            if (model.CandidateGuids != null)
            {
                var candidatesAndSkills = new List<CandidateAndSkill>(await _unitOfWork.CandidatesAndSkills.FindAll(cs => cs.SkillId == id));

                int candidateGuidCount = model.CandidateGuids.Count - 1;

                foreach (var candidateAndSkill in candidatesAndSkills)
                {
                    if (candidateGuidCount > -1)
                    {
                        CandidateAndSkill toUpdate = candidateAndSkill with
                        {
                            CandidateId = model.CandidateGuids[candidateGuidCount--]
                        };

                        await _unitOfWork.CandidatesAndSkills.Update(toUpdate, toUpdate.Id);
                    }   
                }

                if(candidateGuidCount != -1)
                {
                    for(int i = candidateGuidCount; i > -1; i--)
                    {
                        CandidateAndSkill candidateAndSkill = new()
                        {
                            Id = Guid.NewGuid(),
                            SkillId = id,
                            CandidateId = model.CandidateGuids[i]
                        };

                        await _unitOfWork.CandidatesAndSkills.Create(candidateAndSkill);
                    }
                }
            }

            await _unitOfWork.Skills.Update(skill, id);

            await _unitOfWork.SaveChanges();

            return skill;
          
        }
    }
}
