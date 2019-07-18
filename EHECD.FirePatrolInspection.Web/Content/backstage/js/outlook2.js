$(function() {
    InitLeftMenu();
    tabClose();
    tabCloseEven();
});

//初始化左侧
function InitLeftMenu() {

    //$(".easyui-accordion").empty();
    //

    $.each(_menus.menus, function (i, n) {
        var bigmenu = $('#topmenu').accordion('add', {
            title: n.menuname,
            multiple: false,
            iconCls: 'icon-edit',
            selected: false
        });
        $.each(n.menus, function (j, o) {
            if (o.bIsLink == "True") {
                var menulist = '<div class="zxhmenu"><a target="mainFrame" data-href="' + o.url + '" >' + o.menuname + '</a></div>';
                bigmenu.children().last().find(".accordion-body").first().append(menulist);
            } else {
                var secmenuhtml = '<div class="zxhtitle" id="secmenu_' + o.menuid + '">';
                bigmenu.children().last().find(".accordion-body").first().append(secmenuhtml);
                $('#secmenu_' + o.menuid).accordion({
                    multiple: false,
                    selected: false,
                    border:true
                });
                var secmenu = $('#secmenu_' + o.menuid).accordion('add', {
                    title: o.menuname,
                    multiple: false,
                    selected: false,
                    border:false
                });
                var thirdmenulist = '';
                $.each(o.menus, function (k, p) {
                    thirdmenulist += '<div class="zxhmenu"><a target="mainFrame" data-href="' + p.url + '" >' + p.menuname + '</a></div>';
                });
                secmenu.children().last().find(".accordion-body").append(thirdmenulist);
            }
        });
    });

    //$(".easyui-accordion").append(menulist);

    $('.accordion-body .zxhmenu').click(function () {
        var tabTitle = $(this).text();
        var url = $(this).find("a").attr("data-href");
        addTab(tabTitle, url);
        $('.accordion-body div').removeClass("selected");
        $(this).addClass("selected");
    }).hover(function () {
        $(this).addClass("hover");
    }, function () {
        $(this).removeClass("hover");
    });
    //$(".easyui-accordion").accordion();
}

function addTab(subtitle, url) {
    if (!$('#tabs').tabs('exists', subtitle)) {
        $('#tabs').tabs('add', {
            title: subtitle,
            content: createFrame(url),
            closable: true,
            width: $('#mainPanle').width() - 10,
            height: $('#mainPanle').height() - 26,
            tools: [{
                iconCls: 'icon-mini-refresh',
                handler: function () {
                    var i = $(this).parent().parent().index(".tabs li");
                    var tab = $('#tabs').tabs('getTab', i);
                    tab.find("iframe").get(0).contentWindow.location.reload(true);
                }
            }]
        });
    } else {
        $('#tabs').tabs('select', subtitle);
    }
    tabClose();
}

function createFrame(url) {
    var s = '<iframe name="mainFrame" scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:99.7%;"></iframe>';
    return s;
}

function tabClose() {
    /*双击关闭TAB选项卡*/
    $(".tabs-inner").dblclick(function () {
        var subtitle = $(this).children("span").text();
        $('#tabs').tabs('close', subtitle);
    })

    $(".tabs-inner").bind('contextmenu', function (e) {
        $('#mm').menu('show', {
            left: e.pageX,
            top: e.pageY,
        });

        var subtitle = $(this).children("span").text();
        $('#mm').data("currtab", subtitle);

        return false;
    });
}
//绑定右键菜单事件
function tabCloseEven() {
    //关闭当前
    $('#mm-tabclose').click(function () {
        var currtab_title = $('#mm').data("currtab");
        $('#tabs').tabs('close', currtab_title);
    })
    //全部关闭
    $('#mm-tabcloseall').click(function () {
        $('.tabs-inner span').each(function (i, n) {
            var t = $(n).text();
            $('#tabs').tabs('close', t);
        });
    });
    //关闭除当前之外的TAB
    $('#mm-tabcloseother').click(function () {
        var currtab_title = $('#mm').data("currtab");
        $('.tabs-inner span').each(function (i, n) {
            var t = $(n).text();
            if (t != currtab_title)
                $('#tabs').tabs('close', t);
        });
    });
    //关闭当前右侧的TAB
    $('#mm-tabcloseright').click(function () {
        var nextall = $('.tabs-selected').nextAll();
        if (nextall.length == 0) {
            //msgShow('系统提示','后边没有啦~~','error');
            alert('后边没有啦~~');
            return false;
        }
        nextall.each(function (i, n) {
            var t = $('a:eq(0) span', $(n)).text();
            $('#tabs').tabs('close', t);
        });
        return false;
    });
    //关闭当前左侧的TAB
    $('#mm-tabcloseleft').click(function () {
        var prevall = $('.tabs-selected').prevAll();
        if (prevall.length == 0) {
            alert('到头了，前边没有啦~~');
            return false;
        }
        prevall.each(function (i, n) {
            var t = $('a:eq(0) span', $(n)).text();
            $('#tabs').tabs('close', t);
        });
        return false;
    });
}


