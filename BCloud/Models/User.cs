namespace BCloud.Models;

public class User
{
    public User(string id, string name, string password)
    {
        Id = id;
        Name = name;
        Password = password;
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
}