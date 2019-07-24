
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.Common;
using EHECD.FirePatrolInspection.DAL;
using EHECD.FirePatrolInspection.Entity;
using EHECD.WebApi.Attributes;
using System.Linq;
using System.Dynamic;
using EHECD.Core.APIHelper;

namespace EHECD.FirePatrolInspection.Service
{
    /// <summary>
    /// 值班记录业务逻辑
    /// </summary>
    public class DutyRecordService
    {
        static DutyRecordService instance = new DutyRecordService();
        private static DutyRecordDao Dao = DutyRecordDao.Instance;

        private DutyRecordService()
        {
        }

        public static DutyRecordService Instance
        {
            get { return instance; }
        }
		
		#region 获取值班记录数据

        /// <summary>
        /// 获取值班记录数据
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

		#region 获取值班记录详情

        /// <summary>
        /// 获取值班记录详情
        /// </summary>
        /// <param name="iDutyRecordID"></param>
        /// <returns></returns>
        public EHECD_DutyRecord Get(long iDutyRecordID)
        {
            return Dao.Get(iDutyRecordID) ?? new EHECD_DutyRecord();
        }
		
		#endregion
		
		#region 添加编辑值班记录

        /// <summary>
        /// 添加编辑值班记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultMessage Set(EHECD_DutyRecord entity)
        {
            ResultMessage result = new ResultMessage();
						
			if (entity.ID == 0)
						
            {
                //新增值班记录
                result.success = Dao.Insert(entity);
                result.message = result.success ? "添加值班记录成功" : "添加值班记录失败";
            }
            else
            {
                //修改值班记录
                result.success = Dao.Update(entity);
                result.message = result.success ? "编辑值班记录成功" : "编辑值班记录失败";
            }
            return result;
        }
		
		#endregion
		
		#region 批量删除值班记录

        /// <summary>
        /// 批量删除值班记录
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public ResultMessage Delete(string sIds)
        {
            ResultMessage result = new ResultMessage()
            {
                success = Dao.Delete(sIds)
            };
            result.message = result.success ? "删除值班记录成功" : "删除值班记录失败";
            return result;
        }

        #endregion

        #region 获取要导出的值班信息

        /// <summary>
        /// 获取要导出的值班信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_DutyRecord> GetExportData(QueryParams param)
        {
            return Dao.GetExportData(param);
        }

        #endregion

        #region 值班签到

        /// <summary>
        /// 值班签到
        /// </summary>
        /// <param name="iClientID"></param>
        /// <param name="iDutyRoomID"></param>
        /// <returns></returns>
        [APIAttribute(name: "dutyrec.add", desc: "值班签到")]
        [ClientAPI(LoginCheck = true)]
        public ResultMessage CreateDutyRecord(int iClientID, int iDutyRoomID)
        {
            ResultMessage result = new ResultMessage();

            if (iClientID == 0)
            {
                result.message = "值班签到失败";
                return result;
            }

            EHECD_DutyRecord entity = new EHECD_DutyRecord()
            {
                iClientID = iClientID,
                iDutyRoomID = iDutyRoomID
            };

            result.success = Dao.Insert(entity) ? true : false;
            result.message = result.success ? "值班签到成功" : "值班签到失败";
            return result;
        }

        #endregion

        #region 值班签退

        /// <summary>
        /// 值班签退
        /// </summary>
        /// <param name="iClientID"></param>
        /// <param name="iDutyRoomID"></param>
        /// <param name="sDesc"></param>
        /// <param name="sImageSrc"></param>
        /// <returns></returns>
        [APIAttribute(name: "dutyrec.finish", desc: "值班签退")]
        [ClientAPI(LoginCheck = true)]
        public ResultMessage FinishDutyRecord(int iClientID, int iDutyRoomID, string sDesc = "", string sImageSrc = "")
        {
            ResultMessage result = new ResultMessage();

            if (iClientID == 0)
            {
                result.message = "值班签退失败";
                return result;
            }

            EHECD_DutyRecord entity = Dao.GetUnFinishRecord(iClientID, iDutyRoomID) ?? new EHECD_DutyRecord();
            if (entity.ID == 0)
            {
                result.message = "值班已签退，请勿重复操作";
                return result;
            }
            entity.sDesc = sDesc;
            entity.sImageSrc = sImageSrc;

            result.success = Dao.Update(entity) ? true : false;
            result.message = result.success ? "值班签退成功" : "值班签退失败";
            return result;
        }

