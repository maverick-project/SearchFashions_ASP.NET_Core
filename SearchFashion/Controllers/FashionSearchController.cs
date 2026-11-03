using FashionSearch.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SearchFashion.Common;
using SearchFashion.Models;

namespace FashionSearch.Controllers {
   public class FashionSearchController : Controller {
      private readonly rag_fashionsContext _context;

      public FashionSearchController(rag_fashionsContext context)
      {
         _context = context;
      }

      // --- 共通：データベースから今年の最大値を取得するヘルパー ---
      private async Task<int> GetMaxYearAsync()
      {
         return await _context.Fashions.AsQueryable().MaxAsync(x => x.Year)
            ?? DateTime.Now.Year;
      }

      // --- 共通：ViewBag（プルダウンリスト）を設定するヘルパー ---
      private async Task PopulateViewBagAsync(bool initialized, int maxYear, int? selectedYear = null)
      {
         ViewBag.GenderList = new SelectList(
             await _context.Fashions.Where(x => x.Gender != null).Select(x => x.Gender).Distinct().OrderBy(x => x).ToListAsync()
         );
         ViewBag.CategoryList = new SelectList(
             await _context.Fashions.Where(x => x.Category != null).Select(x => x.Category).Distinct().OrderBy(x => x).ToListAsync()
         );
         ViewBag.ArticleTypeList = new SelectList(
             await _context.Fashions.Where(x => x.Articletype != null).Select(x => x.Articletype).Distinct().OrderBy(x => x).ToListAsync()
         );
         ViewBag.SeasonList = new SelectList(
             await _context.Fashions.Where(x => x.Season != null).Select(x => String.IsNullOrWhiteSpace(x.Season) ? "その他": x.Season).Distinct().OrderBy(b => b == "その他" ? 1 : 0).ThenBy(b => b).ToListAsync()
         );
         ViewBag.UsageList = new SelectList(
             await _context.Fashions.Where(x => x.Usage != null).Select(x => String.IsNullOrWhiteSpace(x.Usage) ? "その他": x.Usage).Distinct().OrderBy(b => b == "その他" ? 1 : 0).ThenBy(b => b).ToListAsync()
         );

         var yearOptions = new List<SelectListItem>
            {
                new SelectListItem { Text = $"今年 ({maxYear})", Value = maxYear.ToString() },
                new SelectListItem { Text = $"去年 ({maxYear - 1})", Value = (maxYear - 1).ToString() },
                new SelectListItem { Text = "その他", Value = "その他" }
            };
         ViewBag.YearList = new SelectList(yearOptions, "Value", "Text", selectedYear?.ToString());

         ViewBag.Initialized = initialized;
      }

      // 1. 初期表示 (GET)
      [HttpGet]
      public async Task<IActionResult> Index()
      {
         int maxYear = await GetMaxYearAsync();
         await PopulateViewBagAsync(true, maxYear);

         // 初回は結果を空にする
         var viewModel = new FashionSearchViewModel
         {
            // ページネーションを使用する場合はPaginatedListカスタムリストコレクションを使用する
            // 使用しない場合は普通にリストコレクションで返す
            Results = new PaginatedList<Fashion>()
         };

         return View(viewModel);
      }

      // 2. 検索実行 (Get)
      [HttpGet]
      public async Task<IActionResult> Search(FashionSearchViewModel searchModel)
      {
         if (searchModel.IsInitial)
         {
            searchModel.PageIndex = 1;
         }

         int maxYear = await GetMaxYearAsync();
         await PopulateViewBagAsync(false, maxYear, searchModel.Year);

         // クエリの組み立て（未指定項目は除外）
         var query = _context.Fashions.AsQueryable();

         if (!string.IsNullOrEmpty(searchModel.Gender))
         {
            query = query.Where(x => x.Gender == searchModel.Gender);
         }
         if (!string.IsNullOrEmpty(searchModel.Category))
         {
            query = query.Where(x => x.Category == searchModel.Category);
         }
         if (!string.IsNullOrEmpty(searchModel.ArticleType))
         {
            query = query.Where(x => x.Articletype == searchModel.ArticleType);
         }
         if (!string.IsNullOrEmpty(searchModel.Season))
         {
            query = query.Where(x => x.Season == searchModel.Season);
         }
         if (searchModel.Year.HasValue)
         {
            query = query.Where(x => x.Year == searchModel.Year.Value);
         }
         if (!string.IsNullOrEmpty(searchModel.Usage))
         {
            query = query.Where(x => x.Usage == searchModel.Usage);
         }

         // ソート
         query = query.OrderBy(x => x.Id);

         // 検索結果を格納
         // ページネーションを使用する場合はPaginatedListカスタムリストコレクションを使用する
         // 使用しない場合は普通にリストコレクションで返す
         searchModel.Results = await PaginatedList<Fashion>.CreateAsync(query, searchModel.PageIndex, searchModel.PageSize);

         // 検索結果を同じ Index ビューに返して表示する
         return View("Index", searchModel);
      }
   }
}