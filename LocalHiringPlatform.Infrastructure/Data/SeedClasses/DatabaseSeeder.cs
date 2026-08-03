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
            //await SeedIndustryTypesAsync();
            //await SeedSkillsAsync();
            //await SeedUniversitiesAsync();
            //await SeedEmployerAsync();
            //await SeedJobsAsync();
            //await SeedCandidateUsersAsync();
            //await SeedCandidateProfilesAsync();
            //await SeedCandidateSkillsAsync();
            //await SeedEducationsAsync();
            //await SeedCoursesAsync();

        //    await SeedSpecializationsAsync();

         //   await SeedCourseSpecializationsAsync();

            await SeedCandidateEducationsAsync();

            // Later
            // await SeedSkillsAsync();
            // await SeedCompaniesAsync();
            // await SeedJobsAsync();
        }

        private async Task SeedCandidateEducationsAsync()
        {
            if (await _dbContext.CandidateEducations.AnyAsync())
                return;

            var candidates = await _dbContext.CandidateProfiles.ToListAsync();

            var universities = await _dbContext.Universities.ToListAsync();

            var courseDictionary = await _dbContext.Courses
                .ToDictionaryAsync(x => x.Code);

            var courseSpecializations = await _dbContext.CourseSpecializations
                .GroupBy(x => x.CourseId)
                .ToDictionaryAsync(x => x.Key, x => x.ToList());

            Random random = new();

            foreach (var candidate in candidates)
            {
                string courseCode;

                switch (random.Next(100))
                {
                    case < 35:
                        courseCode = "BTECH";
                        break;

                    case < 55:
                        courseCode = "BCA";
                        break;

                    case < 65:
                        courseCode = "BSC";
                        break;

                    case < 75:
                        courseCode = "MCA";
                        break;

                    case < 85:
                        courseCode = "MBA";
                        break;

                    case < 90:
                        courseCode = "MTECH";
                        break;

                    case < 95:
                        courseCode = "DIPCS";
                        break;

                    default:
                        courseCode = "BCOM";
                        break;
                }

                var course = courseDictionary[courseCode];

                var university =
                    universities[random.Next(universities.Count)];

                int endYear =
                    DateTime.Now.Year -
                    (int)candidate.TotalExperienceYears;

                int duration =
                    courseCode.StartsWith("M") ? 2 :
                    courseCode.StartsWith("DIP") ? 3 :
                    4;

                int startYear = endYear - duration;

                var education = new CandidateEducation
                {
                    CandidateProfileId = candidate.EntityId,
                    CourseId = course.CourseId,
                    UniversityId = university.UniversityId,
                    InstituteName = university.Name,
                    City = university.City,
                    State = university.State,
                    Country = "India",
                    StartYear = startYear,
                    EndYear = endYear,
                    Percentage = random.Next(60, 91),
                    CGPA = null,
                    Grade = null,
                    IsCompleted = true,
                    IsHighestEducation = true
                };

                _dbContext.CandidateEducations.Add(education);

                if (courseSpecializations.TryGetValue(
                        course.CourseId,
                        out var specializations))
                {
                    var specialization =
                        specializations[random.Next(specializations.Count)];

                    _dbContext.CandidateEducationSpecializations.Add(
                        new CandidateEducationSpecialization
                        {
                            CandidateEducation = education,
                            SpecializationId = specialization.SpecializationId
                        });
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        private async Task SeedCourseSpecializationsAsync()
        {
            if (await _dbContext.CourseSpecializations.AnyAsync())
                return;

            var items =
                await ReadJsonAsync<CourseSpecializationSeedModel>(
                    "CourseSpecializations.json");

            var courses = await _dbContext.Courses
                .ToDictionaryAsync(
                    x => x.Code,
                    x => x.CourseId);

            var specializations = await _dbContext.Specializations
                .ToDictionaryAsync(
                    x => x.Code,
                    x => x.SpecializationId);

            foreach (var item in items)
            {
                if (!courses.TryGetValue(item.CourseCode, out var courseId))
                    throw new InvalidOperationException($"Course '{item.CourseCode}' not found.");

                if (!specializations.TryGetValue(item.SpecializationCode, out var specializationId))
                    throw new InvalidOperationException($"Specialization '{item.SpecializationCode}' not found.");

                _dbContext.CourseSpecializations.Add(
                    new CourseSpecialization
                    {
                        CourseId = courseId,
                        SpecializationId = specializationId
                    });
            }

            await _dbContext.SaveChangesAsync();
        }

        private async Task SeedSpecializationsAsync()
        {
            if (await _dbContext.Specializations.AnyAsync())
                return;

            var specializations =
                await ReadJsonAsync<Specialization>("Specializations.json");

            _dbContext.Specializations.AddRange(specializations);

            await _dbContext.SaveChangesAsync();
        }

        private async Task SeedCoursesAsync()
        {
            if (await _dbContext.Courses.AnyAsync())
                return;

            var items =
                await ReadJsonAsync<CourseSeedModel>("Courses.json");

            var educations =
                await _dbContext.Educations
                    .ToDictionaryAsync(
                        x => x.Code,
                        x => x.EducationId);

            foreach (var item in items)
            {
                if (!educations.TryGetValue(
                        item.EducationCode,
                        out var educationId))
                {
                    throw new Exception(
                        $"Education '{item.EducationCode}' not found.");
                }

                _dbContext.Courses.Add(
                    new Course
                    {
                        EducationId = educationId,
                        Code = item.Code,
                        Name = item.Name,
                        DisplayOrder = item.DisplayOrder,
                        IsActive = item.IsActive
                    });
            }

            await _dbContext.SaveChangesAsync();
        }

        private async Task SeedEducationsAsync()
        {
            if (await _dbContext.Educations.AnyAsync())
                return;

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
