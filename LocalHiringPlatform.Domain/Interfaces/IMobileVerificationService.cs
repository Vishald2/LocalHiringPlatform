using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Interfaces
{
    public interface IMobileVerificationService
    {
        Task<string> SendOtpAsync(Guid userId);

        Task VerifyOtpAsync(
            Guid userId,
            string otp);
    }
}
