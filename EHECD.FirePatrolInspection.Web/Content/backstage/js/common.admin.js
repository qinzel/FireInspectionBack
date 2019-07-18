//注册ehecd
window.ehecd = {};

//弹出信息窗口 title:标题 msgString:提示信息 msgType:信息类型 [error,info,question,warning]
function msgShow(title, msgString, msgType) {
    $.messager.alert(title, msgString, msgType);
    $(".messager-window").css("z-index", "99999");
}

function msgAlert(message,callback) {
    $.messager.alert('提示', '<span style="text-align:center"">' + message + '</span>', 'warning', callback && callback());
}

function Message(message,callback) {
    $.messager.alert('提示', '<span style="text-align:center"">' + message + '</span>', 'info', callback && callback());
}

//获取地址栏参数
function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return '';
}

function clockon() {
    var now = new Date();
    var year = now.getFullYear(); //getFullYear getYear
    var month = now.getMonth();
    var date = now.getDate();
    var day = now.getDay();
    var hour = now.getHours();
    var minu = now.getMinutes();
    var sec = now.getSeconds();
    var week;
    month = month + 1;
    if (month < 10) month = "0" + month;
    if (date < 10) date = "0" + date;
    if (hour < 10) hour = "0" + hour;
    if (minu < 10) minu = "0" + minu;
    if (sec < 10) sec = "0" + sec;
    var arr_week = new Array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");
    week = arr_week[day];
    var time = "";
    time = year + "年" + month + "月" + date + "日" + " " + hour + ":" + minu + ":" + sec + " " + week;

    $("#bgclock").html(time);

    var timer = setTimeout("clockon()", 200);
}


//输入框相同验证
$.extend($.fn.validatebox.defaults.rules, {
    equals: {
        validator: function (value, param) {
            return value == $(param[0]).val();
        },
        message: 'Field do not match.'
    }
});

$.extend($.fn.validatebox.defaults.rules, {
    maxLength: {
        validator: function (value, param) {
            return param[0] >= value.length;
        },
        message: '最多输入{0}位字符.'
    }
});

//价格验证
$.extend($.fn.validatebox.defaults.rules, {
    price: {
        validator: function (value, param) {//
            var test = /^[0-9]+(.[0-9]{1,2})?$/;
            return test.test(value) && value > 0;
        },
        message: '请输入正确的金额(两位小数,并且大于0).'
    }
});

//正整数验证
$.extend($.fn.validatebox.defaults.rules, {
    num: {
        validator: function (value, param) {//
            var test = /^[0-9]*[1-9][0-9]*$/;
            return test.test(value);
        },
        message: '请输入正整数.'
    }
});

//0和正整数验证
$.extend($.fn.validatebox.defaults.rules, {
    num0: {
        validator: function (value, param) {//
            var test = /^\d+$/;
            return test.test(value);
        },
        message: '请输入正整数.'
    }
});

//中奖几率验证^[0-9]*$
$.extend($.fn.validatebox.defaults.rules, {
    percent: {
        validator: function (value, param) {//
            var test = /^[0-9]*$/;
            return test.test(value) && value <= 100;

        },
        message: '请输入正确的中奖几率(0到100之间).'
    }
});

//活动题数
$.extend($.fn.validatebox.defaults.rules, {
    qustionnumber: {
        validator: function (value, param) {//
            var test = /^[0-9]*$/;
            return test.test(value) && value <= 100 && value > 0;

        },
        message: '请输入正确的题数(1到100之间).'
    }
});

//手机验证
$.extend($.fn.validatebox.defaults.rules, {
    telephone: {
        validator: function (value, param) {//
            //return !!value.match(/^((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)$/);
            //return !!value.match(/^1[3,5]{1}[0-9]{1}[0-9]{8}$ /);
            var test = /^1[3,4,5,6,7,8,9]{1}[0-9]{1}[0-9]{8}$/;
            return test.test(value);
        },
        message: '请输入正确的手机号.'
    }
});

//手机和座机验证
$.extend($.fn.validatebox.defaults.rules, {
    phone: {
        validator: function (value, param) {//
            var test = /^[0-9]*[1-9][0-9]*$/;
            return test.test(value);
        },
        message: '请输入正确的电话号码.'
    }
});

//URL验证
$.extend($.fn.validatebox.defaults.rules, {
    url: {
        validator: function (value, param) {//
            var test = /(http|https):\/\/([\w.]+\/?)\S*/;
            return test.test(value);
        },
        message: '请输入正确的链接地址（以http或https开头）.'
    }
});



$.extend($.fn.validatebox.defaults.rules, {
    checkHtml: {
        validator: function (value, param) {
            if (value.indexOf('<input') != -1 || value.indexOf('<select') != -1)
                return false;
            if (!/<[^>]+>/g.test(value)) {
                if (value.replace(/(^\s*)|(\s*$)/g, "").length == 0) {
                    return false;
                }
                return true;
            }
            var re = new RegExp("<[^>]+>", "ig");
            while (r = re.exec(value)) {
                var str = r[0].replace(/\<|\>|\"|\'|\&/g, "").replace("/","").replace(/(^\s*)|(\s*$)/g, "");
                if (/^[A-Za-z/]+$/g.test(str)) {
                    return false;
                }
            }
            return true;
        },
        message: '请不要输入非法字符或纯空格'
    }
});



//将表单数据格式化为easyui dg 查询参数
//用法$('#dg').datagrid({ queryParams: $("#searchForm").serializeArray().toFormObject() });   
//searchForm是form表单ID
Array.prototype.toFormObject = function () {
    var obj = {};
    for (var i = 0; i < this.length; i++) {
        var key = this[i]["name"];
        var value = this[i]["value"];
        obj[key] = value;
    }
    return obj;
}

//检查字符串是包含html代码
String.prototype.CheckHtml= function() {
    if (this.indexOf('<input') != -1)
        return false;
    var reg = new RegExp('^<([^>\s]+)[^>]*>(.*?<\/\\1>)?$');
    return !reg.test(this);
}


//时间格式化
Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}




$(function () {
    //ajax全局错误
    $(document).ajaxError(function (event, jqXHR, options, errorMsg) {
        switch (jqXHR.status) {
            case (500):
                //服务器系统内部错误
                msgShow("系统提示", "服务不可用", "error");
                break;
            case (401):
                msgShow("系统提示", "未登录", "error");
                break;
            case (403):
                msgShow("系统提示", "无权限执行此操作", "error");
                break;
            case (408):
                msgShow("系统提示", "请求超时", "error");
                break;
            default:
                msgShow("系统提示", "网络错误", "error");
        }
    });
});
