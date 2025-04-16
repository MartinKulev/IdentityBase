using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using LoginIdentityBase.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using LoginIdentityBase.Areas.Identity.Data;
using LoginIdentityBase.Services;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Configuration.AddJsonFile(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "GitSecrets.json"), optional: true);
string? connectionString = builder.Configuration.GetConnectionString("IdentityBase") ?? builder.Configuration.GetConnectionString("DefaultConnection")!;

builder.Services.AddDbContext<LoginIdentityBaseContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddDefaultIdentity<LoginIdentityBaseUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<LoginIdentityBaseContext>();

builder.Services.AddTransient<EmailSender>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
