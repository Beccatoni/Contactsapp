using ContactsApp.Api.DTOs;
using ContactsApp.Api.Models;

namespace ContactsApp.Api.Services;



public class ContactService : IContactService
{
    private IContactRepository _repository;

    public ContactService(IContactRepository repository)
    {
        _repository = repository;
    }
    
    public Task<List<Contact>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public Task<Contact?> GetContactByIdAsync(int id)
    {
      return  _repository.GetContactByIdAsync(id);
    }

    public async Task CreateContactAsync(CreateContactDto contactDto)
    {
        var newContact = new Contact
        {
            FullName = contactDto.FullName,
            Email = contactDto.Email,
            Phone = contactDto.Phone
        };

        await _repository.CreateContactAsync(newContact);
    }
    public async Task UpdateContactAsync(int id, UpdateContactDto contactDto)
    {
        var contact = await _repository.GetContactByIdAsync(id);
        contact.FullName = contactDto.FullName;
        contact.Email = contactDto.Email;
        await _repository.UpdateContactAsync(contact, id);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}