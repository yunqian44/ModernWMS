/*
 * date：2022-12-23
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
using System.Linq.Expressions;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;

namespace ModernWMS.WMS.Services
{
    /// <summary>
    ///  Stockprocess Service
    /// </summary>
    public class StockprocessService : BaseService<StockprocessEntity>, IStockprocessService
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
        ///Stockprocess  constructor
        /// </summary>
        /// <param name="dBContext">The DBContext</param>
        /// <param name="stringLocalizer">Localizer</param>
        public StockprocessService(
            SqlDBContext dBContext
          , IStringLocalizer<ModernWMS.Core.MultiLanguage> stringLocalizer
            )
        {
            this._dBContext = dBContext;
            this._stringLocalizer = stringLocalizer;
        }
        #endregion

        #region Api
        /// <summary>
        /// page search
        /// </summary>
        /// <param name="pageSearch">args</param>
        /// <param name="currentUser">currentUser</param>
        /// <returns></returns>
        public async Task<(List<StockprocessGetViewModel> data, int totals)> PageAsync(PageSearch pageSearch, CurrentUser currentUser)
        {
            QueryCollection queries = new QueryCollection();
            if (pageSearch.searchObjects.Any())
            {
                pageSearch.searchObjects.ForEach(s =>
                {
                    queries.Add(s);
                });
            }
            var DbSet = _dBContext.GetDbSet<StockprocessEntity>();
            var adjust_DBSet = _dBContext.GetDbSet<StockadjustEntity>().AsNoTracking();
            var adjusted = from a in adjust_DBSet
                           join d in _dBContext.GetDbSet<StockprocessdetailEntity>().AsNoTracking() on a.source_table_id equals d.id
                           where a.job_type == 2
                           group d by d.stock_process_id into ag
                           select new
                           {
                               stockprocess_id = ag.Key
                           };
            var query = from m in DbSet.AsNoTracking()
                        join a in adjusted on m.id equals a.stockprocess_id
                        select new StockprocessGetViewModel
                        {
                            id = m.id,
                            job_code = m.job_code,
                            job_type = m.job_type,
                            process_status = m.process_status,
                            processor = m.processor,
                            process_time = m.process_time,
                            creator = m.creator,
                            create_time = m.create_time,
                            last_update_time = m.last_update_time,
                            tenant_id = m.tenant_id,
                            adjust_status = (m.process_status && (a.stockprocess_id == null ? false : true)) ? true : false
                        };
            query = query
                .Where(t => t.tenant_id.Equals(currentUser.tenant_id))
                .Where(queries.AsExpression<StockprocessGetViewModel>());
            int totals = await query.CountAsync();
            var list = await query.OrderByDescending(t => t.create_time)
                       .Skip((pageSearch.pageIndex - 1) * pageSearch.pageSize)
                       .Take(pageSearch.pageSize)
                       .ToListAsync();
            return (list, totals);
        }

        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns></returns>
        public async Task<List<StockprocessGetViewModel>> GetAllAsync(CurrentUser currentUser)
        {
            var DbSet = _dBContext.GetDbSet<StockprocessEntity>();
            var data = await DbSet.AsNoTracking().Where(t => t.tenant_id.Equals(currentUser.tenant_id)).ToListAsync();
            return data.Adapt<List<StockprocessGetViewModel>>();
        }

        /// <summary>
        /// Get a record by id
        /// </summary>
        /// <returns></returns>
        public async Task<StockprocessWithDetailViewModel> GetAsync(int id)
        {
            var DbSet = _dBContext.GetDbSet<StockprocessEntity>();
            var entity = await DbSet.AsNoTracking().FirstOrDefaultAsync(t => t.id.Equals(id));
            var details = await (from spd in _dBContext.GetDbSet<StockprocessdetailEntity>().AsNoTracking().Where(t => t.stock_process_id == id)
                                 join sku in _dBContext.GetDbSet<SkuEntity>().AsNoTracking() on spd.sku_id equals sku.id
                                 join spu in _dBContext.GetDbSet<SpuEntity>().AsNoTracking() on sku.spu_id equals spu.id
                                 select new StockprocessdetailViewModel
                                 {
                                     id = spd.id,
                                     stock_process_id = spd.stock_process_id,
                                     sku_id = spd.sku_id,
                                     goods_owner_id = spd.goods_owner_id,
                                     goods_location_id = spd.goods_location_id,
                                     qty = spd.qty,
                                     last_update_time = spd.last_update_time,
                                     tenant_id = spd.tenant_id,
                                     is_source = spd.is_source,
                                     sku_code = sku.sku_code,
                                     spu_code = spu.spu_code,
                                     spu_name = spu.spu_name,
                                     unit = sku.unit,
                                 }).ToListAsync();
            if (entity == null)
            {
                return null;
            }
            var res = entity.Adapt<StockprocessWithDetailViewModel>();
            res.source_detail_list = details.Where(t => t.is_source == true).ToList();
            res.target_detail_list = details.Where(t => t.is_source == false).ToList();
            return res;
        }
        /// <summary>
        /// add a new record
        /// </summary>
        /// <param name="viewModel">viewmodel</param>
        /// <param name="currentUser">current user</param>
        /// <returns></returns>
        public async Task<(int id, string msg)> AddAsync(StockprocessViewModel viewModel, CurrentUser currentUser)
        {
            var DbSet = _dBContext.GetDbSet<StockprocessEntity>();
            var entity = viewModel.Adapt<StockprocessEntity>();
            var stock_DBSet = _dBContext.GetDbSet<StockEntity>();

            ParameterExpression parameterExpression = Expression.Parameter(typeof(StockEntity), "c");
            ConstantExpression constan_location = Expression.Constant(entity.detailList[0].goods_location_id);
            PropertyInfo prop_location = typeof(StockEntity).GetProperty("goods_location_id");
            MemberExpression location_exp = Expression.Property(parameterExpression, prop_location);
            BinaryExpression location_full_exp = Expression.Equal(location_exp, constan_location);
            ConstantExpression constan_sku = Expression.Constant(entity.detailList[0].sku_id);
            PropertyInfo prop_sku = typeof(StockEntity).GetProperty("sku_id");
            MemberExpression sku_exp = Expression.Property(parameterExpression, prop_sku);
            BinaryExpression sku_full_exp = Expression.Equal(sku_exp, constan_sku);
            var exp = Expression.And(location_full_exp, sku_full_exp);

            if (entity.detailList.Count > 1)
            {
                for (int i = 1; i <= entity.detailList.Count; i++)
                {
                    ParameterExpression t_parameterExpression = Expression.Parameter(typeof(StockEntity), "c");
                    ConstantExpression t_constan_location = Expression.Constant(entity.detailList[i].goods_location_id);
                    PropertyInfo t_prop_location = typeof(StockEntity).GetProperty("goods_location_id");
                    MemberExpression t_location_exp = Expression.Property(t_parameterExpression, t_prop_location);
                    BinaryExpression t_location_full_exp = Expression.Equal(t_location_exp, t_constan_location);
                    ConstantExpression t_constan_sku = Expression.Constant(entity.detailList[i].sku_id);
                    PropertyInfo t_prop_sku = typeof(StockEntity).GetProperty("sku_id");
                    MemberExpression t_sku_exp = Expression.Property(parameterExpression, t_prop_sku);
                    BinaryExpression t_sku_full_exp = Expression.Equal(t_sku_exp, t_constan_sku);
                    var t_exp = Expression.And(t_location_full_exp, t_sku_full_exp);
                    exp = Expression.Or(t_exp, exp);
                }
            }
            Expression<Func<StockEntity, bool>> predicate_res = Expression.Lambda<Func<StockEntity, bool>>(exp, new ParameterExpression[1] { parameterExpression });
            var stocks = await stock_DBSet.AsNoTracking().Where(predicate_res).ToListAsync();
            var lockeds = await (from d in _dBContext.GetDbSet<StockprocessdetailEntity>().AsNoTracking()
                                 where d.is_update_stock == false && entity.detailList.Where(t => t.is_source == true).Select(t => t.goods_location_id).Contains(d.goods_location_id)
                                 && entity.detailList.Where(t => t.is_source == true).Select(t => t.sku_id).Contains(d.sku_id)
                                 group d by new { d.goods_location_id, d.sku_id } into lg
                                 select new
                                 {
                                     sku_id = lg.Key.sku_id,
                                     goods_location_id = lg.Key.goods_location_id,
                                     qty_locked = lg.Sum(e => e.qty)
                                 }).ToListAsync();
            entity.id = 0;
            entity.create_time = DateTime.Now;
            entity.creator = currentUser.user_name;
            entity.last_update_time = DateTime.Now;
            entity.tenant_id = currentUser.tenant_id;
            await DbSet.AddAsync(entity);
            foreach (var d in entity.detailList)
            {
                var s = stocks.FirstOrDefault(t => t.sku_id == d.sku_id && t.goods_location_id == d.goods_location_id);
                if (s == null)
                {
                    return (0, _stringLocalizer["data_changed"]);
                }
                else if (s.is_freeze == true)
                {
                    return (0, _stringLocalizer["stock_frozen"]);
                }
                var locked = lockeds.FirstOrDefault(t => t.sku_id == d.sku_id && t.goods_location_id == d.goods_location_id);
                if ((s.qty - (locked == null ? 0 : locked.qty_locked)) < d.qty)
                {
                    return (0, _stringLocalizer["data_changed"]);
                }
            }
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
        public async Task<(bool flag, string msg)> UpdateAsync(StockprocessViewModel viewModel)
        {
            var DbSet = _dBContext.GetDbSet<StockprocessEntity>();
            var entity = await DbSet.FirstOrDefaultAsync(t => t.id.Equals(viewModel.id));
            if (entity == null)
            {
                return (false, _stringLocalizer["not_exists_entity"]);
            }
            entity.id = viewModel.id;
            entity.job_code = viewModel.job_code;
            entity.job_type = viewModel.job_type;
            entity.process_status = viewModel.process_status;
            entity.processor = viewModel.processor;
            entity.process_time = viewModel.process_time;
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
            var entity = await _dBContext.GetDbSet<StockprocessEntity>().Where(t => t.id.Equals(id)).FirstOrDefaultAsync();

            var qty = await _dBContext.GetDbSet<StockprocessEntity>().Where(t => t.id.Equals(id) && t.process_status == false).ExecuteDeleteAsync();
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
        /// <param name="currentUser">current user</param>
        /// <returns></returns>
        public async Task<(bool flag, string msg)> ConfirmAdjustment(int id, CurrentUser currentUser)
        {
            var DBSet = _dBContext.GetDbSet<StockprocessEntity>();
            var entity = await DBSet.FirstOrDefaultAsync(t => t.id == id);
            if (entity == null)
            {
                return (false, _stringLocalizer["not_exists_entity"]);
            }
            var detail_DBSet = _dBContext.GetDbSet<StockprocessdetailEntity>();
            var adjust_DBset = _dBContext.GetDbSet<StockadjustEntity>();
            var details = await detail_DBSet.AsNoTracking().Where(t => t.stock_process_id == id).ToListAsync();
            var adjusts = (from d in details
                           select new StockadjustEntity
                           {
                               sku_id = d.sku_id,
                               source_table_id = d.id,
                               is_update_stock = false,
                               goods_location_id = d.goods_location_id,
                               job_type = 2,
                               goods_owner_id = d.goods_owner_id,
                               qty = d.is_source ? -d.qty : d.qty,
                               create_time = DateTime.Now,
                               creator = currentUser.user_name,
                               last_update_time = DateTime.Now,
                               tenant_id = currentUser.tenant_id,
                           }).ToList();
            entity.last_update_time = DateTime.Now;
            await adjust_DBset.AddRangeAsync(adjusts);
            var res = await _dBContext.SaveChangesAsync();
            if (res > 0)
            {
                return (true, _stringLocalizer["operation_success"]);
            }
            return (false, _stringLocalizer["operation_failed"]);
        }

        /// <summary>
        /// confirm processing
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public async Task<(bool flag, string msg)> ConfirmProcess(int id)
        {
            var DBSet = _dBContext.GetDbSet<StockprocessEntity>();
            var entity = await DBSet.FirstOrDefaultAsync(t => t.id == id);
            if (entity == null)
            {
                return (false, _stringLocalizer["not_exists_entity"]);
            }
            entity.process_status = true;
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

