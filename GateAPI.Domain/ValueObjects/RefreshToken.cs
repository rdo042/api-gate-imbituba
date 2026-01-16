namespace GateAPI.Domain.ValueObjects
{
    public class RefreshToken
    {
        public Guid Token { get; private set; }
        public DateTime ExpiryDate { get; private set; }
        public bool IsRevoked { get; private set; }
        public Guid UserId { get; private set; }

        public RefreshToken(DateTime expiryDate, bool isRevoked, Guid userId)
        {
            Token = Guid.NewGuid();
            ExpiryDate = expiryDate;
            IsRevoked = isRevoked;
            UserId = userId;

            Validation();
        }

        private void Validation()
        {
        }
    }
}
