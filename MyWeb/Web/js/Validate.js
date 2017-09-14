
//验证手机号码
function valMobile(reg_tel,errspan) {
    var count = 0;

    var reg1 = /^1[3|4|5|6|7|8]\d{9}$/g;
    var reg2 = /^1[3|4|5|6|7|8]\d{9}$/g;

    if (reg_tel.length == 0) {
        //errspan.removeClass("valid");
        //errspan.removeClass("valid-error");
        //errspan.addClass("valid-error");
        errspan.css("color", "Red");
        errspan.html("请输入11位手机号码");
      
        count = 1;
    } else if (!reg1.test(reg_tel) && !(reg2.test(reg_tel))) {
        //errspan.removeClass("valid");
        //errspan.removeClass("valid-error");
        //errspan.addClass("valid-error");
        errspan.css("color", "Red");
        errspan.html("请输入正确的手机号码");
      
        count = 1;
    } else {
        $.ajax({
            url: "server/Users.ashx",
            type: "POST",
            data: "abc=ExistsMobile&moblie=" + reg_tel,
            async: false,
            success: function (msg) {
                if (msg == "True") {
                    //errspan.removeClass("valid");
                    //errspan.removeClass("valid-error");
                    //errspan.addClass("valid-error");
                    errspan.css("color", "Red");
                    errspan.html("手机号码已存在");
                    errspan.show();
                    count = 1;
                } else {
                    //errspan.removeClass("valid");
                    //errspan.removeClass("valid-error");
                    //errspan.addClass("valid");
                    errspan.css("color", "#000000");
                    errspan.html("该手机号码可用");
                    
                    count = 0;
                }
            }
        });
    }
    errspan.show();
    if (count == 0) {
        return true;
    } else {
        return false;
    }
}

//验证邮箱
function valEmail(reg_email,errspan) {
    var count = 1;
    //格式验证   reVal = /^[\-!#\$%&'\*\+\\\.\/0-9=\?A-Z\^_`a-z{|}~]+@[\-!#\$%&'\*\+\\\.\/0-9=\?A-Z\^_`a-z{|}~]+(\.[\-!#\$%&'\*\+\\\.\/0-9=\?A-Z\^_`a-z{|}~]+)+$/; 
    var yz = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;
    if (reg_email.length == 0) {
        //errspan.removeClass("valid");
        //errspan.removeClass("valid-error");
        //errspan.addClass("valid-error");
        errspan.css("color", "Red");
        errspan.html("请填写邮箱"); 
        count = 1;
    }
    else if (!yz.test(reg_email)) {
        //errspan.removeClass("valid");
        //errspan.removeClass("valid-error");
        //errspan.addClass("valid-error");
        errspan.css("color", "Red");
        errspan.html("邮箱格式不正确"); 
        count = 1;
    }
    else {
        //邮箱是否存在验证
        $.ajax({
            url: "server/Users.ashx",
            type: "POST",
            data: "abc=ExistsEmail&Email=" + reg_email,
            async: false,
            success: function (msg) {
                if (msg == "True") {
                    //存在
                    errspan.html(" 邮箱已存在");
                    count = 1;
                }
                else {
                    //不存在
                    errspan.html("邮箱可用");
                    errspan.css("color", "black");
                    count = 0;
                }
            }
        });
    }
    if (count == 0) {
        errspan.hide();
        return true;
    } else {
        errspan.show();
        return false;
    }
}

//验证用户名
function valUserName(reg_userName,errspan) {
    var count = 1;
    var reg1 = /[^a-zA-Z0-9_\u4e00-\u9fa5]/g; ///[^a-zA-Z0-9]/g;
    if (reg_userName.length < 6 || reg_userName.length > 30) { 
        //if (CharLen(reg_userName) < 6 || CharLen(reg_userName) > 12) {
        //errspan.removeClass("valid");
        //errspan.removeClass("valid-error");
        //errspan.addClass("valid-error");
        errspan.css("color", "Red");
        errspan.html("请输入6-30个字符的用户名");
        
        count = 1;
    } else if (reg1.test(reg_userName)) {
        //errspan.removeClass("valid");
        //errspan.removeClass("valid-error");
        //errspan.addClass("valid-error");
        //errspan.css("color", "Red");
        //errspan.html("由数字.字母.汉字.下划线组成"); 
        //count = 1;
    } else {
        $.ajax({
            url: "server/Users.ashx",
            type: "POST",
            data: "abc=ExistsUserName&UserName=" + reg_userName,
            async: false,
            success: function (msg) {
                if (msg == "True") {
                    //errspan.removeClass("valid");
                    //errspan.removeClass("valid-error");
                    //errspan.addClass("valid-error");
                    errspan.css("color", "Red");
                    errspan.html("该用户名已存在，请重新输入"); 
                    count = 1;
                } else {
                    //errspan.removeClass("valid");
                    //errspan.removeClass("valid-error");
                    //errspan.addClass("valid");
                    errspan.css("color", "#000000");
                    //errspan.html("此昵称一旦注册成功不能修改");
                    
                    count = 0;
                }
            }
        });
    }
    if (count == 0) {
        errspan.hide();
        return true;
    } else {
        errspan.show();
        return false;
    }
}

