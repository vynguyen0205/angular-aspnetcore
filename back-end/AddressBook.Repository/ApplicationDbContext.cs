using AddressBook.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.DataRepository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ContactTag>()
                .HasKey(contactTag => new {contactTag.TagId, contactTag.ContactId});
        }
    }
}
