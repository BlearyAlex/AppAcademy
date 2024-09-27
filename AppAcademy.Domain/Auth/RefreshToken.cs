namespace AppAcademy.Domain.Auth
{
    public class RefreshToken
    {
        public string RefreshTokenId { get; set; } = Guid.NewGuid().ToString();
        public string? Token { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }

        // Relaciones
        public string? UserId { get; set; }
        public User? User { get; set; }
    }
}
