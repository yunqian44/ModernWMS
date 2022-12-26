/*
 * date：2022-12-26
 * developer：NoNo
 */
 using Mapster;
 using Microsoft.EntityFrameworkCore;
 using ModernWMS.Core.DBContext;
 using ModernWMS.Core.Services;
 using ModernWMS.WMS.Entities.Models;
 using ModernWMS.WMS.Entities.ViewModels;
 using ModernWMS.WMS.IServices;
 using ModernWMS.Core.Models;
 using ModernWMS.Core.JWT;
 using Microsoft.Extensions.Localization;
 using ModernWMS.Core.DynamicSearch;
using System.Net.Quic;

namespace ModernWMS.WMS.Services
 {
     /// <summary>
     ///  Stockadjust Service
     /// </summary>
     public class StockadjustService : BaseService<StockadjustEntity>, IStockadjustService
     {
         #region Args
         /// <summary>
         /// The DBContext
         /// </summary>
         private readonly SqlDBContext _dBContext;
 
         /// <summary>
         /// Localizer Service
         /// </summary>
         private readonly IStringLocalizer<ModernWMS.Core.MultiLanguage> _stringLocalizer;
         #endregion
 
         #region constructor
         /// <summary>
         ///Stockadjust  constructor
         /// </summary>
         /// <param name="dBContext">The DBContext</param>
        /// <param name="stringLocalizer">Localizer</param>
         public StockadjustService(
             SqlDBContext dBContext
           , IStringLocalizer<ModernWMS.Core.MultiLanguage> stringLocalizer
             )
         {
             this._dBContext = dBContext;
            this._stringLocalizer= stringLocalizer;
         }
         #endregion
 
         #region Api
         /// <summary>
         /// page search
         /// </summary>
         /// <param name="pageSearch">args</param>
         /// <param name="currentUser">currentUser</param>
         /// <returns></returns>
         public async Task<(List<StockadjustViewModel> data, int totals)> PageAsync(PageSearch pageSearch, CurrentUser currentUser)
         {
             QueryCollection queries = new QueryCollection();
             if (pageSearch.searchObjects.Any())
             {
                 pageSearch.searchObjects.ForEach(s =>
                 {
                     queries.Add(s);
                 });
             }
             var DbSet = _dBContext.GetDbSet<StockadjustEntity>();
             var query = DbSet.AsNoTracking()
                 .Where(t => t.tenant_id.Equals(currentUser.tenant_id))
                 .Where(queries.AsExpression<StockadjustEntity>());
             int totals = await query.CountAsync();
             var list = await query.OrderByDescending(t => t.create_time)
                        .Skip((pageSearch.pageIndex - 1) * pageSearch.pageSize)
                        .Take(pageSearch.pageSize)
                        .ToListAsync();
             return (list.Adapt<List<StockadjustViewModel>>(), totals);
         }
 
         /// <summary>
         /// Get all records
         /// </summary>
         /// <returns></returns>
         public async Task<List<StockadjustViewModel>> GetAllAsync(CurrentUser currentUser)
         {
             var DbSet = _dBContext.GetDbSet<StockadjustEntity>();
             var data = await DbSet.AsNoTracking().Where(t=>t.tenant_id.Equals(currentUser.tenant_id)).ToListAsync();
             return data.Adapt<List<StockadjustViewModel>>();
         }
 
         /// <summary>
         /// Get a record by id
         /// </summary>
         /// <returns></returns>
         public async Task<StockadjustViewModel> GetAsync(int id)
         {
             var DbSet = _dBContext.GetDbSet<StockadjustEntity>();
             var entity = await DbSet.AsNoTracking().FirstOrDefaultAsync(t=>t.id.Equals(id));
             if (entity == null)
             {
                 return null;
             }
             return entity.Adapt<StockadjustViewModel>();
         }
         /// <summary>
         /// add a new record
         /// </summary>
         /// <param name="viewModel">viewmodel</param>
         /// <param name="currentUser">current user</param>
         /// <returns></returns>
         public async Task<(int id, string msg)> AddAsync(StockadjustViewModel viewModel, CurrentUser currentUser)
         {
             var DbSet = _dBContext.GetDbSet<StockadjustEntity>();
             var entity = viewModel.Adapt<StockadjustEntity>();
             entity.id = 0;
             entity.create_time = DateTime.Now;
             entity.creator = currentUser.user_name;
             entity.last_update_time = DateTime.Now;
             entity.tenant_id = currentUser.tenant_id;
             await DbSet.AddAsync(entity);
             await _dBContext.SaveChangesAsync();
             if (entity.id > 0)
             {
                 return (entity.id, _stringLocalizer["save_success"]);
             }
             else
             {
                 return (0, _stringLocalizer["save_failed"]);
             }
         }
         /// <summary>
         /// update a record
         /// </summary>
         /// <param name="viewModel">args</param>
         /// <returns></returns>
         public async Task<(bool flag, string msg)> UpdateAsync(StockadjustViewModel viewModel)
         {
             var DbSet = _dBContext.GetDbSet<StockadjustEntity>();
             var entity = await DbSet.FirstOrDefaultAsync(t => t.id.Equals(viewModel.id));
             if (entity == null)
             {
                 return (false,_stringLocalizer[ "not_exists_entity"]);
             }
             entity.id = viewModel.id;
             entity.job_code = viewModel.job_code;
             entity.sku_id = viewModel.sku_id;
             entity.goods_owner_id = viewModel.goods_owner_id;
             entity.goods_location_id = viewModel.goods_location_id;
             entity.qty = viewModel.qty;
             entity.is_update_stock = viewModel.is_update_stock;
             entity.job_type = viewModel.job_type;
             entity.source_table_id = viewModel.source_table_id;
             entity.last_update_time = DateTime.Now;
             var qty = await _dBContext.SaveChangesAsync();
             if (qty > 0)
             {
                 return (true, _stringLocalizer["save_success"]);
             }
             else
             {
                 return (false, _stringLocalizer["save_failed"]);
             }
         }
         /// <summary>
         /// delete a record
         /// </summary>
         /// <param name="id">id</param>
         /// <returns></returns>
         public async Task<(bool flag, string msg)> DeleteAsync(int id)
         {
            var DBSet = _dBContext.GetDbSet<StockadjustEntity>();
            var entity = await DBSet.Where(t=>t.id == id).FirstOrDefaultAsync();
            if (entity == null)
            {
                return (false, _stringLocalizer["not_exists_entity"]);
            }
            DBSet.Remove(entity);
            var qty = await _dBContext.SaveChangesAsync();
             if (qty > 0)
             {
                 return (true, _stringLocalizer["delete_success"]);
             }
             else
             {
                 return (false, _stringLocalizer["delete_failed"]);
             }
         }

        /// <summary>
        /// confirm adjustment
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public async Task<(bool flag, string msg)> ConfirmAdjustment(int id)
        {
            var adjust_DBset = _dBContext.GetDbSet<StockadjustEntity>();
            var entity = await adjust_DBset.FirstOrDefaultAsync(t => t.id == id);
            if (entity == null)
            {
                return (false, _stringLocalizer["not_exists_entity"]);
            }
            if(entity.job_type == 2)
            {
                var processdetail_DBSet = _dBContext.GetDbSet<StockprocessdetailEntity>();
                var processdetail = await processdetail_DBSet.Where(t => t.id == entity.source_table_id).FirstOrDefaultAsync();
                if (processdetail != null)
                {
                    processdetail.last_update_time = DateTime.Now;
                    processdetail.is_update_stock = true;
                }
            }
            var stock_DBSet = _dBContext.GetDbSet<StockEntity>();
            var stock = await stock_DBSet.Where(t=>t.goods_location_id == entity.goods_location_id&&t.sku_id== entity.sku_id).FirstOrDefaultAsync();
            if(stock == null)
            {
                stock = new StockEntity
                {
                    id = entity.id,
                    sku_id = entity.sku_id,
                    goods_location_id = entity.goods_location_id,
                    qty = entity.qty,
                    goods_owner_id = entity.goods_owner_id,
                    is_freeze = false,
                    last_update_time = DateTime.Now,
                    tenant_id = entity.tenant_id,
                };
            }
            else
            {
                stock.qty += entity.qty;
                stock.goods_owner_id= entity.goods_owner_id;
                stock.last_update_time= DateTime.Now;
            }
            entity.is_update_stock = true;
            entity.last_update_time = DateTime.Now;
            var res = await _dBContext.SaveChangesAsync();
            if (res > 0)
            {
                return (true, _stringLocalizer["operation_success"]);
            }
            return (false, _stringLocalizer["operation_failed"]);
        }
        #endregion
    }
 }
 
