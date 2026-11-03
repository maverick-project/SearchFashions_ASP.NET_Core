using Microsoft.EntityFrameworkCore;

namespace SearchFashion.Common {
   /// <summary>
   /// ページネーションで使用するカスタムコレクションクラス
   /// </summary>
   /// <typeparam name="T"></typeparam>
   public class PaginatedList<T> : List<T>, IPaginatedList {
      public int PageIndex { get; private set; } // 現在表示するページ番号
      public int TotalPages { get; private set; } // 総ページ数
      public int StartPage { get; private set; } // ページネーション(1, 2, 3, 4, 5のやつ)の最初のページ番号
      public int EndPage { get; private set; } // ページネーション(1, 2, 3, 4, 5のやつ)の最後のページ番号
      public bool HasPreviousPage => PageIndex > 1; // 前へが活性表示か
      public bool HasNextPage => PageIndex < TotalPages; // 次へが活性表示か
      public int StartIndex {  get; private set; } // 画面に表示する最初のレコードのindex
      public int EndIndex { get; private set; } // 画面に表示する最後のレコードのindex

      public int TotalCount { get; private set; } // データ総件数

      private const int MAXBUTTONS = 5;

      public PaginatedList()
      { }

      public PaginatedList(List<T> items, int totalCount, int pageIndex, int pageSize)
      {
         PageIndex = pageIndex;
         TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

         // データのリストを設定
         this.AddRange(items);

         // 1. 総ページ数が MAXBUTTONS 以下のときは、全ページをそのまま表示する
         if (TotalPages <= MAXBUTTONS)
         {
            StartPage = 1;
            EndPage = TotalPages;
         }
         else
         {
            // 2. 総ページ数が多いときは、現在のページを中心に配置する
            int half = MAXBUTTONS / 2;
            int start = PageIndex - half;
            int end = PageIndex + half;

            // 左端（1ページ目）からはみ出る場合
            if (start < 1)
            {
               StartPage = 1;
               EndPage = MAXBUTTONS;
            }
            // 右端（最後のページ）からはみ出る場合
            else if (end > TotalPages)
            {
               EndPage = TotalPages;
               StartPage = TotalPages - MAXBUTTONS + 1;
            }
            // どちらでもない場合（真ん中あたりにいるとき）
            else
            {
               StartPage = start;
               EndPage = end;
            }
         }

         TotalCount = totalCount;
         StartIndex = TotalPages == 0 ? 0 : (PageIndex - 1) * pageSize + 1;
         EndIndex = Math.Min(PageIndex * pageSize, TotalCount);
      }

      public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
      {
         // データ総数
         var count = await source.CountAsync();
         // 表示対象のレコードだけに絞り込む
         var items = await source.Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();

         return new PaginatedList<T>(items, count, pageIndex, pageSize);
      }
   }
}