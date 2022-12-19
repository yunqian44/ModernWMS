﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ModernWMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernWMS.Core.Middleware
{
    /// <summary>
    /// ViewModelActionFiter
    /// </summary>
    public class ViewModelActionFiter : ActionFilterAttribute
    {
        /// <summary>
        /// override OnActionExecuting
        /// </summary>
        /// <param name="context">context</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            if (!context.ModelState.IsValid)
            {
                string method = context.HttpContext.Request.Method;
                ResultModel<object> result = new ResultModel<object>();
                StringBuilder msg = new StringBuilder();
                bool flag = false;
                foreach (var item in context.ModelState.Values)
                {
                    if (method.Equals("GET"))
                    {
                        msg.Append($",参数值“{item.AttemptedValue}”未通过校验！");
                    }
                    else
                    {
                        foreach (var error in item.Errors)
                        {
                            if (error.ErrorMessage.Contains("convert")
                                || error.ErrorMessage.Contains("Unexpected character")
                                || error.ErrorMessage.Contains("is not")
                                || error.ErrorMessage.Contains("valid")
                                || error.ErrorMessage.Contains("Input "))
                            {
                                flag = true;
                            }
                            else
                            {
                                msg.Append($",{error.ErrorMessage}");
                            }
                        }
                    }
                }
                if (flag)
                {
                    msg.Append($",您填写的数据存在类型不正确或数值超出类型范围");
                }
                if (msg.ToString().Length > 0)
                {
                    result.ErrorMessage += msg.ToString().Substring(1);
                }
                result.Code = 400;
                context.Result = new JsonResult(result, new Newtonsoft.Json.JsonSerializerSettings()
                {
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                    DateFormatString = "yyyy-MM-dd HH:mm:ss"
                });
            }
            else
            {
                base.OnActionExecuting(context);
            }

        }
    }
}
