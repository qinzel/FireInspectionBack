/**
 * @apiDescription 说明
 * appSecret：EpD70oATq6d7TgUoOHkbODQjVg7CUGaWw7thptHPt3KMWLcEN8rWD6V7RjP5fz7a
 * superToken: SIVB
 * 签名算法：将appKey、appSecret、timestamp、echostr排序后MD5加密
 */
 
/**
@apiDefine Article 文章
*/

/**
@apiDefine Banner 轮播
*/

/**
@apiDefine Client 用户
*/

/**
@apiDefine Sms 短信
*/

/**
@apiDefine Settings 基础设置
*/

/**
@apiDefine SiteMsg 站内信
*/

/**
@apiDefine Timeline 朋友圈
*/

/**
@apiDefine Comment 评论
*/

/**
@apiDefine Feedback 意见反馈
*/

/**
@apiDefine Unit 平台单位
*/

/**
@apiDefine Duty 值班
*/

/**
@apiDefine DutyRecord 值班记录
*/

/**
@apiDefine Device 设备
*/

/**
@apiDefine DeviceType 设备类型
*/

/**
@apiDefine Inspection 巡检
*/

/**
@apiDefine Repair 维修
*/

/**
@apiDefine Change 更换
*/
////////////////////////////////////////////////////////文章//////////////////////////////////////////////////////////////////

/**
@api {post} article.get 获取文章详情
@apiName article.get 
@apiGroup Article

@apiParam {int} iArticleID    文章ID

@apiParamExample {json} 请求样例
{
    "iArticleID": 1
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取文章详情成功",
    "data": {
        "ID": 2,        //主键ID
        "iType": 0,     //文章类别[0:法律知识,1:消防知识,2:资讯,3:帮助,4:关于我们]
        "sTitle": "平台协议",       //标题
        "sSortNumber": "1",       //排序序号
        "sImageSrc": "http://xxx",       //列表图片地址
        "sContent": "同签署本《跃米商城平台服务协议》（下称“本协议”）并使用跃米商城平台服务！",        //内容
        "dCreateTime": "2017-10-09T16:56:54.98",        //创建时间
        "bIsDeleted": false     //删除标识
    }
}
*/

/**
@api {post} article.about 获取关于我们
@apiName article.about 
@apiGroup Article 

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取关于我们成功",
    "data": {
        "ID": 2,        //主键ID
        "iType": 0,     //文章类别[0:法律知识,1:消防知识,2:资讯,3:帮助,4:关于我们]
        "sTitle": "关于我们",       //标题
        "sSortNumber": "1",       //排序序号
        "sImageSrc": "http://xxx",       //列表图片地址
        "sContent": "同签署本《跃米商城平台服务协议》（下称“本协议”）并使用跃米商城平台服务！",        //内容
        "dCreateTime": "2017-10-09T16:56:54.98",        //创建时间
        "bIsDeleted": false     //删除标识
    }
}
*/

/**
@api {post} article.getlist 获取文章列表
@apiName article.getlist 
@apiGroup Article

@apiParam {int} iType       文章类别[0:法律知识,1:消防知识,2:资讯,3:帮助,4:关于我们]
@apiParam {string} sTitle   文章标题
@apiParam {int} page        页码

@apiParamExample {json} 请求样例
{
    "iType": 0,
    "sTitle": "标题是我",
    "page": 1
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取文章列表成功",
    "data": [
        {
            "ID": 2,        //主键ID
            "iType": 0,     //文章类别[0:法律知识,1:消防知识,2:资讯,3:帮助,4:关于我们]
            "sTitle": "平台协议",       //标题
            "sSortNumber": "1",       //排序序号
            "sImageSrc": "http://xxx",       //列表图片地址
            "sContent": "同签署本《跃米商城平台服务协议》（下称“本协议”）并使用跃米商城平台服务！",        //内容
            "dCreateTime": "2017-10-09T16:56:54.98",        //创建时间
            "bIsDeleted": false     //删除标识
        }
    ]
}
*/

/**
@api {post} article.client.getlist 获取文章列表(不含文章内容)
@apiName article.client.getlist 
@apiGroup Article

@apiParam {string} lm        最后更新时间

@apiParamExample {json} 请求样例
{
    "lm": ''
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取文章列表成功",
    "data": {
        "IsModified": true,
        "IsAll": true,
        "Data": [
            {
                "ID": 4,
                "sTitle": "火灾的危害性",
                "sContent": null,
                "sSortNumber": "1",
                "sImageSrc": "http://115.28.48.78:8092/upload/7GHMK4VF6PQE9T30L5O8W2UCA1SYIRXNJL12/Article/Article/image/2018/07/02/6366614446185577937990468.jpg",
                "iType": 1,
                "dCreateTime": "2018-07-02T16:08:01.81",
                "bIsDeleted": false,
                "lastModifyTime": "2019-05-28T17:09:30.587"
            }
        ],
        "LastModifyTime": "2019/7/15 11:11:42"
    }
}
*/

////////////////////////////////////////////////////////文章//////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////轮播//////////////////////////////////////////////////////////////////

/**
@api {post} banner.getlist 获取轮播列表
@apiName banner.getlist 
@apiGroup Banner

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取轮播列表成功",
    "data": [
        {
            "ID": 2,        //主键ID
            "sImageSrc": "http://xxx",       //图片地址
            "sLink": "http://xxx",          //跳转地址
            "iArticleType": 0,              //轮播文章类别[0:无,1:法律知识,2:消防知识,3:资讯,4:帮助,5:关于我们]
            "dCreateTime": "2017-10-09T16:56:54.98",        //创建时间
            "bIsDeleted": false     //删除标识
        }
    ]
}
*/

/**
@api {post} banner.get 获取轮播详情(过时)
@apiName banner.get
@apiGroup Banner

@apiParam {int} iBannerID   轮播ID

@apiParamExample {json} 请求样例
{
    "iBannerID": 1
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取轮播详情成功",
    "data": {
        "ID": 2,        //主键ID
        "sImageSrc": "http://xxx",       //图片地址
        "sLink": "http://xxx",          //跳转地址
        "iArticleType": 0,              //轮播文章类别[0:无,1:法律知识,2:消防知识,3:资讯,4:帮助,5:关于我们]
        "dCreateTime": "2017-10-09T16:56:54.98",        //创建时间
        "bIsDeleted": false     //删除标识
    }
}
*/

////////////////////////////////////////////////////////轮播//////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////用户//////////////////////////////////////////////////////////////////

