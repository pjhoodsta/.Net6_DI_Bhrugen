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


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IMarketForecaster, MarketForecaster>();
builder.Services.AddAppSettingsConfig(builder.Configuration);
//builder.Services.AddScoped<IValidationChecker, CreditValidationChecker>(); 
//builder.Services.AddScoped<IValidationChecker, AddressValidationChecker>();
//decapreated - only for demo purposes
// builder.Services.TryAddEnumerable(ServiceDescriptor.Scoped<IValidationChecker, AddressValidationChecker>());
// builder.Services.TryAddEnumerable(ServiceDescriptor.Scoped<IValidationChecker, CreditValidationChecker>());

builder.Services.TryAddEnumerable(new[]{
    ServiceDescriptor.Transient<IValidationChecker, CreditValidationChecker>(),
    ServiceDescriptor.Transient<IValidationChecker, AddressValidationChecker>()
});


builder.Services.AddScoped<ICreditValidator, CreditValidator>();

builder.Services.AddTransient<TransientService>();
builder.Services.AddTransient<SingletonService>();
builder.Services.AddTransient<ScopedService>();

builder.Services.AddScoped<CreditApprovedHigh>();
builder.Services.AddScoped<CreditApprovedLow>();

builder.Services.AddScoped<Func<CreditApprovedEnum, ICreditApproved>>(ServiceProvider => range => {
    switch (range) {
        case CreditApprovedEnum.Low:
            return ServiceProvider.GetService<CreditApprovedLow>();
        case CreditApprovedEnum.High:
            return ServiceProvider.GetService<CreditApprovedHigh>();
        default: return ServiceProvider.GetService<CreditApprovedLow>();
    }
});

var app = builder.Build();


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

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<CustomMiddleware>();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
