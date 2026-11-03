namespace SearchFashion.Common {
   public interface IPaginatedList {
      public int PageIndex { get; } // 現在表示するページ番号
      public int TotalPages { get; } // 総ページ数
      public int StartPage { get; } // ページネーション(1, 2, 3, 4, 5のやつ)の最初のページ番号
      public int EndPage { get; } // ページネーション(1, 2, 3, 4, 5のやつ)の最後のページ番号
      public bool HasPreviousPage { get; } // 前へが活性表示か
      public bool HasNextPage { get; } // 次へが活性表示か
      public int StartIndex { get; } // 画面に表示する最初のレコードのindex
      public int EndIndex { get; } // 画面に表示する最後のレコードのindex

      public int TotalCount { get; } // データ総件数
   }
}
