using MongoDB.Bson;

namespace EHECD.FirePatrolInspection.Entity
{
    public class EHECD_SiteMsgClientRelation
    {
        /// <summary>
        /// MongoDB关系文档ID
        /// </summary>
        public ObjectId _id { set; get; }


        /// <summary>
        /// 站内信ID
        /// </summary>
        public int iSiteMsgID { set; get; }


        /// <summary>
        /// 用户ID
        /// </summary>
        public int iClientID { set; get; }


        /// <summary>
        /// 阅读标识[true:未读,false:已读]
        /// </summary>
        public bool bIsReaded { set; get; }
    }
}