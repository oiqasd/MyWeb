$(document).ready(function () {
    //$("#lusername").blur(function () {
    //    var value = $(this).val();
    //    if (value !== "") {
    //        $.ajax({
    //            type: 'post',
    //            url: "Form_Action/WebForm_Action.aspx",
    //            data: {
    //                "action": 'VaildateUsername',
    //                "username": value
    //            },
    //            async: false,
    //            success: function (data) {
    //                data = eval("(" + data + ")");//解析json数据
    //                if (data.status = "Success") {
    //                    if (data != 1) {
    //                        //alert("用户名不存在");
    //                        document.getElementById("lusername").focus();
    //                    }

    //                } else {
    //                    alert('Create Fail,Please Check !')
    //                }
    //            }
    //        });
    //    }
    //});

    //    $("#lpassword").blur(function () {
    //        var password = $(this).val();
    //        var username = $("#lusername").val();
    //        if (password !== "") {
    //            $.ajax({
    //                type: 'post',
    //                url: "Form_Action/WebForm_Action.aspx",
    //                data: {
    //                    "action": 'VaildatePassword',
    //                    "username": username,
    //                    "password": password
    //                },
    //                async: false,
    //                success: function (data) {
    //                    data = eval("(" + data + ")");//解析json数据
    //                    if (data.status = "Success") {
    //                        if (data == 2) {
    //                            alert("用户名不存在");
    //                            document.getElementById("lusername").focus();
    //                        } else if (data == 0) {
    //                            alert("密码错误");
    //                            document.getElementById("lpassword").focus();
    //                        }

    //                    } else {
    //                        alert('Create Fail,Please Check !')
    //                    }
    //                }
    //            });
    //        }
    //    });

});

function asynclogin() {
    var password = $("#lpassword").val();
    var username = $("#lusername").val();
    $('#spanLoginErr').html('');
    //if (username == "")
    //{
    //    $('#spanLoginErr').html('用户名不能为空');
    //    return false;
    //}
    //if (password == "") {
    //    $('#spanLoginErr').html('密码不能为空');
    //    return false;
    //}

    $.ajax({
        type: 'post',
        url: "Form_Action/WebForm_Action.aspx",
        data: {
            "action": 'asynclogin',
            "username": username,
            "password": password
        },
        async: false,
        success: function (data) { 
            data = eval("(" + data + ")");//解析json数据   
            if (data[0].Key == 1) { 
                return true;
            }
            else { 
                $('#spanLoginErr').html(data[0].Value); 
                return false;
            }
        }
    });
    return false;
}