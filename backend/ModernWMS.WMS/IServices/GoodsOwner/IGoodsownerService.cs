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
        /// add a new record
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<(int id, string msg)> AddAsync(GoodsownerViewModel viewModel);
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
    }
}
