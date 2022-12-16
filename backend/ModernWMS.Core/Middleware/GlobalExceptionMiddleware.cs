/*
 * 功能：全局异常中间件
 * 日期：2020年4月8日
 * 开发人员：陈天生
 * 重大变更：
 */
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using ModernWMS.Core.Models;

namespace ModernWMS.Core.Middleware
{
    /// <summary>
    /// 全局异常中间件
    /// </summary>
    public class GlobalExceptionMiddleware
    {
        #region 参数
        private readonly RequestDelegate next; //下一步 委托
        private readonly ILogger<GlobalExceptionMiddleware> logger; //日志
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next">注入委托</param>
        /// <param name="logger">注入日志</param>
        public GlobalExceptionMiddleware(RequestDelegate next,
                                         ILogger<GlobalExceptionMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 实现Invoke方法
        /// </summary>
        /// <param name="context">http请求</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                await WriteExceptionAsync(context, ex);
            }
        }
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="e">错误信息</param>
        /// <returns></returns>
        private async Task WriteExceptionAsync(HttpContext context, Exception e)
        {
            if (e != null)
            {
                var response = context.Response;
                var message = e.InnerException == null ? e.Message : e.InnerException.Message;
                response.ContentType = "application/json";

                var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
                if (string.IsNullOrEmpty(ip))
                {
                    ip = context.Connection.RemoteIpAddress.ToString();
                }
                logger.LogError($"\r\n\r\n主机IP:{ip},异常描述：{e.Message}\r\n堆栈信息：{e.StackTrace}");

                string result = Utility.JsonHelper.SerializeObject(ResultModel<object>.Error("抱歉，操作失败了。请联系管理员。"));
                await context.Response.WriteAsync(result).ConfigureAwait(false);
            }
            else
            {
                var code = context.Response.StatusCode;
                switch (code)
                {
                    case 200:
                        return;
                    case 204:
                        return;
                    case 401:
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(Utility.JsonHelper.SerializeObject(ResultModel<object>.Error("Token无效"))).ConfigureAwait(false);
                        break;
                    default:
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(Utility.JsonHelper.SerializeObject(ResultModel<object>.Error("未知错误"))).ConfigureAwait(false);
                        break;
                }
            }
        }
        #endregion

    }
}