//验证码
function valVerification(reg_verification, errspan) {
    var count;

    if (reg_verification.length == 0) {
        //errspan.removeClass("valid");
        //errspan.removeClass("valid-error");
        //errspan.addClass("valid-error");
        errspan.css("color", "Red");
        errspan.html("请填写验证码"); 
        count = 1;
    } else if (reg_verification == getCookie("code")) {
        //errspan.removeClass("valid");
        //errspan.removeClass("valid-error");
        //errspan.addClass("valid");
        //errspan.html("&nbsp;"); 
        count = 0;
    } else {
        //errspan.removeClass("valid");
        //errspan.removeClass("valid-error");
        //errspan.addClass("valid-error");
        errspan.css("color", "Red");
        errspan.html("验证码输入错误，请重新输入"); 
        $("#reg_verification_img").attr("src", "server/CheckCode.aspx?id=" + Math.random());
        count = 1;
    }

    if (count == 0) {
        errspan.hide();
        return true;
    } else {
        errspan.show();
        return false;
    }
}

//验证密码
function valPasswd(reg_pw, spnErrObj) {
    var count = 0;
    if (reg_pw.length < 6) {
        //spnErrObj.removeClass("valid");
        //spnErrObj.removeClass("valid-error");
        //spnErrObj.addClass("valid-error");
        spnErrObj.css("color", "Red");
        spnErrObj.html("请输入6-18个字符的密码，至少包含数字、大写字母、小写字母中的2种");
        spnErrObj.show();
        count = 1;
    } else if (/^\d+$/g.test(reg_pw)) {
        //spnErrObj.removeClass("valid");
        //spnErrObj.removeClass("valid-error");
        //spnErrObj.addClass("valid-error");
        spnErrObj.css("color", "Red");
        spnErrObj.html("请输入6-18个字符的密码，至少包含数字、大写字母、小写字母中的2种");
        spnErrObj.show();
        count = 1;
    } else if (/[^a-zA-Z0-9_]/g.test(reg_pw)) {
        //spnErrObj.removeClass("valid");
        //spnErrObj.removeClass("valid-error");
        //spnErrObj.addClass("valid-error");
        spnErrObj.css("color", "Red");
        spnErrObj.html("请输入6-20个字符的密码，至少包含数字、大写字母、小写字母中的2种");
        spnErrObj.show();
        count = 1;
    }

    //else {
    //    if (/[a-zA-Z]+/.test(reg_pw) && /[0-9]+/.test(reg_pw) && /\W+\D+/.test(reg_pw)) {
    //        spnErrObj.removeClass("valid");
    //        spnErrObj.removeClass("valid-error");
    //        spnErrObj.addClass("valid");
    //        spnErrObj.css("color", "#000000");
    //        spnErrObj.html("密码强度很强");
    //        spnErrObj.show();
    //        count = 0;
    //    } else if (/[a-zA-Z]+/.test(reg_pw) || /[0-9]+/.test(reg_pw) || /\W+\D+/.test(reg_pw)) {
    //        if (/[a-zA-Z]+/.test(reg_pw) && /[0-9]+/.test(reg_pw)) {
    //            spnErrObj.removeClass("valid");
    //            spnErrObj.removeClass("valid-error");
    //            spnErrObj.addClass("valid");
    //            spnErrObj.css("color", "#000000");
    //            spnErrObj.html("密码强度一般");
    //            spnErrObj.show();
    //            count = 0;
    //        } else if (/\[a-zA-Z]+/.test(reg_pw) && /\W+\D+/.test(reg_pw)) {
    //            spnErrObj.removeClass("valid");
    //            spnErrObj.removeClass("valid-error");
    //            spnErrObj.addClass("valid");
    //            spnErrObj.css("color", "#000000");
    //            spnErrObj.html("密码强度一般");
    //            spnErrObj.show();
    //            count = 0;
    //        } else if (/[0-9]+/.test(reg_pw) && /\W+\D+/.test(reg_pw)) {
    //            spnErrObj.removeClass("valid");
    //            spnErrObj.removeClass("valid-error");
    //            spnErrObj.addClass("valid");
    //            spnErrObj.css("color", "#000000");
    //            spnErrObj.html("密码强度一般");
    //            spnErrObj.show();
    //            count = 0;
    //        } else {
    //            spnErrObj.removeClass("valid");
    //            spnErrObj.removeClass("valid-error");
    //            spnErrObj.addClass("valid");
    //            spnErrObj.css("color", "#000000");
    //            spnErrObj.html("密码强度很弱");
    //            spnErrObj.show();
    //            count = 0;
    //        }
    //    }
    //}

    if (count == 0) {
        spnErrObj.hide();
        return true;
    } else {
        return false;
    }
}