/**
@api {post} client.register 用户注册
@apiName client.register  
@apiGroup Client

@apiParam {string} sPhone   手机号
@apiParam {string} sValidate     验证码
@apiParam {string} sPwd     登录密码（MD5加密）
@apiParam {string} sName    姓名
@apiParam {string} sUnitIds     单位ID集
@apiParam {string} sDeptIds     部门ID集
@apiParam {string} sCredentials     证件照

@apiParamExample {json} 请求样例
{
    "sPhone": "13800138000",
    "sValidate": "1234",
    "sPwd": "QWERTYUIOPASDFGHJKLZXCVBNM",
    "sName": "张三",
    "sUnitIds": "1,2,3",
    "sDeptIds": "4,5,6",
    "sCredentials": "http://xxx,http://xxx"
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "用户注册成功",
    "data": 3
}
*/

/**
@api {post} client.login 用户登录
@apiName client.login  
@apiGroup Client

@apiParam {string} sPhone   手机号
@apiParam {string} sPwd     登录密码（MD5加密）

@apiParamExample {json} 请求样例
{
    "sPhone": "13800138000",
    "sPwd": "QWERTYUIOPASDFGHJKLZXCVBNM"
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "用户登录成功",
    "data": {
        "ID": 2,                    //用户ID
        "sPhone": "13800138000",    //手机号
        "sPwd": "",                 //登录密码
        "sName": "张三",              //姓名
        "sImageSrc": "http://xxx",  //头像
        "sCredentials": "http://xxx",//证件照
        "iStatus": false,           //用户状态[0:未冻结,1:已冻结]
        "iUnitID": 1,               //所属单位ID
        "iType": 0,         //用户类别[0:点检员,1:消防,2:维护公司,3:使用公司,4:值班人员]
        "bIsDeleted": false,        //删除标识[0:未删除,1:已删除]
        "dCreateTime": "2017-10-24T15:42:49.763"        //创建时间
    }
}
*/

/**
@api {post} client.logout 用户登出
@apiName client.logout  
@apiGroup Client

@apiParam {string} iClientID   用户ID
@apiParam {string} deviceID    设备标识

@apiParamExample {json} 请求样例
{
    "iClientID": "31",
    "deviceID": "QWERTYUIOPASDFGHJKLZXCVBNM"
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "用户登出成功",
    "data": {
    }
}
*/

/**
@api {post} client.get 获取用户信息
@apiName client.get  
@apiGroup Client

@apiParam {int} iClientID   前端用户ID

@apiParamExample {json} 请求样例
{
    "iClientID": 1
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取用户信息成功",
    "data": {
        "ID": 2,                    //用户ID
        "sPhone": "13800138000",    //手机号
        "sName": "张三",              //姓名
        "iStatus": false,           //用户状态[0:未冻结,1:已冻结]
        "bIsDeleted": false         //删除标识[0:未删除,1:已删除]
    }
}
*/

/**
@api {post} client.self 获取当前用户信息
@apiName client.self  
@apiGroup Client

@apiParam {int} iClientID   前端用户ID

@apiParamExample {json} 请求样例
{
    "iClientID": 1
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取用户信息成功",
    "data": {
        "ID": 2,                    //用户ID
        "sPhone": "13800138000",    //手机号
        "sName": "张三",              //姓名
        "iStatus": false,           //用户状态[0:未冻结,1:已冻结]
        "bIsDeleted": false,        //删除标识[0:未删除,1:已删除]
        "DeptList": [               //所属单位部门集合
            {
                "ID": 4,            //部门ID
                "iUseDeptID": 3,    //单位ID
                "sUnitName": "新能源即墨突然",//单位名称
                "iAuditState": 1,   //审核状态[0:未审核,1:已审核,2:已拒绝]
                "sName": "部门三", //部门名称
                "dCreateTime": "2018-06-22T15:35:25.447",
                "bIsDeleted": false
            }
        ]
    }
}
*/

/**
@api {post} client.editpwd 修改登录密码
@apiName client.editpwd  
@apiGroup Client 

@apiParam {int} iClientID       用户ID
@apiParam {string} sOldPwd      旧登录密码（MD5加密）
@apiParam {string} sPwd         新登录密码（MD5加密）

@apiParamExample {json} 请求样例
{
    "iClientID": 2,
    "sOldPwd": "QWERTYUIOPASDFGHJKLZXCVBNM",
    "sPwd": "QWERTYUIOPASDFGHJKLZXCVBNM"
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "密码修改成功",
    "data": null
}
*/

/**
@api {post} client.findpwd 找回密码
@apiName client.findpwd  
@apiGroup Client

@apiParam {string} sPhone       手机号
@apiParam {string} sPwd         登录密码（MD5加密）
@apiParam {string} sValidate    短信验证码

@apiParamExample {json} 请求样例
{
    "sPhone": "13800138001",
    "sPwd": "123456",
    "sValidate": "12345"
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "密码重置成功",
    "data": null
}
*/

/**
@api {post} client.changeimage 修改用户头像
@apiName client.changeimage  
@apiGroup Client

@apiParam {int} iClientID       用户ID
@apiParam {string} sImageSrc    新头像地址

@apiParamExample {json} 请求样例
{
    "iClientID": 2,
    "sImageSrc": "http://xxx"
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "修改头像成功",
    "data": null
}
*/

/**
@api {post} client.getlist 获取点检员列表(过时)
@apiName client.getlist
@apiGroup Client

@apiParam {int} iUnitID     使用单位ID

@apiParamExample {json} 请求样例
{
    "iUnitID": 1
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取点检员列表成功",
    "data": [
        {
            "iClientID": 1,                                 //点检员ID
            "sPhone": "13800138000",                         //点检员联系方式
            "sName": "张三"                                  //点检员姓名
        }
    ]
}
*/

/**
@api {post} client.getdutylist 获取单位值班列表
@apiName client.getdutylist  
@apiGroup Client

@apiParam {int} iClientID     点检员ID

@apiParamExample {json} 请求样例
{
    "iClientID": 1
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取值班列表成功",
    "data": [
        {
            "ID": 1,                            //点检员ID
            "sPhone": "13800138000",            //点检员联系方式
            "sName": "张三"                       //点检员姓名
        }
    ]
}
*/


