/*
 * 功能：JWT配置项
 * 日期：2020年4月8日
 * 开发人员：陈天生
 * 重大变更：
 */
namespace ModernWMS.Core.JWT
{
    public class TokenSettings
    {
        /// <summary>
        /// 订阅者
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// 发布者
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// 密钥
        /// </summary>
        public string SigningKey { get; set; }
        /// <summary>
        /// 过期分钟数
        /// </summary>
        public int ExpireMinute { get; set; }
    }
}
