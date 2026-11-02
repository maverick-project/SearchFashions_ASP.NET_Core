namespace SearchFashion.ViewModels {
   /// <summary>
   /// ページネーションを使う場合これをViewModelに継承させる
   /// </summary>
   public class PaginatedListViewModel {
      // ページ番号（デフォルトは1）
      public int PageIndex { get; set; } = 1;

      // 1ページあたりの表示件数（デフォルトは10など）
      public int PageSize { get; set; } = 10;

   }
}
