using CRUD_API.Model;
using Microsoft.EntityFrameworkCore;

namespace CRUD_API.Data
{
    public class ContactAPIDbContext : DbContext
    {
        public ContactAPIDbContext(DbContextOptions option) :base(option)
        {

        }
        public DbSet<Contact> Contacts { get; set; }
    }
}
