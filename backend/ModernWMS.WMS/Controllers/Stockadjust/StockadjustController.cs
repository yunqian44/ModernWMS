/*
 * date：2022-12-26
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
    /// stockadjust controller
    /// </summary>
     [Route("stockadjust")]
     [ApiController]
     [ApiExplorerSettings(GroupName = "WMS")]
     public class StockadjustController : BaseController
     {
         #region Args
 
         /// <summary>
         /// stockadjust Service
         /// </summary>
         private readonly IStockadjustService _stockadjustService;
 
         /// <summary>
         /// Localizer Service
         /// </summary>
         private readonly IStringLocalizer<ModernWMS.Core.MultiLanguage> _stringLocalizer;
         #endregion
 
         #region constructor
         /// <summary>
         /// constructor
         /// </summary>
         /// <param name="stockadjustService">stockadjust Service</param>
        /// <param name="stringLocalizer">Localizer</param>
         public StockadjustController(
             IStockadjustService stockadjustService
           , IStringLocalizer<ModernWMS.Core.MultiLanguage> stringLocalizer
             )
         {
             this._stockadjustService = stockadjustService;
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
         public async Task<ResultModel<PageData<StockadjustViewModel>>> PageAsync(PageSearch pageSearch)
         {
             var (data, totals) = await _stockadjustService.PageAsync(pageSearch, CurrentUser);
              
             return ResultModel<PageData<StockadjustViewModel>>.Success(new PageData<StockadjustViewModel>
             {
                 Rows = data,
                 Totals = totals
             });
         }
 
         /// <summary>
         /// get all records
         /// </summary>
         /// <returns>args</returns>
        [HttpGet("all")]
         public async Task<ResultModel<List<StockadjustViewModel>>> GetAllAsync()
         {
             var data = await _stockadjustService.GetAllAsync(CurrentUser);
             if (data.Any())
             {
                 return ResultModel<List<StockadjustViewModel>>.Success(data);
             }
             else
             {
                 return ResultModel<List<StockadjustViewModel>>.Success(new List<StockadjustViewModel>());
             }
         }
 
         /// <summary>
         /// get a record by id
         /// </summary>
         /// <returns>args</returns>
         [HttpGet]
         public async Task<ResultModel<StockadjustViewModel>> GetAsync(int id)
         {
             var data = await _stockadjustService.GetAsync(id);
             if (data!=null)
             {
                 return ResultModel<StockadjustViewModel>.Success(data);
             }
             else
             {
                 return ResultModel<StockadjustViewModel>.Error(_stringLocalizer["not_exists_entity"]);
             }
         }
         /// <summary>
         /// add a new record
         /// </summary>
         /// <param name="viewModel">args</param>
         /// <returns></returns>
         [HttpPost]
         public async Task<ResultModel<int>> AddAsync(StockadjustViewModel viewModel)
         {
             var (id, msg) = await _stockadjustService.AddAsync(viewModel,CurrentUser);
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
         public async Task<ResultModel<bool>> UpdateAsync(StockadjustViewModel viewModel)
         {
             var (flag, msg) = await _stockadjustService.UpdateAsync(viewModel);
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
             var (flag, msg) = await _stockadjustService.DeleteAsync(id);
             if (flag)
             {
                 return ResultModel<string>.Success(msg);
             }
             else
             {
                 return ResultModel<string>.Error(msg);
             }
         }

        /// <summary>
        /// confirm adjustment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("confirm")]
        public async Task<ResultModel<string>> ConfirmAdjustment(int id)
        {
            var (flag, msg) = await _stockadjustService.ConfirmAdjustment(id);
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
 
