
//数组检测某元素的索引值
Array.prototype.indexOf = function (el) {
    for (var i = 0, n = this.length; i < n; i++) {
        if (this[i] === el) {
            return i;
        }
    }
    return -1;
}


//ajax封装
//var host = "http://119.23.78.210";
var host = "http://fire.server190.ehecd.com";
//var host = "http://localhost:2368";
function go_ajax(param, callBack) {
    var url = param.url.indexOf("http") >= 0 ? param.url : host + param.url
    $.ajax({
        url: url,
        type: (param.type || "POST"),
        contentType: (param.contentType || "application/x-www-form-urlencoded"),
        dataType: "json",
        async: param.async === false ? false : true,
        data: $.extend(param.data, !param.noToken ? { appKey: 'NhTnHhEJc5ZCJM9FfmlCINZDvDfyPzAN', appSecret: 'EpD70oATq6d7TgUoOHkbODQjVg7CUGaWw7thptHPt3KMWLcEN8rWD6V7RjP5fz7a', timestamp: new Date().valueOf() } : { token: localStorage.token || "" }),//设置是否需要传token
        success: function (res) {
            if (res.code >= 0) {
                callBack && callBack();
                param.success && param.success(res);

            } else {
                go_alert2(res.msg)
            }

        },
        error: function () {
            param.error && param.error()

        }
    })

};

//表单序列化
function getFormData(form) {
    var string = form.serialize();
    var item_ary = string.split("&");
    item_ary.forEach(function (item, index, ary) {
        ary[index] = item.split("=");

    })
    var data = {};
    item_ary.forEach(function (item, index, ary) {

        if (!data[ary[index][0]] && ary[index][1]) {
            data[ary[index][0]] = decodeURIComponent(ary[index][1]);


        } else if (data[ary[index][0]]) {

            if (data[ary[index][0]] instanceof Array) {
                data[ary[index][0]].push(decodeURIComponent(ary[index][1]));
            } else {
                data[ary[index][0]] = [data[ary[index][0]]];
                data[ary[index][0]].push(decodeURIComponent(ary[index][1]));
            }
        }

    })

    return data

}


/***VUE公共封装***/
var app = {};
var ini_data = {};
var watchjson = {};
function ini() {

    var valname = arguments[0].split("=")[0].split("[]")[0];
    if (arguments[0].indexOf('[]') < 0) {

        var val = arguments[0].split("=")[1] ? arguments[0].split("=")[1] : 0;
        ini_data[valname] = +val ? +val : val;

    } else if (arguments[0].indexOf('[]') >= 0) {

        ini_data[valname] = eval(arguments[0].split("=")[1]) || [];

        if (arguments[1] instanceof Array) {
            //建立数组和数组选中真值之间的映射
            ini_data[valname + "_map"] = {};
            arguments[1].unshift(valname);
            console.log(arguments[1]);
            watchjson[valname] = function () {
                var that = this;
                var item_ary;
                arguments[1].forEach(function (item, index, ary) {
                    item_ary


                })
                that[valname].forEach(function (item, index, ary) {
                    that[valname + "_map"][index] = { checked: false }




                })

            }

        }

    }


}


$("[ini]").each(function () {

    var vue_string = $(this).attr("ini");
    eval(vue_string);
    $(this)[0].removeAttribute('ini');

});
function vue_init(json) {
    var vue_params = {
        el: ".main",
        data: $.extend(ini_data, {
            ajaxFunc: {},//用于保存每个操作函数的相关ajax请求参数
            goAjax: function (json) {

                var that = this;
                var funcName = json.funcString.toString().split("(")[0].split("function")[1].trim();//获取函数名字
                var data = {};

                if (json.ajaxKey) {
                    that.ajaxFunc[funcName][json.ajaxKey].data.forEach(function (item, index, ary) {
                        data[item] = that[item]
                    })
                    go_ajax($.extend({}, that.ajaxFunc[funcName][json.ajaxKey], { data: data }), json.dothing)
                } else {
                    that.ajaxFunc[funcName].data.forEach(function (item, index, ary) {
                        data[item] = that[item]
                    })

                    go_ajax($.extend({}, that.ajaxFunc[funcName], { data: data }), json.dothing)
                }

            }

        }),
        methods: {
            info: function (item) {
                console.log(item);
                return item
            },
            GetUrlparam: function (name) {
                var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
                var r = decodeURI(window.location.search).substr(1).match(reg);
                if (r != null) return unescape(r[2]);
                return null;

            },
            max: function max() {
                var ajaxKey = arguments[1];
                var expression = arguments[0].split(/[><=]{1,2}/);
                //判断该函数需要请求接口
                if (ajaxKey && this.ajaxFunc["max"]) {
                    if (expression.length == 2) {

                        if (eval(+this[expression[0]] + 1 + arguments[0].match(/[><=]{1,2}/) + (this[expression[1]] || expression[1]))) {

                            this.goAjax({
                                funcString: arguments.callee,
                                dothing: function () {
                                    Vue.set(this, expression[0], this[expression[0]] + 1)
                                },
                                ajaxKey: ajaxKey
                            })
                        }


                    } else if (expression.length == 1) {
                        this.goAjax({
                            funcString: arguments.callee,
                            dothing: function () {
                                Vue.set(this, expression[0], this[expression[0]] + 1)
                            },
                            ajaxKey: ajaxKey
                        })

                    };


                } else {
                    if (expression.length == 2) {
                        if (eval(eval("+this." + expression[0]) + 1 + arguments[0].match(/[><=]{1,2}/) + (this[expression[1]] || expression[1]))) {
                            Vue.set(this, expression[0], eval("+this." + expression[0]) + 1)
                        }

                    } else if (expression.length == 1) {
                        Vue.set(this, expression[0], this[expression[0]] + 1)

                    };

                }

            },
            min: function min() {
                var ajaxKey = arguments[1];
                var expression = arguments[0].split(/[><=]{1,2}/);
                //判断该函数需要请求接口
                if (ajaxKey && this.ajaxFunc["min"]) {

                    if (expression.length == 2) {

                        if (eval(+this[expression[0]] - 1 + arguments[0].match(/[><=]{1,2}/) + (this[expression[1]] || expression[1]))) {

                            this.goAjax({
                                funcString: arguments.callee,
                                dothing: function () {
                                    Vue.set(this, expression[0], this[expression[0]] - 1)
                                },
                                ajaxKey: ajaxKey
                            })
                        }


                    } else if (expression.length == 1) {
                        this.goAjax({
                            funcString: arguments.callee,
                            dothing: function () {
                                Vue.set(this, expression[0], this[expression[0]] - 1)
                            },
                            ajaxKey: ajaxKey
                        })

                    };


                } else {

                    if (expression.length == 2) {

                        if (eval(+this[expression[0]] - 1 + arguments[0].match(/[><=]{1,2}/) + (this[expression[1]] || expression[1]))) {
                            Vue.set(this, expression[0], this[expression[0]] - 1)
                        }


                    } else if (expression.length == 1) {
                        Vue.set(this, expression[0], this[expression[0]] - 1)

                    };

                }

            },
            setEach: function (string, val_ary) {
                var target = string.split(".");
                var i = 0;
                function runEnd(i) {
                    if (target[i].test("/\S*[]/")) {



                    } else if (target[i].test("/\S*{}/")) {




                    } else if (target[i].test("/[\S*]|\S[^[]]*/")) {


                    }



                }


                target.forEach(function (item, index, ary) {
                    //			        	if(item.test())



                })
            }
			,
            compute: function (string) {
                var expression = string.split("=");
                var computeAry = expression[1].split(/([a-zA-Z0-9_-]+)/);


            }


        },
        watch: $.extend({}, watchjson)


    };
    for (var key in json) {

        if (key != "data") {
            if (typeof vue_params[key] == "object") {
                $.extend(vue_params[key], json[key])
            } else {
                vue_params[key] = json[key]

            }


        } else {
            $.extend(vue_params.data, json.data)
        }
    };
    if (vue_params.ajaxUrl) {

        for (item in vue_params.ajaxUrl) {
            vue_params.data.ajaxFunc[item] = vue_params.ajaxUrl[item];

        }


    }

    app = new Vue(vue_params);

}

