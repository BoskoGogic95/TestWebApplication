using Business_Access_Layer.Services;
using Business_Access_Layer.Services.IServices;
using Data_Access_Layer.Contracts;
using Data_Access_Layer.Data;
using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Business_Access_Layer.Mappings;

namespace Business_Access_Layer.Extensions
{
    public class DependencyInjection
    {
        private readonly IConfiguration Configuration;
        public DependencyInjection(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void InjectDependencies(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            
            services.AddScoped<Data_Access_Layer.Contracts.ICandidateRepository, CandidateRepository>();
            services.AddScoped<Services.IServices.ICandidateService, CandidateService>();
            services.AddScoped<ISkillRepository, SkillRepository>();
            services.AddScoped<ISkillService, SkillService>();
            services.AddScoped<ICandidateAndSkillRepository, CandidateAndSkillRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
