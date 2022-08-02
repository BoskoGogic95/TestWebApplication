using System;
using Data_Access_Layer.Data;
using Data_Access_Layer.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer.Contracts;

namespace Data_Access_Layer.Models
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DataContext _context;

        public ICandidateRepository Candidates { get; private set; }

        public ISkillRepository Skills { get; private set; }

        public ICandidateAndSkillRepository CandidatesAndSkills { get; private set; }

        public UnitOfWork(DataContext context)
        {
            _context = context;

            Candidates = new CandidateRepository(_context);

            Skills = new SkillRepository(_context);

            CandidatesAndSkills = new CandidateAndSkillRepository(_context);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async void Dispose()
        {
            await _context.DisposeAsync();
        }

    }
}
