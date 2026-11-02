using Microsoft.EntityFrameworkCore;
using SearchFashion.Models;

var builder = WebApplication.CreateBuilder(args);

// ★ ここを追加：rag_fashionsContext を DI に登録する
builder.Services.AddDbContext<rag_fashionsContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("FashionDbConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
   app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=FashionSearch}/{action=Index}")
    .WithStaticAssets();


app.Run();
