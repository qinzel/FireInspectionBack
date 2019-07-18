using System;
using System.Collections.Generic;

namespace EHECD.FirePatrolInspection.Entity
{
    /// <summary>
    /// 设备 
    /// </summary>
    public class EHECD_Device 
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
		/// 设备二维码地址
		/// </summary>
        public string sQRCode { set; get; }

     	
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
        /// 单位上级
        /// </summary>
        public int iParentID { set; get; }


        /// <summary>
        /// 责任人姓名
        /// </summary>
        public string sClientName { set; get; }
        
        /// <summary>
        /// 责任人电话
        /// </summary>
        public string sClientPhone { get; set; }


        /// <summary>
        /// 责任人所属部门
        /// </summary>
        public string sOrganName { set; get; }

     	
		/// <summary>
		/// 生产日期
		/// </summary>
        public string sProductionDate { set; get; }

     	
		/// <summary>
		/// 过期年限
		/// </summary>
        public int iExpiredYears { set; get; }

     	
		/// <summary>
		/// 强制报废年限
		/// </summary>
        public int iForciblyScrappedYears { set; get; }

     	
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
        public bool iAbnormalStatus { set; get; }


        /// <summary>
        /// 选中的设备ID集合
        /// </summary>
        public string sDeviceIDs { set; get; }


        /// <summary>
        /// 添加单位ID（用于查找复制最新一条数据）
        /// </summary>
        public int iCreateUnitID { set; get; }


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime dCreateTime { set; get; }

     	
		/// <summary>
		/// 删除标识[0:未删除,1:已删除]
		/// </summary>
        public int bIsDeleted { set; get; }

        /// <summary>
        /// 设备指标集合
        /// </summary>
        public List<EHECD_DeviceParam> ParamList { set; get; }


        /// <summary>
        /// 上一次巡检明细
        /// </summary>
        public List<EHECD_InspectionDetail> DetailList { set; get; }


        /// <summary>
        /// 关联设备
        /// </summary>
        public List<EHECD_Device> DeviceList { set; get; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastModifyTime { get; set; }
    }
}