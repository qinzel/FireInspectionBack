$(function () { 
    var loginData = {
        sLoginName: '',
        sPassword: ''
    };
    var userPwd = $("#user-pwd"),
        btnLogin = $('#btn-login'),
        tipMsg = $('#tip-msg'),
        userName = $('#user-name');
    function userLogin() {
        tipMsg.text('');
        loginData.sPassword = userPwd.val();
        if (loginData.sPassword.length < 1) {
            tipMsg.text('请输入登录密码');
            return;
        }
        loginData.sLoginName = userName.val();
        if (loginData.sLoginName.length < 1) {
            tipMsg.text('请输入登录用户名');
            return;
        }
        btnLogin.attr('disabled', 'disabled'); 
        $.ajax({
            type: "POST",
            url: "/Admin/User/Login",
            data: loginData,
            dataType: "json",
            success: function (res) {
                if (res.success) {
                    tipMsg.text('');
                    btnLogin.text('登录成功，跳转中');
                    window.location.href = '/Admin/Home';
                }
                else {
                    tipMsg.text('提示:' + res.message );
                    btnLogin.removeAttr('disabled');
                }
            },
            error: function (xhr, msg, e) {
                tipMsg.text('提示:登录异常');
                btnLogin.removeAttr('disabled');
            }
        });
    }
    btnLogin.click(function () {
        userLogin();
    });
    $(document).keydown(function (e) {
        if (e.keyCode == 13) {
            e.preventDefault(); //阻止冒泡
            userLogin();
            return;
        }
    });
});