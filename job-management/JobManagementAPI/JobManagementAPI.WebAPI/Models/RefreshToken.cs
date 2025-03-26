namespace JobManagementAPI.WebAPI.Models
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsRevoked { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}