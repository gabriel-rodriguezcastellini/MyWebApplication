namespace MyWebApplication.Features.User.Authentication
{
    public class MyTokenResponse : TokenResponse
    {
        public string AccessTokenExpiry => AccessExpiry.ToLocalTime().ToString();

        public int RefreshTokenValidityMinutes => (int)RefreshExpiry.Subtract(DateTime.UtcNow).TotalMinutes;
    }
}
