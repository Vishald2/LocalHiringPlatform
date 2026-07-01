using LocalHiringPlatform.Domain.Exceptions;
using LocalHiringPlatform.Domain.Helpers;
using LocalHiringPlatform.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Infrastructure.Services
{
    public class MobileVerificationService
        : IMobileVerificationService
    {
        private readonly IUserRepository _userRepository;

        private readonly IUnitOfWork _unitOfWork;
        private readonly Msg91Helper _msg91Helper;

        public MobileVerificationService(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            Msg91Helper msg91Helper)
        {
            _userRepository =
                userRepository;

            _unitOfWork =
                unitOfWork;

            _msg91Helper = msg91Helper;
        }

        public async Task VerifyMobileAsync(
            Guid userId,
            string accessToken,
            string mobileNumber)
        {
            var user =
                await _userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                throw new BusinessException("User not found.");
            }

            var response =
                await _msg91Helper.VerifyAccessTokenAsync(
                    accessToken);

            Console.WriteLine(response);



            var result =
                JsonSerializer.Deserialize<Msg91VerifyAccessTokenResponse>(
                    response,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });


            if (result == null)
            {
                throw new BusinessException(
                    "Unable to verify mobile number.");
            }

            if (!string.Equals(
                    result.Type,
                    "success",
                    StringComparison.OrdinalIgnoreCase))
            {
                throw new BusinessException(
                    "Mobile verification failed.");
            }

            var verifiedMobile =
                result.Message;

            if (verifiedMobile.StartsWith("91"))
            {
                verifiedMobile =
                    verifiedMobile.Substring(2);
            }

            if (verifiedMobile != mobileNumber)
            {
                throw new BusinessException(
                    "Verified mobile number does not match.");
            }

            user.MobileNumber = mobileNumber;

            user.MobileVerified = true;

            user.MobileVerifiedOn = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
