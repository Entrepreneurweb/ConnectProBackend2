using ConnectPro.Identity.Domain.Aggregates.ValueObjects;
using ConnectPro.SharedKernel;
using ConnectPro.SharedKernel.Exceptions;
using Identity.Domain.Aggregates.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Aggregates.User.Entities
{
    public class User : AggregateRoot<User>
    {
        //private UserId id;
        private Email _email;
        private PasswordHash _passwordHash;
        private bool _isActive;
        private Otp? _pendingOtp;

        private string _ProfilePictureUrl;
        private User() { }

        private User(Email email, PasswordHash passwordHash)
        {
            _email = email;
            _passwordHash = passwordHash;
            _isActive = false;
            _ProfilePictureUrl = string.Empty;
            _pendingOtp = null;
        }
        private User(  Id<User> UserId, Email email, PasswordHash passwordHash ,  bool? isActive , Otp? otp , string? profileImage)
        {
            Id = UserId;
            _email = email;
            _passwordHash = passwordHash;
            _isActive = isActive ?? false;
            _ProfilePictureUrl = profileImage ?? string.Empty;
            _pendingOtp = otp ?? null;
             
        }



        public static User Create(Email email  , PasswordHash password   ) 
        {
           
            
            var user =  new User(email, password );
            user.GenerateOtp();
            return user;
        }

        public static User Reconstitute(  Id<User> UserId, Email email, PasswordHash passwordHash, bool isActive , Otp otp, string profilePicture)
        {
            User user = new User(   UserId , email, passwordHash , isActive , otp , profilePicture);
           // user.Id = UserId;
            return user;
        }

        // user related methods
        public void ChangeEmail(Email newEmail)
        {
            if (newEmail == _email) return;
            var old = _email;
            _email = newEmail;
            _isActive = false;
            //  RaiseDomainEvent(UserEmailChangedEvent.Create(_userId, old, newEmail));
        }

        public void Deactivate()
        {
            if (!_isActive)
                throw new DomainException("L'utilisateur est déjà désactivé.");
            _isActive = false;
        }

        public void Activate()
        {
            if (_isActive)
                throw new DomainException("L'utilisateur est déjà actif.");
            _isActive = true;
        }

        public void ChangeProfilePicture(string newUrl)
        {
            if (newUrl == _ProfilePictureUrl) return;
            var oldUrl = _ProfilePictureUrl;
            _ProfilePictureUrl = newUrl;
            // RaiseDomainEvent(UserProfilePictureChangedEvent.Create(_userId, oldUrl, newUrl));
        }

        public void CreateOtp()
        {
            if (_isActive)
                throw new DomainException("Le compte est déjà actif.");
            if (_pendingOtp != null && !_pendingOtp.IsExpired())
                throw new DomainException("Un OTP est déjà en attente pour ce compte.");
            GenerateOtp();
        }

        public void ReCreateOtp()
        {
            if (_isActive)
                throw new DomainException("Le compte est déjà actif.");
            if (_pendingOtp == null )
                throw new DomainException("Aucun OTP en attente  pour ce compte.");
            GenerateOtp();
        }

        // methodes lié a l'otp
        public void GenerateOtp()
        {
            _pendingOtp = Otp.Create();
           // RaiseDomainEvent(OtpGeneratedEvent.Create(_userId, _email, pendingOtp.Code));
        }

        public void VerifyOtp(OtpCode code)
        {
            if (_isActive)
                throw new DomainException("Le compte est déjà actif.");

            if (_pendingOtp is null)
                throw new DomainException("Aucun OTP en attente pour ce compte.");

            if (_pendingOtp.Code != code)
                throw new DomainException("Code OTP incorrect.");

            _pendingOtp.MarkAsUsed();
            _isActive = true;
            _pendingOtp = null;

            //RaiseDomainEvent(OtpVerifiedEvent.Create(_userId));
        }





        public Email GetEmail() => _email;
        public PasswordHash GetPasswordHash() => _passwordHash;
        public bool IsActive() => _isActive;
        public Otp? GetPendingOtp() => _pendingOtp;
        public string GetProfilePictureUrl() => _ProfilePictureUrl;

    }
}
