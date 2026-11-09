using Lant_Hotelier.Data;
using Lant_Hotelier.Models;
using Lant_Hotelier.Repositories;
using Lant_Hotelier.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Identity cu roluri - o singura inregistrare (fara AddDefaultIdentity!)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders()
.AddDefaultUI();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Repositories
builder.Services.AddScoped<LanturiRepository>();
builder.Services.AddScoped<HoteluriRepository>();
builder.Services.AddScoped<RecenziiRepository>();
builder.Services.AddScoped<GalerieRepository>();
builder.Services.AddScoped<CamereRepository>();
builder.Services.AddScoped<ServiciiRepository>();
builder.Services.AddScoped<OferteRepository>();
builder.Services.AddScoped<EvenimenteRepository>();
builder.Services.AddScoped<RezervariRepository>();

// Services
builder.Services.AddScoped<LanturiService>();
builder.Services.AddScoped<HoteluriService>();
builder.Services.AddScoped<RecenziiService>();
builder.Services.AddScoped<GalerieService>();
builder.Services.AddScoped<CamereService>();
builder.Services.AddScoped<ServiciiService>();
builder.Services.AddScoped<OferteService>();
builder.Services.AddScoped<EvenimenteService>();
builder.Services.AddScoped<RezervariService>();
builder.Services.AddScoped<IdentityService>();

var app = builder.Build();

// Seed roluri la startup
using (var scope = app.Services.CreateScope())
{
    var identityService = scope.ServiceProvider.GetRequiredService<IdentityService>();
    await identityService.EnsureRolesCreatedAsync();
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

app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.MapRazorPages();

app.Run();
