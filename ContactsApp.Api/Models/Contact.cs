namespace ContactsApp.Api.Models;

public class Contact
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public List<string> Phone { get; set; }
    public DateTime CreatedAt { get; set; } 
    public DateTime UpdatedAt { get; set; }
}