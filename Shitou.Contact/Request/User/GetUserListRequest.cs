using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Shitou.Contact.Request
{
    /// <summary>
    /// 获取用户列表
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName ="Request")]
    public class GetUserListRequest : PageRequest
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string MobileNo { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }
    }
}
