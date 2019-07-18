using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHECD.FirePatrolInspection.Entity
{
     public class DeviceClientModel
    {
        /// <summary>
		/// 主键ID
		/// </summary>
        public long ID { set; get; }


        /// <summary>
        /// 设备编号
        /// </summary>
        public string sNumber { set; get; }


        /// <summary>
        /// 设备类型
        /// </summary>
        public int iDeviceTypeID { set; get; }


        /// <summary>
        /// 设备类型名称
        /// </summary>
        public string sDeviceTypeName { set; get; }


        /// <summary>
        /// 数量
        /// </summary>
        public int iNumber { set; get; }


        /// <summary>
        /// 设备名称
        /// </summary>
        public string sName { set; get; }

        /// <summary>
        /// 设备位置
        /// </summary>
        public string sLocation { set; get; }

        /// <summary>
        /// 设备型号
        /// </summary>
        public string sModel { set; get; }

        /// <summary>
        /// 生产厂家
        /// </summary>
        public string sManufacturer { set; get; }

        /// <summary>
        /// 维护公司ID
        /// </summary>
        public int iRepairDeptID { set; get; }


        /// <summary>
        /// 维护公司名称
        /// </summary>
        public string sRepairDeptName { set; get; }


        /// <summary>
        /// 所属使用单位ID
        /// </summary>
        public int iUseDeptID { set; get; }

        /// <summary>
        /// 所属使用单位名称
        /// </summary>
        public string sUnitName { set; get; }

        /// <summary>
        /// 责任人
        /// </summary>
        public int iClientID { set; get; }

        /// <summary>
        /// 责任人姓名
        /// </summary>
        public string sClientName { set; get; }

        /// <summary>
        /// 责任人电话
        /// </summary>
        public string sClientPhone { get; set; }


        /// <summary>
        /// 上次巡检日期
        /// </summary>
        public string sLastInspectionDate { set; get; }


        /// <summary>
        /// 上次维修日期
        /// </summary>
        public string sLastRepairDate { set; get; }


        /// <summary>
        /// 上次更换日期
        /// </summary>
        public string sLastChangeDate { set; get; }


        /// <summary>
        /// 设备异常状态[0:正常,1:异常]
        /// </summary>
        public int iAbnormalStatus { set; get; }
        

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastModifyTime { get; set; }

        /// <summary>
		/// 删除标识[0:未删除,1:已删除]
		/// </summary>
        public int bIsDeleted { set; get; }
    }
}
