using BusinessLogic;
using BusinessLogic.Services;
using BusinessLogic.Interfaces;
using CalendarMVCSite.Filters;
using Microsoft.EntityFrameworkCore;
using Serilog;
using FluentValidation;
using System;
using CalendarMVCSite.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<ExceptionFilter>();
});

builder.Services.AddScoped<IMeetingsService, MeetingsService>();
builder.Services.AddScoped<IRoomsService, RoomsService>();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

//builder.Host.ConfigureLogging(logging =>
//{
//    logging.ClearProviders();
//    logging.AddConsole();
//});

ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseSerilogRequestLogging();


app.UseRouting();

app.UseAuthorization();
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddDbContext<CalendarDbContext>(options =>
    {
        //options.UseInMemoryDatabase("PersonDbContext");
        
        options.UseSqlServer("Server=localhost;Database=MeetingsDb;Integrated Security=true;TrustServerCertificate=True;");
    });

    services.AddScoped<IValidator<CreateMeetingModel>, CreateMeetingModelValidator>();
    services.AddScoped<IValidator<EditMeetingModel>, EditMeetingModelValidator>();
}