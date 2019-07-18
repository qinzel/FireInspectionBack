using System;

namespace EHECD.FirePatrolInspection.Entity
{
    /// <summary>
    /// 包含知识、资讯、帮助和关于我们 
    /// </summary>
    public class EHECD_Article 
    {
     	
		/// <summary>
		/// 主键ID
		/// </summary>
        public int ID { set; get; }

     	
		/// <summary>
		/// 标题
		/// </summary>
        public string sTitle { set; get; }

     	
		/// <summary>
		/// 内容
		/// </summary>
        public string sContent { set; get; }

     	
		/// <summary>
		/// 排序序号
		/// </summary>
        public string sSortNumber { set; get; }

     	
		/// <summary>
		/// 列表图片
		/// </summary>
        public string sImageSrc { set; get; }

     	
		/// <summary>
		/// 文章类别[0:法律知识,1:消防知识,2:资讯,3:帮助,4:关于我们]
		/// </summary>
        public int iType { set; get; }

     	
		/// <summary>
		/// 创建时间
		/// </summary>
        public DateTime dCreateTime { set; get; }

     	
		/// <summary>
		/// 删除标识[0:未删除,1:已删除]
		/// </summary>
        public bool bIsDeleted { set; get; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime lastModifyTime { get; set; }

         }
}
