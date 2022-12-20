using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernWMS.Core.DynamicSearch
{
    /// <summary>
    /// SearchObject
    /// </summary>
    public class SearchObject
    {
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; } = 0;

        /// <summary>
        /// 查找条件标题
        /// </summary>
        public string Label { get; set; } = string.Empty;

        /// <summary>
        /// 查找条件字段名
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 控件类型 TEXTBOX,DATETIMEPICKER,COMBOBOX
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// 操作
        /// </summary>
        public Operators Operator { get; set; } = Operators.Equal;

        /// <summary>
        /// 查找的值
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// 查找的值
        /// </summary>
        public object Value { get; set; } = new object();

        /// <summary>
        /// 下拉框绑定的数据
        /// </summary>
        public List<ComboxItem> comboxItem { get; set; } = new List<ComboxItem>();

    }
}
