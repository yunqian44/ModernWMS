/*
 * date：2022-12-22
 * developer：NoNo
 */
using System;
using System.ComponentModel.DataAnnotations;
using ModernWMS.Core.Utility;

namespace ModernWMS.WMS.Entities.ViewModels
{
    /// <summary>
    /// dispatchpicklist viewModel
    /// </summary>
    public class DispatchpicklistViewModel
    {

         #region constructor
         /// <summary>
         /// constructor
         /// </summary>
         public DispatchpicklistViewModel()
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
        /// dispatchlist_id
        /// </summary>
        [Display(Name = "dispatchlist_id")]
        public int dispatchlist_id { get; set; }  = 0;

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
        /// sku_id
        /// </summary>
        [Display(Name = "sku_id")]
        public int sku_id { get; set; }  = 0;

        /// <summary>
        /// pick_qty
        /// </summary>
        [Display(Name = "pick_qty")]
        public int pick_qty { get; set; }  = 0;

        /// <summary>
        /// picked_qty
        /// </summary>
        [Display(Name = "picked_qty")]
        public int picked_qty { get; set; }  = 0;

        /// <summary>
        /// is_update_stock
        /// </summary>
        [Display(Name = "is_update_stock")]
        public bool is_update_stock { get; set; } =true;

        /// <summary>
        /// last_update_time
        /// </summary>
        [Display(Name = "last_update_time")]
         [DataType(DataType.DateTime, ErrorMessage = "DataType_DateTime")]
        public DateTime last_update_time { get; set; }  = UtilConvert.MinDate;


        #endregion

    }
}
