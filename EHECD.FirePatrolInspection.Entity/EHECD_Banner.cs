using System;

namespace EHECD.FirePatrolInspection.Entity
{
    /// <summary>
    /// 轮播 
    /// </summary>
    public class EHECD_Banner
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { set; get; }


        /// <summary>
        /// 图片地址
        /// </summary>
        public string sImageSrc { set; get; }


        /// <summary>
        /// 链接地址
        /// </summary>
        public string sLink { set; get; }


        /// <summary>
        /// 轮播文章类别[0:无,1:法律知识,2:消防知识,3:资讯,4:帮助,5:关于我们]
        /// </summary>
        public int iArticleType { set; get; }


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime dCreateTime { set; get; }


        /// <summary>
        /// 删除标识[0:未删除,1:已删除]
        /// </summary>
        public bool bIsDeleted { set; get; }
    }
}