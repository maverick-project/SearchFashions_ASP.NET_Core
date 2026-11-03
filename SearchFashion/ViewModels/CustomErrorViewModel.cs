namespace FashionSearch.ViewModels {
   public class CustomErrorViewModel {
      public string? RequestId { get; set; } // ログインユーザーID
      public string? ExceptionMessage { get; set; } // エラーメッセージ
      public string? Url { get; set; }             // エラーが発生したURL

      public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
   }
}
