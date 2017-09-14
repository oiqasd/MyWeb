//注册页面
$(document).ready(function () {
    $("#login-btn").click(function () {window.location.href = '/login.html';});
    $("#reg_verification_img").click(function () { $("#reg_verification_img").attr("src", "server/CheckCode.aspx?id=" + Math.random()); })
    $("#txtVerifyCode").blur(function () {var reg_verification = $(this).val();var errspan = $("#helpinfo");if (!valVerification(reg_verification, errspan)) {$(this).val("");}})
});
function check() {

    var obj;
    var errspan = $("#helpinfo");
    //用户名
    obj = $("#username");
    if (!valUserName(obj.val(), errspan)) {
        obj.focus();
        return false;
    }

    //密码
    obj = $("#password"); 
    if (!valPasswd(obj.val(), errspan)) {
        obj.focus();
        return false;
    }

    //验证码
    obj = $("#txtVerifyCode");
    if (!valVerification(obj.val(), errspan)) {
        obj.val("");
        obj.focus();
        return false;
    }
    return true;
}