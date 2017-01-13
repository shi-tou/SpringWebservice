using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using Webdiyer.WebControls.Mvc;
using Spring.Data.Common;

namespace Shitou.Dao
{
    /// <summary>
    /// 数据访问接口
    /// </summary>
    public interface IBaseDao
    {
        #region 添加、修改、删除
        /// <summary>
        /// 插入记录
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Insert<T>(T t);
        /// <summary>
        /// 批量插入记录
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Insert<T>(List<T> listT);
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Update<T>(T t);
        /// <summary>
        /// 删除记录
        /// </summary>
        bool Delete<T>();
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnName">字段名</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        bool Delete<T>(string columnName, object value);
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hs">删除条件</param>
        /// <returns></returns>
        bool Delete<T>(Hashtable hs);
        /// <summary>
        /// 删除记录（逻辑删除，表里必须有IsDelete字段）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnName">字段名</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        bool DeleteLogical<T>(string columnName, object value);
        #endregion

        #region 获取list
        /// <summary>
        /// 获取指定条件的数据
        /// </summary>
        List<T> GetList<T>();
        /// <summary>
        /// 获取指定条件的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnName">字段名</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        List<T> GetList<T>(string columnName, object value);
        /// <summary>
        /// 获取指定条件的数据
        /// </summary>
        /// <param name="hs">查询条件</param>
        /// <returns></returns>
        List<T> GetList<T>(Hashtable hs);
        /// <summary>
        /// 获取指定sql的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        List<T> GetList<T>(CommandType commandType, string sql, IDbParameters param);
        #endregion

        #region 执行Sql语句
        /// <summary>
        /// 执行sql语句
        /// </summary>
        int ExecteSql(string sql, CommandType commandType = CommandType.Text);
        /// <summary>
        /// 执行sql语句
        /// </summary>
        int ExecteSql(string sql, IDbParameters param, CommandType commandType = CommandType.Text);
        /// <summary>
        /// 执行sql语句
        /// </summary>
        DataSet ExecteSqlDataSet(string sql, IDbParameters parms, CommandType commandType = CommandType.Text);
        /// <summary>
        /// 执行sql语句
        /// </summary>
        DataTable ExecteSqlGetDataTable(string sql, IDbParameters parms, CommandType commandType = CommandType.Text);
        #endregion

        #region 分页
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagedList<T> GetPageList<T>(string sql, IDbParameters param, int pageIndex, int pageSize, string orderBy);
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selectSql"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagedList<T> GetPageList<T>(int pageIndex, int pageSize, string orderBy);
        #endregion
    }
}
