using ConnectPro.SharedKernel.Exceptions;
using Identity.Domain.Aggregates.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Aggregates.User.Entities
{
    public class Otp
    {
        private OtpCode _code;
        private DateTime _expiresAt;
        private bool _isUsed;

        private Otp() { }

        private Otp(OtpCode code, DateTime expiresAt)
        {
            _code = code;
            _expiresAt = expiresAt;
            _isUsed = false;
        }

        public OtpCode Code => _code;
        public DateTime ExpiresAt => _expiresAt;
        public bool IsUsed => _isUsed;

        public static Otp Create() => new(OtpCode.Generate(), DateTime.UtcNow.AddMinutes(15));

        public static Otp Reconstitute(OtpCode code, DateTime expiresAt, bool isUsed)
            => new() { _code = code, _expiresAt = expiresAt, _isUsed = isUsed };

        public bool IsExpired() => DateTime.UtcNow > _expiresAt;

        public void MarkAsUsed()
        {
            if (_isUsed)
                throw new DomainException("Cet OTP a déjà été utilisé.");

            if (IsExpired())
                throw new DomainException("Cet OTP a expiré.");

            _isUsed = true;
        }
    }
}
