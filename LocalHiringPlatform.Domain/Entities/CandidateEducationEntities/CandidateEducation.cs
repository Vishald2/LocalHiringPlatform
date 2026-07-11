using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Entities.CandidateEducationEntities
{
    /* Graduate-BA, Post-Graduate, Doctorate */
    public class CandidateEducation : BaseEntity
    {
        [ForeignKey(nameof(CandidateProfile))]
        public Guid CandidateProfileId { get; set; }

        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }

        [ForeignKey(nameof(University))]
        public int? UniversityId { get; set; }

        [MaxLength(200)]
        public string? InstituteName { get; set; }

        public int? StartYear { get; set; }

        public int? EndYear { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal? Percentage { get; set; }

        [Column(TypeName = "decimal(4,2)")]
        public decimal? CGPA { get; set; }

        [MaxLength(20)]
        public string? Grade { get; set; }

        public bool IsCompleted { get; set; }

        public bool IsHighestEducation { get; set; }

        // Navigation Properties

        public CandidateProfile CandidateProfile { get; set; } = null!;

        public Course Course { get; set; } = null!;

        public University? University { get; set; }

        public ICollection<CandidateEducationSpecialization> CandidateCourseSpecializations { get; set; } = new List<CandidateEducationSpecialization>();
    }
}
