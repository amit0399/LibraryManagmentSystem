using LibraryManagementSystemMVC_Project.BAL.ILogic;
using LibraryManagementSystemMVC_Project.BAL.Logic;

namespace LibraryManagementSystemMVC_Project
{
    public static class ServiceExtension
    {
        public static void DependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<IIssueRepository, IssueRepository>();
            services.AddScoped<IDashboardRepository, DashboardRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
        }
    }
}
