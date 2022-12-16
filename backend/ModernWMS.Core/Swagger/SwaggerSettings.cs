
using System.Collections.Generic;

namespace ModernWMS.Core.Swagger
{
    /// <summary>
    /// Swagger配置项
    /// </summary>
    public class SwaggerSettings
    {
        /// <summary>
        /// SwaggerDoc 的Name属性
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// OpenApiInfo 的Title属性
        /// </summary>
        public string ApiTitle { get; set; }
        /// <summary>
        /// OpenApiInfo 的版本号
        /// </summary>
        public string ApiVersion { get; set; }
        /// <summary>
        /// OpenApiInfo 的描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 是否打开授权验证
        /// </summary>
        public bool SecurityDefinition { get; set; }
         
        /// <summary>
        /// 包含的XML文档
        /// </summary>
        public List<string> XmlFiles { get; set; }
         
    }
}
