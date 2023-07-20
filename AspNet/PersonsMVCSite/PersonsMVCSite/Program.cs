using Microsoft.EntityFrameworkCore;
using PersonsMVCSite.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddDbContext<PersonDbContext>(options =>
    {
        options.UseInMemoryDatabase("PersonDbContext");
    });
}

