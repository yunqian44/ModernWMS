using ModernWMS.Core.JWT;
using ModernWMS.Core.Services;
using ModernWMS.WMS.Entities.Models;
using ModernWMS.WMS.Entities.ViewModels;

namespace ModernWMS.WMS.IServices
{
    /// <summary>
    /// Interface of GoodsownerService
    /// </summary>
    public interface IGoodsownerService : IBaseService<GoodsownerEntity>
    {
        #region Api
        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns></returns>
        Task<List<GoodsownerViewModel>> GetAllAsync();
        /// <summary>
        /// Get a record by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        Task<GoodsownerViewModel> GetAsync(int id);
        /// <summary>
        /// add a new record
        /// </summary>
        /// <param name="viewModel">args</param>
        /// <param name="currentUser">currentUser</param>
        /// <returns></returns>
        Task<(int id, string msg)> AddAsync(GoodsownerViewModel viewModel, CurrentUser currentUser);
        /// <summary>
        /// update a record
        /// </summary>
        /// <param name="viewModel">args</param>
        /// <returns></returns>
        Task<(bool flag, string msg)> UpdateAsync(GoodsownerViewModel viewModel);

        /// <summary>
        /// delete a record
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        Task<(bool flag, string msg)> DeleteAsync(int id);
        #endregion

        #region Import
        /// <summary>
        /// import goodsowners by excel
        /// </summary>
        /// <param name="input">excel data</param>
        /// <param name="currentUser">currentUser</param>
        /// <returns></returns>
        Task<(bool flag, List<GoodsownerImportViewModel> errorData)> ExcelAsync(List<GoodsownerImportViewModel> input, CurrentUser currentUser);
        #endregion
    }
}
