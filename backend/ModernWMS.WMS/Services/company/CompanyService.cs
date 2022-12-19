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
    /// CompanyService
    /// </summary>
    public class CompanyService : BaseService<CompanyEntity>, ICompanyService
    {
        #region Args
        /// <summary>
        /// The DBContext
        /// </summary>
        private readonly SqlDBContext _dBContext;
        #endregion

        #region constructor
        /// <summary>
        /// company service constructor
        /// </summary>
        /// <param name="dBContext">The DBContext</param>
        public CompanyService(
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
        public async Task<List<CompanyViewModel>> GetAllAsync()
        {
            var DbSet = _dBContext.GetDbSet<CompanyEntity>();
            var data = await DbSet.AsNoTracking().ToListAsync();
            return data.Adapt<List<CompanyViewModel>>();
        }

        /// <summary>
        /// add a new record
        /// </summary>
        /// <param name="viewModel">args</param>
        /// <returns></returns>
        public async Task<(int id, string msg)> AddAsync(CompanyViewModel viewModel)
        {
            var DbSet = _dBContext.GetDbSet<CompanyEntity>();
            if (await DbSet.AnyAsync(t => t.company_name.Equals(viewModel.company_name)))
            {
                //国际化
                return (0, "exists company_name");
            }
            var entity = viewModel.Adapt<CompanyEntity>();
            entity.id = 0;
            entity.create_time = DateTime.Now;
            entity.last_update_time = DateTime.Now;
            DbSet.Add(entity);
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
        public async Task<(bool flag, string msg)> UpdateAsync(CompanyViewModel viewModel)
        {
            var DbSet = _dBContext.GetDbSet<CompanyEntity>();
            if (await DbSet.AnyAsync(t => !t.id.Equals(viewModel.id) && t.company_name.Equals(viewModel.company_name)))
            {
                //国际化
                return (false, "exists company_name");
            }
            var entity = await DbSet.FirstOrDefaultAsync(t => t.id.Equals(viewModel.id));
            if (entity == null)
            {
                return (false, "not exists entity");
            }
            entity.company_name = viewModel.company_name;
            entity.city = viewModel.city;
            entity.address = viewModel.address;
            entity.manager = viewModel.manager;
            entity.contact_tel = viewModel.contact_tel;
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
            var qty = await _dBContext.GetDbSet<CompanyEntity>().Where(t => t.id.Equals(id)).ExecuteDeleteAsync();
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
