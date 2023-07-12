namespace WebAPI.Domain.Models;

public class AuthenticationResponse
{
    public string accessToken { get; set; }
    public string userId { get; set; }
    public string refreshTokenId { get; set; }
    public DateTime? accessTokenExpiration { get; set; }
}