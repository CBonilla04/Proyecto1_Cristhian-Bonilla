using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Proyecto1_CristhianBonilla.Services;
using Proyecto1_CristhianBonilla.Utils;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
//configuración de la base de datos
builder.Services.AddDistributedMemoryCache();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
//configuración de envío de correos
var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IReservationOrder, ReservationOrder>();
builder.Services.AddScoped<IFlightScaleService, FlightScaleService>();

builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddHttpClient<IAmadeusApiService, AmadeusApiService>();
builder.Services.AddHttpClient<IHomeService, HomeService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
//permite la autenticación de los usuarios
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Login/LogIn"; 
            });
//permite establecer un tiempo de inactividad para la sesión
builder.Services.AddSession(options =>
{   
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.IsEssential = true;
});

var app = builder.Build();
//inicaliza la base de datos
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.InitializeDatabase();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
//agrega rutas para los archivos de estilos
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "Source")),
    RequestPath = "/source"
});
//agrega rutas para los archivos de plantillas de correo
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "Utils/EmailTemplates")),
    RequestPath = "/emailTemplates"
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

//Garantiza que los usuarios no puedan acceder a las rutas sin haber iniciado sesión
app.Use(async (context, next) =>
{
    var session = context.Session;
    if (session.GetString("CurrentUser") == null && context.Request.Path != "/Session/Expired" && context.Request.Path != "/Login/LogIn"
        && context.Request.Path != "/" && context.Request.Path != "/Users/AddUser")
    {
        session.Clear();
        context.Response.Redirect("/Session/Expired");
        return;
    }

    await next.Invoke();
});
//vista inciial
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=LogIn}/{id?}");

app.Run();
