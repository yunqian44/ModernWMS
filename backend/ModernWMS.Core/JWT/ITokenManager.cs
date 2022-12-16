/*
 * 功能：JWT接口
 * 日期：2020年4月8日
 * 开发人员：陈天生
 * 重大变更：
 */

namespace ModernWMS.Core.JWT
{
    /// <summary>
    /// Token管理类
    /// </summary>
    public interface ITokenManager
    {
        /// <summary>
        /// 生成AccessToken方法
        /// </summary>
        /// <param name="userClaims">自定义信息</param>
        /// <returns>(token,有效分钟数)</returns>
        (string token, int expire) GenerateToken(CurrentUser userClaims);
        /// <summary>
        /// 生成RefreshToken方法
        /// </summary>
        /// <returns></returns>
        string GenerateRefreshToken();
        /// <summary>
        /// 获取刷新token失效分钟数
        /// </summary>
        /// <returns></returns>
        int GetRefreshTokenExpireMinute();
        /// <summary>
        /// 获取token中当前用户信息
        /// </summary>
        /// <returns></returns>
        CurrentUser GetCurrentUser();
        /// <summary>
        /// 获取token中当前用户信息
        /// </summary>
        /// <returns></returns>
        CurrentUser GetCurrentUser(string token);
    }
}
