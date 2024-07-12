using Proyecto1_CristhianBonilla.Utils;
using Microsoft.EntityFrameworkCore;
using Proyecto1_CristhianBonilla.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient<IAmadeusApiService, AmadeusApiService>();
builder.Services.AddHttpClient<IHomeService, HomeService>();

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options => { 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
var app = builder.Build();

;
// Inicializa la base de datos aquí, después de que el app es construido.
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.InitializeDatabase(); // Llama al método de inicialización
    }
//var amadeusApiService = app.Services.GetRequiredService<IAmadeusApiService>();
//await amadeusApiService.AuthenticateAsync("0pYa9rS3KzA0aFecnSAgU8DUI3BOmBql", "bG6OnyjSJclDij03");



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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
