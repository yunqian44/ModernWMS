using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernWMS.Core.DynamicSearch
{
    /// <summary>
    /// 操作
    /// </summary>
    public enum Operators
    {
        /// <summary>
        /// 无操作
        /// </summary>
        None = 0,
        /// <summary>
        /// 等于 =
        /// </summary>
        Equal = 1,
        /// <summary>
        /// 大于 >
        /// </summary>
        GreaterThan = 2,
        /// <summary>
        /// 大于等于 >=
        /// </summary>
        GreaterThanOrEqual = 3,
        /// <summary>
        /// 小于  
        /// </summary>
        LessThan = 4,
        /// <summary>
        /// 小于等于  
        /// </summary>
        LessThanOrEqual = 5,
        /// <summary>
        /// 包含 like
        /// </summary>
        Contains = 6
    }
    /// <summary>
    /// 条件
    /// </summary>
    public enum Condition
    {
        /// <summary>
        /// OR
        /// </summary>
        OrElse = 1,
        /// <summary>
        /// AND
        /// </summary>
        AndAlso = 2
    }
    /// <summary>
    /// 查找条件下拉框类
    /// </summary>
    public class ComboxItem
    {
        /// <summary>
        /// 下拉框value值
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 下拉框text值
        /// </summary>
        public string text { get; set; }
    }
}
