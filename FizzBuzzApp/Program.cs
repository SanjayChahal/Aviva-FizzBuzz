using FizzBuzzApp.Interfaces;
using FizzBuzzApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

// Middle ware components 


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IFizzBuzzService, FizzBuzzService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ISessionService, SessionService>();


// State managment 
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); //This middleware is used to handle exceptions
    app.UseHsts(); //This middleware sets HTTP Strict Transport Security
}

app.UseHttpsRedirection(); //This middleware automatically redirects HTTP requests to HTTPS.
app.UseStaticFiles(); // This middleware is used to serve static files such as HTML, CSS, JavaScript, and images.
app.UseHttpLogging(); // This middleware logs HTTP requests and responses for diagnostic purposes.

app.UseRouting(); // This middleware sets up routing for incoming requests, allowing the application to determine which endpoint should handle each request.
app.UseSession();

app.UseAuthorization(); //This middleware enables authorization, allowing the application to restrict access to certain resources based on user roles or policies.

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

