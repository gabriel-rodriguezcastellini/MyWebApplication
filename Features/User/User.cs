using MongoDB.Entities;

namespace MyWebApplication.Features.User
{
    public class User : Entity
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Age { get; set; }

        static User()
        {
            _ = DB.Index<User>()
              .Key(x => x.Email, KeyType.Ascending)
              .Key(x => x.Password, KeyType.Ascending)
              .CreateAsync();
        }
    }
}
