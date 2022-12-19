using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernWMS.WMS.Entities.ViewModels
{
    public class CompanyViewModel
    {
        #region constructor
        public CompanyViewModel()
        {

        }
        #endregion

        #region Property

        /// <summary>
        /// primary key
        /// </summary>
        public int id { get; set; } = 0;

        /// <summary>
        /// company's Name
        /// </summary>
        [Display(Name = "company_name")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(256,ErrorMessage = "MaxLength")]
        public string company_name { get; set; } = string.Empty;

        /// <summary>
        /// city
        /// </summary>
        [Display(Name = "city")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(128, ErrorMessage = "MaxLength")]
        public string city { get; set; } = string.Empty;

        /// <summary>
        /// address
        /// </summary>
        [Display(Name = "address")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(256, ErrorMessage = "MaxLength")]
        public string address { get; set; } = string.Empty;

        /// <summary>
        /// manager
        /// </summary>
        [Display(Name = "manager")]
        [MaxLength(64, ErrorMessage = "MaxLength")]
        public string manager { get; set; } = string.Empty;

        /// <summary>
        /// contact tel
        /// </summary>
        [Display(Name = "contact_tel")]
        [MaxLength(64, ErrorMessage = "MaxLength")]
        public string contact_tel { get; set; } = string.Empty;

        /// <summary>
        /// create time
        /// </summary>
        [Display(Name = "create_time")]
        [DataType(DataType.DateTime, ErrorMessage = "DataType_DateTime")]
        public DateTime create_time { get; set; } = DateTime.Now;

        /// <summary>
        /// last update time
        /// </summary>
        [Display(Name = "last_update_time")]
        [DataType(DataType.DateTime, ErrorMessage = "DataType_DateTime")]
        public DateTime last_update_time { get; set; } = DateTime.Now;
        #endregion
    }
}
