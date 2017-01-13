using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shitou.Contact
{
    public class MessageCode
    {
        private static Dictionary<string, string> dic;
        static MessageCode()
        {
            dic = new Dictionary<string, string>();
            dic.Add("99998", "无效参数,请重试！");
            dic.Add("99999", "请求服务失败,请重试！");
            dic.Add("10000", "请求服务成功！");
           
        }
        public static string GetMsgContent(string msgCode)
        {
            if (dic.ContainsKey(msgCode))
                return dic[msgCode];
            return "";
        }
    }
}
