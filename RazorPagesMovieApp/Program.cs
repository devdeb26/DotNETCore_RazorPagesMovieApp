using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RazorPagesMovieApp.Data;
using RazorPagesMovieApp.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<RazorPagesMovieAppContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("RazorPagesMovieAppContext")));
}
else
{
    builder.Services.AddDbContext<RazorPagesMovieAppContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ProductionMovieAppContext")));
}
var app = builder.Build();

//For Seeding
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

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

app.MapRazorPages();

app.Run();