/**
@api {post} client.setcid 设置用户设备号
@apiName client.setcid  
@apiGroup Client

@apiParam {string} iClientID     点检员ID
@apiParam {string} cid     移动设备ID
@apiParam {string} deviceType     移动设备系统类型

@apiParamExample {json} 请求样例
{
    "iClientID": 1,
	"cid": "fjorjmewkr34oiofdjspfd",
	"deviceType":"IOS"
}

@apiSuccessExample 返回信息2019/7/24
{
    "success": true,
    "code": 0,
    "message": "",
    "data": null
}
*/

////////////////////////////////////////////////////////用户//////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////短信//////////////////////////////////////////////////////////////////

/**
@api {post} sms.regcode 发送注册短信验证码
@apiName sms.regcode  
@apiGroup Sms

@apiParam {string} sPhone   手机号

@apiParamExample {json} 请求样例
{
    "sPhone": "13800138000"
}

@apiSuccessExample 返回信息 
{
    "success": true,
    "code": 0,
    "message": "短信发送成功",
    "data": "61379"
}
*/

/**
@api {post} sms.findcode 发送找回登码验证码
@apiName sms.findcode
@apiGroup Sms

@apiParam {string} sPhone   手机号

@apiParamExample {json} 请求样例
{
    "sPhone": "13800138000"
}

@apiSuccessExample 返回信息 
{
    "success": true,
    "code": 0,
    "message": "短信发送成功",
    "data": "61379"
}
*/

////////////////////////////////////////////////////////短信//////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////基础设置//////////////////////////////////////////////////////////////////

/**
@api {post} settings.get 获取基础设置信息
@apiName settings.get  
@apiGroup Settings

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取基础设置成功",
    "data": {
        "sPhone": "13800138000",        //平台客服电话
        "sImageSrc1": "13800138000",    //轮播1图片地址
        "sLink1": "http://xxx",         //轮播1链接地址
        "sArticleType1": "5",           //轮播1文章类别[0:法律知识,1:消防知识,2:资讯,3:帮助,4:关于我们,5:无]
        "sImageSrc2": "13800138000",    //轮播2图片地址
        "sLink2": "",                   //轮播2链接地址
        "sArticleType2": "",            //轮播2文章类别[0:法律知识,1:消防知识,2:资讯,3:帮助,4:关于我们,5:无]
        "sImageSrc3": "13800138000",    //轮播3图片地址
        "sLink3": "http://xxx",         //轮播3链接地址
        "sArticleType3": "1",           //轮播3文章类别[0:法律知识,1:消防知识,2:资讯,3:帮助,4:关于我们,5:无]
        "sImageSrc4": "13800138000",    //轮播4图片地址
        "sLink4": "http://xxx",         //轮播4链接地址
        "sArticleType4": "2",           //轮播4文章类别[0:法律知识,1:消防知识,2:资讯,3:帮助,4:关于我们,5:无]
        "sImageSrc5": "13800138000",    //轮播5图片地址
        "sLink5": "",                   //轮播5链接地址
        "sArticleType5": "5"            //轮播5文章类别[0:法律知识,1:消防知识,2:资讯,3:帮助,4:关于我们,5:无]
    }
}
*/

////////////////////////////////////////////////////////基础设置//////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////站内信//////////////////////////////////////////////////////////////////

/**
@api {post} msg.unreadcount 获取未读站内信条数
@apiName msg.unreadcount  
@apiGroup SiteMsg

@apiParam {int} iClientID     用户ID

@apiParamExample {json} 请求样例
{
    "iClientID": 5
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取用户未读的站内信条数成功",
    "data": 1
}
*/

/**
@api {post} msg.getlist 获取用户站内信列表
@apiName msg.getlist  
@apiGroup SiteMsg

@apiParam {int} iClientID   用户ID
@apiParam {int} page        页码

@apiParamExample {json} 请求样例
{
    "iClientID": 1,
    "page": 1
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取用户站内信列表成功",
    "data": [
        {
            "ID": 1,                                        //站内信ID
            "sTitle": "站内信123",                         //站内信标题
            "sContent": "balabalabalabalabala",             //站内信内容
            "iType": 1,                                     //推送类别[0:单位,1:个人]
            "sReceiveDept": "1,2,3",                        //收信人身份
            "sReceiveClient": "13800138000,13800138001",    //收信人账号
            "sOperator": "admin",                           //发送人
            "dCreateTime": "2017-10-11T14:00:04.52",        //创建时间
            "bIsDeleted": false,                            //删除标识[0:未删除,1:已删除]
            "bIsReaded": false                              //阅读标识[0:已阅读,1:未阅读]
        }
    ]
}
*/

/**
@api {post} msg.get 获取站内信详情
@apiName msg.get  
@apiGroup SiteMsg

@apiParam {int} iSiteMsgID  站内信ID
@apiParam {int} iClientID   用户ID

@apiParamExample {json} 请求样例
{
    "iSiteMsgID": 1,
    "iClientID": 1
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取站内信详情成功",
    "data": {
        "ID": 1,                                        //站内信ID
        "sTitle": "站内信123",                         //站内信标题
        "sContent": "balabalabalabalabala",             //站内信内容
        "iType": 1,                                     //推送类别[0:单位,1:个人]
        "sReceiveDept": "1,2,3",                        //收信人身份
        "sReceiveClient": "13800138000,13800138001",    //收信人账号
        "sOperator": "admin",                           //发送人
        "dCreateTime": "2017-10-11T14:00:04.52",        //创建时间
        "bIsDeleted": false                             //删除标识[0:未删除,1:已删除]
    }
}
*/

/**
@api {post} msg.delete 删除站内信
@apiName msg.delete  
@apiGroup SiteMsg

@apiParam {int} iSiteMsgID  站内信ID
@apiParam {int} iClientID   用户ID

@apiParamExample {json} 请求样例
{
    "iSiteMsgID": 1,
    "iClientID": 1
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "删除站内信成功",
    "data": null
}
*/

////////////////////////////////////////////////////////站内信//////////////////////////////////////////////////////////////////


////////////////////////////////////////////////////////意见反馈//////////////////////////////////////////////////////////////////

/**
@api {post} fb.add 提交意见反馈
@apiName fb.add  
@apiGroup Feedback

@apiParam {int} iClientID       用户ID
@apiParam {int} iUnitID         单位ID
@apiParam {string} sTitle       标题
@apiParam {string} sContent     内容
@apiParam {string} sImageSrc    图片

@apiParamExample {json} 请求样例
{
    "iClientID": 5,
    "iUnitID": 5,
    "sTitle": "标题",
    "sContent": "内容",
    "sImageSrc": "http://xxx,http://xxx"
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "提交意见反馈成功",
    "data": null
}
*/

