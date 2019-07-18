
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.Common;
using EHECD.FirePatrolInspection.DAL;
using EHECD.FirePatrolInspection.Entity;
using System.Linq;

namespace EHECD.FirePatrolInspection.Service
{
    /// <summary>
    /// 部门业务逻辑
    /// </summary>
    public class DeptService
    {
        static DeptService instance = new DeptService();
        private static DeptDao Dao = DeptDao.Instance;

        private DeptService()
        {
        }

        public static DeptService Instance
        {
            get { return instance; }
        }
		
		#region 获取部门数据

        /// <summary>
        /// 获取部门数据
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

		#region 获取部门详情

        /// <summary>
        /// 获取部门详情
        /// </summary>
        /// <param name="iDeptID"></param>
        /// <returns></returns>
        public EHECD_Dept Get(int iDeptID)
        {
            return Dao.Get(iDeptID) ?? new EHECD_Dept();
        }
		
		#endregion
		
		#region 添加编辑部门

        /// <summary>
        /// 添加编辑部门
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultMessage Set(EHECD_Dept entity)
        {
            ResultMessage result = new ResultMessage();

            EHECD_Dept dept = Dao.GetListByUnitID(entity.iUseDeptID).Where(o => o.sName.Equals(entity.sName) && o.iUseDeptID == entity.iUseDeptID && o.ID != entity.ID && !o.bIsDeleted).FirstOrDefault();
            if (dept != null)
            {
                result.message = "总后台或当前单位已存在同名部门";
                return result;
            }

            if (entity.ID == 0)
            {
                //新增部门
                result.success = Dao.Insert(entity);
                result.message = result.success ? "添加部门成功" : "添加部门失败";
            }
            else
            {
                //修改部门
                result.success = Dao.Modify(entity);
                result.message = result.success ? "编辑部门成功" : "编辑部门失败";
            }
            return result;
        }

        #endregion

        #region 添加编辑部门

        /// <summary>
        /// 添加编辑部门
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultMessage SetSelf(EHECD_Dept entity)
        {
            ResultMessage result = new ResultMessage();

            EHECD_Dept dept = Dao.GetListByUnitID(entity.iUseDeptID).Where(o => o.sName.Equals(entity.sName) && o.iUseDeptID == entity.iUseDeptID && o.ID != entity.ID && !o.bIsDeleted).FirstOrDefault();
            if (dept != null)
            {
                result.message = "本单位已存在同名部门";
                return result;
            }

            if (entity.ID == 0)
            {
                //新增部门
                result.success = Dao.Insert(entity);
                result.message = result.success ? "添加部门成功" : "添加部门失败";
            }
            else
            {
                //修改部门
                result.success = Dao.Modify(entity);
                result.message = result.success ? "编辑部门成功" : "编辑部门失败";
            }
            return result;
        }

        #endregion

        #region 批量删除部门

        /// <summary>
        /// 批量删除部门
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public ResultMessage Delete(string sIds)
        {
            ResultMessage result = new ResultMessage();
            var iCount = ClientDao.Instance.GetClientsByDeptIDs(sIds);
            if (iCount > 0)
            {
                result.message = "部门下还存在点检员，请确认后再操作";
                return result;
            }
            result.success = Dao.Delete(sIds);
            result.message = result.success ? "删除部门成功" : "删除部门失败";
            return result;
        }

        #endregion

        #region 获取单位下辖所有部门

        /// <summary>
        /// 获取单位下辖所有部门
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Dept> GetListByUnitID(int iUnitID)
        {
            return Dao.GetListByUnitID(iUnitID);
        }

        #endregion
    }
}