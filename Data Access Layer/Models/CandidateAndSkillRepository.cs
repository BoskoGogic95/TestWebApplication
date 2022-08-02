using Data_Access_Layer.Contracts;
using Data_Access_Layer.Data;
using Data_Access_Layer.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class CandidateAndSkillRepository : GenericRepository<CandidateAndSkill>, ICandidateAndSkillRepository
    { 
         private readonly DataContext _context;

         public CandidateAndSkillRepository(DataContext repository) : base(repository)
         {
            _context = repository;
          }
        
    }
}
