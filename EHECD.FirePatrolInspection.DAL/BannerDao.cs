using System.Collections.Generic;
using System.Text;
using EHECD.FirePatrolInspection.Entity;
using EHECD.Common;

namespace EHECD.FirePatrolInspection.DAL
{
    /// <summary>
    /// 轮播操作
    /// </summary>
    public class BannerDao
    {
        static BannerDao instance = new BannerDao();

        private BannerDao()
        {
        }
        public static BannerDao Instance
        {
            get
            {
                return instance;
            }
        }

        #region 获取轮播信息

        /// <summary>
        /// 获取轮播信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Banner> GetList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = "Select * From  EHECD_Banner Where bIsDeleted=0";

            StringBuilder sCondition = new StringBuilder();
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sName"))
            {
                sCondition.AppendFormat(string.Format(" And sName Like '%{0}%'", param.condition["sName"]));
            }

            return DBHelper.QueryRunSqlByPager<EHECD_Banner>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }

        #endregion

        #region 获取轮播信息

        /// <summary>
        /// 获取轮播信息
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EHECD_Banner> GetAllList()
        {
            string sSql = "Select * From  EHECD_Banner Where bIsDeleted=0";

            return DBHelper.Query<EHECD_Banner>(sSql);
        }

        #endregion

        #region 获取轮播详情

        /// <summary>
        /// 获取轮播详情
        /// </summary>
        /// <param name="iBannerID"></param>
        /// <returns></returns>
        public EHECD_Banner Get(int iBannerID)
        {
            return DBHelper.QuerySingle<EHECD_Banner>(iBannerID);
        }

        #endregion

        #region 添加轮播

        /// <summary>
        /// 添加轮播
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(EHECD_Banner entity)
        {
            return DBHelper.Insert<EHECD_Banner>(entity) > 0;
        }

        #endregion

        #region 编辑轮播

        /// <summary>
        /// 编辑轮播
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(EHECD_Banner entity)
        {
            string sSql =
                @"Update [EHECD_Banner] Set 

				[sImageSrc]=@sImageSrc,

				[iArticleType]=@iArticleType,

				[sLink]=@sLink

				Where ID = @ID";
            return DBHelper.Execute(sSql, entity) > 0;
        }

        #endregion

        #region 批量删除轮播

        /// <summary>
        /// 批量删除轮播
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public bool Delete(string sIds)
        {
            sIds = "'" + string.Join("','", sIds.Split(',')) + "'";

            return DBHelper.Execute(string.Format("Update EHECD_Banner Set bIsDeleted=1 Where ID In ({0})", sIds)) > 0;
        }

        #endregion
    }
}
