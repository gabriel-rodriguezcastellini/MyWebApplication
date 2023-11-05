using MongoDB.Entities;

namespace MyWebApplication.Features.User.Profile
{
    public static class Data
    {
        public static Task<User> GetUser(string userID)
        {
            return DB.Find<User>()
                     .MatchID(userID)
                     .ExecuteSingleAsync()!;
        }
    }
}
