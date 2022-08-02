using Data_Access_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public virtual DbSet<Candidate> Candidates { get; set; }

        public virtual DbSet<Skill> Skills { get; set; }

        public virtual DbSet<CandidateAndSkill> CandidatesAndSkills { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Guid SkillId = Guid.NewGuid();
            Guid CandidateId = Guid.NewGuid();

            modelBuilder.Entity<Skill>().HasData(
                new Skill
                {
                    Id = SkillId,
                    Name = "Skill A",
                    Candidates = new List<Candidate>()
                });

            modelBuilder.Entity<Skill>().HasData(
                new Skill
                {
                    Id = Guid.NewGuid(),
                    Name = "Skill B",
                    Candidates = new List<Candidate>()
                });

            modelBuilder.Entity<Skill>().HasData(
                new Skill
                {
                    Id = Guid.NewGuid(),
                    Name = "Skill C",
                    Candidates = new List<Candidate>()
                });

            modelBuilder.Entity<Candidate>().HasData(
                 new Candidate
                 {
                     Id = CandidateId,
                     FullName = "John Johnson",
                     ContactNumber = "1",
                     DateOfBirth = DateTime.UtcNow,
                     Email = "email",
                     Skills = new List<Skill>()
                 }
                );

            modelBuilder.Entity<Candidate>().HasData(
                new Candidate
                {
                    Id = Guid.NewGuid(),
                    FullName = "Jack Jackson",
                    ContactNumber = "2",
                    DateOfBirth = DateTime.UtcNow,
                    Email = "email2",
                    Skills = new List<Skill>()
                }
               );

            modelBuilder.Entity<Candidate>().HasData(
                new Candidate
                {
                    Id = Guid.NewGuid(),
                    FullName = "Johnny Joe",
                    ContactNumber = "3",
                    DateOfBirth = DateTime.UtcNow,
                    Email = "email3",
                    Skills = new List<Skill>()
                }
               );

            modelBuilder.Entity<CandidateAndSkill>().HasData(
                new CandidateAndSkill
                {
                    Id = Guid.NewGuid(),
                    SkillId = SkillId,
                    CandidateId = CandidateId
                }
                );
        }

    }
}
