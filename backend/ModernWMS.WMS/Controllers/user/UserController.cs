/*
 * date：2022-12-20
 * developer：NoNo
 */
 using Microsoft.AspNetCore.Mvc;
 using ModernWMS.Core.Controller;
 using ModernWMS.Core.Models;
 using ModernWMS.WMS.Entities.ViewModels;
 using ModernWMS.WMS.IServices;
 using Microsoft.Extensions.Localization;
 
 namespace ModernWMS.WMS.Controllers
 {
     /// <summary>
     /// user controller
     /// </summary>
     [Route("user")]
     [ApiController]
     [ApiExplorerSettings(GroupName = "Base")]
     public class UserController : BaseController
     {
         #region Args
 
         /// <summary>
         /// user Service
         /// </summary>
         private readonly IUserService _userService;
 
         /// <summary>
         /// Localizer Service
         /// </summary>
         private readonly IStringLocalizer<ModernWMS.Core.MultiLanguage> _stringLocalizer;
        #endregion

        #region constructor
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="userService">user Service</param>
        /// <param name="stringLocalizer">Localizer</param>
        public UserController(
             IUserService userService
           , IStringLocalizer<ModernWMS.Core.MultiLanguage> stringLocalizer
             )
         {
             this._userService = userService;
            this._stringLocalizer= stringLocalizer;
         }
         #endregion
 
         #region Api
         /// <summary>
         /// Get all records
         /// </summary>
         /// <returns>args</returns>
        [HttpGet("all")]
         public async Task<ResultModel<List<UserViewModel>>> GetAllAsync()
         {
             var data = await _userService.GetAllAsync();
             if (data.Any())
             {
                 return ResultModel<List<UserViewModel>>.Success(data);
             }
             else
             {
                 return ResultModel<List<UserViewModel>>.Success(new List<UserViewModel>());
             }
         }
 
         /// <summary>
         /// Get a record by id
         /// </summary>
         /// <returns>args</returns>
         [HttpGet]
         public async Task<ResultModel<UserViewModel>> GetAsync(int id)
         {
             var data = await _userService.GetAsync(id);
             if (data!=null)
             {
                 return ResultModel<UserViewModel>.Success(data);
             }
             else
             {
                 return ResultModel<UserViewModel>.Error(_stringLocalizer["not exists entity"]);
             }
         }
         /// <summary>
         /// add a new record
         /// </summary>
         /// <param name="viewModel">args</param>
         /// <returns></returns>
         [HttpPost]
         public async Task<ResultModel<int>> AddAsync(UserViewModel viewModel)
         {
             viewModel.creator = CurrentUser.user_name;
             var (id, msg) = await _userService.AddAsync(viewModel);
             if (id > 0)
             {
                 return ResultModel<int>.Success(id);
             }
             else
             {
                 return ResultModel<int>.Error(msg);
             }
         }
 
         /// <summary>
         /// update a record
         /// </summary>
         /// <param name="viewModel">args</param>
         /// <returns></returns>
         [HttpPut]
         public async Task<ResultModel<bool>> UpdateAsync(UserViewModel viewModel)
         {
             var (flag, msg) = await _userService.UpdateAsync(viewModel);
             if (flag)
             {
                 return ResultModel<bool>.Success(flag);
             }
             else
             {
                 return ResultModel<bool>.Error(msg, 400, flag);
             }
         }
 
         /// <summary>
         /// delete a record
         /// </summary>
         /// <param name="id">id</param>
         /// <returns></returns>
         [HttpDelete]
         public async Task<ResultModel<string>> DeleteAsync(int id)
         {
             var (flag, msg) = await _userService.DeleteAsync(id);
             if (flag)
             {
                 return ResultModel<string>.Success(msg);
             }
             else
             {
                 return ResultModel<string>.Error(msg);
             }
         }
         #endregion
 
     }
 }
 
