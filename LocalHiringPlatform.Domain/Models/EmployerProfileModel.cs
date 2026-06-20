using LocalHiringPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Models
{
    public class EmployerProfileModel
    {
        //public Guid Id { get; set; }

        //public Guid UserId { get; set; }

        public string? CompanyName { get; set; }

        public string? Industry { get; set; }

        public string? Website { get; set; }

        public string? CompanyDescription { get; set; }

        public bool IsEmailVerified { get; set; } = false;
    }
}
