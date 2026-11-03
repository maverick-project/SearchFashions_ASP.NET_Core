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

１．ページネーション
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



