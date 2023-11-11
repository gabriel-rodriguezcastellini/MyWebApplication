namespace MyWebApplication.Features.Person.Save
{
    public class Request
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string BirthDay { get; set; } = null!;
    }

    public class Response
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public int Age { get; set; }
    }

    public class Person
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
    }
}
