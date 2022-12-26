/*
 * date：2022-12-26
 * developer：NoNo
 */
using System;
using System.ComponentModel.DataAnnotations;
using ModernWMS.Core.Utility;

namespace ModernWMS.WMS.Entities.ViewModels
{
    /// <summary>
    /// stockadjust viewModel
    /// </summary>
    public class StockadjustViewModel
    {

         #region constructor
         /// <summary>
         /// constructor
         /// </summary>
         public StockadjustViewModel()
         {
 
         }
         #endregion
        #region Property

        /// <summary>
        /// id
        /// </summary>
        [Display(Name = "id")]
        public int id { get; set; }  = 0;

        /// <summary>
        /// job_code
        /// </summary>
        [Display(Name = "job_code")]
        [MaxLength(32,ErrorMessage = "MaxLength")]
        public string job_code { get; set; }  = string.Empty;

        /// <summary>
        /// sku_id
        /// </summary>
        [Display(Name = "sku_id")]
        public int sku_id { get; set; }  = 0;

        /// <summary>
        /// goods_owner_id
        /// </summary>
        [Display(Name = "goods_owner_id")]
        public int goods_owner_id { get; set; }  = 0;

        /// <summary>
        /// goods_location_id
        /// </summary>
        [Display(Name = "goods_location_id")]
        public int goods_location_id { get; set; }  = 0;

        /// <summary>
        /// qty
        /// </summary>
        [Display(Name = "qty")]
        public int qty { get; set; }  = 0;

        /// <summary>
        /// creator
        /// </summary>
        [Display(Name = "creator")]
        [MaxLength(64,ErrorMessage = "MaxLength")]
        public string creator { get; set; }  = string.Empty;

        /// <summary>
        /// create_time
        /// </summary>
        [Display(Name = "create_time")]
         [DataType(DataType.DateTime, ErrorMessage = "DataType_DateTime")]
        public DateTime create_time { get; set; }  = UtilConvert.MinDate;

        /// <summary>
        /// last_update_time
        /// </summary>
        [Display(Name = "last_update_time")]
         [DataType(DataType.DateTime, ErrorMessage = "DataType_DateTime")]
        public DateTime last_update_time { get; set; }  = UtilConvert.MinDate;

        /// <summary>
        /// tenant_id
        /// </summary>
        [Display(Name = "tenant_id")]
        public byte tenant_id { get; set; }  = 0;

        /// <summary>
        /// is_update_stock
        /// </summary>
        [Display(Name = "is_update_stock")]
        public bool is_update_stock { get; set; } =true;

        /// <summary>
        /// job_type
        /// </summary>
        [Display(Name = "job_type")]
        public byte job_type { get; set; }  = 0;

        /// <summary>
        /// source_table_id
        /// </summary>
        [Display(Name = "source_table_id")]
        public int source_table_id { get; set; }  = 0;


        #endregion

    }
}