var back = function () {
    var u = navigator.userAgent;
    var isAndroid = u.indexOf('Android') > -1 || u.indexOf('Adr') > -1; //android终端
    var isiOS = !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/); //ios终端
    if (isAndroid) {
        WebViewJavascriptBridge.pageback();
    }
    if (isiOS) {
        //history.back();
        webkit.messageHandlers.pageback.postMessage("");
        //location.href="back.html"
    }
}

// 后退按钮
var goBack = function () {
    $("#backBtn").click(function () {
        goBackFunc();
    })
}
function goBackFunc() {
    var u = navigator.userAgent;
    var isAndroid = u.indexOf('Android') > -1 || u.indexOf('Adr') > -1; //android终端
    var isiOS = !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/); //ios终端
    if (isAndroid) {
        WebViewJavascriptBridge.pageback();
    }
    if (isiOS) {
        //history.back();
        webkit.messageHandlers.pageback.postMessage("");
        //location.href="back.html"
    }
}
//合并ajax数据
function editData(json, string) {
    var vueData = eval(string || "app");
    for (key in json) {
        Vue.set(vueData, key, json[key])
    }
}
    //app上传图片方法
function updateimg(count) {
    var u = navigator.userAgent;
    var isAndroid = u.indexOf('Android') > -1 || u.indexOf('Adr') > -1; //android终端
    var isiOS = !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/); //ios终端
    if (isAndroid) {
        WebViewJavascriptBridge.updateimg(count);
    }
    if (isiOS) {
        webkit.messageHandlers.updateimg.postMessage(count);
    }

};
//传递用户ID
function sendId(id) {
    var u = navigator.userAgent;
    var isAndroid = u.indexOf('Android') > -1 || u.indexOf('Adr') > -1; //android终端
    var isiOS = !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/); //ios终端
    if (isAndroid) {
        WebViewJavascriptBridge.sendId(JSON.stringify(id));
        
    }
    if (isiOS) {
        webkit.messageHandlers.sendId.postMessage(id);
    }
};
//退出登录
function exitApp() {
    var u = navigator.userAgent;
    var isAndroid = u.indexOf('Android') > -1 || u.indexOf('Adr') > -1; //android终端
    var isiOS = !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/); //ios终端
    if (isAndroid) {
        WebViewJavascriptBridge.exitApp();
    }
    if (isiOS) {
        webkit.messageHandlers.exitApp.postMessage("123");
    }
};
//获取个人信息
function getUserInfo() {
    if (!localStorage.iClient) {
        location.href = 'Login.html';
        return false;
    }
    go_ajax({
        url: '/router?method=client.get',
        data: { iClientID: JSON.parse(localStorage.iClient).ID },
        success: function (res) {
            if (res.success) {
                if (!res.data) {
                    go_alert2("用户不存在", function () {
                        location.href = 'Login.html';
                    });
                    return false;
                }
                if (res.data.iStatus) {
                    go_alert2("用户已被冻结", function () {
                        location.href = 'Login.html';
                    });
                    return false;
                }
            }
        }
    })
}
//返回刷新

var isPageHide = false;
window.addEventListener('pageshow', function () {
    if (isPageHide) {
        window.location.reload();
    }
});
window.addEventListener('pagehide', function () {
    isPageHide = true;
});