/**
@api {post} fb.getlist 获取反馈列表
@apiName fb.getlist  
@apiGroup Feedback

@apiParam {int} iClientID       用户ID
@apiParam {int} page            页码

@apiParamExample {json} 请求样例
{
    "iClientID": 2,
    "page": 1
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取反馈列表成功",
    "data": [
        {
            "ID": 1,                                    //评论ID
            "iClientID": 2,                             //发帖人ID
            "sTitle": "标题",                             //标题
            "sContent": "内容",                           //内容
            "sImageSrc": "http://xxx,http://xxx",       //评论图片
            "bIsReplyStatus": false,                    //回复状态[0:未回复,1:已回复]
            "dCreateTime": "2017-10-20T16:50:34.743",   //评论时间
            "bIsDeleted": false                         //删除标识[0:未删除,1:已删除]
        }
    ]
}
*/

/**
@api {post} fb.get 获取反馈详情
@apiName fb.get  
@apiGroup Feedback

@apiParam {int} iFeedbackID     意见反馈ID
@apiParam {int} iClientID       用户ID

@apiParamExample {json} 请求样例
{
    "iFeedbackID": 2,
    "iClientID": 1
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取反馈详情成功",
    "data": {
        "ID": 1,                                    //评论ID
        "iClientID": 2,                             //发帖人ID
        "sTitle": "标题",                             //标题
        "sContent": "内容",                           //内容
        "sImageSrc": "http://xxx,http://xxx",       //评论图片
        "bIsReplyStatus": false,                    //回复状态[0:未回复,1:已回复]
        "dCreateTime": "2017-10-20T16:50:34.743",   //评论时间
        "bIsDeleted": false                         //删除标识[0:未删除,1:已删除]
    }
}
*/

/**
@api {post} fb.delete 删除反馈
@apiName fb.delete  
@apiGroup Feedback

@apiParam {int} iFeedbackID  意见反馈ID
@apiParam {int} iClientID   用户ID

@apiParamExample {json} 请求样例
{
    "iFeedbackID": 2,
    "iClientID": 1
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "删除反馈成功",
    "data": null
}
*/

////////////////////////////////////////////////////////意见反馈//////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////平台单位//////////////////////////////////////////////////////////////////

/**
@api {post} unit.register 单位账号注册
@apiName unit.register  
@apiGroup Unit

@apiParam {string} sPhone           手机号
@apiParam {string} sValidate        验证码
@apiParam {string} sPwd             登录密码（MD5加密）
@apiParam {string} sName            单位名称
@apiParam {string} sAddress         单位地址
@apiParam {string} sAdminName       平台管理员
@apiParam {string} sContact         联系电话
@apiParam {int} iType               单位类型[0:使用单位,1:消防部门,2:维护公司]
@apiParam {string} sCredentials     证件照
@apiParam {string} sLegalPerson     法人（类型为使用单位和消防部门时允许为空）
@apiParam {string} sQualifications  资格证明（类型为使用单位和消防部门时允许为空）

@apiParamExample {json} 请求样例
{
    "sPhone": "13800138000",
    "sValidate": "1234",
    "sPwd": "QWERTYUIOPASDFGHJKLZXCVBNM",
    "sName": "消防大队",
    "sAddress": "成都市花牌坊街",
    "sAdminName": "张三",
    "sContact": "13800138000",
    "iType": 1,
    "sCredentials": "http://xxx,http://xxx",
    "sLegalPerson": "",
    "sQualifications": ""
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "注册成功",
    "data": 3
}
*/

/**
@api {post} unit.getlist 获取单位列表
@apiName unit.getlist   
@apiGroup Unit

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取单位列表成功",
    "data": [
        {
            "ID": 1,                                        // 单位ID
            "sName": "电科十所",                            // 单位名称
            "sAddress": "四川省成都市金牛区营康西路",        // 单位地址
            "iType": 0,                                     // 单位类型[0:使用单位,1:消防部门,2:维护公司]
            "iStatus": false,                               // 状态[0:正常,1:已冻结]
            "bIsDeleted": false,                            // 删除标识[0:未删除,1:已删除]
            "DeptList": [                                   // 部门集合
                {
                    "ID": 1,                                // 部门ID
                    "iUseDeptID": 1,                        // 归属单位ID
                    "sName": "综合部",                     // 部门名称
                    "bIsDeleted": false                     // 删除标识[0:未删除,1:已删除]
                },
                {
                    "ID": 2,                                // 部门ID
                    "iUseDeptID": 1,                        // 归属单位ID
                    "sName": "项目管理部",                   // 部门名称
                    "bIsDeleted": false                     // 删除标识[0:未删除,1:已删除]
                }
            ]
        }
    ]
}
*/

/**
@api {post} unit.getrellist 获取使用单位列表(过时)
@apiName unit.getrellist
@apiGroup Unit

@apiParam {int} iClientID   用户ID

@apiParamExample {json} 请求样例
{
    "iClientID": 1
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取单位列表成功",
    "data": [
        {
            "ID": 1,                                        // 单位ID
            "sName": "电科十所",                            // 单位名称
            "sAddress": "四川省成都市金牛区营康西路",        // 单位地址
            "iType": 0,                                     // 单位类型[0:使用单位,1:消防部门,2:维护公司]
            "iStatus": false,                               // 状态[0:正常,1:已冻结]
            "bIsDeleted": false                             // 删除标识[0:未删除,1:已删除]
        }
    ]
}
*/

/**
@api {post} unit.get 获取单位详情
@apiName unit.get  
@apiGroup Unit

@apiParam {int} iUnitID   单位ID

@apiParamExample {json} 请求样例
{
    "iUnitID": 1
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取单位详情成功",
    "data": {
        "ID": 2,        //主键ID
        "sPhone": "13800138000",       //单位账号
        "sName": "成都消防中队",       //单位名称
        "sAddress": "成都花牌坊街",   //单位地址
        "sAdminName": "张三",         //单位管理员姓名
        "sContact": "13800138001",      //管理员联系电话
        "sLegalPerson": "李四",       //法人
        "sQualifications": "一级",    //资质
        "iType": 0,              //单位类型[0:使用单位,1:消防部门,2:维护公司]
        "sCredentials": "http://xxxx"     //证件照
    }
}
*/

