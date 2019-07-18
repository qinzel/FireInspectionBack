using System.Web.Mvc;
using System.Text;
using EHECD.Common;
using EHECD.FirePatrolInspection.Web.Filter;

namespace EHECD.FirePatrolInspection.Web.Controllers
{
    public class UediterController : Controller
    {
        /**
         * 使用前参数初始化  
         * 用法 @Html.Action("Init", "Uediter", new { area = "", module = "s",field = "d",wh = " ); 
         *  
         * 初始化 UE.ehecd_editer_init(id, v)
         * @param  string[字符串]  id  初始化标签 id
         * @param  string[字符串]  v   uediter默认值 
         * 
         *      demo UE.ehecd_editer_init('ue-test');
         *           UE.ehecd_editer_init('ue-test','测试数据');
         **/
        /// <summary>
        ///  
        /// </summary> 
        /// <param name="module"></param>
        /// <param name="field"></param>
        /// <param name="wh"></param>
        /// <returns></returns>
        [ChildActionOnly]
        [NoSign]
        public ActionResult Init(string module, string field, string wh)
        {
            ViewBag.serverUrl = GetImageServerUri(module, field, wh);
            return PartialView();
        }

        /**
         * 使用前参数初始化  @Html.Action("FilesInit", "Uediter", new { area = "", module = "goods", field = "goods", wh = "500,500|100,100" });
         *    引入插件地址  
         * 
         * 图片上传初始化
         * var selectImages = UE.ehecd_upload_image_init(id,images);
         * @param  string[字符串]  id          附加元素id[不带#] 
         * @param  array [数组]    images      已有图片
         *     demo   UE.ehecd_upload_image_init('images-select-id',[]);
         *            UE.ehecd_upload_image_init('images-select-id',['http://dddddd','http://dddddd']);
         * //获取选择上传图片
         * var sselectImagesData = selectImages.get_imgs();
         * 
         * 
         * 附件上传初始化
         * var selectfiles = UE.ehecd_upload_file_init(id,files);
         * @param  string[字符串]  id          附加元素id[不带#] 
         * @param  array [数组]    files       已有文件
         *     demo   UE.ehecd_upload_file_init('files-select-id',[]);
         *            UE.ehecd_upload_file_init('files-select-id',[{title:'文件名',url:'文件路径'},{title:'文件名',url:'文件路径'}]);
         * //获取选择上传图片
         * var selectfilesData = selectfiles.get_files();
         * 
         */
        /// <summary>
        /// 上传图片初始化
        /// </summary>
        /// <param name="module"></param>
        /// <param name="field"></param>
        /// <param name="wh"></param>
        /// <returns></returns>
        [ChildActionOnly]
        [NoSign]
        public ActionResult FilesInit(string module, string field, string wh)
        {
            ViewBag.serverUrl = GetImageServerUri(module, field, wh);
            return PartialView();
        }

        [NoSign]
        public ActionResult SigleImageInit(string module, string field, string wh)
        {
            ViewBag.serverUrl = GetImageServerUri(module, field, wh) + "&action=custom";
            return PartialView();
        }


        private static string UEDITER_URL = ReadConfig.ReadAppSetting("uediter:url");
        private static string UEDITER_APPKEY = ReadConfig.ReadAppSetting("uediter:appKey");

        private string GetImageServerUri(string module, string field, string wh)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format(@"{0}?module={1}&field={2}&appKey={3}", UEDITER_URL, module, field, UEDITER_APPKEY));
            if (!string.IsNullOrEmpty(wh))
            {
                sb.Append(string.Format(@"&wh={0}", wh));
            }
            return sb.ToString();
        }
    }
}