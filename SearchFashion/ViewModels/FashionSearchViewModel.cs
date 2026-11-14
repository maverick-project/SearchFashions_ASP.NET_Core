using SearchFashion.Common;
using SearchFashion.Models;
using SearchFashion.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace FashionSearch.ViewModels {
   public class FashionSearchViewModel : PaginatedListViewModel, IValidatableObject {
      [Display(Name = "性別")]
      public string? Gender { get; set; }
      [Display(Name = "カテゴリ")]
      public string? Category { get; set; }
      [Display(Name = "商品区分")]
      public string? ArticleType { get; set; }
      [Display(Name = "季節")]
      public string? Season { get; set; }
      [Display(Name = "販売開始年")]
      public int? StartYear { get; set; }
      [Display(Name = "販売終了年")]
      public int? EndYear { get; set; }
      [Display(Name = "用途")]
      public string? Usage { get; set; }

      [Display(Name = "画像")]
      public string? ImagePath { get; set; }

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
