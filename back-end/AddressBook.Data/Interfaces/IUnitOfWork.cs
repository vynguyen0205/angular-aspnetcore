using System.Threading.Tasks;
using AddressBook.Data.Entities;

namespace AddressBook.Data.Interfaces
{
    public interface IUnitOfWork
    {
        void SaveChange();

        Task SaveChangeAsync();

        IRepository<Contact> ContactRepository { get; }

        IRepository<Tag> TagRepository { get; }

        IRepository<ContactTag> ContactTagRepository { get; }
    }
}
