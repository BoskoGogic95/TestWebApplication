using Business_Access_Layer.Models;
using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business_Access_Layer.Services.IServices
{
    public interface ISkillService
    {
        Task<IEnumerable<SkillModel>> GetAll();

        Task<SkillModel> GetById(Guid Id);

        Task<Guid?> Create(SkillModel skill);

        Task<bool> Delete(Guid id);

        Task<Skill> Update(Guid id, SkillModel skill);

        Task<SkillModel> GetByName(string name);
    }
}
