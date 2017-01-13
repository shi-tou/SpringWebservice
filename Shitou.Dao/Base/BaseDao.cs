using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
using Shitou.Common;
using Spring.Data.Core;
using Spring.Data.Common;
using Webdiyer.WebControls.Mvc;
using System.Reflection;
using Shitou.Model;

namespace Shitou.Dao
{
    /// <summary>
    /// Base数据访问
    /// </summary>
    public class BaseDao : AdoDaoSupport, IBaseDao
    {
        #region 添加、修改、删除
        /// <summary>
        /// 插入记录
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        public bool Insert<T>(T t)
        {
            List<T> list = new List<T>();
            list.Add(t);
            return Insert<T>(list);
        }
        /// <summary>
        /// 批量插入记录
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        public bool Insert<T>(List<T> listT)
        {
            try
            {
                if (listT == null || listT.Count == 0)
                    return false;

                List<string> listSql = new List<string>();
                string sql = "select {0} from {1}";
                IDbParameters param = AdoTemplate.CreateDbParameters();
                Type type = typeof(T);
                PropertyInfo[] propertys = type.GetProperties();
                TableMapAttribute attribute = TableMapAttribute.GetInstance(type);
                string tableName = attribute.TableName;
                string cols = "";
                foreach (PropertyInfo pi in propertys)
                {
                    string name = pi.Name;
                    if (cols != "")
                    {
                        cols += ",";
                    }
                    cols += name;
                }
                sql = string.Format(sql, cols, tableName);
                return AdoTemplate.DataTableUpdateWithCommandBuilder(ObjectHelper.CopyToDataTable<T>(listT.ToArray()), CommandType.Text, sql, null, tableName) > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        public bool Update<T>(T t)
        {
            try
            {
                if (t != null)
                {
                    string sql = "update {0} set {1} where {2};";
                    string sqlUpdate = "";
                    string sqlWhere = "";
                    IDbParameters param = AdoTemplate.CreateDbParameters();
                    Type type = typeof(T);
                    PropertyInfo[] propertys = type.GetProperties();
                    TableMapAttribute attribute = TableMapAttribute.GetInstance(type);
                    string tableName = attribute.TableName;
                    string primaryKey = ((TableMapAttribute)attribute).primaryKey;
                    foreach (PropertyInfo pi in propertys)
                    {
                        string name = pi.Name;
                        object value = type.GetProperty(name).GetValue(t, null);
                        //为null则不更新
                        if (value == null || name.ToLower() == "createtime" || name.ToLower() == "createby")
                        {
                            continue;
                        }
                        //主键(作为更新条件)
                        if (name == primaryKey)
                        {
                            sqlWhere = string.Format("{0}=?{0}", name);
                        }
                        else
                        {
                            if (sqlUpdate != "")
                            {
                                sqlUpdate += ",";
                            }
                            sqlUpdate += string.Format("{0}=?{0}", name);
                        }
                        param.AddWithValue(name, value);
                    }
                    sql = string.Format(sql, tableName, sqlUpdate, sqlWhere);
                    return AdoTemplate.ExecuteNonQuery(CommandType.Text, sql.ToString(), param) > 0;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public bool Delete<T>()
        {
            return Delete<T>("", "");
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public bool Delete<T>(string columnName, object value)
        {
            Hashtable hs = new Hashtable();
            if (string.IsNullOrEmpty(columnName))
            {
                hs.Add(columnName, value);
            }
            return Delete<T>(hs);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hs"></param>
        /// <returns></returns>
        public bool Delete<T>(Hashtable hs)
        {
            try
            {
                TableMapAttribute attribute = TableMapAttribute.GetInstance(typeof(T));
                string tableName = attribute.TableName;
                StringBuilder sql = new StringBuilder(string.Format("delete from {0} where 1=1 ", tableName));
                IDbParameters param = AdoTemplate.CreateDbParameters();
                foreach (string key in hs.Keys)
                {
                    sql.AppendFormat(" and {0}=?{0}", key);
                    param.AddWithValue(key, hs[key]);
                }
                return AdoTemplate.ExecuteNonQuery(CommandType.Text, sql.ToString(), param) > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 删除记录（逻辑删除，表里必须有IsDelete字段）
        /// </summary>
        public bool DeleteLogical<T>(string columnName, object value)
        {
            try
            {
                TableMapAttribute attribute = TableMapAttribute.GetInstance(typeof(T));
                string tableName = attribute.TableName;
                IDbParameters param = AdoTemplate.CreateDbParameters();
                string sql = string.Format("update {0} set IsDelete=1 where {1}=@{1}", tableName, columnName);
                param.AddWithValue(columnName, value);
                return AdoTemplate.ExecuteNonQuery(CommandType.Text, sql, param) > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region 获取List
        /// <summary>
        /// 获取指定条件的数据
        /// </summary>
        public List<T> GetList<T>()
        {
            return GetList<T>("", "");
        }
        /// <summary>
        /// 获取指定条件的数据
        /// </summary>
        public List<T> GetList<T>(string columnName, object value)
        {
            Hashtable hs = new Hashtable();
            if (columnName != "")
                hs.Add(columnName, value);
            return GetList<T>(hs);
        }
        /// <summary>
        /// 获取指定条件的数据
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public List<T> GetList<T>(Hashtable hs)
        {
            List<T> list = new List<T>();
            try
            {
                Type type = typeof(T);
                List<PropertyInfo> propertys = type.GetProperties().ToList();
                var attribute = type.GetCustomAttributes(typeof(TableMapAttribute), false).FirstOrDefault();
                if (attribute == null)
                {
                    throw new Exception("类" + type.Name + "必须添加'TableMapAttribute'属性");
                }
                string tableName = ((TableMapAttribute)attribute).TableName;
                string sql = "select * from " + tableName + " where 1=1";
                //对于有IsDelete属性的，过滤已删除的数据
                if (propertys.Exists(p => p.Name.ToLower() == "isdelete"))
                {
                    sql += " and IsDelete=0 ";
                }
                IDbParameters param = AdoTemplate.CreateDbParameters();
                foreach (string key in hs.Keys)
                {
                    sql += string.Format(" and {0}=?{0}", key);
                    param.AddWithValue(key, hs[key]);
                }
                DataTable dt = AdoTemplate.DataTableCreateWithParams(CommandType.Text, sql, param);
                dt.TableName = tableName;
                list = ObjectHelper.CopyToObjects<T>(dt).ToList();
                return list ?? new List<T>();
            }
            catch (Exception ex)
            {
                return list;
            }
        }
        /// <summary>
        /// 获取指定sql的数据
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public List<T> GetList<T>(CommandType commandType, string sql, IDbParameters param)
        {
            try
            {
                DataTable dt = AdoTemplate.DataTableCreateWithParams(CommandType.Text, sql, param);
                return ObjectHelper.CopyToObjects<T>(dt).ToList() ?? default(List<T>);
            }
            catch (Exception ex)
            {
                return default(List<T>);
            }
        }
        #endregion

        #region 执行Sql语句
        /// <summary>
        /// 执行sql语句
        /// </summary>
        public int ExecteSql(string sql, CommandType commandType = CommandType.Text)
        {
            return AdoTemplate.ExecuteNonQuery(commandType, sql);
        }
        /// <summary>
        /// 执行sql语句
        /// </summary>
        public int ExecteSql(string sql, IDbParameters param, CommandType commandType = CommandType.Text)
        {
            return AdoTemplate.ExecuteNonQuery(commandType, sql, param);
        }
        /// <summary>
        /// 执行sql语句
        /// </summary>
        public DataSet ExecteSqlDataSet(string sql, IDbParameters parms, CommandType commandType = CommandType.Text)
        {
            return AdoTemplate.DataSetCreateWithParams(commandType, sql, parms);
        }
        /// <summary>
        /// 执行sql语句
        /// </summary>
        public DataTable ExecteSqlGetDataTable(string sql, IDbParameters parms, CommandType commandType = CommandType.Text)
        {
            return AdoTemplate.DataTableCreateWithParams(commandType, sql, parms);
        }
        #endregion

        #region 分页
        /// <summary>
        /// 分页(最好是通过存储过程去分页取数据，因为数据库sql语法不一样)
        /// 这里只支持了mysql分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagedList<T> GetPageList<T>(string sql, IDbParameters param, int pageIndex, int pageSize, string orderBy)
        {
            PagedList<T> pageList = null;
            if (!string.IsNullOrEmpty(orderBy))
            {
                sql += " order by " + orderBy;
            }
            var obj= AdoTemplate.ExecuteScalar(CommandType.Text, "select count(1) from (" + sql + ") as A", param);
            int totalItemCount = int.Parse(obj.ToString());
            sql += " limit " + (pageIndex - 1) * pageSize + "," + pageSize + "";
            DataTable dt = AdoTemplate.DataTableCreateWithParams(CommandType.Text, sql, param);
            T[] list = ObjectHelper.CopyToObjects<T>(dt);
            pageList = new PagedList<T>(list, pageIndex, pageSize, totalItemCount);
            return pageList;
        }
        /// <summary>
        /// 分页最好是通过存储过程去分页取数据，因为数据库sql语法不一样
        /// 这里只支持了mysql分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selectSql"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagedList<T> GetPageList<T>(int pageIndex, int pageSize, string orderBy)
        {
            Type type = typeof(T);
            List<PropertyInfo> propertys = type.GetProperties().ToList();
            var attribute = type.GetCustomAttributes(typeof(TableMapAttribute), false).FirstOrDefault();
            if (attribute == null)
            {
                throw new Exception("类" + type.Name + "必须添加'TableMapAttribute'属性");
            }
            string tableName = ((TableMapAttribute)attribute).TableName;
            string sql = "select * from " + tableName + " where 1=1";
            //对于有IsDelete属性的，过滤已删除的数据
            if (propertys.Exists(p => p.Name.ToLower() == "isdelete"))
            {
                sql += " and IsDelete=0 ";
            }
            if (!string.IsNullOrEmpty(orderBy))
            {
                sql += " order by " + orderBy;
            }
            int totalItemCount = (int)AdoTemplate.ExecuteScalar(CommandType.Text, "select count(1) from (" + sql + ") as A");
            sql += " limit " + (pageIndex - 1) * pageSize + "," + pageSize + "";
            DataTable dt = AdoTemplate.DataTableCreate(CommandType.Text, sql);
            T[] list = ObjectHelper.CopyToObjects<T>(dt);
            PagedList<T> pageList = new PagedList<T>(list, pageIndex, pageSize, totalItemCount);
            return pageList;
        }
        #endregion
    }
}
