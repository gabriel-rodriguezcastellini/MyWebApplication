using MongoDB.Entities;

namespace MyWebApplication.Features.User.Authentication.RefreshToken
{
    public class RefreshToken : Entity
    {
        public string UserID { get; set; } = null!;
        public string Token { get; set; } = null!;
        public DateTime ExpiryDate { get; set; }

        static RefreshToken()
        {
            //TTL index to automatically purge records after 1 minute once the token has expired
            _ = DB.Index<RefreshToken>()
              .Key(x => x.ExpiryDate, KeyType.Ascending)
              .Option(x => x.ExpireAfter = TimeSpan.FromMinutes(1))
              .CreateAsync();

            //compound index for queries
            _ = DB.Index<RefreshToken>()
              .Key(x => x.UserID, KeyType.Ascending)
              .Key(x => x.Token, KeyType.Ascending)
              .Key(x => x.ExpiryDate, KeyType.Ascending)
              .CreateAsync();
        }
    }
}
