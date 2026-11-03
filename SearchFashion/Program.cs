using Microsoft.EntityFrameworkCore;
using SearchFashion.Models;

var builder = WebApplication.CreateBuilder(args);

// rag_fashionsContext を DI に登録する
builder.Services.AddDbContext<rag_fashionsContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("FashionDbConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
   // 本番環境のときのエラー設定
   // 例外が発生したら "/CustomError/Error" に遷移させる
   app.UseExceptionHandler("/CustomError/Error");

   // HTTPステータスコード（404など）に対するカスタムページを出したい場合
   // app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");

   app.UseHsts();
}
else
{
   // 開発環境のときは詳細なエラー画面（Developer Exception Page）を表示
   app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=FashionSearch}/{action=Index}")
    .WithStaticAssets();


app.Run();
