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

            var query = from ag in asn_group_datas
                        join sg in stock_group_datas on ag.sku_id equals sg.sku_id into sg_left
                        from sg in sg_left.DefaultIfEmpty()
                        join dp in dispatch_group_datas on sg.sku_id equals dp.sku_id into dp_left
                        from dp in dp_left.DefaultIfEmpty()
                        join sku in sku_DBSet on ag.sku_id equals sku.id
                        join spu in spu_DBSet on sku.spu_id equals spu.id
                        select new StockManagementViewModel
                        {
                            spu_name = spu.spu_name,
                            spu_code = spu.spu_code,
                            sku_code = sku.sku_code,
                            qty_asn = ag.qty_asn,
                            qty_available = sg.qty - sg.qty_frozen - dp.qty_locked,
                            qty_frozen = sg.qty_frozen,
                            qty_locked = dp.qty_locked,
                            qty_sorted = ag.qty_sorted,
                            qty_to_sort = ag.qty_to_sort,
                            shortage_qty = ag.shortage_qty,
                            qty_to_unload= ag.qty_to_unload,
                            qty= sg.qty,
                        };
             query = query .Where(queries.AsExpression<StockManagementViewModel>());
             int totals = await query.CountAsync();
             var list = await query.OrderBy(t=>t.sku_code)
                        .Skip((pageSearch.pageIndex - 1) * pageSearch.pageSize)
                        .Take(pageSearch.pageSize)
                        .ToListAsync();
             return (list, totals);
         }
         #endregion
     }
 }
 
