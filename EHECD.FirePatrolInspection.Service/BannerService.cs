using System;
using System.Collections.Generic;
using EHECD.Common;
using EHECD.FirePatrolInspection.DAL;
using EHECD.FirePatrolInspection.Entity;
using System.Linq;
using EHECD.WebApi.Attributes;
using EHECD.Core.APIHelper;

namespace EHECD.FirePatrolInspection.Service
{
    /// <summary>
    /// 轮播业务逻辑
    /// </summary>
    public class BannerService
    {
        static BannerService instance = new BannerService();
        private static BannerDao Dao = BannerDao.Instance;

        private BannerService()
        {
        }

        public static BannerService Instance
        {
            get { return instance; }
        }

        #region 获取轮播数据

        /// <summary>
        /// 获取轮播数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetGridData(QueryParams param)
        {
            int iTotalRecord = 0;
            var list = Dao.GetList(param, ref iTotalRecord);

            return list.EHECDAsPagedString(iTotalRecord);
        }

        #endregion

        #region 获取轮播信息

        /// <summary>
        /// 获取轮播信息
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EHECD_Banner> GetAllList()
        {
            return Dao.GetAllList();
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
            return Dao.Get(iBannerID) ?? new EHECD_Banner();
        }

        #endregion

        #region 添加编辑轮播

        /// <summary>
        /// 添加编辑轮播
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultMessage Set(EHECD_Banner entity)
        {
            ResultMessage result = new ResultMessage();

            if (string.IsNullOrWhiteSpace(entity.sLink))
            {
                if (entity.iArticleType == 5)
                {
                    EHECD_Article article = ArticleDao.Instance.GetAbout();
                    entity.sLink = "/ArticleDetail.html?ID=" + article.ID;
                }
                else
                {
                    entity.sLink = String.Empty;
                }
            }

            if (entity.ID == 0)
            {
                //新增轮播
                var list = Dao.GetAllList();
                if (list != null && list.Count() == 5)
                {
                    result.message = "最多添加5个轮播图";
                    return result;
                }

                result.success = Dao.Insert(entity);
                result.message = result.success ? "添加轮播成功" : "添加轮播失败";
            }
            else
            {
                //修改轮播
                result.success = Dao.Update(entity);
                result.message = result.success ? "编辑轮播成功" : "编辑轮播失败";
            }
            return result;
        }

        #endregion

        #region 批量删除轮播

        /// <summary>
        /// 批量删除轮播
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public ResultMessage Delete(string sIds)
        {
            ResultMessage result = new ResultMessage()
            {
                success = Dao.Delete(sIds)
            };
            result.message = result.success ? "删除轮播成功" : "删除轮播失败";
            return result;
        }

        #endregion

        #region 获取轮播列表

        /// <summary>
        /// 获取轮播列表
        /// </summary>
        /// <returns></returns>
        [APIAttribute(name: "banner.getlist", desc: "获取轮播列表")]
        [ClientAPI]
        public ResultMessage GetBannerList()
        {
            ResultMessage result = new ResultMessage();
            var list = new List<EHECD_Banner>();

            try
            {
                list = Dao.GetAllList().ToList();
                result.success = true;
                result.data = list;
            }
            catch
            {
                result.success = false;
            }

            result.message = result.success ? "获取轮播列表成功" : "获取轮播列表失败";
            return result;
        }

        #endregion

        #region 获取轮播详情

        /// <summary>
        /// 获取轮播详情
        /// </summary>
        /// <param name="iBannerID"></param>
        /// <returns></returns>
        [APIAttribute(name: "banner.get", desc: "获取轮播详情")]
        [ClientAPI]
        public ResultMessage GetRepairRecord(int iBannerID)
        {
            ResultMessage result = new ResultMessage();
            EHECD_Banner entity = Dao.Get(iBannerID) ?? new EHECD_Banner();
            result.success = entity.ID == 0 ? false : true;
            result.message = result.success ? "获取轮播详情成功" : "获取轮播详情失败";
            result.data = entity.ID == 0 ? null : entity;
            return result;
        }

        #endregion
    }
}
