/*
 * date：2022-12-22
 * developer：NoNo
 */
 using Microsoft.AspNetCore.Mvc;
 using ModernWMS.Core.Controller;
 using ModernWMS.Core.Models;
 using ModernWMS.WMS.Entities.ViewModels;
 using ModernWMS.WMS.IServices;
 using Microsoft.Extensions.Localization;
 
 namespace ModernWMS.WMS.Controllers
 {
     /// <summary>
     /// stock controller
     /// </summary>
     [Route("stock")]
     [ApiController]
     [ApiExplorerSettings(GroupName = "wms")]
     public class StockController : BaseController
     {
         #region Args
 
         /// <summary>
         /// stock Service
         /// </summary>
         private readonly IStockService _stockService;
 
         /// <summary>
         /// Localizer Service
         /// </summary>
         private readonly IStringLocalizer<ModernWMS.Core.MultiLanguage> _stringLocalizer;
         #endregion
 
         #region constructor
         /// <summary>
         /// constructor
         /// </summary>
         /// <param name="stockService">stock Service</param>
        /// <param name="stringLocalizer">Localizer</param>
         public StockController(
             IStockService stockService
           , IStringLocalizer<ModernWMS.Core.MultiLanguage> stringLocalizer
             )
         {
             this._stockService = stockService;
            this._stringLocalizer= stringLocalizer;
         }
         #endregion
 
         #region Api
         /// <summary>
         /// page search
         /// </summary>
         /// <param name="pageSearch">args</param>
         /// <returns></returns>
         [HttpPost("list")]
         public async Task<ResultModel<PageData<StockViewModel>>> PageAsync(PageSearch pageSearch)
         {
             var (data, totals) = await _stockService.PageAsync(pageSearch, CurrentUser);
              
             return ResultModel<PageData<StockViewModel>>.Success(new PageData<StockViewModel>
             {
                 Rows = data,
                 Totals = totals
             });
         }
         #endregion
 
     }
 }
 
