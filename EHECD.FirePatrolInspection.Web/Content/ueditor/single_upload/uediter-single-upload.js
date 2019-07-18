﻿/**
 * 使用前参数初始化  @Html.Action("FilesInit", "Uediter", new { area = "", module = "goods", field = "goods", wh = "500,500|100,100" });
 *    引入插件地址  <script src="/Scripts/ueditor/single_upload/uediter-single-upload.js"></script>
 * 
 * 图片上传初始化
 * var selectImages = UE.ehecd_upload_image_init(id,images,maxNum);
 * @param  string[字符串]  id          附加元素id[不带#] 
 * @param  array [数组]    images      已有图片
 * @param  int             maxNum      最大上传张数
 *     demo   UE.ehecd_upload_image_init('images-select-id',[],maxNum);
 *            UE.ehecd_upload_image_init('images-select-id',['http://dddddd','http://dddddd'],55);
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
; (function (_editor) {
    //图片上传初始化
    _editor.ehecd_upload_image_init = function (id, selected, maxNum) {//maxNum 最大上传张数
        maxNum = maxNum || 5; //最大上传张数默认5张图片
        var _html = '<ul id="c-' + id + '" class="ue-select-image-list"><ul>';
        $('#' + id).html(_html);
        var cSelectImage = $('#c-' + id);
        cSelectImage.html('<li><p id="btn-' + id + '" class="btn-ue-upimage-toserver"></p></li>');
        function randerSelectImages(src) {
            return '<li><div style="height:30px"><p class="icon-remove" style="height:15px;"></p></div><img src="' + src + '" /></li>';
        }
        var _num = 0;//当前上传张数
        if (selected && selected.length > 0) {//初始值
            selected.length <= maxNum && (_num += selected.length);
            _html = ''; for (var i = 0; i < selected.length; i++) { _html += randerSelectImages(selected[i]); }
            cSelectImage.append(_html); bindEvent();
        }
        var btnSelect = $('#btn-' + id);
        var selectImg;
        btnSelect.click(function () {
            _editor.SingleUpLoadImage(function (selectImages) {
                _html = '';
                if (selectImages && selectImages.length) {
                    for (var i = 0; i < selectImages.length; i++) {
                        if (_num >= maxNum) { break; }//当前上传张数不能超过最大上传张数 
                        selectImg = Urifrmate(selectImages[i].src);
                        selected.push(selectImg);
                        _html += randerSelectImages(selectImg);
                        ++_num;
                    }
                    cSelectImage.append(_html); bindEvent();
                }
            });
        });
        function Urifrmate(uri) { return (uri.indexOf("-") > 0 ? uri.substring(0, uri.indexOf("-")) + uri.substring(uri.lastIndexOf("."), uri.length) : uri); }
        function bindEvent() {
            cSelectImage.find('li').mouseenter(function () { $(this).find('div').show(); }).mouseleave(function () { $(this).find('div').hide(); });
            cSelectImage.find('div').unbind('click').bind('click', function () {
                var li = $(this).parent(); selected.splice(jQuery.inArray(li.find('img').attr('src'), selected), 1); li.remove(); --_num;
            });
        }
        return { get_imgs: function () { return selected; } }
    };
    //附件上传初始化
    _editor.ehecd_upload_file_init = function (id, files, btnTxt) {
        var _html = '<ul id="c-file-' + id + '" class="file-ue-attachment-list"><ul>';
        $('#' + id).html(_html);
        var cSelectFile = $('#c-file-' + id);
        cSelectFile.html('<li class="btn-file-ue-attachment"><div id="btn-file-' + id + '"><span class="ace-icon fa fa-cloud-upload"></span> ' + (btnTxt || '附件上传') + '</div></li>');
        function randerFiles(data) {
            return '<li><a href="' + data.url + '"><span class="ace-icon fa fa-download"></span> '
                + data.title + '</a> <i class="ace-icon glyphicon glyphicon-trash" ></i></li>';
        }
        if (files && files.length > 0) {//初始值
            _html = ''; for (var i = 0; i < files.length; i++) { _html += randerFiles(files[i]); }
            cSelectFile.append(_html); bindEvent();
        }
        var btnSelect = $('#btn-file-' + id);
        btnSelect.click(function () {
            _editor.SingleUpLoadFile(function (selectFiles) {
                _html = '';
                if (selectFiles && selectFiles.length) {
                    for (var i = 0; i < selectFiles.length; i++) { files.push(selectFiles[i]); _html += randerFiles(selectFiles[i]); }
                    cSelectFile.append(_html); bindEvent();
                }
            });
        });
        Array.prototype.remove = function (val) { var index = this.indexOf(val); if (index > -1) { this.splice(index, 1); } };
        function bindEvent() {
            cSelectFile.find('i').unbind('click').bind('click', function () {
                var li = $(this).parent(); var removeUrl = li.find('a').attr('href');
                for (var i = 0; i < files.length; i++) { if (removeUrl == files[i].url) { files.remove(files[i]); } } li.remove();
            });
        }
        return { get_files: function () { return files; } }
    }
})(UE);