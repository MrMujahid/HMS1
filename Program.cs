using Microsoft.EntityFrameworkCore;
using HMS.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<Db>(options => options.UseSqlServer("Server=127.0.0.1;Database=HMS;User Id=sa;Password=1234;Encrypt=False"));//added


//Important


builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

}).AddCookie("Cookies", options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.Name = "Cookies";
    options.LoginPath = "/";
    options.AccessDeniedPath = "/AccessDenied/Index";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    options.LoginPath = "/";
    options.ReturnUrlParameter = "/";
    options.SlidingExpiration = true;
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

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    //pattern: "{controller=Home}/{action=Index}/{id?}");
pattern: "{controller=Auth}/{action=Index}/{id?}");


app.Run();
