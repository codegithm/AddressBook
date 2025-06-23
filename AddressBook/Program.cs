using AddressBook.Data;
using AddressBook.Middlewares;
using AddressBook.Models;
using AddressBook.Repositories.ApiUsageDataRepository;
using AddressBook.Repositories.ApiUsageDataRepository.Interface;
using AddressBook.Repositories.AuthRepository;
using AddressBook.Repositories.AuthRepository.Interface;
using AddressBook.Repositories.ContactRepository;
using AddressBook.Repositories.ContactRepository.Interface;
using AddressBook.Repositories.ExportDataRepository;
using AddressBook.Repositories.ExportDataRepository.Interface;
using AddressBook.Repositories.ImportDataRepository;
using AddressBook.Repositories.ImportDataRepository.Interface;
using AddressBook.Services.ApiService;
using AddressBook.Services.ApiService.Interface;
using AddressBook.Services.AuthService;
using AddressBook.Services.AuthService.Interface;
using AddressBook.Services.ContactService;
using AddressBook.Services.ContactService.Interface;
using AddressBook.Services.DataExportService;
using AddressBook.Services.ExportDataService;
using AddressBook.Services.ExportDataService.Interface;
using AddressBook.Services.ImportDataService;
using AddressBook.Services.ImportDataService.Interface;
using AddressBook.Services.SystemOverviewService;
using AddressBook.Services.SystemOverviewService.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AddressBookContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbAddressBook")));
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ISystemOverviewService, SystemOverviewService>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IImportDataService, ImportDataService>();
builder.Services.AddScoped<IImportDataRepository, ImportDataRepository>();
builder.Services.AddScoped<IDataExportService, DataExportService>();
builder.Services.AddScoped<IExportDataRepository, ExportDataRepository>();
builder.Services.AddScoped<IApiUsageDataRepository, ApiUsageDataRepository>();
builder.Services.AddScoped<IApiService, ApiService>();

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
})
.AddEntityFrameworkStores<AddressBookContext>()
.AddDefaultTokenProviders();
builder.Services.AddAuthentication();

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

app.UseRouting();
app.UseAuthentication();
app.UseMiddleware<PageAccessMiddleware>();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
