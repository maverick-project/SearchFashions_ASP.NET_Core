using SearchFashion.Common;
using SearchFashion.Models;
using SearchFashion.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace FashionSearch.ViewModels {
   public class FashionSearchViewModel : PaginatedListViewModel, IValidatableObject {
      public string? Gender { get; set; }
      public string? Category { get; set; }
      public string? ArticleType { get; set; }
      public string? Season { get; set; }
      public int? StartYear { get; set; }
      public int? EndYear { get; set; }
      public string? Usage { get; set; }

      // 検索結果のリスト
      // ページネーションを使う場合はListコレクションではなくPaginatedListカスタムコレクションを使う
      public PaginatedList<Fashion>? Results { get; set; } = null;

      public FashionSearchViewModel()
      {
         // 1ページあたりに表示する最大行数
         base.PageSize = 5;
      }

      public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
      {
         if (!CustomValidationHelper.ValidateYearRange(StartYear, EndYear, out var errorMsg))
         {
            // １回だけエラーメッセージを表示させるためStartYearだけ渡す
            yield return new ValidationResult(errorMsg, new[] { nameof(StartYear) });
         }
      }
   }
}
