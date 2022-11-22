using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactsAPI.Data;
using ContactsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContactsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly ContactsAPIDbContext dbContext;

        public ContactsController(ContactsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        // GET: /<controller>/
        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await dbContext.Contacts.ToListAsync());
        }

        // Get Individual Contact
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var isContactExist = await dbContext.Contacts.FindAsync(id);

            if(isContactExist != null)
            {
                return Ok(isContactExist);
            }

            return NotFound();
        }

        // Add Contact
        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Name = addContactRequest.Name,
                Email = addContactRequest.Email,
                Phone = addContactRequest.Phone,
                Address = addContactRequest.Address
            };
            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();

            return Ok(contact);


        }

        // Update Contact 
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {
           var isContactExist = await dbContext.Contacts.FindAsync(id);
            if (isContactExist != null)
            {
                isContactExist.Name = updateContactRequest.Name != null ? updateContactRequest.Name : isContactExist.Name;
                isContactExist.Phone = updateContactRequest.Phone;
                isContactExist.Address = updateContactRequest.Address != null ? updateContactRequest.Address : isContactExist.Address;
                isContactExist.Email = updateContactRequest.Email != null ? updateContactRequest.Email : isContactExist.Email;

                await dbContext.SaveChangesAsync();
                return Ok(isContactExist);
            }

            return NotFound();
        }

        // Delete Contact
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var isContactExist = await dbContext.Contacts.FindAsync(id);

            if (isContactExist != null)
            {
                 dbContext.Remove(isContactExist);
                 await dbContext.SaveChangesAsync();
                return Ok(isContactExist);
            }

            return NotFound();
        }
    }
}

