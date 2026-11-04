namespace SearchFashion.Common {
   public class CustomValidationHelper {
      const string VALIDATE_YEAR_RANGE_ERROR_MESSAGE = "販売年(終了年)は販売年(開始年)以上の値を指定してください。";

      /// <summary>
      /// 日付の前後関係チェックを共通化
      /// </summary>
      public static bool ValidateYearRange(int? start, int? end, out string errorMessage)
      {
         errorMessage = string.Empty;

         if (start.HasValue && end.HasValue && start > end)
         {
            errorMessage = VALIDATE_YEAR_RANGE_ERROR_MESSAGE;
            return false;
         }
         return true;
      }
   }
}
