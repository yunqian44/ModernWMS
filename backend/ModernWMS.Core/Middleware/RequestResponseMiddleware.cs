/*
 * 功能：请求响应中间件
 * 日期：2020年8月26日
 * 开发人员：陈天生
 * 重大变更：
 */
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ModernWMS.Core.Models;

namespace ModernWMS.Core.Middleware
{
    /// <summary>
    /// 请求响应中间件
    /// </summary>
    public class RequestResponseMiddleware
    {
        #region 参数
        /// <summary>
        /// 委托
        /// </summary>
        private readonly RequestDelegate _next;
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILogger<RequestResponseMiddleware> _logger;

        #endregion

        #region 构造函数
        /// <summary>
        /// 请求响应中间件
        /// </summary>
        /// <param name="next">委托</param>
        /// <param name="logger">日志</param>
        public RequestResponseMiddleware(RequestDelegate next
            , ILogger<RequestResponseMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        #endregion

        #region 方法

        public async Task Invoke(HttpContext context)
        {
            if (ModernWMS.Core.Utility.GlobalConsts.IsRequestResponseMiddleware)
            {
                string requestInfo = "", responseInfo = "";
                var originalBodyStream = context.Response.Body;
                var stopwach = Stopwatch.StartNew();
                try
                {

                    requestInfo = await FormatRequest(context.Request);

                    using (var responseBody = new MemoryStream())
                    {
                        context.Response.Body = responseBody;

                        await _next(context);
                        stopwach.Stop();

                        responseInfo = await FormatResponse(context.Response);

                        await responseBody.CopyToAsync(originalBodyStream);
                    }


                    //var logMsg = $@"请求信息: {requestInfo}{Environment.NewLine}响应信息: {responseInfo}{Environment.NewLine}耗时: {stopwach.ElapsedMilliseconds}ms";
                    var logMsg = $@"请求信息: {requestInfo} ;耗时: {stopwach.ElapsedMilliseconds}ms";
                    _logger.LogInformation(logMsg);

                }
                catch (Exception ex)
                {
                    stopwach.Stop();
                    if (ex != null)
                    {
                        var logMsg = $@"请求信息: {requestInfo}{Environment.NewLine}异常: {ex.ToString()}{Environment.NewLine}耗时: {stopwach.ElapsedMilliseconds}ms";

                        _logger.LogError(logMsg);
                        _logger.LogError(ex.ToString());

                        string result = Utility.JsonHelper.SerializeObject(ResultModel<object>.Error(ex.Message));
                        var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(result));
                        await originalBodyStream.WriteAsync(bytes, 0, bytes.Length);

                    }
                }


            }
            else
            {
                await _next(context);
            }
           

        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            HttpRequestRewindExtensions.EnableBuffering(request);
            var body = request.Body;

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            body.Seek(0, SeekOrigin.Begin);
            request.Body = body;

            return $" {request.Method} {request.Scheme}://{request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return $"{response.StatusCode}: {text}";
        }

        #endregion
    }
}
