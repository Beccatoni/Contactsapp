using ContactsApp.Api.DTOs;
using ContactsApp.Api.Models;

namespace ContactsApp.Api.Services;

public interface IContactService
{
    Task<List<Contact>> GetAllAsync();
    Task<Contact?> GetContactByIdAsync(int id);
    Task CreateContactAsync(CreateContactDto contactDto);
    Task UpdateContactAsync(int id, UpdateContactDto contactDto);
    Task DeleteAsync(int id);
}