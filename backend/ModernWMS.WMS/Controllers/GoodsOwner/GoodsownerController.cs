using Microsoft.AspNetCore.Mvc;
using ModernWMS.Core.Controller;
using ModernWMS.Core.Models;
using ModernWMS.WMS.Entities.ViewModels;
using ModernWMS.WMS.IServices;

namespace ModernWMS.WMS.Controllers
{
    /// <summary>
    /// goodsowner controller
    /// </summary>
    [Route("goodsowner")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Base")]
    public class GoodsownerController : BaseController
    {
        #region Args

        /// <summary>
        /// goodsowner Service
        /// </summary>
        private readonly IGoodsownerService _goodsownerService;

        #endregion

        #region constructor
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="goodsownerService">goodsowner Service</param>
        public GoodsownerController(
            IGoodsownerService goodsownerService
            )
        {
            this._goodsownerService = goodsownerService;
        }
        #endregion

        #region Api
        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns>args</returns>
        [HttpGet]
        public async Task<ResultModel<List<GoodsownerViewModel>>> GetAllAsync()
        {
            var data = await _goodsownerService.GetAllAsync();
            if (data.Any())
            {
                return ResultModel<List<GoodsownerViewModel>>.Success(data);
            }
            else
            {
                return ResultModel<List<GoodsownerViewModel>>.Success(new List<GoodsownerViewModel>());
            }
        }

        /// <summary>
        /// add a new record
        /// </summary>
        /// <param name="viewModel">args</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel<int>> AddAsync(GoodsownerViewModel viewModel)
        {
            viewModel.creater = CurrentUser.user_name;
            var (id, msg) = await _goodsownerService.AddAsync(viewModel);
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
        public async Task<ResultModel<bool>> UpdateAsync(GoodsownerViewModel viewModel)
        {
            var (flag, msg) = await _goodsownerService.UpdateAsync(viewModel);
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
            var (flag, msg) = await _goodsownerService.DeleteAsync(id);
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
