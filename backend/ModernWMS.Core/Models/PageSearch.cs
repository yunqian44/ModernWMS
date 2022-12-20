using ModernWMS.Core.DynamicSearch;

namespace ModernWMS.Core.Models
{
    /// <summary>
    /// PageSearch
    /// </summary>
    public class PageSearch
    {
        /// <summary>
        /// 当前页码
        /// </summary> 
        public int pageIndex { get; set; } = 1;

        /// <summary>
        /// 每页行数
        /// </summary>
        public int pageSize { get; set; } = 20;

        /// <summary>
        /// 自定义分类
        /// </summary>
        public string sqlTitle { get; set; } = "";
        
        /// <summary>
        /// 查找条件
        /// </summary>
        public List<SearchObject> searchObjects { get; set; } = new List<SearchObject>();
    }
}
