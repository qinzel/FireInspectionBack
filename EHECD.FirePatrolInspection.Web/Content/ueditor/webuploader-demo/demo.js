/* 上传图片 */
function UploadImage(target, domContainer, imagelist) {
    this.imageList = imagelist || [];
    this.container = domContainer;
    this.target = target.constructor == String ? '#' + target : '#' + $(target).attr("id");
    this.$wrap = target.constructor == String ? $('#' + target) : $(target);
    this.init();
}

UploadImage.prototype = {
    init: function () {
        this.imageList = [];
        this.initContainer();
        this.initUploader();
    },
    initContainer: function () {
        this.$queue = $('<ul class="filelist"></ul>').appendTo(this.$wrap.find('.queueList'));
    },
    /* 初始化容器 */
    initUploader: function () {
        var _this = this,
            $ = jQuery, // just in case. Make sure it's not an other libaray.
            $wrap = _this.$wrap,
            // 图片容器
            $queue = $wrap.find('.filelist'),
            // 状态栏，包括进度和控制按钮
            $statusBar = $wrap.find('.statusBar'),

            // 文件总体选择信息。
            $info = $statusBar.find('.info'),

            // 上传按钮
            $upload = $wrap.find('.uploadBtn'),

            // 没选择文件之前的内容。
            $placeHolder = $wrap.find('.placeholder'),

            // 总体进度条
            $progress = $statusBar.find('.progress').hide(),

            // 添加的文件数量
            fileCount = 0,

            // 添加的文件总大小
            fileSize = 0,

            //设置文件的总数量
            fileLimitCount = parseInt($statusBar.find(".fileLimitCount").val());

        // 优化retina, 在retina下这个值是2
        ratio = window.devicePixelRatio || 1,

        // 缩略图大小
        thumbnailWidth = 110 * ratio,
        thumbnailHeight = 110 * ratio,

        // 可能有pedding, ready, uploading, confirm, done.
        state = 'pedding',

        // 所有文件的进度信息，key为file id
        percentages = {},

        supportTransition = (function () {
            var s = document.createElement('p').style,
                r = 'transition' in s ||
                      'WebkitTransition' in s ||
                      'MozTransition' in s ||
                      'msTransition' in s ||
                      'OTransition' in s;
            s = null;
            return r;
        })(),

        // WebUploader实例
        uploader;

        if (!WebUploader.Uploader.support()) {
            alert('Web Uploader 不支持您的浏览器！如果你使用的是IE浏览器，请尝试升级 flash 播放器');
            throw new Error('WebUploader does not support the browser you are using.');
        }

        // 实例化
        uploader = WebUploader.create({
            pick: {
                id: '#filePicker',
                label: '点击选择图片'
            },
            //拖拽容器
            dnd: _this.target + ' .queueList',
            paste: document.body,

            auto: true,//有文件自动上传
            duplicate: true,//不去重
            chunked: true,//[默认值：false] 是否要分片处理大文件上传
            chunkSize: 5242880,// {Boolean} [可选] [默认值：5242880] 如果要分片，分多大一片？ 默认大小为5M.
            chunkRetry: 4,//{Boolean} [可选] [默认值：2] 如果某个分片由于网络问题出错，允许自动重传多少次？
            threads: 10,// {Boolean} [可选] [默认值：3] 上传并发数。允许同时最大上传进程数
            accept: {
                title: 'Images',
                extensions: 'gif,jpg,jpeg,bmp,png',
                mimeTypes: 'image/*'
            },

            // swf文件路径   
            swf: '/third-party/webuploader/Uploader.swf',
            disableGlobalDnd: true,
            chunked: true,
            server: 'http://115.28.48.78:8092/api/upload/controller?action=custom&module=goods&field=goods_picture&appKey=7GHMK4VF6PQE9T30L5O8W2UCA1SYIRXNJDZB&wh=100,100|200,200;300,300',
            fileNumLimit: fileLimitCount,
            fileSizeLimit: 5 * 1024 * 1024,    // 200 M
            fileSingleSizeLimit: 1 * 1024 * 1024    // 50 M
        });

        // 添加“添加文件”的按钮
        uploader.addButton({
            id: '#filePicker2',
            label: "继续添加"
        });

        // 当有文件添加进来时执行，负责view的创建
        function addFile(file) {
            var $li = $('<li id="' + file.id + '">' +
                    '<p class="title">' + file.name + '</p>' +
                    '<p class="imgWrap"></p>' +
                    '<p class="progress"><span></span></p>' +
                    '</li>'),

                $btns = $('<div class="file-panel">' +
                    '<span class="cancel">删除</span>' +
                    '<span class="rotateRight">向右旋转</span>' +
                    '<span class="rotateLeft">向左旋转</span></div>').appendTo($li),
                $prgress = $li.find('p.progress span'),
                $wrap = $li.find('p.imgWrap'),
                $error = $('<p class="error"></p>').hide().appendTo($li),

                showError = function (code) {
                    switch (code) {
                        case 'exceed_size':
                            text = '文件大小超出';
                            break;

                        case 'interrupt':
                            text = '上传暂停';
                            break;
                        case 'http':
                            text = "网络出错";
                            break;
                        case 'not_allow_type':
                            text = "文件类别不匹配";
                            break;
                        default:
                            text = '上传失败，请重试';
                            break;
                    }

                    $error.text(text).show();
                };

            if (file.getStatus() === 'invalid') {
                showError(file.statusText);
            } else {
                // @todo lazyload
                $wrap.text('预览中');
                uploader.makeThumb(file, function (error, src) {
                    if (error) {
                        $wrap.text('不能预览');
                        return;
                    }

                    var img = $('<img src="' + src + '">');
                    $wrap.empty().append(img);
                }, thumbnailWidth, thumbnailHeight);

                percentages[file.id] = [file.size, 0];
                file.rotation = 0;
            }

            file.on('statuschange', function (cur, prev) {
                if (prev === 'progress') {
                    $prgress.hide().width(0);
                } else if (prev === 'queued') {
                    //隐藏旋转和删除
                    //$li.off( 'mouseenter mouseleave' );
                    $li.find(".file-panel").hide().find("*").hide();
                    //$btns.remove();
                    $btns.hide();
                }

                // 成功
                if (cur === 'error' || cur === 'invalid') {
                    console.log(file.statusText);
                    showError(file.statusText);
                    percentages[file.id][1] = 1;
                } else if (cur === 'interrupt') {
                    showError('interrupt');
                } else if (cur === 'queued') {
                    percentages[file.id][1] = 0;
                } else if (cur === 'progress') {
                    $info.remove();
                    $prgress.css('display', 'block');
                } else if (cur === 'complete') {
                    $li.find(".file-panel").show().find(".cancel").show();
                    $btns.show();
                    //$li.append('<span class="success"></span>');
                }

                $li.removeClass('state-' + prev).addClass('state-' + cur);
            });



            $li.on('mouseenter', function () {
                $btns.stop().animate({ height: 30 });
            });

            $li.on('mouseleave', function () {
                $btns.stop().animate({ height: 0 });
            });

            $btns.on('click', 'span', function () {
                var index = $(this).index(),
                    deg;

                switch (index) {
                    case 0:
                        uploader.removeFile(file);
                        return;

                    case 1:
                        file.rotation += 90;
                        break;

                    case 2:
                        file.rotation -= 90;
                        break;
                }

                if (supportTransition) {
                    deg = 'rotate(' + file.rotation + 'deg)';
                    $wrap.css({
                        '-webkit-transform': deg,
                        '-mos-transform': deg,
                        '-o-transform': deg,
                        'transform': deg
                    });
                } else {
                    $wrap.css('filter', 'progid:DXImageTransform.Microsoft.BasicImage(rotation=' + (~~((file.rotation / 90) % 4 + 4) % 4) + ')');
                    // use jquery animate to rotation
                    // $({
                    //     rotation: rotation
                    // }).animate({
                    //     rotation: file.rotation
                    // }, {
                    //     easing: 'linear',
                    //     step: function( now ) {
                    //         now = now * Math.PI / 180;

                    //         var cos = Math.cos( now ),
                    //             sin = Math.sin( now );

                    //         $wrap.css( 'filter', "progid:DXImageTransform.Microsoft.Matrix(M11=" + cos + ",M12=" + (-sin) + ",M21=" + sin + ",M22=" + cos + ",SizingMethod='auto expand')");
                    //     }
                    // });
                }


            });

            $li.appendTo($queue);
        }

        // 负责view的销毁
        function removeFile(file) {

            var $li = $('#' + file.id);

            delete percentages[file.id];
            updateTotalProgress();
            $li.off().find('.file-panel').off().end().remove();
        }

        function updateTotalProgress() {
            var loaded = 0,
                total = 0,
                spans = $progress.children(),
                percent;

            $.each(percentages, function (k, v) {
                total += v[0];
                loaded += v[0] * v[1];
            });

            percent = total ? loaded / total : 0;

            spans.eq(0).text(Math.round(percent * 100) + '%');
            spans.eq(1).css('width', Math.round(percent * 100) + '%');
            updateStatus();
        }

        function updateStatus() {
            var text = '', stats;

            if (state === 'ready') {
                text = '选中' + fileCount + '张图片，共' +
                        WebUploader.formatSize(fileSize) + '。';
            } else if (state === 'confirm') {
                stats = uploader.getStats();
                if (stats.uploadFailNum) {
                    text = '已成功上传' + stats.successNum + '张照片至XX相册，' +
                        stats.uploadFailNum + '张照片上传失败，<a class="retry" href="#">重新上传</a>失败图片或<a class="ignore" href="#">忽略</a>'
                }

            } else {
                stats = uploader.getStats();
                text = '共' + fileCount + '张（' +
                        WebUploader.formatSize(fileSize) +
                        '），已上传' + stats.successNum + '张';

                if (stats.uploadFailNum) {
                    text += '，失败' + stats.uploadFailNum + '张';
                }
            }

            $info.html(text);
        }

        function setState(val) {
            var file, stats;

            if (val === state) {
                return;
            }

            $upload.removeClass('state-' + state);
            $upload.addClass('state-' + val);
            state = val;

            switch (state) {
                case 'pedding':
                    $placeHolder.removeClass('element-invisible');
                    $queue.parent().removeClass('filled');
                    $queue.hide();
                    $statusBar.addClass('element-invisible');
                    uploader.refresh();
                    break;

                case 'ready':
                    $placeHolder.addClass('element-invisible');
                    $('#filePicker2').removeClass('element-invisible');
                    $queue.parent().addClass('filled');
                    $queue.show();
                    $statusBar.removeClass('element-invisible');
                    uploader.refresh();
                    break;

                case 'uploading':
                    $('#filePicker2').addClass('element-invisible');
                    $progress.show();
                    $upload.text('暂停上传');
                    break;

                case 'paused':
                    $progress.show();
                    $upload.text('继续上传');
                    break;

                case 'confirm':
                    $progress.hide();
                    $upload.text('开始上传').addClass('disabled');

                    stats = uploader.getStats();
                    if (stats.successNum && !stats.uploadFailNum) {
                        setState('finish');
                        return;
                    }
                    break;
                case 'finish':
                    stats = uploader.getStats();
                    if (stats.successNum) {
                        $placeHolder.addClass('element-invisible');
                        $('#filePicker2').removeClass('element-invisible');
                        $queue.parent().addClass('filled');
                        $queue.show();
                        $statusBar.removeClass('element-invisible');
                        uploader.refresh();
                        $upload.removeClass("disabled");
                        $("div.message").html('上传成功');
                    } else {
                        // 没有成功的图片，重设
                        state = 'done';
                        location.reload();
                    }
                    break;
            }

            updateStatus();
        }

        uploader.onUploadProgress = function (file, percentage) {
            var $li = $('#' + file.id),
                $percent = $li.find('.progress span');

            $percent.css('width', percentage * 100 + '%');
            percentages[file.id][1] = percentage;
            updateTotalProgress();
        };

        uploader.onFileQueued = function (file) {
            fileCount++;
            fileSize += file.size;

            if (fileCount === 1) {
                $placeHolder.addClass('element-invisible');
                $statusBar.show();
            }

            addFile(file);
            setState('ready');
            updateTotalProgress();
        };

        uploader.onFileDequeued = function (file) {
            fileCount--;
            fileSize -= file.size;

            if (!fileCount) {
                setState('pedding');
            }

            removeFile(file);
            updateTotalProgress();

        };

        uploader.on('uploadSuccess', function (file, ret) {
            console.log('load success');
            var $file = $('#' + file.id);
            try {
                var responseText = (ret._raw || ret),
                    json = eval('(' + responseText + ')');
                if (json.state == 'SUCCESS') {
                    _this.imageList.push(json);
                    $file.append('<span class="success"></span>');
                } else {
                    $file.find(".error").text(json.state).show();
                }
            } catch (e) {
                $file.find(".error").text(json.state).show();
            }
        });

        uploader.on('uploadError', function (file, code) {
        });

        //图片上传数量判断
        uploader.on('beforeFileQueued', function (file) {
            if (fileCount >= fileLimitCount) {
                alert("文件数量超过限制！");
                return false;
            }
        });

        uploader.on('error', function (code, file) {
            if (code == 'Q_TYPE_DENIED' || code == 'F_EXCEED_SIZE') {
                //addFile(file);
                console.log('add error file');
            }

            if (code == "Q_EXCEED_NUM_LIMIT") {
                alert("只能上传" + fileLimitCount + "张图片");
            }

        });
        uploader.on('uploadComplete', function (file, ret) {
            console.log('load complete');
        });

        uploader.on('all', function (type) {
            var stats;
            switch (type) {
                case 'uploadFinished':
                    setState('confirm');
                    break;

                case 'startUpload':
                    setState('uploading');
                    break;

                case 'stopUpload':
                    setState('paused');
                    break;

            }
        });

        uploader.onError = function (code) {
            console.log('Eroor: ' + code);
        };

        $upload.on('click', function () {
            if ($(this).hasClass('disabled')) {
                return false;
            }

            if (state === 'ready') {
                uploader.upload();
            } else if (state === 'paused') {
                uploader.upload();
            } else if (state === 'uploading') {
                uploader.stop();
            }
        });

        $info.on('click', '.retry', function () {
            uploader.retry();
        });

        $info.on('click', '.ignore', function () {
            console.log('todo');
        });

        $upload.addClass('state-' + state);
        updateTotalProgress();
    },
    getQueueCount: function () {
        var file, i, status, readyFile = 0, files = this.uploader.getFiles();
        for (i = 0; file = files[i++];) {
            status = file.getStatus();
            if (status == 'queued' || status == 'uploading' || status == 'progress') readyFile++;
        }
        return readyFile;
    },
    destroy: function () {
        this.$wrap.remove();
    },
    getInsertList: function () {
        var i, data, list = [],
            align = getAlign(),
            prefix = editor.getOpt('imageUrlPrefix');
        for (i = 0; i < this.imageList.length; i++) {
            data = this.imageList[i];
            list.push({
                src: prefix + data.url,
                _src: prefix + data.url,
                title: data.title,
                alt: data.original,
                floatStyle: align
            });
        }
        return list;
    }
};



