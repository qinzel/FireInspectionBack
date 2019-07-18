
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.Common;
using EHECD.FirePatrolInspection.DAL;
using EHECD.FirePatrolInspection.Entity;
using System.Web;
using System.Linq;

namespace EHECD.FirePatrolInspection.Service
{
    /// <summary>
    /// 值班室业务逻辑
    /// </summary>
    public class DutyRoomService
    {
        static DutyRoomService instance = new DutyRoomService();
        private static DutyRoomDao Dao = DutyRoomDao.Instance;
        public static object async = new object();

        private DutyRoomService()
        {
        }

        public static DutyRoomService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (async)
                    {
                        if (instance == null)
                        {
                            instance = new DutyRoomService();
                        }
                    }

                }
                return instance;
            }
        }
		
		#region 获取值班室数据

        /// <summary>
        /// 获取值班室数据
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

		#region 获取值班室详情

        /// <summary>
        /// 获取值班室详情
        /// </summary>
        /// <param name="iDutyRoomID"></param>
        /// <returns></returns>
        public EHECD_DutyRoom Get(int iDutyRoomID)
        {
            return Dao.Get(iDutyRoomID) ?? new EHECD_DutyRoom();
        }
		
		#endregion
		
		#region 添加编辑值班室

        /// <summary>
        /// 添加编辑值班室
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultMessage Set(EHECD_DutyRoom entity)
        {
            ResultMessage result = new ResultMessage();

            lock (async)
            {
                EHECD_DutyRoom room = Dao.GetListByUnitID(entity.iUseDeptID).Where(o => o.sName.Equals(entity.sName) && o.iUseDeptID == entity.iUseDeptID && o.ID != entity.ID).FirstOrDefault();
                if (room != null)
                {
                    result.message = "本单位已存在同名值班室";
                    return result;
                }

                if (entity.ID == 0)
                {
                    var iVal = Dao.Insert(entity);
                    //新增值班室
                    result.success = iVal > 0 ? true : false;
                    if (result.success)
                    {
                        // 生成值班室二维码（iQRCodeType：0为设备，1为值班室）
                        string qrText = "{ \"iQRCodeType\": 1, \"sQRCodeName\": \"" + entity.sName + "\", \"iUnitID\": " + entity.iUseDeptID + ", \"iTargetID\": " + iVal + " }";
                        EHECD_DutyRoom dutyRoom = Dao.Get(iVal);
                        dutyRoom.sQRCode = QrCodeHelper.CreateQrCode(qrText, "DutyRoom");
                        Dao.UpdateQRCode(dutyRoom);
                    }
                    result.message = result.success ? "添加值班室成功" : "添加值班室失败";
                }
                else
                {
                    //修改值班室
                    result.success = Dao.Update(entity);
                    result.message = result.success ? "编辑值班室成功" : "编辑值班室失败";
                }
                return result;
            }
        }
		
		#endregion
		
		#region 批量删除值班室

        /// <summary>
        /// 批量删除值班室
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public ResultMessage Delete(string sIds)
        {
            ResultMessage result = new ResultMessage()
            {
                success = Dao.Delete(sIds)
            };
            result.message = result.success ? "删除值班室成功" : "删除值班室失败";
            return result;
        }

        #endregion

        #region 获取单位下辖值班室数据

        /// <summary>
        /// 获取单位下辖值班室数据
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_DutyRoom> GetListByUnitID(int iUnitID)
        {
            return Dao.GetListByUnitID(iUnitID);
        }

        #endregion

        #region 根据ID集获取值班室数据

        /// <summary>
        /// 根据ID集获取值班室数据
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_DutyRoom> GetDataByIDs(string sIds)
        {
            return Dao.GetDataByIDs(sIds);
        }

        #endregion
    }
}