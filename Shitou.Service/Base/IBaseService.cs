using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webdiyer.WebControls.Mvc;

namespace Shitou.Service
{
    /// <summary>
    /// 业务基础接口
    /// </summary>
    public interface IBaseService
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

        #region 获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        T GetModel<T>(string columnName, object value);
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        T GetModel<T>(Hashtable hs);
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
        #endregion

        #region 分页
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
