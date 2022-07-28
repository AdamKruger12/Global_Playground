using ShiftTech.Models.Context;
using Microsoft.EntityFrameworkCore;
using ShiftTech.Models.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<CardContext>(opt => opt.UseInMemoryDatabase("CardServer"));
//this is to populate the config with some basic cards i found on the internet...
var optionBuilder = new DbContextOptionsBuilder<CardContext>().UseInMemoryDatabase("CardServer");
var context = new CardContext(optionBuilder.Options);
DbInitializer.Initialize(context);


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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=ValidateCard}/{id?}");

app.Run();
