
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace ModernWMS.Core.Models
{
    /// <summary>
    /// login input viewmodel
    /// </summary>
    public class LoginInputViewModel
    {
        /// <summary>
        /// username
        /// </summary>
        [Required(ErrorMessage ="Required")]
        [Display(Name = "UserName")]
        public string user_name { get; set; } = string.Empty;
        /// <summary>
        /// password
        /// </summary>
        public string password { get; set; } = string.Empty;
    }
}
