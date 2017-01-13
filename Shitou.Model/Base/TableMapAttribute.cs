using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shitou.Model
{
    /// <summary>
    /// 实体与数据表的映射（实体必须与数据表命名一致，不区分大小写）
    /// </summary>
    public class TableMapAttribute : System.Attribute
    {
        /// <summary>
        /// 注入属性值
        /// </summary>
        /// <param name="tableName">映射到数据库的表名</param>
        /// <param name="primaryKey">主键(默认为"ID")</param>
        public TableMapAttribute(string tableName, string primaryKey = "ID")
        {
            this.tableName = tableName;
            this.primaryKey = primaryKey;
        }
        //表名
        public string tableName;
        public string TableName
        {
            set { tableName = value; }
            get { return tableName; }
        }
        //主键
        public string primaryKey;
        public string PrimaryKey
        {
            set { primaryKey = value; }
            get { return primaryKey; }
        }

        /// <summary>
        /// 获取TableMapAttribute实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static TableMapAttribute GetInstance(Type type)
        {
            var attribute = type.GetCustomAttributes(typeof(TableMapAttribute), false).FirstOrDefault();
            if (attribute == null)
            {
                throw new Exception("类" + type.Name + "必须添加'TableMapAttribute'属性");
            }
            return (TableMapAttribute)attribute;
        }
    }
}
