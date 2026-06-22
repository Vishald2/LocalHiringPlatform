using LocalHiringPlatform.Domain.Exceptions;
using LocalHiringPlatform.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Infrastructure.Services
{
    public class MobileVerificationService
        : IMobileVerificationService
    {
        private readonly IUserRepository _userRepository;

        private readonly IUnitOfWork _unitOfWork;

        public MobileVerificationService(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository =
                userRepository;

            _unitOfWork =
                unitOfWork;
        }

        public async Task<string> SendOtpAsync(
            Guid userId)
        {
            var user = await _userRepository
                    .GetByIdAsync(userId);

            if (user == null)
            {
                throw new BusinessException("User not found.");
            }

            var otp = new Random().Next(100000, 999999).ToString();

            user.MobileVerificationCode = otp;

            user.MobileVerificationCodeExpiry =
                DateTime.UtcNow
                    .AddMinutes(5);

            await _unitOfWork
                .SaveChangesAsync();

            return otp;
        }

        public async Task VerifyOtpAsync(
            Guid userId,
            string otp)
        {
            var user =
                await _userRepository
                    .GetByIdAsync(userId);

            if (user == null)
            {
                throw new BusinessException(
                    "User not found.");
            }

            if (user.MobileVerificationCode
                != otp)
            {
                throw new BusinessException(
                    "Invalid OTP.");
            }

            if (user.MobileVerificationCodeExpiry
                < DateTime.UtcNow)
            {
                throw new BusinessException(
                    "OTP expired.");
            }

            user.MobileVerified =
                true;

            user.MobileVerifiedOn =
                DateTime.UtcNow;

            user.MobileVerificationCode =
                null;

            user.MobileVerificationCodeExpiry =
                null;

            await _unitOfWork
                .SaveChangesAsync();
        }
    }
}
