using Microsoft.EntityFrameworkCore;
using Proyecto1_CristhianBonilla.Services;
using Proyecto1_CristhianBonilla.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IReservationOrder, ReservationOrder>();
builder.Services.AddScoped<IFlightScaleService, FlightScaleService>();
builder.Services.AddHttpClient<IAmadeusApiService, AmadeusApiService>();
builder.Services.AddHttpClient<IHomeService, HomeService>();
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

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

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=LogIn}/{id?}");

app.Run();
