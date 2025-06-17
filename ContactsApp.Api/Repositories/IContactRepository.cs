namespace ContactsApp.Api.Models;

public interface IContactRepository
{
    Task<List<Contact>> GetAllAsync();
    Task<Contact?> GetContactByIdAsync(int id);
    Task CreateContactAsync(Contact contact);
    Task UpdateContactAsync(Contact contact, int id);
    Task DeleteAsync(int id);
}