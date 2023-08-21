using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using OfficeTelerik;
using OfficeTelerik.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider,CustomAuthStatePrvider>();
builder.Services.AddAuthenticationCore();
//builder.Services.AddTelerikBlazor();



var app = builder.Build();

builder.Configuration.GetSection("Config").Bind(new Config());
DataAccessLayer.DBTools.DBConfig.ConnectionString = Config.ConnectionString;
builder.Configuration.GetSection("MyValues").Bind(new AppSettingValues());


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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
