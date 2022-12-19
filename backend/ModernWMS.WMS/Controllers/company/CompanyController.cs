using Microsoft.AspNetCore.Mvc;
using ModernWMS.Core.Controller;
using ModernWMS.Core.Models;
using ModernWMS.WMS.Entities.ViewModels;
using ModernWMS.WMS.IServices;

namespace ModernWMS.WMS.Controllers
{
    /// <summary>
    /// account
    /// </summary>
    [Route("company")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Base")]
    public class CompanyController: BaseController
    {
        #region Args

        /// <summary>
        /// company Service
        /// </summary>
        private readonly ICompanyService _companyService;

        #endregion

        #region constructor
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="companyService">company Service</param>
        public CompanyController(
            ICompanyService companyService
            )
        {
            this._companyService = companyService;
        }
        #endregion

        #region Api
        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns>args</returns>
        [HttpGet]
        public async Task<ResultModel<List<CompanyViewModel>>> GetAllAsync()
        {
            var data = await _companyService.GetAllAsync();
            if (data.Any())
            {
                return ResultModel<List<CompanyViewModel>>.Success(data);
            }
            else
            {
                return ResultModel<List<CompanyViewModel>>.Success(new List<CompanyViewModel>());
            }
        }

        /// <summary>
        /// add a new record
        /// </summary>
        /// <param name="viewModel">args</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel<int>> AddAsync(CompanyViewModel viewModel)
        {
            var (id, msg) = await _companyService.AddAsync(viewModel);
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
        public async Task<ResultModel<bool>> UpdateAsync(CompanyViewModel viewModel)
        {
            var (flag, msg) = await _companyService.UpdateAsync(viewModel);
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
            var (flag, msg) = await _companyService.DeleteAsync(id);
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
