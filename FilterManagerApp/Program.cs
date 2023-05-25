using FilterManagerApp.Data.Entities;
using FilterManagerApp.Data.Repositories;
using FilterManagerApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FilterManagerApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddSingleton<IApp, App>();
            services.AddSingleton<ICsvReader<Filter>, CsvReader<Filter>>();
            services.AddSingleton<IFilterGenerator, FilterGenerator>();
            services.AddSingleton<IUserCommunication, UserComunnication>();
            services.AddSingleton<IFilterRepository, FilterRepository>();
            services.AddSingleton<IFileWriters<Filter>, FileWriters<Filter>>();
            services.AddSingleton<IEventHandlerService, EventHandlerService>();
            services.AddDbContext<FilterManagerAppDbContext>();

            var serviceProvider = services.BuildServiceProvider();
            var app = serviceProvider.GetRequiredService<IApp>();

            app.Run();

        }
    }
}