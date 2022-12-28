/*
 * date：2022-12-22
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
using System.Runtime.Intrinsics.Arm;
using System.Net.WebSockets;

namespace ModernWMS.WMS.Services
{
    /// <summary>
    ///  Stock Service
    /// </summary>
    public class StockService : BaseService<StockEntity>, IStockService
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
        ///Stock  constructor
        /// </summary>
        /// <param name="dBContext">The DBContext</param>
        /// <param name="stringLocalizer">Localizer</param>
        public StockService(
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
        /// stock page search
        /// </summary>
        /// <param name="pageSearch">args</param>
        /// <param name="currentUser">currentUser</param>
        /// <returns></returns>
        public async Task<(List<StockManagementViewModel> data, int totals)> StockPageAsync(PageSearch pageSearch, CurrentUser currentUser)
        {
            QueryCollection queries = new QueryCollection();
            if (pageSearch.searchObjects.Any())
            {
                pageSearch.searchObjects.ForEach(s =>
                {
                    queries.Add(s);
                });
            }

            var DbSet = _dBContext.GetDbSet<StockEntity>().Where(t => t.tenant_id.Equals(currentUser.tenant_id));
            var asn_DBSet = _dBContext.GetDbSet<AsnEntity>().Where(t => t.tenant_id.Equals(currentUser.tenant_id));
            var dispatch_DBSet = _dBContext.GetDbSet<DispatchlistEntity>().Where(t => t.tenant_id.Equals(currentUser.tenant_id));
            var sku_DBSet = _dBContext.GetDbSet<SkuEntity>().AsNoTracking();
            var spu_DBSet = _dBContext.GetDbSet<SpuEntity>().AsNoTracking();
            var processdetail_DBSet = _dBContext.GetDbSet<StockprocessdetailEntity>().AsNoTracking();
            var stock_group_datas = from stock in DbSet.AsNoTracking()
                                    group stock by stock.sku_id into sg
                                    select new
                                    {
                                        sku_id = sg.Key,
                                        qty_frozen = sg.Where(t => t.is_freeze == true).Sum(e => e.qty),
                                        qty = sg.Sum(t => t.qty)
                                    };
            var asn_group_datas = from asn in asn_DBSet.AsNoTracking()
                                  group asn by asn.sku_id into ag
                                  select new
                                  {
                                      sku_id = ag.Key,
                                      qty_asn = ag.Where(t => t.asn_status == 0).Sum(t => t.asn_qty),
                                      qty_to_unload = ag.Where(t => t.asn_status == 1).Sum(t => t.asn_qty),
                                      qty_to_sort = ag.Where(t => t.asn_status == 2).Sum(t => t.asn_qty),
                                      qty_sorted = ag.Where(t => t.asn_status == 3).Sum(t => t.sorted_qty),
                                      shortage_qty = ag.Where(t => t.asn_status == 4).Sum(t => t.shortage_qty)
                                  };
            var dispatch_group_datas = from dp in dispatch_DBSet.AsNoTracking()
                                       group dp by dp.sku_id into dg
                                       select new
                                       {
                                           sku_id = dg.Key,
                                           qty_locked = dg.Sum(t => t.lock_qty)
                                       };
            var process_locked_group_datas = from pd in processdetail_DBSet
                                             where pd.is_update_stock == false
                                             group pd by pd.sku_id into pdg
                                             select new
                                             {
                                                 sku_id = pdg.Key,
                                                 qty_locked = pdg.Sum(t => t.qty)
                                             };
            var query = from ag in asn_group_datas
                        join sg in stock_group_datas on ag.sku_id equals sg.sku_id into sg_left
                        from sg in sg_left.DefaultIfEmpty()
                        join dp in dispatch_group_datas on sg.sku_id equals dp.sku_id into dp_left
                        from dp in dp_left.DefaultIfEmpty()
                        join pl in process_locked_group_datas on ag.sku_id equals pl.sku_id into pl_left
                        from pl in pl_left.DefaultIfEmpty()
                        join sku in sku_DBSet on ag.sku_id equals sku.id
                        join spu in spu_DBSet on sku.spu_id equals spu.id
                        select new StockManagementViewModel
                        {
                            sku_id = ag.sku_id,
                            spu_name = spu.spu_name,
                            spu_code = spu.spu_code,
                            sku_code = sku.sku_code,
                            qty_asn = ag.qty_asn,
                            qty_available = (sg.qty == null ? 0 : sg.qty) - (sg.qty_frozen == null ? 0 : sg.qty_frozen) - (dp.qty_locked == null ? 0 : dp.qty_locked) - (pl.qty_locked == null ? 0 : pl.qty_locked),
                            qty_frozen = sg.qty_frozen == null ? 0 : sg.qty_frozen,
                            qty_locked = (dp.qty_locked == null ? 0 : dp.qty_locked) + (pl.qty_locked == null ? 0 : pl.qty_locked),
                            qty_sorted = ag.qty_sorted,
                            qty_to_sort = ag.qty_to_sort,
                            shortage_qty = ag.shortage_qty,
                            qty_to_unload = ag.qty_to_unload,
                            qty = sg.qty == null ? 0 : sg.qty,
                        };
            query = query.Where(queries.AsExpression<StockManagementViewModel>());
            int totals = await query.CountAsync();
            var list = await query.OrderBy(t => t.sku_code)
                       .Skip((pageSearch.pageIndex - 1) * pageSearch.pageSize)
                       .Take(pageSearch.pageSize)
                       .ToListAsync();
            return (list, totals);
        }

        /// <summary>
        /// location stock page search
        /// </summary>
        /// <param name="pageSearch">args</param>
        /// <param name="currentUser">currentUser</param>
        /// <returns></returns>
        public async Task<(List<LocationStockManagementViewModel> data, int totals)> LocationStockPageAsync(PageSearch pageSearch, CurrentUser currentUser)
        {
            QueryCollection queries = new QueryCollection();
            if (pageSearch.searchObjects.Any())
            {
                pageSearch.searchObjects.ForEach(s =>
                {
                    queries.Add(s);
                });
            }

            var DbSet = _dBContext.GetDbSet<StockEntity>().Where(t => t.tenant_id.Equals(currentUser.tenant_id));
            var dispatchpick_DBSet = _dBContext.GetDbSet<DispatchpicklistEntity>();
            var dispatch_DBSet = _dBContext.GetDbSet<DispatchlistEntity>().Where(t => t.tenant_id.Equals(currentUser.tenant_id));
            var sku_DBSet = _dBContext.GetDbSet<SkuEntity>().AsNoTracking();
            var spu_DBSet = _dBContext.GetDbSet<SpuEntity>().AsNoTracking();
            var location_DBSet = _dBContext.GetDbSet<GoodslocationEntity>().AsNoTracking();
            var processdetail_DBSet = _dBContext.GetDbSet<StockprocessdetailEntity>().AsNoTracking();
            var stock_group_datas = from stock in DbSet.AsNoTracking()
                                    group stock by new { stock.sku_id, stock.goods_location_id } into sg
                                    select new
                                    {
                                        sku_id = sg.Key.sku_id,
                                        goods_location_id = sg.Key.goods_location_id,
                                        qty_frozen = sg.Where(t => t.is_freeze == true).Sum(e => e.qty),
                                        qty = sg.Sum(t => t.qty)
                                    };

            var dispatch_group_datas = from dp in dispatch_DBSet.AsNoTracking()
                                       join dpp in dispatchpick_DBSet.AsNoTracking() on dp.id equals dpp.dispatchlist_id
                                       where dp.dispatch_status > 1 && dp.dispatch_status < 6
                                       group dpp by new { dpp.sku_id, dpp.goods_location_id } into dg
                                       select new
                                       {
                                           sku_id = dg.Key.sku_id,
                                           goods_location_id = dg.Key.goods_location_id,
                                           qty_locked = dg.Sum(t => t.pick_qty)
                                       };
            var process_locked_group_datas = from pd in processdetail_DBSet
                                             where pd.is_update_stock == false
                                             group pd by new { pd.sku_id, pd.goods_location_id } into pdg
                                             select new
                                             {
                                                 sku_id = pdg.Key.sku_id,
                                                 goods_location_id = pdg.Key.goods_location_id,
                                                 qty_locked = pdg.Sum(t => t.qty)
                                             };
            var query = from sg in stock_group_datas
                        join dp in dispatch_group_datas on new { sg.sku_id, sg.goods_location_id } equals new { dp.sku_id, dp.goods_location_id } into dp_left
                        from dp in dp_left.DefaultIfEmpty()
                        join pl in process_locked_group_datas on new { sg.sku_id, sg.goods_location_id } equals new { pl.sku_id, pl.goods_location_id } into pl_left
                        from pl in pl_left.DefaultIfEmpty()
                        join sku in sku_DBSet on sg.sku_id equals sku.id
                        join spu in spu_DBSet on sku.spu_id equals spu.id
                        join gl in location_DBSet on sg.goods_location_id equals gl.id
                        select new LocationStockManagementViewModel
                        {
                            sku_id = sg.sku_id,
                            spu_name = spu.spu_name,
                            spu_code = spu.spu_code,
                            sku_code = sku.sku_code,
                            sku_name = sku.sku_name,
                            qty_available = sg.qty - sg.qty_frozen - (dp.qty_locked == null ? 0 : dp.qty_locked) - (pl.qty_locked == null ? 0 : pl.qty_locked),
                            qty_frozen = sg.qty_frozen,
                            qty_locked = (dp.qty_locked == null ? 0 : dp.qty_locked) + (pl.qty_locked == null ? 0 : pl.qty_locked),
                            qty = sg.qty,
                            location_name = gl.location_name,
                            warehouse = gl.warehouse_name,
                        };
            query = query.Where(queries.AsExpression<LocationStockManagementViewModel>());
            int totals = await query.CountAsync();
            var list = await query.OrderBy(t => t.sku_code)
                       .Skip((pageSearch.pageIndex - 1) * pageSearch.pageSize)
                       .Take(pageSearch.pageSize)
                       .ToListAsync();
            return (list, totals);
        }

        /// <summary>
        ///  page search select
        /// </summary>
        /// <param name="pageSearch">args</param>
        /// <param name="currentUser">currentUser</param>
        /// <returns></returns>
        public async Task<(List<StockViewModel> data, int totals)> SelectPageAsync(PageSearch pageSearch, CurrentUser currentUser)
        {
            QueryCollection queries = new QueryCollection();
            if (pageSearch.searchObjects.Any())
            {
                pageSearch.searchObjects.ForEach(s =>
                {
                    queries.Add(s);
                });
            }

            var DbSet = _dBContext.GetDbSet<StockEntity>().Where(t => t.tenant_id.Equals(currentUser.tenant_id));
            var dispatchpick_DBSet = _dBContext.GetDbSet<DispatchpicklistEntity>();
            var dispatch_DBSet = _dBContext.GetDbSet<DispatchlistEntity>().Where(t => t.tenant_id.Equals(currentUser.tenant_id));
            var sku_DBSet = _dBContext.GetDbSet<SkuEntity>().AsNoTracking();
            var spu_DBSet = _dBContext.GetDbSet<SpuEntity>().AsNoTracking();
            var location_DBSet = _dBContext.GetDbSet<GoodslocationEntity>().AsNoTracking();
            var processdetail_DBSet = _dBContext.GetDbSet<StockprocessdetailEntity>().AsNoTracking();
            var dispatch_group_datas = from dp in dispatch_DBSet.AsNoTracking()
                                       join dpp in dispatchpick_DBSet.AsNoTracking() on dp.id equals dpp.dispatchlist_id
                                       where dp.dispatch_status > 1 && dp.dispatch_status < 6
                                       group dpp by new { dpp.sku_id, dpp.goods_location_id } into dg
                                       select new
                                       {
                                           sku_id = dg.Key.sku_id,
                                           goods_location_id = dg.Key.goods_location_id,
                                           qty_locked = dg.Sum(t => t.pick_qty)
                                       };
            var process_locked_group_datas = from pd in processdetail_DBSet
                                             where pd.is_update_stock == false
                                             group pd by new { pd.sku_id, pd.goods_location_id } into pdg
                                             select new
                                             {
                                                 sku_id = pdg.Key.sku_id,
                                                 goods_location_id = pdg.Key.goods_location_id,
                                                 qty_locked = pdg.Sum(t => t.qty)
                                             };
            var query = from sg in DbSet.AsNoTracking()
                        join dp in dispatch_group_datas on new { sg.sku_id, sg.goods_location_id } equals new { dp.sku_id, dp.goods_location_id } into dp_left
                        from dp in dp_left.DefaultIfEmpty()
                        join pl in process_locked_group_datas on new { sg.sku_id, sg.goods_location_id } equals new { pl.sku_id, pl.goods_location_id } into pl_left
                        from pl in pl_left.DefaultIfEmpty()
                        join sku in sku_DBSet on sg.sku_id equals sku.id
                        join spu in spu_DBSet on sku.spu_id equals spu.id
                        join gl in location_DBSet on sg.goods_location_id equals gl.id
                        where sg.tenant_id == currentUser.tenant_id
                        select new StockViewModel
                        {
                            sku_id = sg.sku_id,
                            spu_name = spu.spu_name,
                            spu_code = spu.spu_code,
                            sku_code = sku.sku_code,
                            sku_name = sku.sku_name,
                            qty_available = sg.is_freeze ? 0:( sg.qty  - (dp.qty_locked == null ? 0 : dp.qty_locked) - (pl.qty_locked == null ? 0 : pl.qty_locked)),
                            qty = sg.qty,
                            location_name = gl.location_name,
                            warehouse = gl.warehouse_name,
                            goods_location_id = sg.goods_location_id,
                            goods_owner_id = sg.goods_owner_id,
                            is_freeze = sg.is_freeze,
                            id = sg.id,
                            last_update_time = sg.last_update_time,
                            tenant_id = sg.tenant_id,
                        };
            query = query.Where(queries.AsExpression<StockViewModel>());
            int totals = await query.CountAsync();
            var list = await query.OrderBy(t => t.sku_code)
                       .Skip((pageSearch.pageIndex - 1) * pageSearch.pageSize)
                       .Take(pageSearch.pageSize)
                       .ToListAsync();
            return (list, totals);
        }
        #endregion
    }
}

