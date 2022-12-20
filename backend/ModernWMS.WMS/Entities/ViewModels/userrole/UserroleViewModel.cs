/*
 * date：2022-12-20
 * developer：NoNo
 */
using System;
using System.ComponentModel.DataAnnotations;
using ModernWMS.Core.Utility;

namespace ModernWMS.WMS.Entities.ViewModels
{
    /// <summary>
    /// userrole viewModel
    /// </summary>
    public class UserroleViewModel
    {

         #region constructor
         /// <summary>
         /// constructor
         /// </summary>
         public UserroleViewModel()
         {
 
         }
         #endregion
        #region Property

        /// <summary>
        /// id
        /// </summary>
        [Display(Name = "id")]
        [MaxLength(10,ErrorMessage = "MaxLength")]
        public int id { get; set; }  = 0;

        /// <summary>
        /// role_name
        /// </summary>
        [Display(Name = "role_name")]
        [MaxLength(32,ErrorMessage = "MaxLength")]
        public string role_name { get; set; }  = string.Empty;

        /// <summary>
        /// is_valid
        /// </summary>
        [Display(Name = "is_valid")]
        [MaxLength(1,ErrorMessage = "MaxLength")]
        public bool is_valid { get; set; } = false;

        /// <summary>
        /// create_time
        /// </summary>
        [Display(Name = "create_time")]
        [MaxLength(23,ErrorMessage = "MaxLength")]
         [DataType(DataType.DateTime, ErrorMessage = "DataType_DateTime")]
        public DateTime create_time { get; set; }  = UtilConvert.MinDate;

        /// <summary>
        /// last_update_time
        /// </summary>
        [Display(Name = "last_update_time")]
        [MaxLength(23,ErrorMessage = "MaxLength")]
         [DataType(DataType.DateTime, ErrorMessage = "DataType_DateTime")]
        public DateTime last_update_time { get; set; }  = UtilConvert.MinDate;

        /// <summary>
        /// tenant_id
        /// </summary>
        [Display(Name = "tenant_id")]
        [MaxLength(3,ErrorMessage = "MaxLength")]
        public byte tenant_id { get; set; }  = 0;


        #endregion

    }
}
