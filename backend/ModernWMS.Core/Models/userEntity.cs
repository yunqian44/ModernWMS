using Microsoft.AspNetCore.Mvc.RazorPages;
using ModernWMS.Core.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModernWMS.Core.Models
{
    /// <summary>
    /// 人员档案 实体
    /// </summary>
    [Table("user")]
    public class userEntity : BaseModel
    {

        #region 属性

        /// <summary>
        /// user's number
        /// </summary>
        public string user_num { get; set; } = string.Empty;

        /// <summary>
        /// user's name
        /// </summary>
        public string user_name { get; set; } = string.Empty;

        /// <summary>
        /// contact
        /// </summary>
        public string contact_tel { get; set; } = string.Empty;

        /// <summary>
        /// user's role
        /// </summary>
        public string user_role { get; set; } = string.Empty;

        /// <summary>
        /// sex
        /// </summary>
        public string sex { get; set; } = string.Empty;

        /// <summary>
        /// is_valid
        /// </summary>
        public bool is_valid { get; set; } = false;

        /// <summary>
        /// password
        /// </summary>
        public string auth_string { get; set; } = string.Empty;

        /// <summary>
        /// creator
        /// </summary>
        public string creator { get; set; } = string.Empty;

        /// <summary>
        /// createtime
        /// </summary>
        public DateTime create_time { get; set; } = UtilConvert.MinDate;

        /// <summary>
        /// last update time
        /// </summary>
        public DateTime last_update_time { get; set; } = UtilConvert.MinDate;

        /// <summary>
        /// tenant
        /// </summary>
        public byte tenant_id { get; set; } = 0;


        #endregion

    }
}
