using Shitou.Contact.Request;
using Shitou.Contact.Response;
using Spring.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webdiyer.WebControls.Mvc;

namespace Shitou.Dao
{
    /// <summary>
    /// 用户模块数据访问
    /// </summary>
    public class UserDao : BaseDao, IUserDao
    {
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PagedList<GetUserListResponse> GetUserList(GetUserListRequest request)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append(@"select * from T_User where 1=1 ");
            IDbParameters param = AdoTemplate.CreateDbParameters();
           
            if (!string.IsNullOrEmpty(request.RealName))
            {
                sbSql.Append(" and RealName like ?RealName");
                param.AddWithValue("RealName", "%" + request.RealName + "%");
            }
            if (!string.IsNullOrEmpty(request.MobileNo))
            {
                sbSql.Append(" and MobileNo like ?MobileNo");
                param.AddWithValue("MobileNo", "%" + request.MobileNo + "%");
            }

            sbSql.Append(" order by CreateTime desc");
            return GetPageList<GetUserListResponse>(sbSql.ToString(), param, request.PageIndex, request.PageSize, request.OrderBy);
        }
    }
}
