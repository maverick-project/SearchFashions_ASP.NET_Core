namespace SearchFashion.ViewModels {
   /// <summary>
   /// ページネーション使用時にコントローラーに渡すパラメータ
   /// ページネーションを使う場合これを自分が使用するViewModelに継承させる
   /// </summary>
   public class PaginatedListViewModel {
      // ページ番号（デフォルトは1）
      public int PageIndex { get; set; } = 1;

      // 1ページあたりの表示件数（共通なので修正禁止。
      // PaginatedListViewModelを継承したサブクラス側で修正すること）
      public int PageSize { get; set; } = 10;

      // trueの場合ページネーションを初期化する
      public bool IsInitial { get; set; } = true;
   }
}
