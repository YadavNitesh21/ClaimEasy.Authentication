namespace Authentication.DTOs
{
    public class TokenResponseDto
    {
        public string Token { get; set; }
        public DateTime GeneratedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
