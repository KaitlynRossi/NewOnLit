using ASPProject.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// 💡 Persist Data Protection keys to the file system
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(@"./keys")) // Ensure this folder exists or use an absolute path
    .SetApplicationName("MyApp"); // Helps in multi-instance environments

// Add session support
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".MyApp.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Set up SQLite DbContext (or change to your preferred database)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=books.db"));

// Add any other services you need
builder.Services.AddScoped<BookRepository>();

var app = builder.Build();

// Seed the database (only once after building the app)
using (var scope = app.Services.CreateScope()) // This ensures scoped services can be resolved
{
    var services = scope.ServiceProvider;
    DbInitializer.Seed(services); // Pass the scoped service provider to Seed
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession(); // Must come before UseRouting

app.UseRouting();

// Set up routing for MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(static endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
#pragma warning restore ASP0014 // Suggest using top level route registrations

app.MapControllerRoute(
    name: "myview",
    pattern: "Home/MyView",
    defaults: new { controller = "Home", action = "MyView" });


app.Run();
