using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Resend;
using StudyTimer.Domain.Identity;
using StudyTimer.MVC.Services;
using StudyTimer.Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddControllersWithViews()
    .AddNToastNotifyToastr();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();

string connectionString = builder.Configuration.GetSection("Team7PostgreSQLDB").Value;
builder.Services.AddDbContext<StudyTimerDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    options.ValidationInterval = TimeSpan.FromMinutes(30);
});

//builder.Services.AddMvc().AddNToastNotifyToastr();

builder.Services.AddIdentity<User, Role>(options =>
{
    // User Password Options
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    // User Username and Email Options
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@$";
    options.User.RequireUniqueEmail = true;

}).AddEntityFrameworkStores<StudyTimerDbContext>()
    .AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider);


builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    options.ValidationInterval = TimeSpan.FromMinutes(30);
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = new PathString("/Auth/Login");
    options.LogoutPath = new PathString("/Auth/Logout");
    options.Cookie = new CookieBuilder
    {
        Name = "StudyTimer",
        HttpOnly = true,
        SameSite = SameSiteMode.Strict,
        SecurePolicy = CookieSecurePolicy.SameAsRequest // Always
    };
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = System.TimeSpan.FromDays(7);
    options.AccessDeniedPath = new PathString("/Auth/AccessDenied");
});

var connectionStringResend = builder.Configuration.GetSection("Resend").Value;

builder.Services.AddOptions();
builder.Services.AddHttpClient<ResendClient>();
builder.Services.Configure<ResendClientOptions>(o =>
{
    //o.ApiToken = Environment.GetEnvironmentVariable("re_L8odUwat_7aPtnQLNGSjWV62JCuuXMfQj")!; //Dikkat Sil
    o.ApiToken = Environment.GetEnvironmentVariable(connectionStringResend)!;
});


builder.Services.AddTransient<IResend, ResendClient>();
builder.Services.AddScoped<AuthManager>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IToastService, ToastService>();
builder.Services.AddScoped<StudySessionManager>();



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

app.UseAuthorization();

app.UseSession();

app.UseNToastNotify();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
