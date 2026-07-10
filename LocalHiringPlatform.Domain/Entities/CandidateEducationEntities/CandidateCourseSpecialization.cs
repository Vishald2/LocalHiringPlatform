using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Entities.CandidateEducationEntities
{
    public class CandidateCourseSpecialization
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CandidateEducationSpecializationId { get; set; }

        [ForeignKey(nameof(CandidateProfile))]
        public Guid ProfileId { get; set; }

        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }

        [ForeignKey(nameof(Specialization))]
        public int SpecializationId { get; set; }

        public CandidateProfile Profile { get; set; }

        public Course Course { get; set; } = null!;

        public Specialization Specialization { get; set; } = null!;
    }
}
