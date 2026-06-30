using LocalHiringPlatform.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Interfaces
{
    public interface ISmsService
    {
        Task SendOtpAsync(string mobileNumber);

        Task<OtpVerificationResult> VerifyOtpAsync(string mobileNumber, string otp);
    }
}
