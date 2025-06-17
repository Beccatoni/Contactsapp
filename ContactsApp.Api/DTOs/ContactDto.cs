using System.ComponentModel.DataAnnotations;

namespace ContactsApp.Api.DTOs;

public class ContactDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public List<string> Phone { get; set; }
    
}

public class CreateContactDto
{
    [Required]
    public string FullName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [MinLength(1)]
    public List<string> Phone { get; set; }
}

public class UpdateContactDto
{
    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;
}
