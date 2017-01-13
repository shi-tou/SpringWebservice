using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Shitou.Contact.Response
{
    [Serializable]
    [XmlRoot(ElementName = "Response")]
    public class AppResponse
    {
        public AppResponse()
        {
            IsOk = false;
            MsgCode = "99999";
        }
        [XmlElement("IsOk")]
        public bool IsOk { get; set; }
        private string msgCode = "";
        [XmlElement("MsgCode")]
        public string MsgCode
        {
            get { return msgCode; }
            set
            {
                msgCode = value;
                MsgContent = MessageCode.GetMsgContent(value);
            }
        }
        [XmlElement("MsgContent")]
        public string MsgContent { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "Response")]
    public class AppResponse<T>
    {
        public AppResponse()
        {
            IsOk = false;
            MsgCode = "99999";
        }
        [XmlElement("IsOk")]
        public bool IsOk { get; set; }
        private string msgCode = "";
        [XmlElement("MsgCode")]
        public string MsgCode
        {
            get { return msgCode; }
            set
            {
                msgCode = value;
                MsgContent = MessageCode.GetMsgContent(value);
            }
        }
        [XmlElement("MsgContent")]
        public string MsgContent { get; set; }
        public T ReturnData { get; set; }
    }
}
