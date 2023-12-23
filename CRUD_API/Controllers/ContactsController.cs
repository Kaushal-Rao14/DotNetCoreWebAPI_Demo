using CRUD_API.Data;
using CRUD_API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD_API.Controllers
{
    [ApiController]
    [Route("api/Contacts")]
    //[Route("api/controller")]
    public class ContactsController : Controller
    {
        private readonly ContactAPIDbContext DbContext;

        public ContactsController(ContactAPIDbContext dbContext)
        {
            this.DbContext = dbContext;
        }


        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var  contact =await DbContext.Contacts.FindAsync(id);
            if(contact==null)
            {
                return NotFound();
            }
           
            return Ok(contact);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllContacts()
        {
            return Ok(await DbContext.Contacts.ToListAsync());
        }
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
            await DbContext.Contacts.AddAsync(contact);
            await DbContext.SaveChangesAsync();
            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateContact( [FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {
            var contact = await DbContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                contact.Name = updateContactRequest.Name;
                contact.Email = updateContactRequest.Email;
                contact.Phone = updateContactRequest.Phone;
                contact.Address = updateContactRequest.Address;
                await DbContext.SaveChangesAsync();
                return Ok(contact);
            }
           

              return NotFound();
        }


        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await DbContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                DbContext.Remove(contact);
                await DbContext.SaveChangesAsync();
                return Ok(contact);

            }

           
            return NotFound();
        }
    }
}
