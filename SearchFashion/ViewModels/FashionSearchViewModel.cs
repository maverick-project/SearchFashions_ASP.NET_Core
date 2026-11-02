using SearchFashion.Models;

namespace FashionSearch.ViewModels {
   public class FashionSearchViewModel {
      public string? Gender { get; set; }
      public string? Category { get; set; }
      public string? ArticleType { get; set; }
      public string? Season { get; set; }
      public int? Year { get; set; } // 今年、去年、おととしの数値、または未指定(null)が入る
      public string? Usage { get; set; }

      // 検索結果のリスト
      public List<Fashion> Results { get; set; } = new();
   }
}
