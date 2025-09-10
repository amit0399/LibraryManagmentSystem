using LibraryManagementSystemMVC_Project;
using LibraryManagementSystemMVC_Project.DatabaseConnection;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystemMVC_Project.MiddleWares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();


//Add Services for database Connection
builder.Services.AddDbContext<DBConnectionFactory>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Add Services to Inject in DI Container 
builder.Services.DependencyInjection();

//Add session to the container
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(15);
    option.Cookie.HttpOnly = true;
    option.Cookie.IsEssential = true;
});



var app = builder.Build();




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseMiddleware<CheckLoggerMiddlewares>();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    //pattern: "{controller=Home}/{action=Index}/{id?}");
pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
