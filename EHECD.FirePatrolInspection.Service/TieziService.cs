
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.Common;
using EHECD.FirePatrolInspection.DAL;
using EHECD.FirePatrolInspection.Entity;
using EHECD.WebApi.Attributes;
using System.Linq;
using EHECD.Core.APIHelper;

namespace EHECD.FirePatrolInspection.Service
{
    /// <summary>
    /// 帖子业务逻辑
    /// </summary>
    public class TieziService
    {
        static TieziService instance = new TieziService();
        private static TieziDao Dao = TieziDao.Instance;

        private TieziService()
        {
        }

        public static TieziService Instance
        {
            get { return instance; }
        }
		
		#region 获取帖子数据

        /// <summary>
        /// 获取帖子数据
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

        #region 获取单位帖子数据

        /// <summary>
        /// 获取单位帖子数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetUnitGridData(QueryParams param)
        {
            int iTotalRecord = 0;
            var list = Dao.GetUnitTieziList(param, ref iTotalRecord);

            return list.EHECDAsPagedString(iTotalRecord);
        }

        #endregion

        #region 获取帖子详情

        /// <summary>
        /// 获取帖子详情
        /// </summary>
        /// <param name="iTieziID"></param>
        /// <returns></returns>
        public EHECD_Tiezi Get(long iTieziID)
        {
            EHECD_Tiezi entity = Dao.Get(iTieziID) ?? new EHECD_Tiezi();
            if (entity.ID > 0)
            {
                entity.CommentList = CommentDao.Instance.GetListByTieziID(iTieziID).ToList();
            }
            return entity;
        }
		
		#endregion
		
		#region 添加编辑帖子

        /// <summary>
        /// 添加编辑帖子
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultMessage Set(EHECD_Tiezi entity)
        {
            ResultMessage result = new ResultMessage();

            //新增帖子
            result.success = Dao.Insert(entity);
            result.message = result.success ? "添加帖子成功" : "添加帖子失败";
            return result;
        }
		
		#endregion
		
		#region 批量删除帖子

        /// <summary>
        /// 批量删除帖子
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public ResultMessage Delete(string sIds)
        {
            ResultMessage result = new ResultMessage()
            {
                success = Dao.Delete(sIds)
            };
            result.message = result.success ? "删除帖子成功" : "删除帖子失败";
            return result;
        }

        #endregion

        #region 获取朋友圈帖子列表

        /// <summary>
        /// 获取朋友圈帖子列表
        /// </summary>
        /// <param name="iType"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [APIAttribute(name: "tl.getlist", desc: "获取朋友圈帖子列表")]
        public ResultMessage GetTieziList(int iClientID, int page)
        {
            var iTotalRecord = 0;

            QueryParams param = new QueryParams();
            param.rows = 15;
            param.page = page;
            param.condition.Add("iClientID", iClientID);

            ResultMessage result = new ResultMessage();

            if (iClientID == 0)
            {
                result.message = "获取朋友圈帖子列表失败";
                return result;
            }

            var list = new List<EHECD_Tiezi>();

            try
            {
                list = Dao.GetTieziList(param, ref iTotalRecord).ToList();
                result.success = true;
                result.data = list;
            }
            catch
            {
                result.success = false;
            }

            result.message = result.success ? "获取朋友圈帖子列表成功" : "获取朋友圈帖子列表失败";
            return result;
        }

        #endregion

        #region 获取朋友圈帖子详情

        /// <summary>
        /// 获取朋友圈帖子详情
        /// </summary>
        /// <param name="iTieziID"></param>
        /// <returns></returns>
        [APIAttribute(name: "tl.get", desc: "获取朋友圈帖子详情")]
        public ResultMessage GetTiezi(int iTieziID)
        {
            ResultMessage result = new ResultMessage();
            EHECD_Tiezi entity = Get(iTieziID);
            result.success = entity.ID == 0 ? false : true;
            result.message = result.success ? "获取朋友圈帖子详情成功" : "获取朋友圈帖子详情失败";
            result.data = entity.ID == 0 ? null : entity;
            return result;
        }

        #endregion

        #region 获取我的帖子列表

        /// <summary>
        /// 获取朋友圈帖子列表
        /// </summary>
        /// <param name="iType"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [APIAttribute(name: "tl.getmylist", desc: "获取我的帖子列表")]
        public ResultMessage GetMyTieziList(int iClientID, int page)
        {
            var iTotalRecord = 0;

            QueryParams param = new QueryParams();
            param.rows = 15;
            param.page = page;
            param.condition.Add("iClientID", iClientID);

            ResultMessage result = new ResultMessage();

            if (iClientID == 0)
            {
                result.message = "获取我的帖子列表失败";
                return result;
            }

            var list = new List<EHECD_Tiezi>();

            try
            {
                list = Dao.GetList(param, ref iTotalRecord).ToList();
                result.success = true;
                result.data = list;
            }
            catch
            {
                result.success = false;
            }

            result.message = result.success ? "获取我的帖子列表成功" : "获取我的帖子列表失败";
            return result;
        }

        #endregion

        #region 删除我的帖子

        /// <summary>
        /// 删除我的帖子
        /// </summary>
        /// <param name="iTieziID"></param>
        /// <returns></returns>
        [APIAttribute(name: "tl.delete", desc: "删除我的帖子")]
        public ResultMessage DeleteTiezi(int iTieziID)
        {
            ResultMessage result = new ResultMessage();
            result.success = Dao.DeleteTiezi(iTieziID) ? false : true;
            result.message = result.success ? "删除帖子成功" : "删除帖子失败";
            return result;
        }

        #endregion

        #region 发布帖子

        /// <summary>
        /// 发布帖子
        /// </summary>
        /// <param name="iClientID"></param>
        /// <param name="sTitle"></param>
        /// <param name="sContent"></param>
        /// <param name="sImageSrc"></param>
        /// <returns></returns>
        [APIAttribute(name: "tl.add", desc: "发布帖子")]
        [ClientAPI(LoginCheck = true)]
        public ResultMessage CreateTiezi(int iClientID, string sTitle, string sContent, string sImageSrc = "")
        {
            ResultMessage result = new ResultMessage();

            if (iClientID == 0)
            {
                result.message = "发表帖子失败";
                return result;
            }

            EHECD_Tiezi entity = new EHECD_Tiezi()
            {
                iClientID = iClientID,
                sTitle = sTitle,
                sContent = sContent,
                sImageSrc = sImageSrc
            };

            result.success = Dao.Insert(entity) ? true : false;
            result.message = result.success ? "发表帖子成功" : "发表帖子失败";
            return result;
        }

        #endregion
    }
}