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

        public Task VerifyMobileAsync(Guid userId, string accessToken)
        {
            throw new NotImplementedException();
        }
    }
}
