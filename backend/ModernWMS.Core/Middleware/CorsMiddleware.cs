/*
 * 功能：跨域中间件
 * 日期：2020年8月5日
 * 开发人员：陈天生
 * 重大变更：
 */
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ModernWMS.Core.Middleware
{
    /// <summary>
    /// 跨域中间件
    /// </summary>
    public class CorsMiddleware
    {
        #region 参数
        /// <summary>
        /// 代理
        /// </summary>
        private readonly RequestDelegate _next;
        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next"></param>
        public CorsMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        #endregion

        #region 执行方法
        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="httpContext">上下文</param>
        /// <returns></returns>
        public Task Invoke(HttpContext httpContext)
        {

            if (httpContext.Request.Method == "OPTIONS")
            {
                httpContext.Response.Headers.Add("Access-Control-Allow-Origin", httpContext.Request.Headers["Origin"]);
                httpContext.Response.Headers.Add("Access-Control-Allow-Headers", httpContext.Request.Headers["Access-Control-Request-Headers"]);
                httpContext.Response.Headers.Add("Access-Control-Allow-Methods", "PUT,POST,GET,DELETE,OPTIONS,HEAD,PATCH");
                httpContext.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
                httpContext.Response.Headers.Add("Access-Control-Max-Age", "86400");//缓存一天
                httpContext.Response.StatusCode = StatusCodes.Status200OK;
                return Task.CompletedTask;
            }
            if (httpContext.Request.Headers["Origin"] != "")
            {
                httpContext.Response.Headers.Add("Access-Control-Allow-Origin", httpContext.Request.Headers["Origin"]);
            }

            httpContext.Response.Headers.Add("Access-Control-Allow-Headers", httpContext.Request.Headers["Access-Control-Request-Headers"]);
            httpContext.Response.Headers.Add("Access-Control-Allow-Methods", "PUT,POST,GET,DELETE,OPTIONS,HEAD,PATCH");
            httpContext.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
            httpContext.Response.Headers.Add("Access-Control-Max-Age", "86400");//缓存一天
            return _next.Invoke(httpContext);
        }
        #endregion

    }
}
