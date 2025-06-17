using ContactsApp.Api.Data;
using ContactsApp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp.Api.Repositories;

public class ContactRepository: IContactRepository
{
    private readonly ContactsDbContext _context;

    public ContactRepository(ContactsDbContext context)
    {
        _context = context;
    }

    public async Task<List<Contact>> GetAllAsync()
    {
       return await _context.Contacts.ToListAsync();
    }

    public async Task<Contact?> GetContactByIdAsync(int id)
    {
        var contact = await _context.Contacts.FindAsync(id);

        if (contact == null)
        {
            throw new KeyNotFoundException("Oops, Contact not found");
        }

        return contact;
    }
    
    public async Task CreateContactAsync(Contact contact)
    {
       await  _context.Contacts.AddAsync(contact);
       await _context.SaveChangesAsync();
    }

    public async Task UpdateContactAsync(Contact contact, int id)
    {
        var availableContact = await _context.Contacts.FindAsync(id);

        if (contact == null)
        {
            throw new KeyNotFoundException("Oops, Contact not found");
        }
        _context.Contacts.Update(contact);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var contact = await _context.Contacts.FindAsync(id);

        if (contact == null)
        {
            throw new KeyNotFoundException("Oops, You are deleting a contact that doesn't exist.");
        }
        _context.Contacts.Remove(contact);
        await _context.SaveChangesAsync();
    }
    
    
    
}