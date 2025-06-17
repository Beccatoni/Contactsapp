using ContactsApp.Api.DTOs;
using ContactsApp.Api.Models;
using ContactsApp.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApp.Api.Controllers;

public class ContactsController: ControllerBase
{
    private readonly IContactService _service;

    public ContactsController(IContactService service)
    {
        _service = service;
    }
    
    // GET: api/Contacts
    [HttpGet]
    public async Task<ActionResult<List<Contact>>> GetAllAsync()
    {
        return Ok(await _service.GetAllAsync());
    }
    
    // GET: api/Contacts/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Contact>> GetContactByIdAsync(int id)
    {
       var contact = await _service.GetContactByIdAsync(id);
       return contact == null ? NotFound() : Ok(contact);
    }
    
    // POST: api/Contacts/addnew
    [HttpPost]
    public async Task<ActionResult<Contact>> CreateContactAsync(CreateContactDto contactDto)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        await _service.CreateContactAsync(contactDto);
        return Ok("Contact created successfully");
    }
    
    // PUT api/Contacts/5
    [HttpPut("{id}")]
    public async Task<ActionResult<Contact>> UpdateContactAsync(int id, UpdateContactDto contactDto)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        await _service.UpdateContactAsync(id, contactDto);
        return Ok("Contact updated successfully");
    }
    
    // DELETE api/Contacts/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Contact>> DeleteAsync(int id)
    {
        await _service.DeleteAsync(id);
        return Ok("Contact deleted successfully");
    }

}