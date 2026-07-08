using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Entities.CandidateEducation
{
    public class CandidateEducationSpecialization
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CandidateEducationSpecializationId { get; set; }

        [ForeignKey(nameof(CandidateEducation))]
        public Guid CandidateEducationEntityId { get; set; }

        [ForeignKey(nameof(CourseSpecialization))]
        public int CourseSpecializationId { get; set; }

        public CandidateEducation CandidateEducation { get; set; } = null!;

        public CourseSpecialization CourseSpecialization { get; set; } = null!;
    }
}
