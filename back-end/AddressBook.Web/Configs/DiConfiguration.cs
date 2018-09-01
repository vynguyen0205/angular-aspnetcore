using AddressBook.Data.Interfaces;
using AddressBook.DataRepository.DataAccess;
using AddressBook.Service.BusinessServices;
using AddressBook.ServiceImplementation.BusinessServices;
using Microsoft.Extensions.DependencyInjection;

namespace AddressBook.Web.Configs
{
    public static class  DiConfiguration
    {
        public static void SetUp(IServiceCollection services)
        {
            //Set up data access classes
            SetUpDataAccess(services);

            //Set up services
            SetUpServices(services);
        }

        private static void SetUpDataAccess(IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddTransient<ISqlCommandExecutor, SqlCommandExecutor>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        private static void SetUpServices(IServiceCollection services)
        {
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<ITagService, TagService>();
        }
    }
}
