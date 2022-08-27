using Microsoft.AspNetCore.Identity;
using WazeCreditGreen.Data;
using WazeCreditGreen.Service;
using WazeCreditGreen.Utility.AppSettingClasses;
using WazeCreditGreen.Utility.DIConfig;
using WazeCreditGreen.Service.LifeTimeExample;
using WazeCreditGreen.Middleware;
using WazeCreditGreen.Models;
using Microsoft.EntityFrameworkCore;
//https://github.com/dotnet/extensions/issues/2084
using Microsoft.Extensions.DependencyInjection.Extensions;
//https://github.com/andrewlock/NetEscapades.Extensions.Logging/issues/2
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);


//Using Serilog, without ILogger
// Log.Logger = new LoggerConfiguration()
// .MinimumLevel.Debug()
// .WriteTo.File("logs/creditApp-log-{Date}.txt")
// .CreateLogger();


// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddAppSettingsConfig(builder.Configuration).AddAllServices();


var app = builder.Build();

//https://stackoverflow.com/questions/69938319/how-to-get-iloggerfactory-in-net-6
var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseMigrationsEndPoint();
} else {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You m ay want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
loggerFactory.AddFile("logs/creditApp-log-{Date}.txt");
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<CustomMiddleware>();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
