using System.Threading.Tasks;
using AddressBook.Data.Entities;
using AddressBook.Data.Interfaces;

namespace AddressBook.DataRepository.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SaveChange()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveChangeAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        private IRepository<Contact> _contactRepository;
        public IRepository<Contact> ContactRepository
        {
            get
            {
                return _contactRepository = _contactRepository ?? new EfRepository<Contact>(_dbContext);
            }
        }

        private IRepository<Tag> _tagRepository;
        public IRepository<Tag> TagRepository
        {
            get
            {
                return _tagRepository = _tagRepository ?? new EfRepository<Tag>(_dbContext);
            }
        }

        private IRepository<ContactTag> _contactTagRepository;
        public IRepository<ContactTag> ContactTagRepository
        {
            get
            {
                return _contactTagRepository = _contactTagRepository ?? new EfRepository<ContactTag>(_dbContext);
            }
        }
    }
}
