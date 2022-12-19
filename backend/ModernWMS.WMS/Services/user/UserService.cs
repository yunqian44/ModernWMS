/*
 * date：2022-12-20
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
 using Microsoft.Extensions.Localization;
namespace ModernWMS.WMS.Services
 {
     /// <summary>
     ///  User Service
     /// </summary>
     public class UserService : BaseService<userEntity>, IUserService
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
         ///User  constructor
         /// </summary>
         /// <param name="dBContext">The DBContext</param>
        /// <param name="stringLocalizer">Localizer</param>
         public UserService(
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
         /// Get all records
         /// </summary>
         /// <returns></returns>
         public async Task<List<UserViewModel>> GetAllAsync()
         {
             var DbSet = _dBContext.GetDbSet<userEntity>();
             var data = await DbSet.AsNoTracking().ToListAsync();
             return data.Adapt<List<UserViewModel>>();
         }
 
         /// <summary>
         /// Get a record by id
         /// </summary>
         /// <returns></returns>
         public async Task<UserViewModel> GetAsync(int id)
         {
             var DbSet = _dBContext.GetDbSet<userEntity>();
             var entity = await DbSet.AsNoTracking().FirstOrDefaultAsync(t=>t.id.Equals(id));
             if (entity == null)
             {
                 return null;
             }
             return entity.Adapt<UserViewModel>();
         }
         /// <summary>
         /// add a new record
         /// </summary>
         /// <param name="viewModel">viewmodel</param>
         /// <returns></returns>
         public async Task<(int id, string msg)> AddAsync(UserViewModel viewModel)
         {
             var DbSet = _dBContext.GetDbSet<userEntity>();
             var entity = viewModel.Adapt<userEntity>();
             entity.id = 0;
             entity.create_time = DateTime.Now;
             entity.last_update_time = DateTime.Now;
             await DbSet.AddAsync(entity);
             await _dBContext.SaveChangesAsync();
             if (entity.id > 0)
             {
                 return (entity.id, _stringLocalizer["add_success"]);
             }
             else
             {
                 return (0, _stringLocalizer["add_failed"]);
             }
         }
         /// <summary>
         /// update a record
         /// </summary>
         /// <param name="viewModel">args</param>
         /// <returns></returns>
         public async Task<(bool flag, string msg)> UpdateAsync(UserViewModel viewModel)
         {
             var DbSet = _dBContext.GetDbSet<userEntity>();
             var entity = await DbSet.FirstOrDefaultAsync(t => t.id.Equals(viewModel.id));
             if (entity == null)
             {
                 return (false,_stringLocalizer[ "not exists entity"]);
             }
             entity.id = viewModel.id;
             entity.user_num = viewModel.user_num;
             entity.user_name = viewModel.user_name;
             entity.contact_tel = viewModel.contact_tel;
             entity.user_role = viewModel.user_role;
             entity.sex = viewModel.sex;
             entity.is_valid = viewModel.is_valid;
             entity.auth_string = viewModel.auth_string;
             entity.last_update_time = DateTime.Now;
             var qty = await _dBContext.SaveChangesAsync();
             if (qty > 0)
             {
                 return (true, _stringLocalizer["update_success"]);
             }
             else
             {
                 return (false, _stringLocalizer["update_failed"]);
             }
         }
         /// <summary>
         /// delete a record
         /// </summary>
         /// <param name="id">id</param>
         /// <returns></returns>
         public async Task<(bool flag, string msg)> DeleteAsync(int id)
         {
             var qty = await _dBContext.GetDbSet<userEntity>().Where(t => t.id.Equals(id)).ExecuteDeleteAsync();
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
 
