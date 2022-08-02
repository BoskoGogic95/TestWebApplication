using Business_Access_Layer.Models;
using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business_Access_Layer.Services.IServices
{
    public interface ICandidateService
    {
        Task<Guid?> Create(CandidateModel model);
        
        Task<CandidateModel> GetById(Guid id);

        Task<IEnumerable<CandidateModel>> GetAll();

        Task<CandidateModel> GetByName(string name);

        Task<Candidate> Update(Guid id, CandidateModel model);

        Task<bool> Delete(Guid id);



    }
}
