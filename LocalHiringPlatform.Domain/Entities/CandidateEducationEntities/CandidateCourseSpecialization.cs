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
        public int CandidateEducationSpecializationId { get; set; }

        public Guid CandidateEducationEntityId { get; set; }

        public int SpecializationId { get; set; }

        public CandidateEducation CandidateEducation { get; set; } = null!;

        public Specialization Specialization { get; set; } = null!;
    }
}
