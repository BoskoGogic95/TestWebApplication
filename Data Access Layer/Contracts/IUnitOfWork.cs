using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        ICandidateRepository Candidates { get; }

        ISkillRepository Skills { get; }

        ICandidateAndSkillRepository CandidatesAndSkills { get;  }

        Task SaveChanges();

    }
}
