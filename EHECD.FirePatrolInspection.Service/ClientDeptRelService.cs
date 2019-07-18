			
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.Common;
using EHECD.FirePatrolInspection.DAL;
using EHECD.FirePatrolInspection.Entity;

namespace EHECD.FirePatrolInspection.Service
{
    /// <summary>
    /// 前端用户所属单位关系表业务逻辑
    /// </summary>
    public class ClientDeptRelService
    {
        static ClientDeptRelService instance = new ClientDeptRelService();
        private static ClientDeptRelDao Dao = ClientDeptRelDao.Instance;

        private ClientDeptRelService()
        {
        }

        public static ClientDeptRelService Instance
        {
            get { return instance; }
        }
		
		#region 获取前端用户所属单位关系表数据

        /// <summary>
        /// 获取前端用户所属单位关系表数据
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

		#region 获取前端用户所属单位关系表详情

        /// <summary>
        /// 获取前端用户所属单位关系表详情
        /// </summary>
        /// <param name="iClientDeptRelID"></param>
        /// <returns></returns>
        public EHECD_ClientDeptRel Get(long iClientDeptRelID)
        {
            return Dao.Get(iClientDeptRelID) ?? new EHECD_ClientDeptRel();
        }
		
		#endregion
		
		#region 添加编辑前端用户所属单位关系表

        /// <summary>
        /// 添加编辑前端用户所属单位关系表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultMessage Set(EHECD_ClientDeptRel entity)
        {
            ResultMessage result = new ResultMessage();
						
			if (entity.ID == 0)
						
            {
                //新增前端用户所属单位关系表
                result.success = Dao.Insert(entity);
                result.message = result.success ? "添加前端用户所属单位关系表成功" : "添加前端用户所属单位关系表失败";
            }
            else
            {
                //修改前端用户所属单位关系表
                result.success = Dao.Update(entity);
                result.message = result.success ? "编辑前端用户所属单位关系表成功" : "编辑前端用户所属单位关系表失败";
            }
            return result;
        }
		
		#endregion
		
		#region 批量删除前端用户所属单位关系表

        /// <summary>
        /// 批量删除前端用户所属单位关系表
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public ResultMessage Delete(string sIds)
        {
            ResultMessage result = new ResultMessage()
            {
                success = Dao.Delete(sIds)
            };
            result.message = result.success ? "删除前端用户所属单位关系表成功" : "删除前端用户所属单位关系表失败";
            return result;
        }
		
		#endregion
    }
}

