/*
 * date：2022-12-27
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
     /// dispatchlist controller
     /// </summary>
     [Route("dispatchlist")]
     [ApiController]
     [ApiExplorerSettings(GroupName = "WMS")]
     public class DispatchlistController : BaseController
     {
         #region Args
 
         /// <summary>
         /// dispatchlist Service
         /// </summary>
         private readonly IDispatchlistService _dispatchlistService;
 
         /// <summary>
         /// Localizer Service
         /// </summary>
         private readonly IStringLocalizer<ModernWMS.Core.MultiLanguage> _stringLocalizer;
         #endregion
 
         #region constructor
         /// <summary>
         /// constructor
         /// </summary>
         /// <param name="dispatchlistService">dispatchlist Service</param>
        /// <param name="stringLocalizer">Localizer</param>
         public DispatchlistController(
             IDispatchlistService dispatchlistService
           , IStringLocalizer<ModernWMS.Core.MultiLanguage> stringLocalizer
             )
         {
             this._dispatchlistService = dispatchlistService;
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
         public async Task<ResultModel<PageData<DispatchlistViewModel>>> PageAsync(PageSearch pageSearch)
         {
             var (data, totals) = await _dispatchlistService.PageAsync(pageSearch, CurrentUser);
              
             return ResultModel<PageData<DispatchlistViewModel>>.Success(new PageData<DispatchlistViewModel>
             {
                 Rows = data,
                 Totals = totals
             });
         }
 
       
 
         /// <summary>
         /// get a record by id
         /// </summary>
         /// <returns>args</returns>
         [HttpGet]
         public async Task<ResultModel<DispatchlistViewModel>> GetAsync(int id)
         {
             var data = await _dispatchlistService.GetAsync(id);
             if (data!=null)
             {
                 return ResultModel<DispatchlistViewModel>.Success(data);
             }
             else
             {
                 return ResultModel<DispatchlistViewModel>.Error(_stringLocalizer["not_exists_entity"]);
             }
         }
         /// <summary>
         /// add a new record
         /// </summary>
         /// <param name="viewModel">args</param>
         /// <returns></returns>
         [HttpPost]
         public async Task<ResultModel<int>> AddAsync(DispatchlistViewModel viewModel)
         {
             var (id, msg) = await _dispatchlistService.AddAsync(viewModel,CurrentUser);
             if (id > 0)
             {
                 return ResultModel<int>.Success(id);
             }
             else
             {
                 return ResultModel<int>.Error(msg);
             }
         }
 
         /// <summary>
         /// update a record
         /// </summary>
         /// <param name="viewModel">args</param>
         /// <returns></returns>
         [HttpPut]
         public async Task<ResultModel<bool>> UpdateAsync(DispatchlistViewModel viewModel)
         {
             var (flag, msg) = await _dispatchlistService.UpdateAsync(viewModel);
             if (flag)
             {
                 return ResultModel<bool>.Success(flag);
             }
             else
             {
                 return ResultModel<bool>.Error(msg, 400, flag);
             }
         }
 
         /// <summary>
         /// delete a record
         /// </summary>
         /// <param name="id">id</param>
         /// <returns></returns>
         [HttpDelete]
         public async Task<ResultModel<string>> DeleteAsync(int id)
         {
             var (flag, msg) = await _dispatchlistService.DeleteAsync(id);
             if (flag)
             {
                 return ResultModel<string>.Success(msg);
             }
             else
             {
                 return ResultModel<string>.Error(msg);
             }
         }
         #endregion
 
     }
 }
 
