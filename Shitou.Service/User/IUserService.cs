using Shitou.Contact.Request;
using Shitou.Contact.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webdiyer.WebControls.Mvc;

namespace Shitou.Service
{
    /// <summary>
    /// 用户模块访问接口
    /// </summary>
    public interface IUserService : IBaseService
    {
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        AppResponse<List<GetUserListResponse>> GetUserList(GetUserListRequest request);
    }
}
