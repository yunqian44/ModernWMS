using System;
using System.Collections.Generic;

namespace ModernWMS.Core.Models
{
    /// <summary>
    /// LoginOutputViewModel
    /// </summary>
    public class LoginOutputViewModel
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string user_num { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string user_name { get; set; }

        /// <summary>
        ///  用户标识
        /// </summary>
        public int user_id { get; set; }

        /// <summary>
        ///  用户标识
        /// </summary>
        public string user_role { get; set; }
        
        /// <summary>
        /// token有效分钟数
        /// </summary>
        public int expire { get; set; }

        /// <summary>
        /// 访问token
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// 刷新token
        /// </summary>
        public string refresh_token { get; set; }

    }
}
