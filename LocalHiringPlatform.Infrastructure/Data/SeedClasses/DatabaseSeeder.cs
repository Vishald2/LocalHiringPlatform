using LocalHiringPlatform.Domain.Entities;
using LocalHiringPlatform.Domain.Entities.CandidateEducationEntities;
using LocalHiringPlatform.Domain.Entities.Experience;
using LocalHiringPlatform.Infrastructure.Data.SeedModels;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace LocalHiringPlatform.Infrastructure.Data.SeedClasses
{
    public class DatabaseSeeder
    {
        private readonly ApplicationDbContext _dbContext;

        public DatabaseSeeder(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SeedAsync()
        {
           // await SeedIndustryTypesAsync();
         //   await SeedSkillsAsync();

         //  await SeedUniversitiesAsync();


            // Later
            // await SeedSkillsAsync();
            // await SeedCompaniesAsync();
            // await SeedJobsAsync();
        }

        public async Task SeedIndustryTypesAsync()
        {
            if (await _dbContext.IndustryTypes.AnyAsync())
            {
                return;
            }

            var path = Path.Combine(
            AppContext.BaseDirectory,
                "Data",
                "Seed",
                "IndustryTypes.json"
            );

            Console.WriteLine(path);

            var json =
                await File.ReadAllTextAsync(path);

            var industryTypes =
            JsonSerializer.Deserialize<List<IndustryType>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (industryTypes == null || industryTypes.Count == 0)
            {
                return;
            }

            _dbContext.IndustryTypes.AddRange(industryTypes);

            await _dbContext.SaveChangesAsync();
        }

        private async Task SeedSkillsAsync()
        {
            if (await _dbContext.Skills.AnyAsync())
            {
                return;
            }

            var skills =
                await ReadJsonAsync<Skill>("Skills.json");

            _dbContext.Skills.AddRange(skills);

            await _dbContext.SaveChangesAsync();
        }

        private async Task SeedUniversitiesAsync()
        {
            if (await _dbContext.Universities.AnyAsync())
            {
                return;
            }

            var universities =
                await ReadJsonAsync<University>(
                    "Universities.json");

            _dbContext.Universities.AddRange(universities);

            await _dbContext.SaveChangesAsync();
        }

        private async Task<List<T>> ReadJsonAsync<T>(
    string fileName)
        {
            var filePath =
                Path.Combine(
                    AppContext.BaseDirectory,
                    "Data",
                    "Seed",
                    fileName);

            var json =
                await File.ReadAllTextAsync(filePath);

            return JsonSerializer.Deserialize<List<T>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                })!;
        }
    }
}
