﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using NuGet.Protocol;
using ASP.NET_MVC_testapp.Settings;
using SendGrid.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI.Services;
using ASP.NET_MVC_testapp.SendGrid;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<MyDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDbContext") ?? throw new InvalidOperationException("Connection string 'MyDbContext' not found.")));
//builder.Services.AddDbContext<DbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext") ?? throw new InvalidOperationException("Connection string 'DbContext' not found.")));
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MyDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDbContext")));
builder.Services.AddDefaultIdentity<IdentityUser>().AddDefaultTokenProviders().AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<MyDbContext>();


builder.Services.Configure<SendGridSettings>(builder.Configuration.GetSection("SendGridSettings"));

builder.Services.AddSendGrid(options =>
{
    options.ApiKey = builder.Configuration.GetSection("SendGridSettings").GetValue<string>("ApiKey");
});

builder.Services.AddScoped<IEmailSender, EmailSenderServices>();

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

app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
