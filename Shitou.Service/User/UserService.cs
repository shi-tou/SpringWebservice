using Shitou.Contact.Request;
using Shitou.Contact.Response;
using Shitou.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webdiyer.WebControls.Mvc;

namespace Shitou.Service
{
    /// <summary>
    /// 用户模块数据访问
    /// </summary>
    public class UserService : BaseService, IUserService
    {
        private IUserDao userDao;
        public IUserDao UserDao
        {
            set { userDao = value; }
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public AppResponse<List<GetUserListResponse>> GetUserList(GetUserListRequest request)
        {
            var response = new AppResponse<List<GetUserListResponse>>();
            response.ReturnData = userDao.GetUserList(request);
            response.IsOk = true;
            response.MsgCode = "10000";
            return response;
        }
    }
}