/**
@api {post} unit.getrprlist 获取使用单位已关联的维护公司列表
@apiName unit.getrprlist  
@apiGroup Unit

@apiParam {int} iUnitID   单位ID

@apiParamExample {json} 请求样例
{
    "iUnitID": 1
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取使用单位已关联的维护公司列表成功",
    "data": [
        {
            "ID": 1,                                        // 维护公司ID
            "sName": "电科十所",                            // 维护公司名称
            "sAddress": "四川省成都市金牛区营康西路",        // 维护公司地址
            "iType": 0,                                     // 单位类型[0:使用单位,1:消防部门,2:维护公司]
            "iStatus": false,                               // 状态[0:正常,1:已冻结]
            "bIsDeleted": false                             // 删除标识[0:未删除,1:已删除]
        }
    ]
}
*/

////////////////////////////////////////////////////////平台单位//////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////值班//////////////////////////////////////////////////////////////////

/**
@api {post} duty.getlist 获取值班列表
@apiName duty.getlist  
@apiGroup Duty

@apiParam {int} iClientID   前端人员ID

@apiParamExample {json} 请求样例
{
    "iClientID": 1
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取值班列表成功",
    "data": [
        {
            "ID": 1,                                    //单位ID
            "sPhone": "13800138001",                    //单位账号
            "sName": "电科十所",                        //单位名称
            "sAddress": "四川省成都市金牛区营康西路",    //单位地址
            "sAdminName": "李大壮",                    //管理员姓名
            "sContact": "13812138321",                  //管理员联系方式
            "sLegalPerson": "",                         //法人
            "sQualifications": "",                      //资质
            "iType": 0,                                 //单位类型[0:使用单位,1:消防部门,2:维护公司]
            "sCredentials": "http://xxx",               //证件照
            "iStatus": false,                           //状态[0:正常,1:已冻结]
            "DeptList": [                               //部门集合
                {
                    "ID": 1,                            //部门ID
                    "sName": "综合部",                 //部门名称
                    "ClientList": [                     //部门下辖人员集合
                        {
                            "ID": 1,                    //人员ID
                            "sPhone": "13800138000",    //电话
                            "sName": "张三",              //姓名
                            "sImageSrc": "https://xxx",   //头像
                            "iType": 0                  //用户类别[0:点检员,1:消防,2:维护公司,3:使用公司,4:值班人员]
                        }
                    ]
                }
            ],
            "ClientList": [                             //值班人员列表
                {
                    "ID": 4,                            //值班人ID
                    "sPhone": "13900138621",            //值班人电话
                    "sName": "赵柳",                    //值班人姓名
                    "sImageSrc": "",                    //头像
                    "iStatus": false,                   //状态[0:正常,1:已冻结]
                    "sCredentials": null,               //证件照
                    "iUnitID": 1,                       //归属单位ID
                    "iType": 4                          //用户类别[0:点检员,1:消防,2:维护公司,3:使用公司,4:值班人员]
                }
            ]
        }
    ]
}
*/

////////////////////////////////////////////////////////值班//////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////值班记录//////////////////////////////////////////////////////////////////

/**
@api {post} dutyrec.getstatus 获取签到状态
@apiName dutyrec.getstatus  
@apiGroup DutyRecord

@apiParam {int} iDutyRoomID 值班室ID
@apiParam {int} iClientID   值班人员ID

@apiParamExample {json} 请求样例
{
    "iDutyRoomID": 1,
    "iClientID": 1
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取签到状态成功",
    "data": 0       // 签到状态[0:未签到,1:已签到]
}
*/

/**
@api {post} dutyrec.add 值班签到 
@apiName dutyrec.add  
@apiGroup DutyRecord

@apiParam {int} iClientID   前端人员ID
@apiParam {int} iDutyRoomID 值班室ID

@apiParamExample {json} 请求样例
{
    "iClientID": 1,
    "iDutyRoomID": 1
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "值班签到成功",
    "data": null
}
*/

/**
@api {post} dutyrec.finish 值班签退 
@apiName dutyrec.finish  
@apiGroup DutyRecord

@apiParam {int} iClientID       值班人员ID
@apiParam {int} iDutyRoomID     值班室ID
@apiParam {string} sDesc        值班情况描述
@apiParam {string} sImageSrc    值班图片

@apiParamExample {json} 请求样例
{
    "iClientID": 1,
    "iDutyRoomID": 1,
    "sDesc": "OK",
    "sImageSrc": "http://xxx"
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "值班签退成功",
    "data": null
}
*/

/**
@api {post} dutyrec.get 获取值班记录详情
@apiName dutyrec.get  
@apiGroup DutyRecord

@apiParam {int} iDutyRecordID   值班记录ID

@apiParamExample {json} 请求样例
{
    "iDutyRecordID": 1
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取值班记录详情成功",
    "data": {
        "ID": 6,                    //主键ID
        "iClientID": 4,             //值班人员ID
        "iDutyRoomID": 3,           //值班室ID
        "sStartTime": "2018-05-31 11:26:00",//值班开始时间
        "sEndTime": "",                 //值班结束时间
        "iTimeLength": 0,               //值班时长分钟数
        "sDesc": "",                    //值班记录描述
        "sImageSrc": ""                 //图片地址
    }
}
*/

/**
@api {post} dutyrec.getlist 获取值班记录列表
@apiName dutyrec.getlist  
@apiGroup DutyRecord

@apiParam {int} iClientID   值班人员ID
@apiParam {int} page   页码

@apiParamExample {json} 请求样例
{
    "iClientID": 1,
    "page": 1
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取值班记录列表成功",
    "data": {
        "iTotalTimeLength": 640,        //本月值班总分钟数
        "DataList": [{                  //值班记录集合
            "ID": 6,                    //主键ID
            "iClientID": 4,             //值班人员ID
            "sPhone": "13900138621",    //值班人员联系电话
            "sClientName": "赵柳",      //值班人员姓名
            "iDutyRoomID": 3,           //值班室ID
            "sDutyRoomName": "六一节值班室",//值班室名称
            "sUnitName": "电科十所",        //值班室所属单位
            "sStartTime": "2018-05-31 11:26:00",//值班开始时间
            "sEndTime": "",                 //值班结束时间
            "iTimeLength": 0,               //值班时长分钟数
            "sDesc": "",                    //值班记录描述
            "sImageSrc": ""                 //图片地址
        }]
    }
}
*/

////////////////////////////////////////////////////////值班记录//////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////设备//////////////////////////////////////////////////////////////////

