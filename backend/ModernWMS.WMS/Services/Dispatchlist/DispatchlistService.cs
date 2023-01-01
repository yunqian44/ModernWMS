/*
 * date：2022-12-27
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
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net.WebSockets;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace ModernWMS.WMS.Services
{
    /// <summary>
    ///  Dispatchlist Service
    /// </summary>
    public class DispatchlistService : BaseService<DispatchlistEntity>, IDispatchlistService
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
        ///Dispatchlist  constructor
        /// </summary>
        /// <param name="dBContext">The DBContext</param>
        /// <param name="stringLocalizer">Localizer</param>
        public DispatchlistService(
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
        public async Task<(List<DispatchlistViewModel> data, int totals)> PageAsync(PageSearch pageSearch, CurrentUser currentUser)
        {
            QueryCollection queries = new QueryCollection();
            if (pageSearch.searchObjects.Any())
            {
                pageSearch.searchObjects.ForEach(s =>
                {
                    queries.Add(s);
                });
            }
            var DbSet = _dBContext.GetDbSet<DispatchlistEntity>();
            var query = DbSet.AsNoTracking()
                .Where(t => t.tenant_id.Equals(currentUser.tenant_id))
                .Where(queries.AsExpression<DispatchlistEntity>());
            if (pageSearch.sqlTitle.Contains("dispatch_status"))
            {
                var dispatch_status = Convert.ToByte(pageSearch.sqlTitle.Trim().ToLower().Replace("dispatch_status", "").Replace("：", "").Replace(":", "").Replace("=", ""));
                query = query.Where(t => t.dispatch_status.Equals(dispatch_status));
            }
            int totals = await query.CountAsync();
            var list = await query.OrderByDescending(t => t.create_time)
                       .Skip((pageSearch.pageIndex - 1) * pageSearch.pageSize)
                       .Take(pageSearch.pageSize)
                       .ToListAsync();
            return (list.Adapt<List<DispatchlistViewModel>>(), totals);
        }
        /// <summary>
        /// advanced dispatch order page search 
        /// </summary>
        /// <param name="pageSearch">args</param>
        /// <param name="currentUser">currentUser</param>
        /// <returns></returns>
        public async Task<(List<PreDispatchlistViewModel> data, int totals)> AdvancedDispatchlistPageAsync(PageSearch pageSearch, CurrentUser currentUser)
        {
            QueryCollection queries = new QueryCollection();
            if (pageSearch.searchObjects.Any())
            {
                pageSearch.searchObjects.ForEach(s =>
                {
                    queries.Add(s);
                });
            }
            var DbSet = _dBContext.GetDbSet<DispatchlistEntity>();
            var query = from d in DbSet.AsNoTracking().Where(t => t.tenant_id.Equals(currentUser.tenant_id))
                        group d by new { d.dispatch_no, d.dispatch_status, d.customer_id, d.customer_name, d.create_time, d.creator }
                        into dg
                        select new PreDispatchlistViewModel
                        {
                            dispatch_no = dg.Key.dispatch_no,
                            dispatch_status = dg.Key.dispatch_status,
                            customer_id = dg.Key.customer_id,
                            customer_name = dg.Key.customer_name,
                            qty = dg.Sum(t => t.qty),
                            volume = dg.Sum(t => t.volume),
                            weight = dg.Sum(t => t.weight),
                        };
            query = query.Where(queries.AsExpression<PreDispatchlistViewModel>());
            if (pageSearch.sqlTitle.Contains("dispatch_status"))
            {
                var dispatch_status = Convert.ToByte(pageSearch.sqlTitle.Trim().ToLower().Replace("dispatch_status", "").Replace("：", "").Replace(":", "").Replace("=", ""));
                query = query.Where(t => t.dispatch_status.Equals(dispatch_status));
            }
            int totals = await query.CountAsync();
            var list = await query.OrderByDescending(t => t.create_time)
                       .Skip((pageSearch.pageIndex - 1) * pageSearch.pageSize)
                       .Take(pageSearch.pageSize)
                       .ToListAsync();
            return (list, totals);
        }
        /// <summary>
        /// Get dispatchlist by dispatch_no
        /// </summary>
        /// <returns></returns>
        public async Task<List<DispatchlistDetailViewModel>> GetAllAsync(string dispatch_no, CurrentUser currentUser)
        {
            var DbSet = _dBContext.GetDbSet<DispatchlistEntity>();
            var Spus = _dBContext.GetDbSet<SpuEntity>();
            var Skus = _dBContext.GetDbSet<SkuEntity>();
            var data = await (from dl in DbSet.AsNoTracking()
                              join sku in Skus.AsNoTracking() on dl.sku_id equals sku.id
                              join spu in Spus.AsNoTracking() on sku.spu_id equals spu.id
                              where dl.dispatch_no == dispatch_no && dl.tenant_id == currentUser.tenant_id
                              select new DispatchlistDetailViewModel
                              {
                                  id = dl.id,
                                  dispatch_no = dl.dispatch_no,
                                  sku_code = sku.sku_code,
                                  spu_code = spu.spu_code,
                                  spu_description = spu.spu_description,
                                  spu_name = spu.spu_name,
                                  bar_code = spu.bar_code,
                                  dispatch_status = dl.dispatch_status,
                                  customer_id = dl.customer_id,
                                  customer_name = dl.customer_name,
                                  sku_id = dl.sku_id,
                                  qty = dl.qty,
                                  weight = dl.weight,
                                  volume = dl.volume,
                                  creator = dl.creator,
                                  create_time = dl.create_time,
                                  damage_qty = dl.damage_qty,
                                  lock_qty = dl.lock_qty,
                                  picked_qty = dl.picked_qty,
                                  intrasit_qty = dl.intrasit_qty,
                                  package_qty = dl.package_qty,
                                  weighing_qty = dl.weighing_qty,
                                  actual_qty = dl.actual_qty,
                                  sign_qty = dl.sign_qty,
                                  package_no = dl.package_no,
                                  package_person = dl.package_person,
                                  package_time = dl.package_time,
                                  weighing_no = dl.weighing_no,
                                  weighing_person = dl.weighing_person,
                                  weighing_weight = dl.weighing_weight,
                                  waybill_no = dl.waybill_no,
                                  carrier = dl.carrier,
                                  freightfee = dl.freightfee,
                              }
                              ).ToListAsync();
            return data.Adapt<List<DispatchlistDetailViewModel>>();
        }

        /// <summary>
        /// add a new Dispatchlist 
        /// </summary>
        /// <param name="viewModel">viewmodel</param>
        /// <param name="currentUser">current user</param>
        /// <returns></returns>
        public async Task<(bool flag, string msg)> AddAsync(List<DispatchlistAddViewModel> viewModel, CurrentUser currentUser)
        {
            var DbSet = _dBContext.GetDbSet<DispatchlistEntity>();
            var entities = viewModel.Adapt<List<DispatchlistEntity>>();
            var sku_id_list = entities.Select(t => t.sku_id).ToList();
            var skus = await _dBContext.GetDbSet<SkuEntity>().Where(t => sku_id_list.Contains(t.id)).ToListAsync();
            var dispatch_no = await GetOrderCode(currentUser);
            foreach (var entity in entities)
            {
                var sku = skus.FirstOrDefault(t => t.id == entity.sku_id);
                entity.id = 0;
                entity.create_time = DateTime.Now;
                entity.creator = currentUser.user_name;
                entity.last_update_time = DateTime.Now;
                entity.tenant_id = currentUser.tenant_id;
                if (sku != null)
                {
                    entity.volume = entity.qty * sku.volume;
                    entity.weight = entity.qty * sku.weight;
                }
                entity.dispatch_no = dispatch_no;
            }
            await DbSet.AddRangeAsync(entities);
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
        /// <param name="dispatch_no">dispatch_no</param>
        /// <param name="currentUser">current user</param>
        /// <returns></returns>
        public async Task<(bool flag, string msg)> DeleteAsync(string dispatch_no, CurrentUser currentUser)
        {
            var entities = await _dBContext.GetDbSet<DispatchlistEntity>().Where(t => t.dispatch_no.Equals(dispatch_no) && t.tenant_id == currentUser.tenant_id).Include(e => e.detailList).ToListAsync();
            if (entities.Any(t => t.dispatch_status > 1))
            {
                return (false, _stringLocalizer["status_not_delete"]);
            }
            _dBContext.GetDbSet<DispatchlistEntity>().RemoveRange(entities);
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
        /// Dispatchlist details with available stock
        /// </summary>
        /// <param name="dispatch_no">dispatch_no</param>
        /// <param name="currentUser">current user</param>
        /// <returns></returns>
        public async Task<List<DispatchlistConfirmDetailViewModel>> ConfirmOrderCheck(string dispatch_no, CurrentUser currentUser)
        {
            var DbSet = _dBContext.GetDbSet<DispatchlistEntity>();
            var dispatchpick_DBSet = _dBContext.GetDbSet<DispatchpicklistEntity>();
            var stock_DbSet = _dBContext.GetDbSet<StockEntity>();
            var asn_DBSet = _dBContext.GetDbSet<AsnEntity>();
            var dispatch_DBSet = _dBContext.GetDbSet<DispatchlistEntity>();
            var sku_DBSet = _dBContext.GetDbSet<SkuEntity>().AsNoTracking();
            var spu_DBSet = _dBContext.GetDbSet<SpuEntity>().AsNoTracking();
            var processdetail_DBSet = _dBContext.GetDbSet<StockprocessdetailEntity>().AsNoTracking();
            var move_DBSet = _dBContext.GetDbSet<StockmoveEntity>();
            var owner_DBSet = _dBContext.GetDbSet<GoodsownerEntity>();
            var location_DBSet = _dBContext.GetDbSet<GoodslocationEntity>();
            var stock_group_datas = from stock in stock_DbSet.AsNoTracking()
                                    group stock by new { stock.id, stock.sku_id, stock.goods_location_id, stock.goods_owner_id } into sg
                                    select new
                                    {
                                        stock_id = sg.Key.id,
                                        goods_owner_id = sg.Key.goods_owner_id,
                                        sku_id = sg.Key.sku_id,
                                        goods_location_id = sg.Key.goods_location_id,
                                        qty_frozen = sg.Where(t => t.is_freeze == true).Sum(e => e.qty),
                                        qty = sg.Sum(t => t.qty)
                                    };
            var dispatch_group_datas = from dp in DbSet.AsNoTracking()
                                       join dpp in dispatchpick_DBSet.AsNoTracking() on dp.id equals dpp.dispatchlist_id
                                       where dp.dispatch_status > 1 && dp.dispatch_status < 6
                                       group dpp by new { dpp.sku_id, dpp.goods_location_id, dpp.goods_owner_id } into dg
                                       select new
                                       {
                                           goods_owner_id = dg.Key.goods_owner_id,
                                           sku_id = dg.Key.sku_id,
                                           goods_location_id = dg.Key.goods_location_id,
                                           qty_locked = dg.Sum(t => t.pick_qty)
                                       };
            var process_locked_group_datas = from pd in processdetail_DBSet
                                             where pd.is_update_stock == false
                                             group pd by new { pd.sku_id, pd.goods_location_id, pd.goods_owner_id } into pdg
                                             select new
                                             {
                                                 goods_owner_id = pdg.Key.goods_owner_id,
                                                 sku_id = pdg.Key.sku_id,
                                                 goods_location_id = pdg.Key.goods_location_id,
                                                 qty_locked = pdg.Sum(t => t.qty)
                                             };
            var move_locked_group_datas = from m in move_DBSet.AsNoTracking()
                                          where m.move_status == 0
                                          group m by new { m.sku_id, m.orig_goods_location_id, m.goods_owner_id } into mg
                                          select new
                                          {
                                              goods_owner_id = mg.Key.goods_owner_id,
                                              sku_id = mg.Key.sku_id,
                                              goods_location_id = mg.Key.orig_goods_location_id,
                                              qty_locked = mg.Sum(t => t.qty)
                                          };
            var datas = await (from dl in DbSet
                               join sg in stock_group_datas on dl.sku_id equals sg.sku_id into sg_left
                               from sg in sg_left.DefaultIfEmpty()
                               join dp in dispatch_group_datas on new { sg.sku_id, sg.goods_location_id, sg.goods_owner_id } equals new { dp.sku_id, dp.goods_location_id, dp.goods_owner_id } into dp_left
                               from dp in dp_left.DefaultIfEmpty()
                               join pl in process_locked_group_datas on new { sg.sku_id, sg.goods_location_id, sg.goods_owner_id } equals new { pl.sku_id, pl.goods_location_id, pl.goods_owner_id } into pl_left
                               from pl in pl_left.DefaultIfEmpty()
                               join m in move_locked_group_datas on new { sg.sku_id, sg.goods_location_id, sg.goods_owner_id } equals new { m.sku_id, m.goods_location_id, m.goods_owner_id } into m_left
                               from m in m_left.DefaultIfEmpty()
                               join sku in sku_DBSet on dl.sku_id equals sku.id
                               join spu in spu_DBSet on sku.spu_id equals spu.id
                               join owner in owner_DBSet.AsNoTracking() on sg.goods_owner_id equals owner.id into owner_left
                               from owner in owner_left.DefaultIfEmpty()
                               join gl in location_DBSet.AsNoTracking() on sg.goods_location_id equals gl.id into gl_left
                               from gl in gl_left.DefaultIfEmpty()
                               where dl.dispatch_no == dispatch_no && dl.tenant_id == currentUser.tenant_id
                               select new
                               {
                                   stock_id = sg.stock_id,
                                   goods_owner_name = owner.goods_owner_name == null ? "" : owner.goods_owner_name,
                                   goods_location_id = sg.goods_location_id == null ? 0 : sg.goods_location_id,
                                   goods_owner_id = sg.goods_owner_id == null ? 0 : sg.goods_owner_id,
                                   spu_name = spu.spu_name,
                                   spu_code = spu.spu_code,
                                   sku_code = sku.sku_code,
                                   qty_available = (sg.qty == null ? 0 : sg.qty) - (sg.qty_frozen == null ? 0 : sg.qty_frozen) - (dp.qty_locked == null ? 0 : dp.qty_locked) - (pl.qty_locked == null ? 0 : pl.qty_locked) - (m.qty_locked == null ? 0 : m.qty_locked),
                                   qty = dl.qty,
                                   sku_id = dl.sku_id,
                                   id = dl.id,
                                   spu_description = spu.spu_description,
                                   dispatch_status = dl.dispatch_status,
                                   bar_code = spu.bar_code,
                                   customer_id = dl.customer_id,
                                   customer_name = dl.customer_name,
                                   dispatch_no = dl.dispatch_no,
                                   location_name = gl.location_name == null ? "" : gl.location_name,
                                   warehouse_area_name = gl.warehouse_area_name == null ? "" : gl.warehouse_area_name,
                                   warehouse_name = gl.warehouse_name == null ? "" : gl.warehouse_name
                               }).ToListAsync();
            var res = (from d in datas
                       group d by new
                       {
                           d.spu_name,
                           d.spu_code,
                           d.sku_code,
                           d.qty,
                           d.sku_id,
                           d.id,
                           d.spu_description,
                           d.dispatch_status,
                           d.bar_code,
                           d.customer_id,
                           d.customer_name,
                           d.dispatch_no,
                       }
                       into dg
                       select new DispatchlistConfirmDetailViewModel
                       {
                           dispatchlist_id = dg.Key.id,
                           sku_id = dg.Key.id,
                           dispatch_no = dg.Key.dispatch_no,
                           sku_code = dg.Key.sku_code,
                           spu_code = dg.Key.spu_code,
                           dispatch_status = dg.Key.dispatch_status,
                           spu_description = dg.Key.spu_description,
                           spu_name = dg.Key.spu_name,
                           bar_code = dg.Key.bar_code,
                           customer_id = dg.Key.customer_id,
                           customer_name = dg.Key.customer_name,
                           qty = dg.Key.qty,
                           qty_available = dg.Sum(t => t.qty_available),
                           confirm = dg.Key.qty > dg.Sum(t => t.qty_available) ? false : true
                       }).ToList();
            foreach (var r in res)
            {
                var picklist = (from d in datas.Where(t => t.sku_id == r.sku_id).OrderBy(o => o.qty_available)
                                select new DispatchlistConfirmPickDetailViewModel
                                {
                                    stock_id = d.sku_id,
                                    dispatchlist_id = r.dispatchlist_id,
                                    goods_location_id = d.goods_location_id,
                                    qty_available = d.qty_available,
                                    goods_owner_id = d.goods_owner_id,
                                    goods_owner_name = d.goods_owner_name,
                                    location_name = d.location_name,
                                    warehouse_area_name = d.warehouse_area_name,
                                    warehouse_name = d.warehouse_name,
                                    pick_qty = 0
                                }
                              ).OrderByDescending(o => o.qty_available).ToList();
                int pick_qty = 0;
                foreach (var pick in picklist)
                {
                    if (pick_qty >= r.qty)
                    {
                        break;
                    }
                    pick.pick_qty = (r.qty <= (pick_qty + pick.qty_available)) ? (r.qty - pick_qty) : pick.qty_available;
                    pick_qty += pick.pick_qty;
                }
                r.pick_list = picklist;
            }
            return res;

        }

        /// <summary>
        ///  Confirm orders and create  dispatchpicklist
        /// </summary>
        /// <param name="viewModels">viewModels</param>
        /// <param name="currentUser">current user</param>
        /// <returns></returns>
        public async Task<(bool flag, string msg)> ConfirmOrder(List<DispatchlistConfirmDetailViewModel> viewModels, CurrentUser currentUser)
        {
            var DBSet = _dBContext.GetDbSet<DispatchlistEntity>();
            var dispatchlist_id_list = viewModels.Select(t => t.dispatchlist_id).ToList();
            var dispatchlist_datas = await DBSet.Where(t => dispatchlist_id_list.Contains(t.id)).ToListAsync();
            var pick_DBSet = _dBContext.GetDbSet<DispatchpicklistEntity>();
            var stock_DBSet = _dBContext.GetDbSet<StockEntity>();
            var pick_datas = new List<DispatchpicklistEntity>();
            var stock_id_list = new List<int>();
            var processdetail_DBSet = _dBContext.GetDbSet<StockprocessdetailEntity>().AsNoTracking();
            var move_DBSet = _dBContext.GetDbSet<StockmoveEntity>();
            var new_dispatchlists = new List<DispatchlistEntity>();
            var topick_viewmodels = new List<StockViewModel>();
            foreach (var vm in viewModels.Where(t => t.confirm == true).ToList())
            {
                stock_id_list.AddRange(vm.pick_list.Where(t => t.pick_qty > 0).Select(t => t.stock_id).ToList());
            }
          
            foreach (var vm in viewModels)
            {
                var d = dispatchlist_datas.Where(t => t.id == vm.dispatchlist_id).FirstOrDefault();
                if (d == null)
                {
                    return (false, _stringLocalizer["data_changed"]);
                }
                if (vm.confirm == true)
                {
                    d.last_update_time = DateTime.Now;
                    d.lock_qty = vm.pick_list.Sum(t => t.pick_qty);
                    foreach (var p in vm.pick_list.Where(t => t.pick_qty > 0).ToList())
                    {
                        pick_datas.Add(new DispatchpicklistEntity
                        {
                            sku_id = vm.sku_id,
                            is_update_stock = false,
                            dispatchlist_id = p.dispatchlist_id,
                            goods_location_id = p.goods_location_id,
                            goods_owner_id = p.goods_owner_id,
                            last_update_time = DateTime.Now,
                            pick_qty = p.pick_qty,
                        });
                        topick_viewmodels.Add(new StockViewModel { id = p.stock_id,qty = p.pick_qty});
                    }
                    if (d.lock_qty < d.qty)
                    {
                        d.qty = d.lock_qty;
                        new_dispatchlists.Add(new DispatchlistEntity
                        {
                            sku_id = vm.sku_id,
                            dispatch_status = 1,
                            qty = d.qty - d.lock_qty,
                        });
                    }
                }
                else
                {
                    new_dispatchlists.Add(new DispatchlistEntity
                    {
                        sku_id = vm.sku_id,
                        dispatch_status = 1,
                        qty = vm.qty,
                    });
                    DBSet.Remove(d);
                }
            }
            var stock_group_datas = from stock in stock_DBSet.AsNoTracking()
                                    where stock_id_list.Contains(stock.id)
                                    group stock by new { stock.id, stock.sku_id, stock.goods_location_id, stock.goods_owner_id } into sg
                                    select new
                                    {
                                        stock_id = sg.Key.id,
                                        goods_owner_id = sg.Key.goods_owner_id,
                                        sku_id = sg.Key.sku_id,
                                        goods_location_id = sg.Key.goods_location_id,
                                        qty_frozen = sg.Where(t => t.is_freeze == true).Sum(e => e.qty),
                                        qty = sg.Sum(t => t.qty)
                                    };
            var dispatch_group_datas = from dp in DBSet.AsNoTracking()
                                       join dpp in pick_DBSet.AsNoTracking() on dp.id equals dpp.dispatchlist_id
                                       where dp.dispatch_status > 1 && dp.dispatch_status < 6
                                       group dpp by new { dpp.sku_id, dpp.goods_location_id, dpp.goods_owner_id } into dg
                                       select new
                                       {
                                           goods_owner_id = dg.Key.goods_owner_id,
                                           sku_id = dg.Key.sku_id,
                                           goods_location_id = dg.Key.goods_location_id,
                                           qty_locked = dg.Sum(t => t.pick_qty)
                                       };
            var process_locked_group_datas = from pd in processdetail_DBSet
                                             where pd.is_update_stock == false
                                             group pd by new { pd.sku_id, pd.goods_location_id, pd.goods_owner_id } into pdg
                                             select new
                                             {
                                                 goods_owner_id = pdg.Key.goods_owner_id,
                                                 sku_id = pdg.Key.sku_id,
                                                 goods_location_id = pdg.Key.goods_location_id,
                                                 qty_locked = pdg.Sum(t => t.qty)
                                             };
            var move_locked_group_datas = from m in move_DBSet.AsNoTracking()
                                          where m.move_status == 0
                                          group m by new { m.sku_id, m.orig_goods_location_id, m.goods_owner_id } into mg
                                          select new
                                          {
                                              goods_owner_id = mg.Key.goods_owner_id,
                                              sku_id = mg.Key.sku_id,
                                              goods_location_id = mg.Key.orig_goods_location_id,
                                              qty_locked = mg.Sum(t => t.qty)
                                          };
            var stock_datas = await (from sg in stock_group_datas
                                     join dp in dispatch_group_datas on new { sg.sku_id, sg.goods_location_id, sg.goods_owner_id } equals new { dp.sku_id, dp.goods_location_id, dp.goods_owner_id } into dp_left
                                     from dp in dp_left.DefaultIfEmpty()
                                     join pl in process_locked_group_datas on new { sg.sku_id, sg.goods_location_id, sg.goods_owner_id } equals new { pl.sku_id, pl.goods_location_id, pl.goods_owner_id } into pl_left
                                     from pl in pl_left.DefaultIfEmpty()
                                     join m in move_locked_group_datas on new { sg.sku_id, sg.goods_location_id, sg.goods_owner_id } equals new { m.sku_id, m.goods_location_id, m.goods_owner_id } into m_left
                                     from m in m_left.DefaultIfEmpty()
                                     select new
                                     {
                                         stock_id = sg.stock_id,
                                         qty_available = (sg.qty == null ? 0 : sg.qty) - (sg.qty_frozen == null ? 0 : sg.qty_frozen) - (dp.qty_locked == null ? 0 : dp.qty_locked) - (pl.qty_locked == null ? 0 : pl.qty_locked) - (m.qty_locked == null ? 0 : m.qty_locked),
                                     }).ToListAsync();
            var if_not_stock = (from  tp in  topick_viewmodels
                               join s in stock_datas on tp.id equals s.stock_id
                               where tp.qty>s.qty_available select tp).Any();
            if(if_not_stock)
            {
                return (false, _stringLocalizer["data_changed"]);
            }
            await pick_DBSet.AddRangeAsync(pick_datas);
            var dispatch_no = await GetOrderCode(currentUser);
            new_dispatchlists.ForEach(t =>
            {
                t.dispatch_no = dispatch_no;
                t.creator = currentUser.user_name;
                t.create_time = DateTime.Now;
            });
            await DBSet.AddRangeAsync(new_dispatchlists);
            var qty =  await _dBContext.SaveChangesAsync();
            if (qty > 0)
            {
                return (true, _stringLocalizer["operation_success"]);
            }
            else
            {
                return (false, _stringLocalizer["operation_failed"]);
            }
        }


        /// <summary>
        /// get next order code number
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetOrderCode(CurrentUser currentUser)
        {
            string code;
            string date = DateTime.Now.ToString("yyyy" + "MM" + "dd");
            string maxNo = await _dBContext.GetDbSet<DispatchlistEntity>().Where(t => t.tenant_id == currentUser.tenant_id).MaxAsync(t => t.dispatch_no);
            if (maxNo == null)
            {
                code = date + "-0001";
            }
            else
            {
                string maxDate = maxNo.Substring(0, 8);
                string maxDateNo = maxNo.Substring(9, 4);
                if (date == maxDate)
                {
                    int.TryParse(maxDateNo, out int dd);
                    int newDateNo = dd + 1;
                    code = date + "-" + newDateNo.ToString("0000");
                }
                else
                {
                    code = date + "-0001";
                }
            }

            return code;
        }
        #endregion
    }
}

