using Microsoft.AspNetCore.Mvc;
using ModernWMS.Core.Controller;
using ModernWMS.Core.Models;
using ModernWMS.WMS.Entities.ViewModels;
using ModernWMS.WMS.IServices;

namespace ModernWMS.WMS.Controllers
{
    /// <summary>
    /// customer controller
    /// </summary>
    [Route("customer")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Base")]
    public class CustomerController : BaseController
    {
        #region Args

        /// <summary>
        /// customer Service
        /// </summary>
        private readonly ICustomerService _customerService;

        #endregion

        #region constructor
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="customerService">customer Service</param>
        public CustomerController(
            ICustomerService customerService
            )
        {
            this._customerService = customerService;
        }
        #endregion

        #region Api
        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns>args</returns>
        [HttpGet]
        public async Task<ResultModel<List<CustomerViewModel>>> GetAllAsync()
        {
            var data = await _customerService.GetAllAsync();
            if (data.Any())
            {
                return ResultModel<List<CustomerViewModel>>.Success(data);
            }
            else
            {
                return ResultModel<List<CustomerViewModel>>.Success(new List<CustomerViewModel>());
            }
        }

        /// <summary>
        /// add a new record
        /// </summary>
        /// <param name="viewModel">args</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel<int>> AddAsync(CustomerViewModel viewModel)
        {
            viewModel.creater = CurrentUser.user_name;
            var (id, msg) = await _customerService.AddAsync(viewModel);
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
        public async Task<ResultModel<bool>> UpdateAsync(CustomerViewModel viewModel)
        {
            var (flag, msg) = await _customerService.UpdateAsync(viewModel);
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
            var (flag, msg) = await _customerService.DeleteAsync(id);
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
