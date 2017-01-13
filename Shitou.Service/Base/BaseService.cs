using Shitou.Dao;
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
    /// 业务基础访问服务
    /// </summary>
    public class BaseService : IBaseService
    {
        private IBaseDao baseDao;
        public IBaseDao BaseDao
        {
            set { baseDao = value; }
        }
        
        public bool Insert<T>(T t)
        {
            return baseDao.Insert<T>(t);
        }

        public bool Insert<T>(List<T> listT)
        {
            return baseDao.Insert<T>(listT);
        }

        public bool Update<T>(T t)
        {
            return baseDao.Update<T>(t);
        }

        public bool Delete<T>()
        {
            return baseDao.Delete<T>();
        }

        public bool Delete<T>(Hashtable hs)
        {
            return baseDao.Delete<T>(hs);
        }

        public bool Delete<T>(string columnName, object value)
        {
            return baseDao.Delete<T>(columnName,value);
        }

        public bool DeleteLogical<T>(string columnName, object value)
        {
            return baseDao.DeleteLogical<T>(columnName,value);
        }

        public T GetModel<T>(string columnName, object value)
        {
            return baseDao.GetList<T>(columnName, value).FirstOrDefault();
        }

        public T GetModel<T>(Hashtable hs)
        {
            return baseDao.GetList<T>(hs).FirstOrDefault();
        }

        public List<T> GetList<T>()
        {
            return baseDao.GetList<T>();
        }

        public List<T> GetList<T>(Hashtable hs)
        {
            return baseDao.GetList<T>(hs);
        }

        public List<T> GetList<T>(string columnName, object value)
        {
            return baseDao.GetList<T>(columnName,value);
        }

        public PagedList<T> GetPageList<T>(int pageIndex, int pageSize, string orderBy)
        {
            return baseDao.GetPageList<T>(pageIndex, pageSize, orderBy);
        }

        
    }
}