/**
@api {post} device.add 添加设备
@apiName device.add  
@apiGroup Device

@apiParam {int} iClientID       点检员ID
@apiParam {string} sNumber      设备编号
@apiParam {int} iDeviceTypeID   设备类型ID
@apiParam {int} iNumber         数量
@apiParam {string} sName        设备名称
@apiParam {string} sLocation                设备位置
@apiParam {string} sModel                   设备型号
@apiParam {string} sSpec                    设备规格
@apiParam {string} sInstallDate             安装日期
@apiParam {string} sManufacturer            生产厂家
@apiParam {string} iRepairDeptID            维护公司ID
@apiParam {string} iUseDeptID               使用单位ID
@apiParam {int} iCreateUnitID               添加设备单位ID
@apiParam {string} sProductionDate          生产日期
@apiParam {string} iExpiredYears            过期年限
@apiParam {string} iForciblyScrappedYears   强制报废年限
@apiParam {string} sRelDeviceIDs            关联设备ID集

@apiParamExample {json} 请求样例
{
    "iClientID": 1,
    "sNumber": "1234567890",
    "iDeviceTypeID": 1,
    "iNumber": 2,
    "sName": "灭火器",
    "sLocation": "8栋1单元1楼楼梯间",
    "sModel": "T090",
    "sSpec": "b92",
    "sInstallDate": "2018-01-01",
    "sManufacturer": "成都市灭火设备厂",
    "iRepairDeptID": 2,
    "iUseDeptID": 1,
    "iCreateUnitID": 2,
    "sProductionDate": "2015-01-01",
    "iExpiredYears": 5,
    "iForciblyScrappedYears": 8,
    "sRelDeviceIDs": "1,2"
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "添加设备成功",
    "data": 1
}
*/

/**
@api {post} device.param 获取新增设备需要的参数(过时)
@apiName device.param
@apiGroup Device

@apiParam {int} iClientID   点检员ID

@apiParamExample {json} 请求样例
{
    "iClientID": 1
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取新增设备需要的参数成功",
    "data": {
        "UseDeptList": [                //使用单位集合
            {
                "ID": 1,                //使用单位ID
                "sName": "电科十所"     //使用单位名称
            }
        ]
    }
}
*/

/**
@api {post} device.getlist 获取设备列表
@apiName device.getlist  
@apiGroup Device

@apiParam {int} iClientID   点检员ID
@apiParam {string} sUnitID   单位ID
@apiParam {int} page        页码
@apiParam {string} iStatus   查看状态["":全部,"0":正常,"1":异常]
@apiParam {string} bIsRecorded   当月是否巡检["":全部,"0":未巡检,"1":已巡检]
@apiParam {string} bIsExpired   是否超期["":全部,"0":未超期,"1":已超期]
@apiParam {string} sName   设备名称

@apiParamExample {json} 请求样例
{
    "iClientID": 1,
    "sUnitID": "0",
    "page": 2,
    "iStatus": "",
    "bIsRecorded": "",
    "bIsExpired": "",
    "sName": ""
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取设备列表成功",
    "data": [
        {
            "ID": 1,            //设备ID
            "sNumber": "123456",                    //设备编号
            "sName": "灭火器",                     //设备名称
            "sLocation": "1楼楼梯间",               //安装位置
            "sProductionDate": "2015-10-10",        //生产日期
            "iClientID": 1,                         //责任人ID
            "sClientName": "张三",                    //责任人姓名
            "sUnitName": "电科十所",                //单位名称
            "sOrganName": "综合部",                //部门名称
            "iAbnormalStatus": false,               //巡检结果[0:正常,1:异常]
            "dCreateTime": "2017-10-09T17:50:33.9"  //创建时间
        }
    ]
}
*/

/**
@api {post} device.getalllist 获取所有设备(过时)
@apiName device.getalllist
@apiGroup Device

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取所有设备成功",
    "data": [
        {
            "ID": 1,            //设备ID
            "sNumber": "123456",                    //设备编号
            "sName": "灭火器",                     //设备名称
            "sLocation": "1楼楼梯间",               //安装位置
            "sProductionDate": "2015-10-10",        //生产日期
            "iClientID": 1,                         //责任人ID
            "sClientName": "张三",                    //责任人姓名
            "sUnitName": "电科十所",                //单位名称
            "sOrganName": "综合部",                //部门名称
            "iAbnormalStatus": false,               //巡检结果[0:正常,1:异常]
            "dCreateTime": "2017-10-09T17:50:33.9"  //创建时间
        }
    ]
}
*/

/**
@api {post} device.getlistbyunit 获取单位下辖设备列表
@apiName device.getlistbyunit  
@apiGroup Device

@apiParam {int} iUnitID 使用单位ID
@apiParam {int} page    页码
@apiParam {string} iStatus   查看状态["":全部,"0":正常,"1":异常]
@apiParam {string} bIsRecorded   当月是否巡检["":全部,"0":未巡检,"1":已巡检]
@apiParam {string} bIsExpired   是否超期["":全部,"0":未超期,"1":已超期]
@apiParam {string} bHasProductDate   是否已设置生产日期超期[0:否,1:是]
@apiParam {string} sName   设备名称

@apiParamExample {json} 请求样例
{
    "iUnitID": 1,
    "page": 2,
    "iStatus": "",
    "bIsRecorded": "",
    "bIsExpired": "",
    "sName": ""
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取单位下辖设备列表成功",
    "data": [
        {
            "ID": 1,            //设备ID
            "sNumber": "123456",                    //设备编号
            "sName": "灭火器",                     //设备名称
            "sLocation": "1楼楼梯间",               //安装位置
            "sProductionDate": "2015-10-10",        //生产日期
            "iClientID": 1,                         //责任人ID
            "sClientName": "张三",                    //责任人姓名
            "sUnitName": "电科十所",                //单位名称
            "sOrganName": "综合部",                //部门名称
            "iAbnormalStatus": false,               //巡检结果[0:正常,1:异常]
            "dCreateTime": "2017-10-09T17:50:33.9"  //创建时间
        }
    ]
}
*/

