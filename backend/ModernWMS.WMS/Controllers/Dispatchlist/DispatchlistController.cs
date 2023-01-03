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
        /// advanced dispatch order page search
        /// </summary>
        /// <param name="pageSearch">args</param>
        /// <returns></returns>
        [HttpPost("advanced-list")]
        public async Task<ResultModel<PageData<PreDispatchlistViewModel>>> AdvancedDispatchlistPageAsync(PageSearch pageSearch)
        {
            var (data, totals) = await _dispatchlistService.AdvancedDispatchlistPageAsync(pageSearch, CurrentUser);

            return ResultModel<PageData<PreDispatchlistViewModel>>.Success(new PageData<PreDispatchlistViewModel>
            {
                Rows = data,
                Totals = totals
            });
        }
        /// <summary>
        /// add a new record
        /// </summary>
        /// <param name="viewModel">args</param>
        /// <returns></returns>
        [HttpPost]
         public async Task<ResultModel<string>> AddAsync(List<DispatchlistAddViewModel> viewModel)
         {
             var (flag, msg) = await _dispatchlistService.AddAsync(viewModel,CurrentUser);
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
        /// get Dispatchlist details with available stock
        /// </summary>
        /// <param name="dispatch_no">dispatch_no</param>
        /// <returns></returns>
        [HttpGet("confirm-check")]
        public async Task<ResultModel<List<DispatchlistConfirmDetailViewModel>>> ConfirmOrderCheck(string dispatch_no)
        {
            var datas  = await _dispatchlistService.ConfirmOrderCheck(dispatch_no, CurrentUser);
            return ResultModel<List<DispatchlistConfirmDetailViewModel>>.Success(datas);
        }
        /// <summary>
        /// delete a record
        /// </summary>
        /// <param name="dispatch_no">dispatch_no</param>
        /// <returns></returns>
        [HttpDelete]
         public async Task<ResultModel<string>> DeleteAsync(string dispatch_no)
         {
             var (flag, msg) = await _dispatchlistService.DeleteAsync(dispatch_no,CurrentUser);
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
        ///  Confirm orders and create  dispatchpicklist
        /// </summary>
        /// <param name="viewModels">viewModels</param>
        /// <returns></returns>
        [HttpPost("confirm-order")]
        public async Task<ResultModel<string >> ConfirmOrder(List<DispatchlistConfirmDetailViewModel> viewModels)
        {
            var (flag, msg) = await _dispatchlistService.ConfirmOrder(viewModels, CurrentUser);
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
        ///  confirm dispatchpicklist picked by dispatch_no
        /// </summary>
        /// <param name="dispatch_no">viewModels</param>
        /// <returns></returns>
        [HttpPut("confirm-pick-dispatchlistno")]
        public async Task<ResultModel<string>> ConfirmPickByDispatchNo(string dispatch_no)
        {
            var (flag, msg) = await _dispatchlistService.ConfirmPickByDispatchNo(dispatch_no, CurrentUser);
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
        ///  package dispatchpicklist
        /// </summary>
        /// <param name="viewModels">viewModels</param>
        /// <returns></returns>
        [HttpPost("package")]
        public async Task<ResultModel<string>> Package(List<DispatchlistPackageViewModel> viewModels)
        {
            var (flag, msg) = await _dispatchlistService.Package(viewModels, CurrentUser);
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
        ///  weight dispatchpicklist
        /// </summary>
        /// <param name="viewModels">viewModels</param>
        /// <returns></returns>
        [HttpPost("weight")]
        public async Task<ResultModel<string>> Weight(List<DispatchlistWeightViewModel> viewModels)
        {
            var (flag, msg) = await _dispatchlistService.Weight(viewModels, CurrentUser);
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
        ///  dispatchpicklist outbound delivery
        /// </summary>
        /// <param name="viewModels">viewModels</param>
        /// <returns></returns>
        [HttpPost("delivery")]
        public async Task<ResultModel<string>> Delivery(List<DispatchlistDeliveryViewModel> viewModels)
        {
            var (flag, msg) = await _dispatchlistService.Delivery(viewModels, CurrentUser);
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
        ///  set dispatchlist freightfee
        /// </summary>
        /// <param name="viewModels">viewModels</param>
        /// <returns></returns>
        [HttpPost("freightfee")]
        public async Task<ResultModel<string>> SetFreightfee(List<DispatchlistFreightfeeViewModel> viewModels)
        {
            var (flag, msg) = await _dispatchlistService.SetFreightfee(viewModels);
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
        ///  sign for arrival
        /// </summary>
        /// <param name="viewModels">viewModels</param>
        /// <returns></returns>
        [HttpPost("sign")]
        public async Task<ResultModel<string>> SignForArrival(List<DispatchlistSignViewModel> viewModels)
        {
            var (flag, msg) = await _dispatchlistService.SignForArrival(viewModels);
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
 
