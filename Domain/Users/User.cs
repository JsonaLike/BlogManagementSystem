using ERP.Domain.Core.Models;
namespace ERP.Domain.Modules.Users
{
    public class User : AggregateRoot
    {
        public User()
        { }

        #region Behaviours
        

        public void ResetUserPassword(
            string passwordHash,
            string saltKey,
            Guid modifiedBy)
        {

            PasswordHash = passwordHash;
            SaltKey = saltKey;
        }

       

        public void InvalidLoginAttempt()
        {
            InValidLogInAttemps++;
           
        }

        public void LoginSuccessfully()
        {
            InValidLogInAttemps = 0;
            LastLogInOn = DateTimeOffset.UtcNow;
        }

        public void SetRefreshToken(string refreshToken)
        {

            RefreshToken = refreshToken;
            RefreshTokenExpiryTime = DateTimeOffset.Now.AddDays(5);
        }

        public void RevokeRefreshToken(Guid modifiedBy)
        {

            RefreshToken = null;
            RefreshTokenExpiryTime = null;
        }

        public bool ValidateRefreshToken(string refreshToken)
        {
            if (refreshToken != RefreshToken)
            {
                throw new Exception("Invalid Refresh Token");
            }

            if (RefreshTokenExpiryTime <= DateTimeOffset.Now)
            {
                throw new Exception("Refresh Token Has Expired");
            }

            return true;
        }

        #endregion

        #region Stats
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string SaltKey { get; set; }
        public DateTimeOffset? LastLogInOn { get; set; }
        public byte InValidLogInAttemps { get; set; }
        public string? RefreshToken { get; set; }
        public DateTimeOffset? RefreshTokenExpiryTime { get; set; }
        public bool IsSuperUser { get; set; }

        public ICollection<UserRole> UserRoles { get; protected set; }
        #endregion
    }
}