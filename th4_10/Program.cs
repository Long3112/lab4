﻿using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;
using th4_10.Data;

var builder = WebApplication.CreateBuilder(args);
//builder.Configuration
// .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("Secrets.json");
//Đăng ký SchoolContext là một DbContext của ứng dụng
builder.Services.AddDbContext<SchoolContext>(options => options
.UseSqlServer(builder.Configuration.GetConnectionString("SchoolContext")));
// Add services to the container.
builder.Services.AddControllersWithViews();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    DbInitializer.Initialize(services);
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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