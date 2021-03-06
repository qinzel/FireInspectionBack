define({ "api": [
  {
    "type": "post",
    "url": "article.about",
    "title": "获取关于我们",
    "name": "article_about",
    "group": "Article",
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取关于我们成功\",\n    \"data\": {\n        \"ID\": 2,        //主键ID\n        \"iType\": 0,     //文章类别[0:法律知识,1:消防知识,2:资讯,3:帮助,4:关于我们]\n        \"sTitle\": \"关于我们\",       //标题\n        \"sSortNumber\": \"1\",       //排序序号\n        \"sImageSrc\": \"http://xxx\",       //列表图片地址\n        \"sContent\": \"同签署本《跃米商城平台服务协议》（下称“本协议”）并使用跃米商城平台服务！\",        //内容\n        \"dCreateTime\": \"2017-10-09T16:56:54.98\",        //创建时间\n        \"bIsDeleted\": false     //删除标识\n    }\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "文章",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=article.about"
      }
    ]
  },
  {
    "type": "post",
    "url": "article.client.getlist",
    "title": "获取文章列表(不含文章内容)",
    "name": "article_client_getlist",
    "group": "Article",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "lm",
            "description": "<p>最后更新时间</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"lm\": ''\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取文章列表成功\",\n    \"data\": {\n        \"IsModified\": true,\n        \"IsAll\": true,\n        \"Data\": [\n            {\n                \"ID\": 4,\n                \"sTitle\": \"火灾的危害性\",\n                \"sContent\": null,\n                \"sSortNumber\": \"1\",\n                \"sImageSrc\": \"http://115.28.48.78:8092/upload/7GHMK4VF6PQE9T30L5O8W2UCA1SYIRXNJL12/Article/Article/image/2018/07/02/6366614446185577937990468.jpg\",\n                \"iType\": 1,\n                \"dCreateTime\": \"2018-07-02T16:08:01.81\",\n                \"bIsDeleted\": false,\n                \"lastModifyTime\": \"2019-05-28T17:09:30.587\"\n            }\n        ],\n        \"LastModifyTime\": \"2019/7/15 11:11:42\"\n    }\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "文章",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=article.client.getlist"
      }
    ]
  },
  {
    "type": "post",
    "url": "article.get",
    "title": "获取文章详情",
    "name": "article_get",
    "group": "Article",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iArticleID",
            "description": "<p>文章ID</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iArticleID\": 1\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取文章详情成功\",\n    \"data\": {\n        \"ID\": 2,        //主键ID\n        \"iType\": 0,     //文章类别[0:法律知识,1:消防知识,2:资讯,3:帮助,4:关于我们]\n        \"sTitle\": \"平台协议\",       //标题\n        \"sSortNumber\": \"1\",       //排序序号\n        \"sImageSrc\": \"http://xxx\",       //列表图片地址\n        \"sContent\": \"同签署本《跃米商城平台服务协议》（下称“本协议”）并使用跃米商城平台服务！\",        //内容\n        \"dCreateTime\": \"2017-10-09T16:56:54.98\",        //创建时间\n        \"bIsDeleted\": false     //删除标识\n    }\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "文章",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=article.get"
      }
    ]
  },
  {
    "type": "post",
    "url": "article.getlist",
    "title": "获取文章列表",
    "name": "article_getlist",
    "group": "Article",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iType",
            "description": "<p>文章类别[0:法律知识,1:消防知识,2:资讯,3:帮助,4:关于我们]</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sTitle",
            "description": "<p>文章标题</p>"
          },
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "page",
            "description": "<p>页码</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iType\": 0,\n    \"sTitle\": \"标题是我\",\n    \"page\": 1\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取文章列表成功\",\n    \"data\": [\n        {\n            \"ID\": 2,        //主键ID\n            \"iType\": 0,     //文章类别[0:法律知识,1:消防知识,2:资讯,3:帮助,4:关于我们]\n            \"sTitle\": \"平台协议\",       //标题\n            \"sSortNumber\": \"1\",       //排序序号\n            \"sImageSrc\": \"http://xxx\",       //列表图片地址\n            \"sContent\": \"同签署本《跃米商城平台服务协议》（下称“本协议”）并使用跃米商城平台服务！\",        //内容\n            \"dCreateTime\": \"2017-10-09T16:56:54.98\",        //创建时间\n            \"bIsDeleted\": false     //删除标识\n        }\n    ]\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "文章",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=article.getlist"
      }
    ]
  },
  {
    "type": "post",
    "url": "banner.get",
    "title": "获取轮播详情(过时)",
    "name": "banner_get",
    "group": "Banner",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iBannerID",
            "description": "<p>轮播ID</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iBannerID\": 1\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取轮播详情成功\",\n    \"data\": {\n        \"ID\": 2,        //主键ID\n        \"sImageSrc\": \"http://xxx\",       //图片地址\n        \"sLink\": \"http://xxx\",          //跳转地址\n        \"iArticleType\": 0,              //轮播文章类别[0:无,1:法律知识,2:消防知识,3:资讯,4:帮助,5:关于我们]\n        \"dCreateTime\": \"2017-10-09T16:56:54.98\",        //创建时间\n        \"bIsDeleted\": false     //删除标识\n    }\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "轮播",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=banner.get"
      }
    ]
  },
  {
    "type": "post",
    "url": "banner.getlist",
    "title": "获取轮播列表",
    "name": "banner_getlist",
    "group": "Banner",
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取轮播列表成功\",\n    \"data\": [\n        {\n            \"ID\": 2,        //主键ID\n            \"sImageSrc\": \"http://xxx\",       //图片地址\n            \"sLink\": \"http://xxx\",          //跳转地址\n            \"iArticleType\": 0,              //轮播文章类别[0:无,1:法律知识,2:消防知识,3:资讯,4:帮助,5:关于我们]\n            \"dCreateTime\": \"2017-10-09T16:56:54.98\",        //创建时间\n            \"bIsDeleted\": false     //删除标识\n        }\n    ]\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "轮播",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=banner.getlist"
      }
    ]
  },
  {
    "type": "post",
    "url": "change.add",
    "title": "添加更换记录",
    "name": "change_add",
    "group": "Change",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iClientID",
            "description": "<p>点检员ID</p>"
          },
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iDeviceID",
            "description": "<p>设备ID</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sModel",
            "description": "<p>设备型号</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sSpec",
            "description": "<p>设备规格</p>"
          },
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iNumber",
            "description": "<p>数量</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sInstallDate",
            "description": "<p>安装日期</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sManufacturer",
            "description": "<p>厂家</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sProductionDate",
            "description": "<p>生产日期</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sDesc",
            "description": "<p>说明备注</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iClientID\": 1,\n    \"iDeviceID\": 2,\n    \"sModel\": \"B90\",\n    \"sSpec\": \"X001\",\n    \"iNumber\": 1,\n    \"sInstallDate\": \"2018-05-21\",\n    \"sManufacturer\": \"成都市灭火设备一厂\",\n    \"sProductionDate\": \"2017-01-01\",\n    \"sDesc\": \"喷头裂口，已更换。\"\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"添加更换记录成功\",\n    \"data\": null\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "更换",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=change.add"
      }
    ]
  },
  {
    "type": "post",
    "url": "change.getlist",
    "title": "获取更换记录列表",
    "name": "change_getlist",
    "group": "Change",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iDeviceID",
            "description": "<p>设备ID</p>"
          },
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "page",
            "description": "<p>页码</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iDeviceID\": 1,\n    \"page\": 2\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取更换记录列表成功\",\n    \"data\": [\n        {\n            \"ID\": 1,            //更换记录ID\n            \"iDeviceID\": 1,                         //设备ID\n            \"iClientID\": 2,                         //更换人ID\n            \"dCreateTime\": \"2017-10-09T17:50:33.9\", //创建时间\n            \"sDesc\": \"备注\"                         //备注\n        }\n    ]\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "更换",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=change.getlist"
      }
    ]
  },
  {
    "type": "post",
    "url": "client.changeimage",
    "title": "修改用户头像",
    "name": "client_changeimage",
    "group": "Client",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iClientID",
            "description": "<p>用户ID</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sImageSrc",
            "description": "<p>新头像地址</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iClientID\": 2,\n    \"sImageSrc\": \"http://xxx\"\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"修改头像成功\",\n    \"data\": null\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "用户",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=client.changeimage"
      }
    ]
  },
  {
    "type": "post",
    "url": "client.editpwd",
    "title": "修改登录密码",
    "name": "client_editpwd",
    "group": "Client",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iClientID",
            "description": "<p>用户ID</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sOldPwd",
            "description": "<p>旧登录密码（MD5加密）</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sPwd",
            "description": "<p>新登录密码（MD5加密）</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iClientID\": 2,\n    \"sOldPwd\": \"QWERTYUIOPASDFGHJKLZXCVBNM\",\n    \"sPwd\": \"QWERTYUIOPASDFGHJKLZXCVBNM\"\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"密码修改成功\",\n    \"data\": null\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "用户",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=client.editpwd"
      }
    ]
  },
  {
    "type": "post",
    "url": "client.findpwd",
    "title": "找回密码",
    "name": "client_findpwd",
    "group": "Client",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sPhone",
            "description": "<p>手机号</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sPwd",
            "description": "<p>登录密码（MD5加密）</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sValidate",
            "description": "<p>短信验证码</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"sPhone\": \"13800138001\",\n    \"sPwd\": \"123456\",\n    \"sValidate\": \"12345\"\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"密码重置成功\",\n    \"data\": null\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "用户",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=client.findpwd"
      }
    ]
  },
  {
    "type": "post",
    "url": "client.get",
    "title": "获取用户信息",
    "name": "client_get",
    "group": "Client",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iClientID",
            "description": "<p>前端用户ID</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iClientID\": 1\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取用户信息成功\",\n    \"data\": {\n        \"ID\": 2,                    //用户ID\n        \"sPhone\": \"13800138000\",    //手机号\n        \"sName\": \"张三\",              //姓名\n        \"iStatus\": false,           //用户状态[0:未冻结,1:已冻结]\n        \"bIsDeleted\": false         //删除标识[0:未删除,1:已删除]\n    }\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "用户",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=client.get"
      }
    ]
  },
  {
    "type": "post",
    "url": "client.getdutylist",
    "title": "获取单位值班列表",
    "name": "client_getdutylist",
    "group": "Client",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iClientID",
            "description": "<p>点检员ID</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iClientID\": 1\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取值班列表成功\",\n    \"data\": [\n        {\n            \"ID\": 1,                            //点检员ID\n            \"sPhone\": \"13800138000\",            //点检员联系方式\n            \"sName\": \"张三\"                       //点检员姓名\n        }\n    ]\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "用户",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=client.getdutylist"
      }
    ]
  },
  {
    "type": "post",
    "url": "client.getlist",
    "title": "获取点检员列表(过时)",
    "name": "client_getlist",
    "group": "Client",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iUnitID",
            "description": "<p>使用单位ID</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iUnitID\": 1\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取点检员列表成功\",\n    \"data\": [\n        {\n            \"iClientID\": 1,                                 //点检员ID\n            \"sPhone\": \"13800138000\",                         //点检员联系方式\n            \"sName\": \"张三\"                                  //点检员姓名\n        }\n    ]\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "用户",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=client.getlist"
      }
    ]
  },
  {
    "type": "post",
    "url": "client.login",
    "title": "用户登录",
    "name": "client_login",
    "group": "Client",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sPhone",
            "description": "<p>手机号</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sPwd",
            "description": "<p>登录密码（MD5加密）</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"sPhone\": \"13800138000\",\n    \"sPwd\": \"QWERTYUIOPASDFGHJKLZXCVBNM\"\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"用户登录成功\",\n    \"data\": {\n        \"ID\": 2,                    //用户ID\n        \"sPhone\": \"13800138000\",    //手机号\n        \"sPwd\": \"\",                 //登录密码\n        \"sName\": \"张三\",              //姓名\n        \"sImageSrc\": \"http://xxx\",  //头像\n        \"sCredentials\": \"http://xxx\",//证件照\n        \"iStatus\": false,           //用户状态[0:未冻结,1:已冻结]\n        \"iUnitID\": 1,               //所属单位ID\n        \"iType\": 0,         //用户类别[0:点检员,1:消防,2:维护公司,3:使用公司,4:值班人员]\n        \"bIsDeleted\": false,        //删除标识[0:未删除,1:已删除]\n        \"dCreateTime\": \"2017-10-24T15:42:49.763\"        //创建时间\n    }\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "用户",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=client.login"
      }
    ]
  },
  {
    "type": "post",
    "url": "client.logout",
    "title": "用户登出",
    "name": "client_logout",
    "group": "Client",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "iClientID",
            "description": "<p>用户ID</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "deviceID",
            "description": "<p>设备标识</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iClientID\": \"31\",\n    \"deviceID\": \"QWERTYUIOPASDFGHJKLZXCVBNM\"\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"用户登出成功\",\n    \"data\": {\n    }\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "用户",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=client.logout"
      }
    ]
  },
  {
    "type": "post",
    "url": "client.register",
    "title": "用户注册",
    "name": "client_register",
    "group": "Client",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sPhone",
            "description": "<p>手机号</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sValidate",
            "description": "<p>验证码</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sPwd",
            "description": "<p>登录密码（MD5加密）</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sName",
            "description": "<p>姓名</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sUnitIds",
            "description": "<p>单位ID集</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sDeptIds",
            "description": "<p>部门ID集</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sCredentials",
            "description": "<p>证件照</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"sPhone\": \"13800138000\",\n    \"sValidate\": \"1234\",\n    \"sPwd\": \"QWERTYUIOPASDFGHJKLZXCVBNM\",\n    \"sName\": \"张三\",\n    \"sUnitIds\": \"1,2,3\",\n    \"sDeptIds\": \"4,5,6\",\n    \"sCredentials\": \"http://xxx,http://xxx\"\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"用户注册成功\",\n    \"data\": 3\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "用户",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=client.register"
      }
    ]
  },
  {
    "type": "post",
    "url": "client.self",
    "title": "获取当前用户信息",
    "name": "client_self",
    "group": "Client",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iClientID",
            "description": "<p>前端用户ID</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iClientID\": 1\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取用户信息成功\",\n    \"data\": {\n        \"ID\": 2,                    //用户ID\n        \"sPhone\": \"13800138000\",    //手机号\n        \"sName\": \"张三\",              //姓名\n        \"iStatus\": false,           //用户状态[0:未冻结,1:已冻结]\n        \"bIsDeleted\": false,        //删除标识[0:未删除,1:已删除]\n        \"DeptList\": [               //所属单位部门集合\n            {\n                \"ID\": 4,            //部门ID\n                \"iUseDeptID\": 3,    //单位ID\n                \"sUnitName\": \"新能源即墨突然\",//单位名称\n                \"iAuditState\": 1,   //审核状态[0:未审核,1:已审核,2:已拒绝]\n                \"sName\": \"部门三\", //部门名称\n                \"dCreateTime\": \"2018-06-22T15:35:25.447\",\n                \"bIsDeleted\": false\n            }\n        ]\n    }\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "用户",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=client.self"
      }
    ]
  },
  {
    "type": "post",
    "url": "client.setcid",
    "title": "设置用户设备号",
    "name": "client_setcid",
    "group": "Client",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "iClientID",
            "description": "<p>点检员ID</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "cid",
            "description": "<p>移动设备ID</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "deviceType",
            "description": "<p>移动设备系统类型</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iClientID\": 1,\n\t\"cid\": \"fjorjmewkr34oiofdjspfd\",\n\t\"deviceType\":\"IOS\"\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息2019/7/24",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"\",\n    \"data\": null\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "用户",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=client.setcid"
      }
    ]
  },
  {
    "description": "<p>说明 appSecret：EpD70oATq6d7TgUoOHkbODQjVg7CUGaWw7thptHPt3KMWLcEN8rWD6V7RjP5fz7a superToken: SIVB 签名算法：将appKey、appSecret、timestamp、echostr排序后MD5加密</p>",
    "type": "",
    "url": "",
    "version": "0.0.0",
    "filename": "./apidata.js",
    "group": "D__HX______FireInspectionBack_EHECD_FirePatrolInspection_Web_ApiDoc_apidata_js",
    "groupTitle": "D__HX______FireInspectionBack_EHECD_FirePatrolInspection_Web_ApiDoc_apidata_js",
    "name": ""
  },
  {
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "optional": false,
            "field": "varname1",
            "description": "<p>No type.</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "varname2",
            "description": "<p>With type.</p>"
          }
        ]
      }
    },
    "type": "",
    "url": "",
    "version": "0.0.0",
    "filename": "./html/main.js",
    "group": "D__HX______FireInspectionBack_EHECD_FirePatrolInspection_Web_ApiDoc_html_main_js",
    "groupTitle": "D__HX______FireInspectionBack_EHECD_FirePatrolInspection_Web_ApiDoc_html_main_js",
    "name": ""
  },
  {
    "type": "post",
    "url": "devicetype.getlist",
    "title": "获取设备分类列表",
    "name": "devicetype_getlist",
    "group": "DeviceType",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iUnitID",
            "description": "<p>单位ID</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iUnitID\": 1\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取设备列表成功\",\n    \"data\": [\n        {\n            \"ID\": 1,                        //设备分类ID\n            \"iUseDeptID\": 1,                //创建单位ID（总平台创建的分类该字段为0）\n            \"sName\": \"测试类别\",            //设备名称\n            \"dCreateTime\": \"2018-05-21T14:02:52.717\",   //创建时间\n            \"bIsDeleted\": false             //删除标识[0:未删除,1:已删除]\n        }\n    ]\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "设备类型",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=devicetype.getlist"
      }
    ]
  },
  {
    "type": "post",
    "url": "device.add",
    "title": "添加设备",
    "name": "device_add",
    "group": "Device",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iClientID",
            "description": "<p>点检员ID</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sNumber",
            "description": "<p>设备编号</p>"
          },
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iDeviceTypeID",
            "description": "<p>设备类型ID</p>"
          },
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iNumber",
            "description": "<p>数量</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sName",
            "description": "<p>设备名称</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sLocation",
            "description": "<p>设备位置</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sModel",
            "description": "<p>设备型号</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sSpec",
            "description": "<p>设备规格</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sInstallDate",
            "description": "<p>安装日期</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sManufacturer",
            "description": "<p>生产厂家</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "iRepairDeptID",
            "description": "<p>维护公司ID</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "iUseDeptID",
            "description": "<p>使用单位ID</p>"
          },
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iCreateUnitID",
            "description": "<p>添加设备单位ID</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sProductionDate",
            "description": "<p>生产日期</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "iExpiredYears",
            "description": "<p>过期年限</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "iForciblyScrappedYears",
            "description": "<p>强制报废年限</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sRelDeviceIDs",
            "description": "<p>关联设备ID集</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iClientID\": 1,\n    \"sNumber\": \"1234567890\",\n    \"iDeviceTypeID\": 1,\n    \"iNumber\": 2,\n    \"sName\": \"灭火器\",\n    \"sLocation\": \"8栋1单元1楼楼梯间\",\n    \"sModel\": \"T090\",\n    \"sSpec\": \"b92\",\n    \"sInstallDate\": \"2018-01-01\",\n    \"sManufacturer\": \"成都市灭火设备厂\",\n    \"iRepairDeptID\": 2,\n    \"iUseDeptID\": 1,\n    \"iCreateUnitID\": 2,\n    \"sProductionDate\": \"2015-01-01\",\n    \"iExpiredYears\": 5,\n    \"iForciblyScrappedYears\": 8,\n    \"sRelDeviceIDs\": \"1,2\"\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"添加设备成功\",\n    \"data\": 1\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "设备",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=device.add"
      }
    ]
  },
  {
    "type": "post",
    "url": "device.clately",
    "title": "获取点检员最近添加的设备",
    "name": "device_clately",
    "group": "Device",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iClientID",
            "description": "<p>点检员ID</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iClientID\": 1\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取设备详情成功\",\n    \"data\": {\n        \"ID\": 1,            //设备ID\n        \"sNumber\": \"123456\",                    //设备编号\n        \"sDeviceTypeName\": \"灭火设备\",          //设备类型\n        \"iNumber\": 1,                           //设备数量\n        \"sName\": \"灭火器\",                     //设备名称\n        \"sLocation\": \"1楼楼梯间\",               //安装位置\n        \"sModel\": \"B90\",                       //设备型号\n        \"sSpec\": \"BMW-3\",                       //设备规格\n        \"sInstallDate\": \"2017-01-01\",           //安装日期\n        \"sManufacturer\": \"成都消防设备厂\",     //生产厂家\n        \"sQRCode\": \"http://xxx\",                //设备二维码\n        \"sRepairDeptName\": \"维修一厂\",          //维修单位名称\n        \"sUnitName\": \"电科十所\",                //使用单位名称\n        \"sClientName\": \"张三\",                    //责任人姓名\n        \"sProductionDate\": \"2015-10-10\",        //生产日期\n        \"iExpiredYears\": 5,                       //过期年限\n        \"iForciblyScrappedYears\": 8,            //强制报废年限\n        \"sLastInspectionDate\": \"2018-05-20\",    //上次巡检日期\n        \"sLastRepairDate\": \"2017-03-10\",        //上次维修时间\n        \"sLastChangeDate\": \"2018-01-01\"         //上次更换时间\n    }\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "设备",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=device.clately"
      }
    ]
  },
  {
    "type": "post",
    "url": "device.client.getDeviceParam",
    "title": "查询设备指标(移动端)",
    "name": "device_client_getDeviceParam",
    "group": "Device",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iClientID",
            "description": "<p>点检员ID</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "lm",
            "description": "<p>最后更新时间</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iClientID\": 1,\n\t\"lm\": \"\"\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"成功\",\n    \"data\": {\n        \"IsModified\": true,\n        \"IsAll\": true,\n        \"Data\": [\n            {\n                \"ID\": 17,\n                \"sName\": \"外观\",\n                \"iDeviceTypeID\": 12,\n                \"sDeviceTypeName\": null,\n                \"iUseDeptID\": 21,\n                \"sUnitName\": null,\n                \"dCreateTime\": \"2018-07-07T10:51:24.73\",\n                \"bIsDeleted\": false,\n                \"bIsSelected\": 0\n            }\n        ],\n        \"LastModifyTime\": \"2019/7/15 11:28:29\"\n    }\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "设备",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=device.client.getDeviceParam"
      }
    ]
  },
  {
    "type": "post",
    "url": "device.client.getlist",
    "title": "获取设备列表(移动端)",
    "name": "device_client_getlist",
    "group": "Device",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iClientID",
            "description": "<p>点检员ID</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "lm",
            "description": "<p>最后更新时间</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iClientID\": 1,\n\t\"lm\": \"\"\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取设备列表成功\",\n    \"data\": {\n        \"IsModified\": true,\n        \"IsAll\": true,\n        \"Data\": [\n            {\n                \"ID\": 11841,\n                \"sNumber\": \"2502T05-1\",\n                \"iDeviceTypeID\": 14,\n                \"sDeviceTypeName\": \"灭火器\",\n                \"iNumber\": 1,\n                \"sName\": \"干粉灭火器\",\n                \"sLocation\": \"25号楼2层通道（电梯口）\",\n                \"sModel\": \"MFZ/ABC4\",\n                \"sManufacturer\": \"龙威\",\n                \"iRepairDeptID\": 27,\n                \"sRepairDeptName\": null,\n                \"iUseDeptID\": 24,\n                \"sUnitName\": \"中国电子科技集团公司第十研究所\",\n                \"iClientID\": 31,\n                \"sClientName\": \"余茂\",\n                \"sClientPhone\": null,\n                \"sLastInspectionDate\": null,\n                \"sLastRepairDate\": null,\n                \"sLastChangeDate\": null,\n                \"iAbnormalStatus\": 0,\n                \"LastModifyTime\": \"2019-05-28T17:09:03.287\",\n                \"bIsDeleted\": 0\n            }\n        ],\n        \"LastModifyTime\": \"2019/7/15 11:25:46\"\n    }\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "设备",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=device.client.getlist"
      }
    ]
  },
  {
    "type": "post",
    "url": "device.get",
    "title": "获取设备详情",
    "name": "device_get",
    "group": "Device",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iDeviceID",
            "description": "<p>设备ID</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iDeviceID\": 1\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取设备详情成功\",\n    \"data\": {\n        \"ID\": 1,            //设备ID\n        \"sNumber\": \"123456\",                    //设备编号\n        \"sDeviceTypeName\": \"灭火设备\",          //设备类型\n        \"iNumber\": 1,                           //设备数量\n        \"sName\": \"灭火器\",                     //设备名称\n        \"sLocation\": \"1楼楼梯间\",               //安装位置\n        \"sModel\": \"B90\",                       //设备型号\n        \"sSpec\": \"BMW-3\",                       //设备规格\n        \"sInstallDate\": \"2017-01-01\",           //安装日期\n        \"sManufacturer\": \"成都消防设备厂\",     //生产厂家\n        \"sQRCode\": \"http://xxx\",                //设备二维码\n        \"sRepairDeptName\": \"维修一厂\",          //维修单位名称\n        \"sUnitName\": \"电科十所\",                //使用单位名称\n        \"sClientName\": \"张三\",                    //责任人姓名\n        \"sProductionDate\": \"2015-10-10\",        //生产日期\n        \"iExpiredYears\": 5,                       //过期年限\n        \"iForciblyScrappedYears\": 8,            //强制报废年限\n        \"sLastInspectionDate\": \"2018-05-20\",    //上次巡检日期\n        \"sLastRepairDate\": \"2017-03-10\",        //上次维修时间\n        \"sLastChangeDate\": \"2018-01-01\",        //上次更换时间\n        \"ParamList\": [                          //指标集合\n            {\n                \"ID\": 1,                        //指标ID\n                \"sName\": \"把手\"               //指标名称\n            }\n        ],\n        \"DetailList\": [],                       //上次巡检详情\n        \"DeviceList\": []                        //关联设备集合\n    }\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "设备",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=device.get"
      }
    ]
  },
  {
    "type": "post",
    "url": "device.getalllist",
    "title": "获取所有设备(过时)",
    "name": "device_getalllist",
    "group": "Device",
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取所有设备成功\",\n    \"data\": [\n        {\n            \"ID\": 1,            //设备ID\n            \"sNumber\": \"123456\",                    //设备编号\n            \"sName\": \"灭火器\",                     //设备名称\n            \"sLocation\": \"1楼楼梯间\",               //安装位置\n            \"sProductionDate\": \"2015-10-10\",        //生产日期\n            \"iClientID\": 1,                         //责任人ID\n            \"sClientName\": \"张三\",                    //责任人姓名\n            \"sUnitName\": \"电科十所\",                //单位名称\n            \"sOrganName\": \"综合部\",                //部门名称\n            \"iAbnormalStatus\": false,               //巡检结果[0:正常,1:异常]\n            \"dCreateTime\": \"2017-10-09T17:50:33.9\"  //创建时间\n        }\n    ]\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "设备",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=device.getalllist"
      }
    ]
  },
  {
    "type": "post",
    "url": "device.getlist",
    "title": "获取设备列表",
    "name": "device_getlist",
    "group": "Device",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iClientID",
            "description": "<p>点检员ID</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sUnitID",
            "description": "<p>单位ID</p>"
          },
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "page",
            "description": "<p>页码</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "iStatus",
            "description": "<p>查看状态[&quot;&quot;:全部,&quot;0&quot;:正常,&quot;1&quot;:异常]</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "bIsRecorded",
            "description": "<p>当月是否巡检[&quot;&quot;:全部,&quot;0&quot;:未巡检,&quot;1&quot;:已巡检]</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "bIsExpired",
            "description": "<p>是否超期[&quot;&quot;:全部,&quot;0&quot;:未超期,&quot;1&quot;:已超期]</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sName",
            "description": "<p>设备名称</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iClientID\": 1,\n    \"sUnitID\": \"0\",\n    \"page\": 2,\n    \"iStatus\": \"\",\n    \"bIsRecorded\": \"\",\n    \"bIsExpired\": \"\",\n    \"sName\": \"\"\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取设备列表成功\",\n    \"data\": [\n        {\n            \"ID\": 1,            //设备ID\n            \"sNumber\": \"123456\",                    //设备编号\n            \"sName\": \"灭火器\",                     //设备名称\n            \"sLocation\": \"1楼楼梯间\",               //安装位置\n            \"sProductionDate\": \"2015-10-10\",        //生产日期\n            \"iClientID\": 1,                         //责任人ID\n            \"sClientName\": \"张三\",                    //责任人姓名\n            \"sUnitName\": \"电科十所\",                //单位名称\n            \"sOrganName\": \"综合部\",                //部门名称\n            \"iAbnormalStatus\": false,               //巡检结果[0:正常,1:异常]\n            \"dCreateTime\": \"2017-10-09T17:50:33.9\"  //创建时间\n        }\n    ]\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "设备",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=device.getlist"
      }
    ]
  },
  {
    "type": "post",
    "url": "device.getlistbyunit",
    "title": "获取单位下辖设备列表",
    "name": "device_getlistbyunit",
    "group": "Device",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iUnitID",
            "description": "<p>使用单位ID</p>"
          },
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "page",
            "description": "<p>页码</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "iStatus",
            "description": "<p>查看状态[&quot;&quot;:全部,&quot;0&quot;:正常,&quot;1&quot;:异常]</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "bIsRecorded",
            "description": "<p>当月是否巡检[&quot;&quot;:全部,&quot;0&quot;:未巡检,&quot;1&quot;:已巡检]</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "bIsExpired",
            "description": "<p>是否超期[&quot;&quot;:全部,&quot;0&quot;:未超期,&quot;1&quot;:已超期]</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "bHasProductDate",
            "description": "<p>是否已设置生产日期超期[0:否,1:是]</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sName",
            "description": "<p>设备名称</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iUnitID\": 1,\n    \"page\": 2,\n    \"iStatus\": \"\",\n    \"bIsRecorded\": \"\",\n    \"bIsExpired\": \"\",\n    \"sName\": \"\"\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取单位下辖设备列表成功\",\n    \"data\": [\n        {\n            \"ID\": 1,            //设备ID\n            \"sNumber\": \"123456\",                    //设备编号\n            \"sName\": \"灭火器\",                     //设备名称\n            \"sLocation\": \"1楼楼梯间\",               //安装位置\n            \"sProductionDate\": \"2015-10-10\",        //生产日期\n            \"iClientID\": 1,                         //责任人ID\n            \"sClientName\": \"张三\",                    //责任人姓名\n            \"sUnitName\": \"电科十所\",                //单位名称\n            \"sOrganName\": \"综合部\",                //部门名称\n            \"iAbnormalStatus\": false,               //巡检结果[0:正常,1:异常]\n            \"dCreateTime\": \"2017-10-09T17:50:33.9\"  //创建时间\n        }\n    ]\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "设备",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=device.getlistbyunit"
      }
    ]
  },
  {
    "type": "post",
    "url": "device.param",
    "title": "获取新增设备需要的参数(过时)",
    "name": "device_param",
    "group": "Device",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iClientID",
            "description": "<p>点检员ID</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iClientID\": 1\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取新增设备需要的参数成功\",\n    \"data\": {\n        \"UseDeptList\": [                //使用单位集合\n            {\n                \"ID\": 1,                //使用单位ID\n                \"sName\": \"电科十所\"     //使用单位名称\n            }\n        ]\n    }\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "设备",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=device.param"
      }
    ]
  },
  {
    "type": "post",
    "url": "device.ulately",
    "title": "获取单位最近添加的设备",
    "name": "device_ulately",
    "group": "Device",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iUnitID",
            "description": "<p>单位ID</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iUnitID\": 1\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取设备详情成功\",\n    \"data\": {\n        \"ID\": 1,            //设备ID\n        \"sNumber\": \"123456\",                    //设备编号\n        \"sDeviceTypeName\": \"灭火设备\",          //设备类型\n        \"iNumber\": 1,                           //设备数量\n        \"sName\": \"灭火器\",                     //设备名称\n        \"sLocation\": \"1楼楼梯间\",               //安装位置\n        \"sModel\": \"B90\",                       //设备型号\n        \"sSpec\": \"BMW-3\",                       //设备规格\n        \"sInstallDate\": \"2017-01-01\",           //安装日期\n        \"sManufacturer\": \"成都消防设备厂\",     //生产厂家\n        \"sQRCode\": \"http://xxx\",                //设备二维码\n        \"sRepairDeptName\": \"维修一厂\",          //维修单位名称\n        \"sUnitName\": \"电科十所\",                //使用单位名称\n        \"sClientName\": \"张三\",                    //责任人姓名\n        \"sProductionDate\": \"2015-10-10\",        //生产日期\n        \"iExpiredYears\": 5,                       //过期年限\n        \"iForciblyScrappedYears\": 8,            //强制报废年限\n        \"sLastInspectionDate\": \"2018-05-20\",    //上次巡检日期\n        \"sLastRepairDate\": \"2017-03-10\",        //上次维修时间\n        \"sLastChangeDate\": \"2018-01-01\"         //上次更换时间\n    }\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "设备",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=device.ulately"
      }
    ]
  },
  {
    "type": "post",
    "url": "dutyrec.add",
    "title": "值班签到",
    "name": "dutyrec_add",
    "group": "DutyRecord",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iClientID",
            "description": "<p>前端人员ID</p>"
          },
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iDutyRoomID",
            "description": "<p>值班室ID</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iClientID\": 1,\n    \"iDutyRoomID\": 1\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"值班签到成功\",\n    \"data\": null\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "值班记录",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=dutyrec.add"
      }
    ]
  },
  {
    "type": "post",
    "url": "dutyrec.finish",
    "title": "值班签退",
    "name": "dutyrec_finish",
    "group": "DutyRecord",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iClientID",
            "description": "<p>值班人员ID</p>"
          },
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iDutyRoomID",
            "description": "<p>值班室ID</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sDesc",
            "description": "<p>值班情况描述</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sImageSrc",
            "description": "<p>值班图片</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iClientID\": 1,\n    \"iDutyRoomID\": 1,\n    \"sDesc\": \"OK\",\n    \"sImageSrc\": \"http://xxx\"\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"值班签退成功\",\n    \"data\": null\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "值班记录",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=dutyrec.finish"
      }
    ]
  },
  {
    "type": "post",
    "url": "dutyrec.get",
    "title": "获取值班记录详情",
    "name": "dutyrec_get",
    "group": "DutyRecord",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iDutyRecordID",
            "description": "<p>值班记录ID</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iDutyRecordID\": 1\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取值班记录详情成功\",\n    \"data\": {\n        \"ID\": 6,                    //主键ID\n        \"iClientID\": 4,             //值班人员ID\n        \"iDutyRoomID\": 3,           //值班室ID\n        \"sStartTime\": \"2018-05-31 11:26:00\",//值班开始时间\n        \"sEndTime\": \"\",                 //值班结束时间\n        \"iTimeLength\": 0,               //值班时长分钟数\n        \"sDesc\": \"\",                    //值班记录描述\n        \"sImageSrc\": \"\"                 //图片地址\n    }\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "值班记录",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=dutyrec.get"
      }
    ]
  },
  {
    "type": "post",
    "url": "dutyrec.getlist",
    "title": "获取值班记录列表",
    "name": "dutyrec_getlist",
    "group": "DutyRecord",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iClientID",
            "description": "<p>值班人员ID</p>"
          },
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "page",
            "description": "<p>页码</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iClientID\": 1,\n    \"page\": 1\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取值班记录列表成功\",\n    \"data\": {\n        \"iTotalTimeLength\": 640,        //本月值班总分钟数\n        \"DataList\": [{                  //值班记录集合\n            \"ID\": 6,                    //主键ID\n            \"iClientID\": 4,             //值班人员ID\n            \"sPhone\": \"13900138621\",    //值班人员联系电话\n            \"sClientName\": \"赵柳\",      //值班人员姓名\n            \"iDutyRoomID\": 3,           //值班室ID\n            \"sDutyRoomName\": \"六一节值班室\",//值班室名称\n            \"sUnitName\": \"电科十所\",        //值班室所属单位\n            \"sStartTime\": \"2018-05-31 11:26:00\",//值班开始时间\n            \"sEndTime\": \"\",                 //值班结束时间\n            \"iTimeLength\": 0,               //值班时长分钟数\n            \"sDesc\": \"\",                    //值班记录描述\n            \"sImageSrc\": \"\"                 //图片地址\n        }]\n    }\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "值班记录",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=dutyrec.getlist"
      }
    ]
  },
  {
    "type": "post",
    "url": "dutyrec.getstatus",
    "title": "获取签到状态",
    "name": "dutyrec_getstatus",
    "group": "DutyRecord",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iDutyRoomID",
            "description": "<p>值班室ID</p>"
          },
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iClientID",
            "description": "<p>值班人员ID</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iDutyRoomID\": 1,\n    \"iClientID\": 1\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取签到状态成功\",\n    \"data\": 0       // 签到状态[0:未签到,1:已签到]\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "值班记录",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=dutyrec.getstatus"
      }
    ]
  },
  {
    "type": "post",
    "url": "duty.getlist",
    "title": "获取值班列表",
    "name": "duty_getlist",
    "group": "Duty",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iClientID",
            "description": "<p>前端人员ID</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iClientID\": 1\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取值班列表成功\",\n    \"data\": [\n        {\n            \"ID\": 1,                                    //单位ID\n            \"sPhone\": \"13800138001\",                    //单位账号\n            \"sName\": \"电科十所\",                        //单位名称\n            \"sAddress\": \"四川省成都市金牛区营康西路\",    //单位地址\n            \"sAdminName\": \"李大壮\",                    //管理员姓名\n            \"sContact\": \"13812138321\",                  //管理员联系方式\n            \"sLegalPerson\": \"\",                         //法人\n            \"sQualifications\": \"\",                      //资质\n            \"iType\": 0,                                 //单位类型[0:使用单位,1:消防部门,2:维护公司]\n            \"sCredentials\": \"http://xxx\",               //证件照\n            \"iStatus\": false,                           //状态[0:正常,1:已冻结]\n            \"DeptList\": [                               //部门集合\n                {\n                    \"ID\": 1,                            //部门ID\n                    \"sName\": \"综合部\",                 //部门名称\n                    \"ClientList\": [                     //部门下辖人员集合\n                        {\n                            \"ID\": 1,                    //人员ID\n                            \"sPhone\": \"13800138000\",    //电话\n                            \"sName\": \"张三\",              //姓名\n                            \"sImageSrc\": \"https://xxx\",   //头像\n                            \"iType\": 0                  //用户类别[0:点检员,1:消防,2:维护公司,3:使用公司,4:值班人员]\n                        }\n                    ]\n                }\n            ],\n            \"ClientList\": [                             //值班人员列表\n                {\n                    \"ID\": 4,                            //值班人ID\n                    \"sPhone\": \"13900138621\",            //值班人电话\n                    \"sName\": \"赵柳\",                    //值班人姓名\n                    \"sImageSrc\": \"\",                    //头像\n                    \"iStatus\": false,                   //状态[0:正常,1:已冻结]\n                    \"sCredentials\": null,               //证件照\n                    \"iUnitID\": 1,                       //归属单位ID\n                    \"iType\": 4                          //用户类别[0:点检员,1:消防,2:维护公司,3:使用公司,4:值班人员]\n                }\n            ]\n        }\n    ]\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "值班",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=duty.getlist"
      }
    ]
  },
  {
    "type": "post",
    "url": "fb.add",
    "title": "提交意见反馈",
    "name": "fb_add",
    "group": "Feedback",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iClientID",
            "description": "<p>用户ID</p>"
          },
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iUnitID",
            "description": "<p>单位ID</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sTitle",
            "description": "<p>标题</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sContent",
            "description": "<p>内容</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sImageSrc",
            "description": "<p>图片</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iClientID\": 5,\n    \"iUnitID\": 5,\n    \"sTitle\": \"标题\",\n    \"sContent\": \"内容\",\n    \"sImageSrc\": \"http://xxx,http://xxx\"\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"提交意见反馈成功\",\n    \"data\": null\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "意见反馈",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=fb.add"
      }
    ]
  },
  {
    "type": "post",
    "url": "fb.delete",
    "title": "删除反馈",
    "name": "fb_delete",
    "group": "Feedback",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iFeedbackID",
            "description": "<p>意见反馈ID</p>"
          },
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iClientID",
            "description": "<p>用户ID</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iFeedbackID\": 2,\n    \"iClientID\": 1\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"删除反馈成功\",\n    \"data\": null\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "意见反馈",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=fb.delete"
      }
    ]
  },
  {
    "type": "post",
    "url": "fb.get",
    "title": "获取反馈详情",
    "name": "fb_get",
    "group": "Feedback",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iFeedbackID",
            "description": "<p>意见反馈ID</p>"
          },
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iClientID",
            "description": "<p>用户ID</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iFeedbackID\": 2,\n    \"iClientID\": 1\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取反馈详情成功\",\n    \"data\": {\n        \"ID\": 1,                                    //评论ID\n        \"iClientID\": 2,                             //发帖人ID\n        \"sTitle\": \"标题\",                             //标题\n        \"sContent\": \"内容\",                           //内容\n        \"sImageSrc\": \"http://xxx,http://xxx\",       //评论图片\n        \"bIsReplyStatus\": false,                    //回复状态[0:未回复,1:已回复]\n        \"dCreateTime\": \"2017-10-20T16:50:34.743\",   //评论时间\n        \"bIsDeleted\": false                         //删除标识[0:未删除,1:已删除]\n    }\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "意见反馈",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=fb.get"
      }
    ]
  },
  {
    "type": "post",
    "url": "fb.getlist",
    "title": "获取反馈列表",
    "name": "fb_getlist",
    "group": "Feedback",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iClientID",
            "description": "<p>用户ID</p>"
          },
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "page",
            "description": "<p>页码</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iClientID\": 2,\n    \"page\": 1\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取反馈列表成功\",\n    \"data\": [\n        {\n            \"ID\": 1,                                    //评论ID\n            \"iClientID\": 2,                             //发帖人ID\n            \"sTitle\": \"标题\",                             //标题\n            \"sContent\": \"内容\",                           //内容\n            \"sImageSrc\": \"http://xxx,http://xxx\",       //评论图片\n            \"bIsReplyStatus\": false,                    //回复状态[0:未回复,1:已回复]\n            \"dCreateTime\": \"2017-10-20T16:50:34.743\",   //评论时间\n            \"bIsDeleted\": false                         //删除标识[0:未删除,1:已删除]\n        }\n    ]\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "意见反馈",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=fb.getlist"
      }
    ]
  },
  {
    "type": "post",
    "url": "ins.add",
    "title": "添加巡检记录",
    "name": "ins_add",
    "group": "Inspection",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iClientID",
            "description": "<p>点检员ID</p>"
          },
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iDeviceID",
            "description": "<p>设备ID</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sParamList",
            "description": "<p>参数和状态集合</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sDesc",
            "description": "<p>说明备注</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iClientID\": 1,\n    \"iDeviceID\": 2,\n    \"sParamList\": \"[{ iDeviceParamID: 1, iStatus: 0 }, { iDeviceParamID: 2, iStatus: 1 }]\",\n    \"sDesc\": \"巡检完毕\"\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"添加巡检记录成功\",\n    \"data\": null\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "巡检",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=ins.add"
      }
    ]
  },
  {
    "type": "post",
    "url": "ins.adds",
    "title": "批量添加巡检记录",
    "name": "ins_adds",
    "group": "Inspection",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iClientID",
            "description": "<p>点检员ID</p>"
          },
          {
            "group": "Parameter",
            "type": "array",
            "optional": false,
            "field": "sRecordList",
            "description": "<p>巡检设备及参数集合</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iClientID\": 1,\n    \"sRecordList\": [{ iDeviceID: 1, DetailList: [{ iDeviceParamID: 1, iStatus: 0 }, { iDeviceParamID: 2, iStatus: 1 }], sDesc: \"巡检好了\" }]\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"添加巡检记录成功\",\n    \"data\": null\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "巡检",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=ins.adds"
      }
    ]
  },
  {
    "type": "post",
    "url": "ins.get",
    "title": "获取巡检记录详情",
    "name": "ins_get",
    "group": "Inspection",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iInspectionRecordID",
            "description": "<p>巡检记录ID</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iInspectionRecordID\": 1\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取巡检记录详情成功\",\n    \"data\": {\n        \"ID\": 1,            //巡检记录ID\n        \"iDeviceID\": 1,                         //设备ID\n        \"iClientID\": 2,                         //巡检人ID\n        \"iStatus\": false,                       //巡检结果[0:正常,1:异常]\n        \"dCreateTime\": \"2017-10-09T17:50:33.9\", //创建时间\n        \"sDesc\": \"备注\"                         //备注\n    }\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "巡检",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=ins.get"
      }
    ]
  },
  {
    "type": "post",
    "url": "ins.getlist",
    "title": "获取巡检记录列表",
    "name": "ins_getlist",
    "group": "Inspection",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iDeviceID",
            "description": "<p>设备ID</p>"
          },
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "page",
            "description": "<p>页码</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iDeviceID\": 1,\n    \"page\": 2\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取巡检记录列表成功\",\n    \"data\": [\n        {\n            \"ID\": 1,            //巡检记录ID\n            \"iDeviceID\": 1,                         //设备ID\n            \"iClientID\": 2,                         //巡检人ID\n            \"iStatus\": false,                       //巡检结果[0:正常,1:异常]\n            \"dCreateTime\": \"2017-10-09T17:50:33.9\", //创建时间\n            \"sDesc\": \"备注\"                         //备注\n        }\n    ]\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "巡检",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=ins.getlist"
      }
    ]
  },
  {
    "type": "post",
    "url": "repair.add",
    "title": "添加维修记录",
    "name": "repair_add",
    "group": "Repair",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iClientID",
            "description": "<p>点检员ID</p>"
          },
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iDeviceID",
            "description": "<p>设备ID</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sDesc",
            "description": "<p>说明备注</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iClientID\": 1,\n    \"iDeviceID\": 2,\n    \"sDesc\": \"维修完毕，重新绑定螺栓。\"\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"添加维修记录成功\",\n    \"data\": null\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "维修",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=repair.add"
      }
    ]
  },
  {
    "type": "post",
    "url": "repair.getlist",
    "title": "获取维修记录列表",
    "name": "repair_getlist",
    "group": "Repair",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iDeviceID",
            "description": "<p>设备ID</p>"
          },
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "page",
            "description": "<p>页码</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iDeviceID\": 1,\n    \"page\": 2\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取维修记录列表成功\",\n    \"data\": [\n        {\n            \"ID\": 1,            //维修记录ID\n            \"iDeviceID\": 1,                         //设备ID\n            \"iClientID\": 2,                         //维修人ID\n            \"dCreateTime\": \"2017-10-09T17:50:33.9\", //创建时间\n            \"sDesc\": \"备注\"                         //备注\n        }\n    ]\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "维修",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=repair.getlist"
      }
    ]
  },
  {
    "type": "post",
    "url": "settings.get",
    "title": "获取基础设置信息",
    "name": "settings_get",
    "group": "Settings",
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取基础设置成功\",\n    \"data\": {\n        \"sPhone\": \"13800138000\",        //平台客服电话\n        \"sImageSrc1\": \"13800138000\",    //轮播1图片地址\n        \"sLink1\": \"http://xxx\",         //轮播1链接地址\n        \"sArticleType1\": \"5\",           //轮播1文章类别[0:法律知识,1:消防知识,2:资讯,3:帮助,4:关于我们,5:无]\n        \"sImageSrc2\": \"13800138000\",    //轮播2图片地址\n        \"sLink2\": \"\",                   //轮播2链接地址\n        \"sArticleType2\": \"\",            //轮播2文章类别[0:法律知识,1:消防知识,2:资讯,3:帮助,4:关于我们,5:无]\n        \"sImageSrc3\": \"13800138000\",    //轮播3图片地址\n        \"sLink3\": \"http://xxx\",         //轮播3链接地址\n        \"sArticleType3\": \"1\",           //轮播3文章类别[0:法律知识,1:消防知识,2:资讯,3:帮助,4:关于我们,5:无]\n        \"sImageSrc4\": \"13800138000\",    //轮播4图片地址\n        \"sLink4\": \"http://xxx\",         //轮播4链接地址\n        \"sArticleType4\": \"2\",           //轮播4文章类别[0:法律知识,1:消防知识,2:资讯,3:帮助,4:关于我们,5:无]\n        \"sImageSrc5\": \"13800138000\",    //轮播5图片地址\n        \"sLink5\": \"\",                   //轮播5链接地址\n        \"sArticleType5\": \"5\"            //轮播5文章类别[0:法律知识,1:消防知识,2:资讯,3:帮助,4:关于我们,5:无]\n    }\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "基础设置",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=settings.get"
      }
    ]
  },
  {
    "type": "post",
    "url": "msg.delete",
    "title": "删除站内信",
    "name": "msg_delete",
    "group": "SiteMsg",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iSiteMsgID",
            "description": "<p>站内信ID</p>"
          },
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iClientID",
            "description": "<p>用户ID</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iSiteMsgID\": 1,\n    \"iClientID\": 1\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"删除站内信成功\",\n    \"data\": null\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "站内信",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=msg.delete"
      }
    ]
  },
  {
    "type": "post",
    "url": "msg.get",
    "title": "获取站内信详情",
    "name": "msg_get",
    "group": "SiteMsg",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iSiteMsgID",
            "description": "<p>站内信ID</p>"
          },
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iClientID",
            "description": "<p>用户ID</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iSiteMsgID\": 1,\n    \"iClientID\": 1\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取站内信详情成功\",\n    \"data\": {\n        \"ID\": 1,                                        //站内信ID\n        \"sTitle\": \"站内信123\",                         //站内信标题\n        \"sContent\": \"balabalabalabalabala\",             //站内信内容\n        \"iType\": 1,                                     //推送类别[0:单位,1:个人]\n        \"sReceiveDept\": \"1,2,3\",                        //收信人身份\n        \"sReceiveClient\": \"13800138000,13800138001\",    //收信人账号\n        \"sOperator\": \"admin\",                           //发送人\n        \"dCreateTime\": \"2017-10-11T14:00:04.52\",        //创建时间\n        \"bIsDeleted\": false                             //删除标识[0:未删除,1:已删除]\n    }\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "站内信",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=msg.get"
      }
    ]
  },
  {
    "type": "post",
    "url": "msg.getlist",
    "title": "获取用户站内信列表",
    "name": "msg_getlist",
    "group": "SiteMsg",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iClientID",
            "description": "<p>用户ID</p>"
          },
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "page",
            "description": "<p>页码</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iClientID\": 1,\n    \"page\": 1\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取用户站内信列表成功\",\n    \"data\": [\n        {\n            \"ID\": 1,                                        //站内信ID\n            \"sTitle\": \"站内信123\",                         //站内信标题\n            \"sContent\": \"balabalabalabalabala\",             //站内信内容\n            \"iType\": 1,                                     //推送类别[0:单位,1:个人]\n            \"sReceiveDept\": \"1,2,3\",                        //收信人身份\n            \"sReceiveClient\": \"13800138000,13800138001\",    //收信人账号\n            \"sOperator\": \"admin\",                           //发送人\n            \"dCreateTime\": \"2017-10-11T14:00:04.52\",        //创建时间\n            \"bIsDeleted\": false,                            //删除标识[0:未删除,1:已删除]\n            \"bIsReaded\": false                              //阅读标识[0:已阅读,1:未阅读]\n        }\n    ]\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "站内信",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=msg.getlist"
      }
    ]
  },
  {
    "type": "post",
    "url": "msg.unreadcount",
    "title": "获取未读站内信条数",
    "name": "msg_unreadcount",
    "group": "SiteMsg",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iClientID",
            "description": "<p>用户ID</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iClientID\": 5\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取用户未读的站内信条数成功\",\n    \"data\": 1\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "站内信",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=msg.unreadcount"
      }
    ]
  },
  {
    "type": "post",
    "url": "sms.findcode",
    "title": "发送找回登码验证码",
    "name": "sms_findcode",
    "group": "Sms",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sPhone",
            "description": "<p>手机号</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"sPhone\": \"13800138000\"\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息 ",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"短信发送成功\",\n    \"data\": \"61379\"\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "短信",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=sms.findcode"
      }
    ]
  },
  {
    "type": "post",
    "url": "sms.regcode",
    "title": "发送注册短信验证码",
    "name": "sms_regcode",
    "group": "Sms",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sPhone",
            "description": "<p>手机号</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"sPhone\": \"13800138000\"\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息 ",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"短信发送成功\",\n    \"data\": \"61379\"\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "短信",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=sms.regcode"
      }
    ]
  },
  {
    "type": "post",
    "url": "unit.get",
    "title": "获取单位详情",
    "name": "unit_get",
    "group": "Unit",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iUnitID",
            "description": "<p>单位ID</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iUnitID\": 1\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取单位详情成功\",\n    \"data\": {\n        \"ID\": 2,        //主键ID\n        \"sPhone\": \"13800138000\",       //单位账号\n        \"sName\": \"成都消防中队\",       //单位名称\n        \"sAddress\": \"成都花牌坊街\",   //单位地址\n        \"sAdminName\": \"张三\",         //单位管理员姓名\n        \"sContact\": \"13800138001\",      //管理员联系电话\n        \"sLegalPerson\": \"李四\",       //法人\n        \"sQualifications\": \"一级\",    //资质\n        \"iType\": 0,              //单位类型[0:使用单位,1:消防部门,2:维护公司]\n        \"sCredentials\": \"http://xxxx\"     //证件照\n    }\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "平台单位",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=unit.get"
      }
    ]
  },
  {
    "type": "post",
    "url": "unit.getlist",
    "title": "获取单位列表",
    "name": "unit_getlist",
    "group": "Unit",
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取单位列表成功\",\n    \"data\": [\n        {\n            \"ID\": 1,                                        // 单位ID\n            \"sName\": \"电科十所\",                            // 单位名称\n            \"sAddress\": \"四川省成都市金牛区营康西路\",        // 单位地址\n            \"iType\": 0,                                     // 单位类型[0:使用单位,1:消防部门,2:维护公司]\n            \"iStatus\": false,                               // 状态[0:正常,1:已冻结]\n            \"bIsDeleted\": false,                            // 删除标识[0:未删除,1:已删除]\n            \"DeptList\": [                                   // 部门集合\n                {\n                    \"ID\": 1,                                // 部门ID\n                    \"iUseDeptID\": 1,                        // 归属单位ID\n                    \"sName\": \"综合部\",                     // 部门名称\n                    \"bIsDeleted\": false                     // 删除标识[0:未删除,1:已删除]\n                },\n                {\n                    \"ID\": 2,                                // 部门ID\n                    \"iUseDeptID\": 1,                        // 归属单位ID\n                    \"sName\": \"项目管理部\",                   // 部门名称\n                    \"bIsDeleted\": false                     // 删除标识[0:未删除,1:已删除]\n                }\n            ]\n        }\n    ]\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "平台单位",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=unit.getlist"
      }
    ]
  },
  {
    "type": "post",
    "url": "unit.getrellist",
    "title": "获取使用单位列表(过时)",
    "name": "unit_getrellist",
    "group": "Unit",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iClientID",
            "description": "<p>用户ID</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iClientID\": 1\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取单位列表成功\",\n    \"data\": [\n        {\n            \"ID\": 1,                                        // 单位ID\n            \"sName\": \"电科十所\",                            // 单位名称\n            \"sAddress\": \"四川省成都市金牛区营康西路\",        // 单位地址\n            \"iType\": 0,                                     // 单位类型[0:使用单位,1:消防部门,2:维护公司]\n            \"iStatus\": false,                               // 状态[0:正常,1:已冻结]\n            \"bIsDeleted\": false                             // 删除标识[0:未删除,1:已删除]\n        }\n    ]\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "平台单位",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=unit.getrellist"
      }
    ]
  },
  {
    "type": "post",
    "url": "unit.getrprlist",
    "title": "获取使用单位已关联的维护公司列表",
    "name": "unit_getrprlist",
    "group": "Unit",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iUnitID",
            "description": "<p>单位ID</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"iUnitID\": 1\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"获取使用单位已关联的维护公司列表成功\",\n    \"data\": [\n        {\n            \"ID\": 1,                                        // 维护公司ID\n            \"sName\": \"电科十所\",                            // 维护公司名称\n            \"sAddress\": \"四川省成都市金牛区营康西路\",        // 维护公司地址\n            \"iType\": 0,                                     // 单位类型[0:使用单位,1:消防部门,2:维护公司]\n            \"iStatus\": false,                               // 状态[0:正常,1:已冻结]\n            \"bIsDeleted\": false                             // 删除标识[0:未删除,1:已删除]\n        }\n    ]\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "平台单位",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=unit.getrprlist"
      }
    ]
  },
  {
    "type": "post",
    "url": "unit.register",
    "title": "单位账号注册",
    "name": "unit_register",
    "group": "Unit",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sPhone",
            "description": "<p>手机号</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sValidate",
            "description": "<p>验证码</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sPwd",
            "description": "<p>登录密码（MD5加密）</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sName",
            "description": "<p>单位名称</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sAddress",
            "description": "<p>单位地址</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sAdminName",
            "description": "<p>平台管理员</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sContact",
            "description": "<p>联系电话</p>"
          },
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "iType",
            "description": "<p>单位类型[0:使用单位,1:消防部门,2:维护公司]</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sCredentials",
            "description": "<p>证件照</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sLegalPerson",
            "description": "<p>法人（类型为使用单位和消防部门时允许为空）</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "sQualifications",
            "description": "<p>资格证明（类型为使用单位和消防部门时允许为空）</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "请求样例",
          "content": "{\n    \"sPhone\": \"13800138000\",\n    \"sValidate\": \"1234\",\n    \"sPwd\": \"QWERTYUIOPASDFGHJKLZXCVBNM\",\n    \"sName\": \"消防大队\",\n    \"sAddress\": \"成都市花牌坊街\",\n    \"sAdminName\": \"张三\",\n    \"sContact\": \"13800138000\",\n    \"iType\": 1,\n    \"sCredentials\": \"http://xxx,http://xxx\",\n    \"sLegalPerson\": \"\",\n    \"sQualifications\": \"\"\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "examples": [
        {
          "title": "返回信息",
          "content": "{\n    \"success\": true,\n    \"code\": 0,\n    \"message\": \"注册成功\",\n    \"data\": 3\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "./apidata.js",
    "groupTitle": "平台单位",
    "sampleRequest": [
      {
        "url": "http://114.115.250.165/router/ApiTest?method=unit.register"
      }
    ]
  }
] });
