using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using Shitou.Contact.Response;
using Shitou.Contact.Request;
using Shitou.Model;

namespace Shitou.WebService
{
    /// <summary>
    /// 使用Spring.NET提供的将业务逻辑类开放为WebService方式时，此业务逻辑类必须要有一个接口。
    /// </summary>
    public interface IAppService
    {
        #region 测试
        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        string Test();

        #endregion

        #region 用户
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //AppResponse<List<GetUserListResponse>> GetUserList(GetUserListRequest request);
        /// <summary>
        /// 用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //AppResponse AddUser(UserInfo request);
        /// <summary>
        /// 用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //AppResponse<UserInfo> GetUserInfo(string mobileNo);
        #endregion
    }
}
