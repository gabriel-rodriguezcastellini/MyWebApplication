using MongoDB.Entities;

namespace MyWebApplication.Features.User.Authentication.Login
{
    public static class Data
    {
        public static async Task<string?> GetUserID(string email, string password)
        {
            return await DB.Find<User, string>()
                           .Match(u => u.Email == email && u.Password == password) //never store clear text passwords in db
                           .Project(u => u.ID)
                           .ExecuteSingleAsync();
        }
    }
}
