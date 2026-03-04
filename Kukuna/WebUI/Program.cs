using AccesoDatos.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<SqlServerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<AccesoDatos.DAShoppingLists>();
builder.Services.AddScoped<AccesoDatos.DAMealPlans>();
builder.Services.AddScoped<AccesoDatos.DAIngredients>();
builder.Services.AddScoped<AccesoDatos.DARecipes>();
builder.Services.AddScoped<AccesoDatos.DAUnits>();
builder.Services.AddScoped<AccesoDatos.DARecipeIngredients>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
