using AMS.Data;
using AMS.Repository;
using AMS.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<IAccountServices, AccountServices>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<AmsContext>
    (s =>
        s.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), x => x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
    )
);

builder.Services.AddAuthentication("AMSCookies")
    .AddCookie("AMSCookies",
    (x) =>
    {
        x.LoginPath = "/Account/Login";
        x.AccessDeniedPath = "/Account/AccessDenied";
        x.Cookie.Name = "AMSCookies";
        x.Cookie.SameSite = SameSiteMode.Strict;
        x.Cookie.IsEssential = true;
        x.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    });

//builder.Services.AddScoped<IAccountRepository, AccountRepository>();
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
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