/**
@api {post} device.get 获取设备详情
@apiName device.get  
@apiGroup Device

@apiParam {int} iDeviceID  设备ID

@apiParamExample {json} 请求样例
{
    "iDeviceID": 1
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取设备详情成功",
    "data": {
        "ID": 1,            //设备ID
        "sNumber": "123456",                    //设备编号
        "sDeviceTypeName": "灭火设备",          //设备类型
        "iNumber": 1,                           //设备数量
        "sName": "灭火器",                     //设备名称
        "sLocation": "1楼楼梯间",               //安装位置
        "sModel": "B90",                       //设备型号
        "sSpec": "BMW-3",                       //设备规格
        "sInstallDate": "2017-01-01",           //安装日期
        "sManufacturer": "成都消防设备厂",     //生产厂家
        "sQRCode": "http://xxx",                //设备二维码
        "sRepairDeptName": "维修一厂",          //维修单位名称
        "sUnitName": "电科十所",                //使用单位名称
        "sClientName": "张三",                    //责任人姓名
        "sProductionDate": "2015-10-10",        //生产日期
        "iExpiredYears": 5,                       //过期年限
        "iForciblyScrappedYears": 8,            //强制报废年限
        "sLastInspectionDate": "2018-05-20",    //上次巡检日期
        "sLastRepairDate": "2017-03-10",        //上次维修时间
        "sLastChangeDate": "2018-01-01",        //上次更换时间
        "ParamList": [                          //指标集合
            {
                "ID": 1,                        //指标ID
                "sName": "把手"               //指标名称
            }
        ],
        "DetailList": [],                       //上次巡检详情
        "DeviceList": []                        //关联设备集合
    }
}
*/

/**
@api {post} device.ulately 获取单位最近添加的设备
@apiName device.ulately
@apiGroup Device

@apiParam {int} iUnitID  单位ID

@apiParamExample {json} 请求样例
{
    "iUnitID": 1
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取设备详情成功",
    "data": {
        "ID": 1,            //设备ID
        "sNumber": "123456",                    //设备编号
        "sDeviceTypeName": "灭火设备",          //设备类型
        "iNumber": 1,                           //设备数量
        "sName": "灭火器",                     //设备名称
        "sLocation": "1楼楼梯间",               //安装位置
        "sModel": "B90",                       //设备型号
        "sSpec": "BMW-3",                       //设备规格
        "sInstallDate": "2017-01-01",           //安装日期
        "sManufacturer": "成都消防设备厂",     //生产厂家
        "sQRCode": "http://xxx",                //设备二维码
        "sRepairDeptName": "维修一厂",          //维修单位名称
        "sUnitName": "电科十所",                //使用单位名称
        "sClientName": "张三",                    //责任人姓名
        "sProductionDate": "2015-10-10",        //生产日期
        "iExpiredYears": 5,                       //过期年限
        "iForciblyScrappedYears": 8,            //强制报废年限
        "sLastInspectionDate": "2018-05-20",    //上次巡检日期
        "sLastRepairDate": "2017-03-10",        //上次维修时间
        "sLastChangeDate": "2018-01-01"         //上次更换时间
    }
}
*/

/**
@api {post} device.clately 获取点检员最近添加的设备
@apiName device.clately  
@apiGroup Device

@apiParam {int} iClientID  点检员ID

@apiParamExample {json} 请求样例
{
    "iClientID": 1
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取设备详情成功",
    "data": {
        "ID": 1,            //设备ID
        "sNumber": "123456",                    //设备编号
        "sDeviceTypeName": "灭火设备",          //设备类型
        "iNumber": 1,                           //设备数量
        "sName": "灭火器",                     //设备名称
        "sLocation": "1楼楼梯间",               //安装位置
        "sModel": "B90",                       //设备型号
        "sSpec": "BMW-3",                       //设备规格
        "sInstallDate": "2017-01-01",           //安装日期
        "sManufacturer": "成都消防设备厂",     //生产厂家
        "sQRCode": "http://xxx",                //设备二维码
        "sRepairDeptName": "维修一厂",          //维修单位名称
        "sUnitName": "电科十所",                //使用单位名称
        "sClientName": "张三",                    //责任人姓名
        "sProductionDate": "2015-10-10",        //生产日期
        "iExpiredYears": 5,                       //过期年限
        "iForciblyScrappedYears": 8,            //强制报废年限
        "sLastInspectionDate": "2018-05-20",    //上次巡检日期
        "sLastRepairDate": "2017-03-10",        //上次维修时间
        "sLastChangeDate": "2018-01-01"         //上次更换时间
    }
}
*/


/**
@api {post} device.client.getlist 获取设备列表(移动端)
@apiName device.client.getlist  
@apiGroup Device

@apiParam {int} iClientID  点检员ID
@apiParam {string} lm  最后更新时间

@apiParamExample {json} 请求样例
{
    "iClientID": 1,
	"lm": ""
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取设备列表成功",
    "data": {
        "IsModified": true,
        "IsAll": true,
        "Data": [
            {
                "ID": 11841,
                "sNumber": "2502T05-1",
                "iDeviceTypeID": 14,
                "sDeviceTypeName": "灭火器",
                "iNumber": 1,
                "sName": "干粉灭火器",
                "sLocation": "25号楼2层通道（电梯口）",
                "sModel": "MFZ/ABC4",
                "sManufacturer": "龙威",
                "iRepairDeptID": 27,
                "sRepairDeptName": null,
                "iUseDeptID": 24,
                "sUnitName": "中国电子科技集团公司第十研究所",
                "iClientID": 31,
                "sClientName": "余茂",
                "sClientPhone": null,
                "sLastInspectionDate": null,
                "sLastRepairDate": null,
                "sLastChangeDate": null,
                "iAbnormalStatus": 0,
                "LastModifyTime": "2019-05-28T17:09:03.287",
                "bIsDeleted": 0
            }
        ],
        "LastModifyTime": "2019/7/15 11:25:46"
    }
}
*/

/**
@api {post} device.client.getDeviceParam 查询设备指标(移动端)
@apiName device.client.getDeviceParam  
@apiGroup Device

@apiParam {int} iClientID  点检员ID
@apiParam {string} lm  最后更新时间

@apiParamExample {json} 请求样例
{
    "iClientID": 1,
	"lm": ""
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "成功",
    "data": {
        "IsModified": true,
        "IsAll": true,
        "Data": [
            {
                "ID": 17,
                "sName": "外观",
                "iDeviceTypeID": 12,
                "sDeviceTypeName": null,
                "iUseDeptID": 21,
                "sUnitName": null,
                "dCreateTime": "2018-07-07T10:51:24.73",
                "bIsDeleted": false,
                "bIsSelected": 0
            }
        ],
        "LastModifyTime": "2019/7/15 11:28:29"
    }
}
*/


////////////////////////////////////////////////////////设备//////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////设备类型//////////////////////////////////////////////////////////////////

