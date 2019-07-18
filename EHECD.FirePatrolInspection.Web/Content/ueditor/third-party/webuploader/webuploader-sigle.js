(function ($) {
    $.fn.extend({
        uploader: function (upload_url, callback) {
            var html = new Array();
            html.push('<div class="uploader_progress" style="position: fixed; top: 0px; left: 0px; width: 100%; height: 100%; z-index: 2147483647; display: none; background: rgba(0, 0, 0, 0.498039);">');
            html.push('        <div style="text-align: center;width: 100%;font: 3em bold;position: absolute;top: 40%;color: red;">0%</div>');
            html.push('        <div style="position:relative;top:50%;width:80%;margin-left:auto;margin-right:auto;height:10px;background-color:#ccc">');
            html.push('            <div style="width: 0%; height: 10px; background-color: rgb(11, 172, 19);"></div>');
            html.push('        </div>');
            html.push('</div>');

            if ($(".uploader_progress").length <= 0)
                $(document.body).append(html.join(''));
            if (!upload_url) {
                console.error('请传入服务器地址');
                return;
            }
            var options = {
                _server: upload_url,
            };

            //webuploader封装（实现进度条）
            var InitUploader = function (_this, _progress) {

                //创建上传控件
                var uploader = WebUploader.create({
                    auto: true,
                    server: options._server,
                    pick: {
                        id: _this,
                        multiple: false
                    }, 
                    swf: "/Content/ueditor/third-party/webuploader/Uploader.swf",
                    crop: true,
                    //是否去重 (为ture不重，为false去重)
                    duplicate: true,
                    // 只允许选择文件，可选。
                    accept: {
                        title: 'Images',
                        extensions: 'gif,jpg,jpeg,bmp,png',
                        mimeTypes: 'image/*'
                    },
                    fileSizeLimit: 200 * 1024 * 1024,    // 200 M
                    fileSingleSizeLimit: 50 * 1024 * 1024    // 50 M
                });

                //图片加入时
                uploader.on('fileQueued', function (file) {
                    uploader.makeThumb(file, function (error, src) {
                        if (!error) {
                            _this.find("img").attr("src", src);
                        }
                    }, 100, 100);
                    _progress.show();
                    _progress.css("display", "block");
                });

                //文件上传成功
                uploader.on('uploadSuccess', function (file, result) {
                    if (result.state == "SUCCESS") {
                        _this.find("input[type=hidden]").val(result.filePath);
                        callback && callback(result.filePath);
                    }
                });
                //图片进度更新
                uploader.on('uploadProgress', function (file, percentage) {
                    var val = (percentage * 100).toFixed(0);
                    _progress.find("div:eq(0)").html(val + '%');
                    _progress.find("div div").css('width', val + '%');
                });
                //图片完成
                uploader.on('uploadComplete', function (file) {
                    _progress.find("div:eq(0)").html('0%');
                    _progress.find("div div").css('width', '0%');
                    _progress.hide();
                });
            }
            return this.each(function () {
                var _this = $(this), _progress = $(".uploader_progress");
                InitUploader(_this, _progress);
            });
        }
    });
})(jQuery);