        #endregion

        #region 获取值班记录详情

        /// <summary>
        /// 获取值班记录详情
        /// </summary>
        /// <param name="iDutyRecordID"></param>
        /// <returns></returns>
        [APIAttribute(name: "dutyrec.get", desc: "获取值班记录详情")]
        public ResultMessage GetInfo(long iDutyRecordID)
        {
            ResultMessage result = new ResultMessage();
            EHECD_DutyRecord entity = Dao.Get(iDutyRecordID) ?? new EHECD_DutyRecord();
            result.success = entity.ID == 0 ? false : true;
            result.message = result.success ? "获取值班记录详情成功" : "获取值班记录详情失败";
            result.data = entity.ID == 0 ? null : entity;
            return result;
        }

        #endregion

        #region 获取本月值班记录列表

        /// <summary>
        /// 获取本月值班记录列表
        /// </summary>
        /// <param name="iClientID"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [APIAttribute(name: "dutyrec.getlist", desc: "获取本月值班记录列表")]
        public ResultMessage GetDeviceList(int iClientID, int page)
        {
            var iTotalRecord = 0;

            QueryParams param = new QueryParams();
            param.rows = 15;
            param.page = page;
            param.condition.Add("iClientID", iClientID);
            param.condition.Add("sStartTime", DateTime.Now.ToString("yyyy-MM-01 00:00:00"));
            param.condition.Add("sEndTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            ResultMessage result = new ResultMessage();

            if (iClientID == 0)
            {
                result.message = "获取本月值班记录列表失败";
                return result;
            }

            var list = new List<EHECD_DutyRecord>();

            try
            {
                list = Dao.GetList(param, ref iTotalRecord).ToList();
                result.success = true;
                dynamic obj = new ExpandoObject();
                obj.iTotalTimeLength = list.Sum(o => o.iTimeLength);
                obj.DataList = list;
                result.data = obj;
            }
            catch
            {
                result.success = false;
            }

            result.message = result.success ? "获取本月值班记录列表成功" : "获取本月值班记录列表失败";
            return result;
        }

        #endregion

        #region 查询当前值班室该值班员是否签到签退状态

        /// <summary>
        /// 查询当前值班室该值班员是否签到签退状态
        /// </summary>
        /// <param name="iDutyRoomID"></param>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        [APIAttribute(name: "dutyrec.getstatus", desc: "获取签到状态")]
        public ResultMessage GetDutyPeopleStatus(int iDutyRoomID, int iClientID)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                EHECD_DutyRecord entity = Dao.GetDutyPeopleStatus(iDutyRoomID, iClientID) ?? new EHECD_DutyRecord();
                if (entity.ID > 0)
                {
                    if (!string.IsNullOrWhiteSpace(entity.sStartTime) && !string.IsNullOrWhiteSpace(entity.sEndTime))
                    {
                        // 已经签到并签退，需要重新签到
                        result.data = 0;
                    }
                    else if (!string.IsNullOrWhiteSpace(entity.sStartTime) && string.IsNullOrWhiteSpace(entity.sEndTime))
                    {
                        // 已经签到，并未签退
                        result.data = 1;
                    }
                }
                else
                {
                    // 不存在，需要签到
                    result.data = 0;
                }
                result.success = true;
            }
            catch
            {
                result.success = false;
            }
            result.message = result.success ? "获取签到状态成功" : "获取签到状态失败";
            return result;
        }

        #endregion
    }
}