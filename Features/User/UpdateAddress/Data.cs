using MongoDB.Entities;

namespace MyWebApplication.Features.User.UpdateAddress;

public static class Data
{
    public static async Task UpdateAddress(User user)
    {
        await user.SaveAsync();
    }

    public static async Task<User?> GetUser(string id)
    {
        return await DB.Find<User>().Match(x => x.ID == id).ExecuteSingleAsync();
    }
}
