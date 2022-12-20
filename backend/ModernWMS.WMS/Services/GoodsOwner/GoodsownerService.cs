using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using ModernWMS.Core.DBContext;
using ModernWMS.Core.Services;
using ModernWMS.WMS.Entities.Models;
using ModernWMS.WMS.Entities.ViewModels;
using ModernWMS.WMS.IServices;

namespace ModernWMS.WMS.Services
{
    /// <summary>
    /// Goods owner Service
    /// </summary>
    public class GoodsownerService : BaseService<GoodsownerEntity>, IGoodsownerService
    {
        #region Args
        /// <summary>
        /// The DBContext
        /// </summary>
        private readonly SqlDBContext _dBContext;

        /// <summary>
        /// Localization
        /// </summary>
        private readonly IStringLocalizer<Core.MultiLanguage> _stringLocalizer;
        #endregion

        #region constructor
        /// <summary>
        /// company service constructor
        /// </summary>
        /// <param name="dBContext">The DBContext</param>
        /// <param name="stringLocalizer">Localization</param>
        public GoodsownerService(
            SqlDBContext dBContext
          , IStringLocalizer<Core.MultiLanguage> stringLocalizer
            )
        {
            this._dBContext = dBContext;
            this._stringLocalizer = stringLocalizer;
        }
        #endregion

        #region Api
        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns></returns>
        public async Task<List<GoodsownerViewModel>> GetAllAsync()
        {
            var DbSet = _dBContext.GetDbSet<GoodsownerEntity>();
            var data = await DbSet.AsNoTracking().ToListAsync();
            return data.Adapt<List<GoodsownerViewModel>>();
        }

        /// <summary>
        /// add a new record
        /// </summary>
        /// <param name="viewModel">args</param>
        /// <returns></returns>
        public async Task<(int id, string msg)> AddAsync(GoodsownerViewModel viewModel)
        {
            var DbSet = _dBContext.GetDbSet<GoodsownerEntity>();
            if (await DbSet.AnyAsync(t => t.goods_owner_name.Equals(viewModel.goods_owner_name)))
            {
                return (0, string.Format(_stringLocalizer["exists_entity"], _stringLocalizer["goods_owner_name"], viewModel.goods_owner_name));
            }
            var entity = viewModel.Adapt<GoodsownerEntity>();
            entity.id = 0;
            entity.create_time = DateTime.Now;
            entity.last_update_time = DateTime.Now;
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
        public async Task<(bool flag, string msg)> UpdateAsync(GoodsownerViewModel viewModel)
        {
            var DbSet = _dBContext.GetDbSet<GoodsownerEntity>();
            if (await DbSet.AnyAsync(t => !t.id.Equals(viewModel.id) && t.goods_owner_name.Equals(viewModel.goods_owner_name)))
            {
                return (false, string.Format(_stringLocalizer["exists_entity"], _stringLocalizer["goods_owner_name"], viewModel.goods_owner_name));
            }
            var entity = await DbSet.FirstOrDefaultAsync(t => t.id.Equals(viewModel.id));
            if (entity == null)
            {
                return (false, _stringLocalizer["not_exists_entity"]);
            }
            entity.goods_owner_name = viewModel.goods_owner_name;
            entity.city = viewModel.city;
            entity.address = viewModel.address;
            entity.manager = viewModel.manager;
            entity.contact_tel = viewModel.contact_tel;
            entity.is_valid = viewModel.is_valid;
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
            var qty = await _dBContext.GetDbSet<GoodsownerEntity>().Where(t => t.id.Equals(id)).ExecuteDeleteAsync();
            if (qty > 0)
            {
                return (true, _stringLocalizer["delete_success"]);
            }
            else
            {
                return (false, _stringLocalizer["delete_failed"]);
            }
        }
        #endregion
    }
}
