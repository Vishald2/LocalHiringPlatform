using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Models
{
    public class ChangePasswordModel
    {
        public string CurrentPassword { get; set; }
            = string.Empty;

        public string NewPassword { get; set; }
            = string.Empty;

        public string ConfirmPassword { get; set; }
            = string.Empty;
    }
}
