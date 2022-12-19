using ModernWMS.Core.Services;
using ModernWMS.WMS.Entities.Models;
using ModernWMS.WMS.Entities.ViewModels;

namespace ModernWMS.WMS.IServices
{
    /// <summary>
    /// Interface of CompanyService
    /// </summary>
    public interface ICompanyService : IBaseService<CompanyEntity>
    {
        #region Api
        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns></returns>
        Task<List<CompanyViewModel>> GetAllAsync();
        /// <summary>
        /// add a new record
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<(int id, string msg)> AddAsync(CompanyViewModel viewModel);
        /// <summary>
        /// update a record
        /// </summary>
        /// <param name="viewModel">args</param>
        /// <returns></returns>
        Task<(bool flag, string msg)> UpdateAsync(CompanyViewModel viewModel);

        /// <summary>
        /// delete a record
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        Task<(bool flag, string msg)> DeleteAsync(int id);
        #endregion
    }
}
