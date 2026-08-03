using LocalHiringPlatform.Domain.Entities;
using LocalHiringPlatform.Domain.Entities.CandidateEducationEntities;
using LocalHiringPlatform.Domain.Entities.Experience;
using LocalHiringPlatform.Domain.Enums;
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
            // await SeedSkillsAsync();
            // await SeedUniversitiesAsync();
            // await SeedEmployerAsync();
            //await SeedJobsAsync();

            // await SeedCandidateUsersAsync();
            //  await SeedCandidateProfilesAsync();

            //await SeedCandidateSkillsAsync();

            SeedEducationsAsync();

            // Later
            // await SeedSkillsAsync();
            // await SeedCompaniesAsync();
            // await SeedJobsAsync();
        }

        private async Task SeedEducationsAsync()
        {
            //if (await _dbContext.Educations.AnyAsync())
            //    return;

            var educations =
                await ReadJsonAsync<Education>("Educations.json");

            _dbContext.Educations.AddRange(educations);

            await _dbContext.SaveChangesAsync();
        }

        private async Task SeedCandidateSkillsAsync()
        {
            if (await _dbContext.CandidateSkills.AnyAsync())
                return;

            var candidates = await _dbContext.CandidateProfiles
                .OrderBy(x => x.FullName)
                .ToListAsync();

            var skills = await _dbContext.Skills.ToListAsync();

            Random random = new();

            foreach (var candidate in candidates)
            {
                List<string> requiredSkills;

                switch (random.Next(5))
                {
                    case 0: // .NET
                        requiredSkills = new()
                {
                    "C#",
                    ".NET",
                    "ASP.NET Core",
                    "SQL Server",
                    "Azure"
                };
                        break;

                    case 1: // React
                        requiredSkills = new()
                {
                    "React",
                    "TypeScript",
                    "JavaScript",
                    "HTML",
                    "CSS"
                };
                        break;

                    case 2: // Java
                        requiredSkills = new()
                {
                    "Java",
                    "Spring Boot",
                    "Hibernate",
                    "MySQL",
                    "Docker"
                };
                        break;

                    case 3: // Python
                        requiredSkills = new()
                {
                    "Python",
                    "Django",
                    "FastAPI",
                    "PostgreSQL",
                    "Machine Learning"
                };
                        break;

                    default: // Node.js
                        requiredSkills = new()
                {
                    "Node.js",
                    "Express.js",
                    "MongoDB",
                    "JavaScript",
                    "Redis"
                };
                        break;
                }

                foreach (var skillName in requiredSkills)
                {
                    var skill = skills.FirstOrDefault(x => x.SkillName == skillName);

                    if (skill == null)
                        continue;

                    _dbContext.CandidateSkills.Add(
                        new CandidateSkill
                        {
                            CandidateProfileId = candidate.EntityId,
                            SkillId = skill.SkillId,
                            ExperienceInMonths =
                                random.Next(6, 121)
                        });
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        private async Task SeedCandidateUsersAsync()
        {
            if (await _dbContext.Users.AnyAsync(x => x.Role == UserRole.Candidate))
                return;

            for (int i = 1; i <= 100; i++)
            {
                _dbContext.Users.Add(new User
                {
                    Email = $"candidate{i:000}@localhire.demo",
                    MobileNumber = $"98100{i:00000}",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Pass@123"),
                    Role = UserRole.Candidate,
                    IsActive = true,
                    EmailVerified = true,
                    MobileVerified = true,
                    EmailVerifiedOn = DateTime.UtcNow,
                    MobileVerifiedOn = DateTime.UtcNow
                });
            }

            await _dbContext.SaveChangesAsync();
        }

        private async Task SeedCandidateProfilesAsync()
        {
            if (await _dbContext.CandidateProfiles.AnyAsync())
                return;

            var candidateUsers = await _dbContext.Users
                .Where(x => x.Role == UserRole.Candidate)
                .OrderBy(x => x.Email)
                .ToListAsync();

            string[] firstNames =
            {
        "Rahul","Aman","Rohit","Vikas","Ankit",
        "Sandeep","Mohit","Nitin","Amit","Karan",
        "Neha","Priya","Pooja","Sneha","Anjali",
        "Kavita","Riya","Simran","Shreya","Nidhi"
    };

            string[] lastNames =
            {
        "Sharma","Verma","Gupta","Singh","Kumar",
        "Yadav","Jain","Agarwal","Mehta","Bansal"
    };

            string[] cities =
            {
        "Delhi","Noida","Gurugram","Panipat",
        "Chandigarh","Jaipur","Lucknow","Pune",
        "Hyderabad","Bengaluru"
    };

            string[] states =
            {
        "Delhi","Uttar Pradesh","Haryana","Haryana",
        "Chandigarh","Rajasthan","Uttar Pradesh",
        "Maharashtra","Telangana","Karnataka"
    };

            Random random = new();

            for (int i = 0; i < candidateUsers.Count; i++)
            {
                var experience = random.Next(0, 11);

                _dbContext.CandidateProfiles.Add(
                    new CandidateProfile
                    {
                        UserId = candidateUsers[i].EntityId,

                        FullName =
                            $"{firstNames[i % firstNames.Length]} {lastNames[(i / firstNames.Length) % lastNames.Length]}",

                        City = cities[i % cities.Length],

                        State = states[i % states.Length],

                        DateOfBirth =
                            DateTime.Today.AddYears(-(22 + random.Next(0, 13)))
                                          .AddDays(random.Next(365)),

                        Gender =
                            i % 2 == 0
                                ? Gender.Male
                                : Gender.Female,

                        ProfileSummary =
                            $"Software professional with {experience} years of experience in application development.",

                        CurrentSalary =
                            experience == 0
                                ? null
                                : 400000 + (experience * 250000),

                        ExpectedSalary =
                            experience == 0
                                ? 500000
                                : 600000 + (experience * 300000),

                        TotalExperienceYears = experience,

                        ResumeUrl = string.Empty,

                        ResumeFileName = null,

                        ResumeFilePath = null,

                        ProfileCompletionPercentage =
                            random.Next(80, 101),

                        IsOpenToWork = true
                    });
            }

            await _dbContext.SaveChangesAsync();
        }
        private async Task SeedJobsAsync()
        {
            if (await _dbContext.Jobs.AnyAsync())
                return;

            var jobs = new List<Job>();

            var folder = Path.Combine(
                AppContext.BaseDirectory,
                "Data",
                "Seed",
                "Jobs");

            foreach (var file in Directory.GetFiles(folder, "*.json"))
            {
                var fileName = Path.GetFileName(file);

                jobs.AddRange(await ReadJsonAsync<Job>($"Jobs/{fileName}"));
            }

            var employers = await _dbContext.EmployerProfiles.ToListAsync();

            for (int i = 0; i < jobs.Count; i++)
            {
                jobs[i].EmployerProfileId =
                    employers[i % employers.Count].EntityId;
            }

            _dbContext.Jobs.AddRange(jobs);

            await _dbContext.SaveChangesAsync();
        }
        public async Task SeedEmployerAsync()
        {
            if (await _dbContext.Users.AnyAsync())
            {
                return;
            }

            string[] companies =
                {
                    "TechNova Solutions",
                    "ByteCraft Technologies",
                    "CloudNest Systems",
                    "FinEdge Technologies",
                    "HealthSync Solutions",
                    "RetailX India",
                    "CodeSphere Labs",
                    "NextGen Software",
                    "Digital Bridge",
                    "FutureSoft Technologies",
                    "Vertex Solutions",
                    "BluePeak Systems",
                    "Apex Infotech",
                    "PrimeLogic",
                    "SoftVision Technologies",
                    "Innova Systems",
                    "Skyline Software",
                    "Quantum Technologies",
                    "DataCore Solutions",
                    "SmartByte Technologies"
                };

            for (int i = 1; i <= 20; i++)
            {
                var employerUser = new User
                {
                    Email = $"hr{i:000}@company{i:000}.com",
                    MobileNumber = $"987650{i:0000}",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Pass@123"),
                    Role = UserRole.Employer,
                    EmailVerified = true,
                    MobileVerified = true,
                    IsActive = true,
                    EmailVerifiedOn = DateTime.UtcNow,
                    MobileVerifiedOn = DateTime.UtcNow
                };

                _dbContext.Users.Add(employerUser);

                _dbContext.EmployerProfiles.Add(new EmployerProfile
                {
                    User = employerUser,
                    CompanyName = companies[i - 1],
                    Website = $"https://www.{companies[i - 1].Replace(" ", "").ToLower()}.com",
                    Industry = "Information Technology",
                    CompanyDescription = $"Company {i} is a software development and IT consulting company specializing in cloud solutions, web applications, and enterprise software."
                });
            }

            await _dbContext.SaveChangesAsync();
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
