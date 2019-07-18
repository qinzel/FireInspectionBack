﻿/*
*插件作者:周祥
*发布时间:2014年12月12日
*插件介绍:图片上传本地预览插件 兼容浏览器(IE 谷歌 火狐) 不支持safari 当然如果是使用这些内核的浏览器基本都兼容
*插件网站:http://jquery.decadework.com
*作  者QQ:200592114
*使用方法:
*界面构造(IMG标签外必须拥有DIV 而且必须给予DIV控件ID)
* <div id="imgdiv"><img id="imgShow" width="120" height="120" /></div>
* <input type="file" id="up_img" />
*调用代码:
* new uploadPreview({ UpBtn: "up_img", DivShow: "imgdiv", ImgShow: "imgShow" });
*参数说明:
*UpBtn:选择文件控件ID;
*DivShow:DIV控件ID;
*ImgShow:图片控件ID;
*Width:预览宽度;
*Height:预览高度;
*ImgType:支持文件类型 格式:["jpg","png"];
*callback:选择文件后回调方法;

*版本:v1.4
更新内容如下:
1.修复回调.

*版本:v1.3
更新内容如下:
1.修复多层级框架获取路径BUG.
2.去除对jquery插件的依赖.
*/

/*
*author:周祥
*date:2014年12月12日
*work:图片预览插件
*/

/*乔乔毛JQ改进版2016-6-30*/

//重要的申请说三遍，上传input file 不能隐藏 ，不能隐藏 ，不能隐藏

var uploadPreview = function(setting,callBack) {

    /*
    *author:周祥
    *date:2014年12月11日
    *work:this(当前对象)
    */
    var _self = this;
    /*
    *author:周祥
    *date:2014年12月11日
    *work:判断为null或者空值
    */
    _self.IsNull = function(value) {
        if (typeof (value) == "function") { return false; }
        if (value == undefined || value == null || value == "" || value.length == 0) {
            return true;
        }
        return false;
    }
    /*
    *author:周祥
    *date:2014年12月11日
    *work:默认配置
    */
    _self.DefautlSetting = {
        UpBtn: "",
        DivShow: "",
        ImgShow: "",
        Width: 100,
        Height: 100,
        ImgType: ["gif", "jpeg", "jpg", "bmp", "png"],
        ErrMsg: "选择文件错误,图片类型必须是(gif,jpeg,jpg,bmp,png)中的一种",
        callback: function() { }
    };
    /*
    *author:周祥
    *date:2014年12月11日
    *work:读取配置
    */
   
//  _self.Setting = {
//      UpBtn: _self.IsNull(setting.UpBtn) ? _self.DefautlSetting.UpBtn : setting.UpBtn,
//      DivShow: _self.IsNull(setting.DivShow) ? _self.DefautlSetting.DivShow : setting.DivShow,
//      ImgShow: _self.IsNull(setting.ImgShow) ? _self.DefautlSetting.ImgShow : setting.ImgShow,
//      Width: _self.IsNull(setting.Width) ? _self.DefautlSetting.Width : setting.Width,
//      Height: _self.IsNull(setting.Height) ? _self.DefautlSetting.Height : setting.Height,
//      ImgType: _self.IsNull(setting.ImgType) ? _self.DefautlSetting.ImgType : setting.ImgType,
//      ErrMsg: _self.IsNull(setting.ErrMsg) ? _self.DefautlSetting.ErrMsg : setting.ErrMsg,
//      callback: _self.IsNull(setting.callback) ? _self.DefautlSetting.callback : setting.callback
//  };
    _self.Setting=$.extend({},_self.DefautlSetting,setting);
    
    /*
    *author:周祥
    *date:2014年12月11日
    *work:获取文本控件URL
    */
    _self.getObjectURL = function(file) {
 
        var url = null;
        if (window.createObjectURL != undefined) {
     
            url = window.createObjectURL(file);
        } else if (window.URL != undefined) {
       
            url = window.URL.createObjectURL(file);
        } else if (window.webkitURL != undefined) {
        
            url = window.webkitURL.createObjectURL(file);
        }
        return url;
    }
    /*
    *author:周祥
    *date:2014年12月11日
    *work:绑定事件
    */
    _self.Bind = function() {

       _self.Setting.UpBtn.change(function() {
              
            if ($(this).val()) {
                if (!RegExp("\.(" + _self.Setting.ImgType.join("|") + ")$", "i").test(this.value.toLowerCase())) {
                    alert(_self.Setting.ErrMsg);
                    this.value = "";
                    
                    return false;
                }
                if (navigator.userAgent.indexOf("MSIE") > -1) {
                	
                    try {
                        $(this).siblings("img").prop("src") = _self.getObjectURL(this.files[0]);
                    } catch (e) {

                        var div=$(this).parents(_self.Setting.DivShow)[0];
                        $(this)[0].select();
                        top.parent.document.body.focus();
                        //居然可以直接获取图片的本地路径，
                        var src = document.selection.createRange().text;
                        var parent_dom=$(this).parent();
                        while(!parent_dom.find("img").length)
                        {
                        	parent_dom=parent_dom.parent();
                        }
                        parent_dom.find("img")[0].src=src;
                        parent_dom.find("img").load(function(){
                        	var that=$(this);
                       	  typeof callBack=="function"&&callBack(that)
                       })
                       
//以下是使用滤镜的效果，发现不用滤镜 ，暂时注释！
//                      document.selection.empty();
//                      if($(this).siblings("img").length)
//                       {
//                       	 $(this).siblings("img")[0].style.display = "none";
//                       }
//                      else
//                      {
//                        	var file_parent=$(this);
//	                    	while(!file_parent.find("img").length)
//	                    	  {
//	                    	  	file_parent=file_parent.parent();
//	                    	  	
//	                    	  }
//	                         file_parent.find("img")[0].style.display = "none"; 	
//	                    }
//                     
//                      div.style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale)";
//                      div.style.width = _self.Setting.Width + "px";
//                      div.style.height = _self.Setting.Height + "px";
//                      div.filters.item("DXImageTransform.Microsoft.AlphaImageLoader").src = src;
                    }
                } else {
                	  
                    if($(this).siblings("img").length)
                    {
                    	 $(this).siblings("img").prop("src",_self.getObjectURL($(this)[0].files[0])) ;
                    }else{
                    	var file_parent=$(this);
                    	while(!file_parent.find("img").length)
                    	  {
                    	  	file_parent=file_parent.parent();
                    	  	
                    	  }
                    	file_parent.find("img").prop("src",_self.getObjectURL($(this)[0].files[0])) ;
                        file_parent.find("img").load(function(){
                          var that=$(this);
                       	  typeof callBack=="function"&&callBack(that)
                       })
                    }
                   
                }
                _self.Setting.callback();
            }
        })
   
    }
    /*
    *author:周祥
    *date:2014年12月11日
    *work:执行绑定事件
    */
    _self.Bind();
}