using Mapster;
using Microsoft.EntityFrameworkCore;
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
        #endregion

        #region constructor
        /// <summary>
        /// Goods owner constructor
        /// </summary>
        /// <param name="dBContext">The DBContext</param>
        public GoodsownerService(
            SqlDBContext dBContext
            )
        {
            this._dBContext = dBContext;
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
                //国际化
                return (0, "exists goods_owner_name");
            }
            var entity = viewModel.Adapt<GoodsownerEntity>();
            entity.id = 0;
            entity.create_time = DateTime.Now;
            entity.last_update_time = DateTime.Now;
            await DbSet.AddAsync(entity);
            await _dBContext.SaveChangesAsync();
            if (entity.id > 0)
            {
                return (entity.id, "add_success");
            }
            else
            {
                return (0, "add_failed");
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
                //国际化
                return (false, "exists goods_owner_name");
            }
            var entity = await DbSet.FirstOrDefaultAsync(t => t.id.Equals(viewModel.id));
            if (entity == null)
            {
                return (false, "not exists entity");
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
                return (true, "update_success");
            }
            else
            {
                return (false, "update_failed");
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
                return (true, "delete_success");
            }
            else
            {
                return (false, "delete_failed");
            }
        }
        #endregion
    }
}
