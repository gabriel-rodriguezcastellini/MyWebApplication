using MongoDB.Entities;

namespace MyWebApplication.Features.User.SignUp
{
    public static class Data
    {
        public static async Task<string> CreateUser(User user)
        {
            await user.SaveAsync();
            return user.ID!;
        }
    }
}
