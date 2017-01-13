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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;
using Shitou.Contact.Request;
using Shitou.Contact.Response;
using Shitou.Service;
using Shitou.Model;
using Shitou.Common;

namespace Shitou.WebService
{
    public class AppService : IAppService
    {
        private IUserService userService;
        public IUserService UserService
        {
            set { userService = value; }
        }

        #region 测试
        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        public string Test()
        {
            LogHelper.Debug("测试Debug");
            LogHelper.Info("测试Info");
            LogHelper.Error("测试Error");
            LogHelper.Warn("测试Warn");
            return "abc";
        }

        #endregion

        #region 用户
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public AppResponse<List<GetUserListResponse>> GetUserList(GetUserListRequest request)
        {
            return userService.GetUserList(request);
        }
        /// <summary>
        /// 用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public AppResponse AddUser(UserInfo request)
        {
            var response = new AppResponse();
            response.IsOk = userService.Insert<UserInfo>(request);
            if (response.IsOk)
                response.MsgCode = "10000";
            else
                response.MsgCode = "99999";
            return response;
        }
        /// <summary>
        /// 用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public AppResponse<UserInfo> GetUserInfo(string mobileNo)
        {
            var response = new AppResponse<UserInfo>();
            try
            {
                response.ReturnData = userService.GetModel<UserInfo>("MobileNo", mobileNo);
                response.IsOk = true;
                response.MsgCode = "10000";
            }
            catch
            {
                response.IsOk = false;
                response.MsgCode = "99999";
            }
            return response;
        }
        #endregion

    }
}
