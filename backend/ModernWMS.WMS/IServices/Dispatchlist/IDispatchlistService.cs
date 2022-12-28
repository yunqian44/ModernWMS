/*
 * date：2022-12-27
 * developer：NoNo
 */
 using ModernWMS.Core.Services;
 using ModernWMS.Core.Models;
 using ModernWMS.Core.JWT;
 using ModernWMS.WMS.Entities.Models;
 using ModernWMS.WMS.Entities.ViewModels;
 
 namespace ModernWMS.WMS.IServices
 {
     /// <summary>
     /// Interface of DispatchlistService
     /// </summary>
     public interface IDispatchlistService : IBaseService<DispatchlistEntity>
     {
         #region Api
         /// <summary>
         /// page search
         /// </summary>
         /// <param name="pageSearch">args</param>
         /// <param name="currentUser">current user</param>
         /// <returns></returns>
         Task<(List<DispatchlistViewModel> data, int totals)> PageAsync(PageSearch pageSearch, CurrentUser currentUser);
         /// <summary>
         /// Get all records
         /// </summary>
         /// <returns></returns>
         Task<List<DispatchlistViewModel>> GetAllAsync(CurrentUser currentUser);
         /// <summary>
         /// Get a record by id
         /// </summary>
         /// <param name="id">primary key</param>
         /// <returns></returns>
         Task<DispatchlistViewModel> GetAsync(int id);
         /// <summary>
         /// add a new record
         /// </summary>
         /// <param name="viewModel">viewmodel</param>
         /// <param name="currentUser">current user</param>
         /// <returns></returns>
         Task<(int id, string msg)> AddAsync(DispatchlistViewModel viewModel, CurrentUser currentUser);
         /// <summary>
         /// update a record
         /// </summary>
         /// <param name="viewModel">viewmodel</param>
         /// <returns></returns>
         Task<(bool flag, string msg)> UpdateAsync(DispatchlistViewModel viewModel);
 
         /// <summary>
         /// delete a record
         /// </summary>
         /// <param name="id">id</param>
         /// <returns></returns>
         Task<(bool flag, string msg)> DeleteAsync(int id);
         #endregion
     }
 }
 
