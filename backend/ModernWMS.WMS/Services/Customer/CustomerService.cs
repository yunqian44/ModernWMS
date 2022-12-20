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
    /// customer Service
    /// </summary>
    public class CustomerService : BaseService<CustomerEntity>, ICustomerService
    {
        #region Args
        /// <summary>
        /// The DBContext
        /// </summary>
        private readonly SqlDBContext _dBContext;
        #endregion

        #region constructor
        /// <summary>
        /// customer constructor
        /// </summary>
        /// <param name="dBContext">The DBContext</param>
        public CustomerService(
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
        public async Task<List<CustomerViewModel>> GetAllAsync()
        {
            var DbSet = _dBContext.GetDbSet<CustomerEntity>();
            var data = await DbSet.AsNoTracking().ToListAsync();
            return data.Adapt<List<CustomerViewModel>>();
        }

        /// <summary>
        /// add a new record
        /// </summary>
        /// <param name="viewModel">args</param>
        /// <returns></returns>
        public async Task<(int id, string msg)> AddAsync(CustomerViewModel viewModel)
        {
            var DbSet = _dBContext.GetDbSet<CustomerEntity>();
            if (await DbSet.AnyAsync(t => t.customer_name.Equals(viewModel.customer_name)))
            {
                //国际化
                return (0, "exists customer_name");
            }
            var entity = viewModel.Adapt<CustomerEntity>();
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
        public async Task<(bool flag, string msg)> UpdateAsync(CustomerViewModel viewModel)
        {
            var DbSet = _dBContext.GetDbSet<CustomerEntity>();
            if (await DbSet.AnyAsync(t => !t.id.Equals(viewModel.id) && t.customer_name.Equals(viewModel.customer_name)))
            {
                //国际化
                return (false, "exists customer_name");
            }
            var entity = await DbSet.FirstOrDefaultAsync(t => t.id.Equals(viewModel.id));
            if (entity == null)
            {
                return (false, "not exists entity");
            }
            entity.customer_name = viewModel.customer_name;
            entity.city = viewModel.city;
            entity.address = viewModel.address;
            entity.email = viewModel.email;
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
            var qty = await _dBContext.GetDbSet<CustomerEntity>().Where(t => t.id.Equals(id)).ExecuteDeleteAsync();
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
