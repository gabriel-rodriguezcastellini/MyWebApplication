using MongoDB.Entities;

namespace MyWebApplication.Features.User.Authentication.RefreshToken
{
    public static class Data
    {
        public static async Task StoreToken(string userId, DateTime refreshExpiry, string refreshToken)
        {
            _ = await DB.DeleteAsync<RefreshToken>(rt => rt.UserID == userId);

            await new RefreshToken
            {
                UserID = userId,
                ExpiryDate = refreshExpiry,
                Token = refreshToken
            }.SaveAsync();
        }

        public static Task<bool> TokenIsValid(string userId, string refreshToken)
        {
            return DB.Find<RefreshToken>()
                     .Match(t => t.UserID == userId &&
                                 t.Token == refreshToken &&
                                 t.ExpiryDate >= DateTime.UtcNow)
                     .ExecuteAnyAsync();
        }
    }
}
