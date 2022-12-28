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
using ModernWMS.WMS.Entities.ViewModels.Dispatchlist;

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
                        group d by new { d.dispatch_no, d.dispatch_status, d.customer_id, d.customer_name ,d.create_time,d.creator}
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
        public async Task<List<DispatchlistViewModel>> GetAllAsync(string dispatch_no)
         {
             var DbSet = _dBContext.GetDbSet<DispatchlistEntity>();
             var data = await DbSet.AsNoTracking().Where(t=>t.dispatch_no==dispatch_no).ToListAsync();
             return data.Adapt<List<DispatchlistViewModel>>();
         }
 
         /// <summary>
         /// Get a record by id
         /// </summary>
         /// <returns></returns>
         public async Task<DispatchlistViewModel> GetAsync(int id)
         {
             var DbSet = _dBContext.GetDbSet<DispatchlistEntity>();
             var entity = await DbSet.AsNoTracking().FirstOrDefaultAsync(t=>t.id.Equals(id));
             if (entity == null)
             {
                 return null;
             }
             return entity.Adapt<DispatchlistViewModel>();
         }
        /// <summary>
        /// add a new Dispatchlist
        /// </summary>
        /// <param name="viewModel">viewmodel</param>
        /// <param name="currentUser">current user</param>
        /// <returns></returns>
        public async Task<(int id, string msg)> AddAsync(DispatchlistViewModel viewModel, CurrentUser currentUser)
         {
             var DbSet = _dBContext.GetDbSet<DispatchlistEntity>();
             var entity = viewModel.Adapt<DispatchlistEntity>();
             entity.id = 0;
             entity.create_time = DateTime.Now;
             entity.creator = currentUser.user_name;
             entity.last_update_time = DateTime.Now;
             entity.tenant_id = currentUser.tenant_id;
            entity.dispatch_no = await GetOrderCode();
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
         public async Task<(bool flag, string msg)> UpdateAsync(DispatchlistViewModel viewModel)
         {
             var DbSet = _dBContext.GetDbSet<DispatchlistEntity>();
             var entity = await DbSet.FirstOrDefaultAsync(t => t.id.Equals(viewModel.id));
             if (entity == null)
             {
                 return (false,_stringLocalizer[ "not_exists_entity"]);
             }
             entity.id = viewModel.id;
             entity.dispatch_no = viewModel.dispatch_no;
             entity.dispatch_status = viewModel.dispatch_status;
             entity.customer_id = viewModel.customer_id;
             entity.customer_name = viewModel.customer_name;
             entity.sku_id = viewModel.sku_id;
             entity.qty = viewModel.qty;
             entity.weight = viewModel.weight;
             entity.volume = viewModel.volume;
             entity.damage_qty = viewModel.damage_qty;
             entity.lock_qty = viewModel.lock_qty;
             entity.picked_qty = viewModel.picked_qty;
             entity.intrasit_qty = viewModel.intrasit_qty;
             entity.package_qty = viewModel.package_qty;
             entity.weighing_qty = viewModel.weighing_qty;
             entity.actual_qty = viewModel.actual_qty;
             entity.sign_qty = viewModel.sign_qty;
             entity.package_no = viewModel.package_no;
             entity.package_person = viewModel.package_person;
             entity.package_time = viewModel.package_time;
             entity.weighing_no = viewModel.weighing_no;
             entity.weighing_person = viewModel.weighing_person;
             entity.weighing_weight = viewModel.weighing_weight;
             entity.waybill_no = viewModel.waybill_no;
             entity.carrier = viewModel.carrier;
             entity.freightfee = viewModel.freightfee;
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
             var qty = await _dBContext.GetDbSet<DispatchlistEntity>().Where(t => t.id.Equals(id)).ExecuteDeleteAsync();
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
        /// get next order code number
        /// </summary>
        /// <returns></returns>
        public async  Task<string> GetOrderCode()
        {
            string code;
            string date = DateTime.Now.ToString("yyyy" + "MM" + "dd");
            string maxNo =await _dBContext.GetDbSet<DispatchlistEntity>().MaxAsync(t => t.dispatch_no);
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
 
