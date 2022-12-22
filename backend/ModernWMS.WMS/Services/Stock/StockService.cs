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
         public async Task<(List<StockViewModel> data, int totals)> PageAsync(PageSearch pageSearch, CurrentUser currentUser)
         {
             QueryCollection queries = new QueryCollection();
             if (pageSearch.searchObjects.Any())
             {
                 pageSearch.searchObjects.ForEach(s =>
                 {
                     queries.Add(s);
                 });
             }
             
             var DbSet = _dBContext.GetDbSet<StockEntity>();
            var asn_DBSet = _dBContext.GetDbSet<AsnEntity>();
            var dispatch_DBSet = _dBContext.GetDbSet<DispatchlistEntity>();

            var query = DbSet.AsNoTracking()
                 .Where(t => t.tenant_id.Equals(currentUser.tenant_id))
                 .Where(queries.AsExpression<StockEntity>());
             int totals = await query.CountAsync();
             var list = await query.OrderByDescending(t => t.id)
                        .Skip((pageSearch.pageIndex - 1) * pageSearch.pageSize)
                        .Take(pageSearch.pageSize)
                        .ToListAsync();
             return (list.Adapt<List<StockViewModel>>(), totals);
         }
         #endregion
     }
 }
 
