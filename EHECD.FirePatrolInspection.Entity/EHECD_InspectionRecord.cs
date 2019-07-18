using System;
using System.Collections.Generic;

namespace EHECD.FirePatrolInspection.Entity
{
    /// <summary>
    /// 巡检记录 
    /// </summary>
    public class EHECD_InspectionRecord 
    {
     	
		/// <summary>
		/// 主键ID
		/// </summary>
        public long ID { set; get; }

     	
		/// <summary>
		/// 设备ID
		/// </summary>
        public long iDeviceID { set; get; }


        /// <summary>
        /// 记录类别[0:巡检,1:维修,2:更换]
        /// </summary>
        public int iRecordType { set; get; }


        /// <summary>
        /// 操作人ID
        /// </summary>
        public int iClientID { set; get; }


        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime dCreateTime { set; get; }

     	
		/// <summary>
		/// 巡检结果[0:正常,1:异常]
		/// </summary>
        public bool iStatus { set; get; }


        /// <summary>
        /// 描述或说明
        /// </summary>
        public string sDesc { set; get; }


        /// <summary>
        /// 数量
        /// </summary>
        public int iNumber { set; get; }


        /// <summary>
        /// 设备型号
        /// </summary>
        public string sModel { set; get; }


        /// <summary>
        /// 设备规格
        /// </summary>
        public string sSpec { set; get; }


        /// <summary>
        /// 安装日期
        /// </summary>
        public string sInstallDate { set; get; }


        /// <summary>
        /// 生产厂家
        /// </summary>
        public string sManufacturer { set; get; }


        /// <summary>
        /// 生产日期
        /// </summary>
        public string sProductionDate { set; get; }


        /// <summary>
        /// 记录来源[0:前端添加,1:后台添加]
        /// </summary>
        public int iSource { set; get; }


        /// <summary>
        /// 检测指标
        /// </summary>
        public List<EHECD_InspectionDetail> DetailList { set; get; }


        /// <summary>
        /// 设备编号
        /// </summary>
        public string sNumber { set; get; }


        /// <summary>
        /// 设备名称
        /// </summary>
        public string sName { set; get; }

        /// <summary>
        /// 设备安装位置
        /// </summary>
        public string sLocation { set; get; }


        /// <summary>
        /// 记录操作人
        /// </summary>
        public string sOperator { set; get; }


        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string sClientName { set; get; }


        /// <summary>
        /// 责任人姓名
        /// </summary>
        public string sClientPersonName { set; get; }


        /// <summary>
        /// 责任人所属单位
        /// </summary>
        public string sUnitName { set; get; }

        
        /// <summary>
        /// 责任人所属部门
        /// </summary>
        public string sOrganName { set; get; }


        /// <summary>
        /// 维修单位名称
        /// </summary>
        public string sRepairDeptName { set; get; }


        /// <summary>
        /// 设备类型名称
        /// </summary>
        public string sDeviceTypeName { set; get; }
    }
}