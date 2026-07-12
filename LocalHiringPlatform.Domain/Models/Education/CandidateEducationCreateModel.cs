using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Models.Education
{
    public class CandidateEducationCreateModel
    {
        public Guid? EntityId { get; set; }
        public int EducationId { get; set; }

        public int CourseId { get; set; }

        public int? UniversityId { get; set; }

        public string? InstituteName { get; set; }

        public int? StartYear { get; set; }

        public int? EndYear { get; set; }

        public decimal? Percentage { get; set; }

        public decimal? CGPA { get; set; }

        public string? Grade { get; set; }

        public bool IsCompleted { get; set; }

        public bool IsHighestEducation { get; set; }

        public List<int> CourseSpecializationIds { get; set; } = new();
    }
}
