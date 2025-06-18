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
    
    [Required]
    public string Phone { get; set; }
}

public class UpdateContactDto
{
    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
}
