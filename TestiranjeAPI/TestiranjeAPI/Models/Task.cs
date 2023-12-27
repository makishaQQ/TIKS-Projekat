namespace TestiranjeAPI.Models;

public class Task
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public User User { get; set; }
    public Party Party { get; set; }

    public Task() { }

    public Task(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