/**
@api {post} devicetype.getlist 获取设备分类列表
@apiName devicetype.getlist  
@apiGroup DeviceType

@apiParam {int} iUnitID   单位ID

@apiParamExample {json} 请求样例
{
    "iUnitID": 1
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取设备列表成功",
    "data": [
        {
            "ID": 1,                        //设备分类ID
            "iUseDeptID": 1,                //创建单位ID（总平台创建的分类该字段为0）
            "sName": "测试类别",            //设备名称
            "dCreateTime": "2018-05-21T14:02:52.717",   //创建时间
            "bIsDeleted": false             //删除标识[0:未删除,1:已删除]
        }
    ]
}
*/

////////////////////////////////////////////////////////设备类型//////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////巡检//////////////////////////////////////////////////////////////////

/**
@api {post} ins.add 添加巡检记录
@apiName ins.add  
@apiGroup Inspection

@apiParam {int} iClientID       点检员ID
@apiParam {int} iDeviceID       设备ID
@apiParam {string} sParamList   参数和状态集合
@apiParam {string} sDesc        说明备注

@apiParamExample {json} 请求样例
{
    "iClientID": 1,
    "iDeviceID": 2,
    "sParamList": "[{ iDeviceParamID: 1, iStatus: 0 }, { iDeviceParamID: 2, iStatus: 1 }]",
    "sDesc": "巡检完毕"
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "添加巡检记录成功",
    "data": null
}
*/

/**
@api {post} ins.adds 批量添加巡检记录
@apiName ins.adds  
@apiGroup Inspection

@apiParam {int} iClientID       点检员ID
@apiParam {array} sRecordList   巡检设备及参数集合

@apiParamExample {json} 请求样例
{
    "iClientID": 1,
    "sRecordList": [{ iDeviceID: 1, DetailList: [{ iDeviceParamID: 1, iStatus: 0 }, { iDeviceParamID: 2, iStatus: 1 }], sDesc: "巡检好了" }]
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "添加巡检记录成功",
    "data": null
}
*/

/**
@api {post} ins.getlist 获取巡检记录列表
@apiName ins.getlist  
@apiGroup Inspection

@apiParam {int} iDeviceID  设备ID
@apiParam {int} page        页码

@apiParamExample {json} 请求样例
{
    "iDeviceID": 1,
    "page": 2
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取巡检记录列表成功",
    "data": [
        {
            "ID": 1,            //巡检记录ID
            "iDeviceID": 1,                         //设备ID
            "iClientID": 2,                         //巡检人ID
            "iStatus": false,                       //巡检结果[0:正常,1:异常]
            "dCreateTime": "2017-10-09T17:50:33.9", //创建时间
            "sDesc": "备注"                         //备注
        }
    ]
}
*/

/**
@api {post} ins.get 获取巡检记录详情
@apiName ins.get  
@apiGroup Inspection

@apiParam {int} iInspectionRecordID  巡检记录ID

@apiParamExample {json} 请求样例
{
    "iInspectionRecordID": 1
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取巡检记录详情成功",
    "data": {
        "ID": 1,            //巡检记录ID
        "iDeviceID": 1,                         //设备ID
        "iClientID": 2,                         //巡检人ID
        "iStatus": false,                       //巡检结果[0:正常,1:异常]
        "dCreateTime": "2017-10-09T17:50:33.9", //创建时间
        "sDesc": "备注"                         //备注
    }
}
*/

////////////////////////////////////////////////////////巡检//////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////维修//////////////////////////////////////////////////////////////////

/**
@api {post} repair.add 添加维修记录
@apiName repair.add  
@apiGroup Repair

@apiParam {int} iClientID       点检员ID
@apiParam {int} iDeviceID       设备ID
@apiParam {string} sDesc        说明备注

@apiParamExample {json} 请求样例
{
    "iClientID": 1,
    "iDeviceID": 2,
    "sDesc": "维修完毕，重新绑定螺栓。"
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "添加维修记录成功",
    "data": null
}
*/

/**
@api {post} repair.getlist 获取维修记录列表
@apiName repair.getlist  
@apiGroup Repair

@apiParam {int} iDeviceID  设备ID
@apiParam {int} page        页码

@apiParamExample {json} 请求样例
{
    "iDeviceID": 1,
    "page": 2
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取维修记录列表成功",
    "data": [
        {
            "ID": 1,            //维修记录ID
            "iDeviceID": 1,                         //设备ID
            "iClientID": 2,                         //维修人ID
            "dCreateTime": "2017-10-09T17:50:33.9", //创建时间
            "sDesc": "备注"                         //备注
        }
    ]
}
*/

////////////////////////////////////////////////////////维修//////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////更换//////////////////////////////////////////////////////////////////

/**
@api {post} change.add 添加更换记录
@apiName change.add  
@apiGroup Change

@apiParam {int} iClientID           点检员ID
@apiParam {int} iDeviceID           设备ID
@apiParam {string} sModel           设备型号
@apiParam {string} sSpec            设备规格
@apiParam {int} iNumber             数量
@apiParam {string} sInstallDate     安装日期
@apiParam {string} sManufacturer    厂家
@apiParam {string} sProductionDate  生产日期
@apiParam {string} sDesc            说明备注

@apiParamExample {json} 请求样例
{
    "iClientID": 1,
    "iDeviceID": 2,
    "sModel": "B90",
    "sSpec": "X001",
    "iNumber": 1,
    "sInstallDate": "2018-05-21",
    "sManufacturer": "成都市灭火设备一厂",
    "sProductionDate": "2017-01-01",
    "sDesc": "喷头裂口，已更换。"
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "添加更换记录成功",
    "data": null
}
*/

/**
@api {post} change.getlist 获取更换记录列表
@apiName change.getlist  
@apiGroup Change

@apiParam {int} iDeviceID   设备ID
@apiParam {int} page        页码

@apiParamExample {json} 请求样例
{
    "iDeviceID": 1,
    "page": 2
}

@apiSuccessExample 返回信息
{
    "success": true,
    "code": 0,
    "message": "获取更换记录列表成功",
    "data": [
        {
            "ID": 1,            //更换记录ID
            "iDeviceID": 1,                         //设备ID
            "iClientID": 2,                         //更换人ID
            "dCreateTime": "2017-10-09T17:50:33.9", //创建时间
            "sDesc": "备注"                         //备注
        }
    ]
}
*/

////////////////////////////////////////////////////////更换//////////////////////////////////////////////////////////////////
