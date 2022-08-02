using Data_Access_Layer.Contracts;
using Data_Access_Layer.Data;
using Data_Access_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class CandidateRepository : GenericRepository<Candidate>, ICandidateRepository
    {
         private readonly DataContext _context;

         public CandidateRepository(DataContext repository) : base(repository)
         {
             _context = repository;
         }
       
    }
}
