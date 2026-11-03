using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FashionSearch.ViewModels;

namespace SearchFashion.Controllers {
   public class CustomErrorController : Controller {
      [Route("CustomError/Error")]
      public IActionResult Error()
      {
         // 発生した例外の情報を取得する
         var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

         string loginUser = "ゲスト";
         try
         {
            loginUser = HttpContext.Session.GetString("UserName");
         }
         catch(Exception)
         {
            // セッションが無効な場合例外になるのでその場合は何もしない
         }

         var model = new CustomErrorViewModel
         {
            RequestId = loginUser, // SESSION変数のログインユーザーIDを設定する
            ExceptionMessage = exceptionFeature?.Error?.Message, // 例外のメッセージ
            Url = exceptionFeature?.Path                         // エラーが起きたパス
         };

         // Views/Shared/CustomError.cshtml を共有として呼び出す
         return View("~/Views/Shared/CustomError.cshtml", model);
      }
   }
}
