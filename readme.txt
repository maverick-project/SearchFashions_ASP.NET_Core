１．ASP.NET CoreにPostgreSQLのEFを使用するための環境構築手順(DBファーストの場合)
　　1)NuGet パッケージのインストール
	Npgsql.EntityFrameworkCore.PostgreSQL

    2)拡張機能のインストール
　　　　EF Core Power Tools
　　　　※ほかにもあった気がする

　　3)appsettings.json に接続文字列を設定する
        "ConnectionStrings": {
	   "FashionDbConnection": "Host=localhost;Port=5432;Database=rag_fashions;Username=postgres;Password=postgres;"
	}

　　4)DbContext と モデルの準備
　　    ①プロジェクトファイル名上で右クリック
        ②EF Core Power Toolsをクリック
　　　　③リバースエンジニアリングをクリック

    5)Program.cs で DbContext を登録する
　　　　builder.Services.AddDbContext<rag_fashionsContext>(options =>
    　　　options.UseNpgsql(builder.Configuration.GetConnectionString("FashionDbConnection")));

２．ページネーション
　　1)共通機能
　　　　Common/IPaginatedList.cs
　　　　Common/PaginatedList.cs
　　　　ViewModels/PaginatedListViewModel.cs
　　　　Views/Shared/_PagenationPartial.cshtml
　　　　Views/Shared/_PagenationSummaryPartial.cshtml

　　2)組み込み手順
　　　　①ViewModel
　　　　　　・PaginatedListViewModelクラスを継承する
　　　　　　・必要に応じてPageSizeを設定する(PaginatedListViewModelは修正しないこと)
　　　　　　・ResultsプロパティをPaginatedListカスタムリストコレクションで定義する
　　　　②Controllers
　　　　　　・クエリによる検索結果をPaginatedListカスタムリストコレクションとしてResultsに格納する。
　　　　③Views
　　　　　　・テーブルの上にページネーションのサマリー表示を組み込む
　　　　　　・テーブルの下にページネーションUIを表示

３．カスタムエラーページ

　　1)共通機能
　　　　Controllers/CustomErrorController.cs
　　　　ViewModels/CustomErrorViewModel.cs
　　　　Views/Shared/CustomError.cshtml
　　　　　デザインの修正が必要
        Program.cs
　　　　　UseExceptionHandler()の呼び出しなどCustomErrorに遷移させる処理の追加

　　2)組み込み手順
　　　　なし

４．複合バリデーションチェック

　　1)共通機能
　　　　Common/CustomValidationHelper.cs
　　　　　現在年の大小判定の未実装。必要に応じて機能追加

　　2)組み込み手順
　　　・ViewModels/FashionSearchViewModel.cs
　　　　　IValidatableObjectインターフェースを継承し、Validateを実装
　　　・Controllers/FashionSearchController.cs
　　　　　if (!ModelState.IsValid)の実装
　　　・ViewModels/FashionSearchViewModel.cs
　　　　　IValidatableObjectインターフェース付与
　　　　　Validateの実装
　　　・Views/FashionSearch/Index.cshtml
　　　　　<div asp-validation-summary="All" class="alert alert-danger" role="alert"></div>の実装
      
