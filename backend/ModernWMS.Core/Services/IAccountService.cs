using System.Collections.Generic;
using System.Threading.Tasks;
using ModernWMS.Core.Models;

namespace ModernWMS.Core.Services
{
    /// <summary>
    /// account service interface
    /// </summary>
    public interface IAccountService
    {

        /// <summary>
        /// login
        /// </summary>
        /// <param name="loginInput">user 's account infomation</param>
        /// <returns></returns>
        Task<LoginOutputViewModel> Login(LoginInputViewModel loginInput);

        string HelloWorld();
    }
}
