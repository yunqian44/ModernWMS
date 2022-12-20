/*
 * date：2022-12-20
 * developer：AMo
 */
using System.ComponentModel.DataAnnotations;

namespace ModernWMS.WMS.Entities.ViewModels
{
    /// <summary>
    /// rolemenu viewModel
    /// </summary>
    public class RolemenuViewModel
    {

        #region constructor
        /// <summary>
        /// constructor
        /// </summary>
        public RolemenuViewModel()
        {

        }
        #endregion

        #region Property

        /// <summary>
        /// id
        /// </summary>
        [Display(Name = "id")]
        [MaxLength(10, ErrorMessage = "MaxLength")]
        public int id { get; set; } = 0;
         
        /// <summary>
        /// menu_id
        /// </summary>
        [Display(Name = "menu_id")]
        [MaxLength(10, ErrorMessage = "MaxLength")]
        public int menu_id { get; set; } = 0;

        /// <summary>
        /// menu_name
        /// </summary>
        [Display(Name = "menu_name")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(32, ErrorMessage = "MaxLength")]
        public string menu_name { get; set; } = string.Empty;

        /// <summary>
        /// authority
        /// </summary>
        [Display(Name = "authority")]
        [MaxLength(3, ErrorMessage = "MaxLength")]
        public byte authority { get; set; } = 0;

        #endregion

    }
}
