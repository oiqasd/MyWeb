
$(document).ready(function () {
    $("#divEdit").css("display", "none");
    $("#ToSave").css("display", "none");
    $("#ToCancel").css("display", "none");

    $("#ToEdit").click(function () {
        PersonalInfo.AlterInfo();
        $("#divShow").css("display", "none");
        $("#ToEdit").css("display", "none");
        $("#divEdit").css("display", "block");
        $("#ToSave").css("display", "block");
        $("#ToCancel").css("display", "block");
    });

    $("#ToSave").click(function () {
        if (!PersonalInfo.Add()) return;
        $("#divShow").css("display", "block");
        $("#ToEdit").css("display", "block");
        $("#divEdit").css("display", "none");
        $("#ToSave").css("display", "none");
        $("#ToCancel").css("display", "none");
    });

    $("#ToCancel").click(function () {
        PersonalInfo.Cancel();
        $("#divShow").css("display", "block");
        $("#ToEdit").css("display", "block");
        $("#divEdit").css("display", "none");
        $("#ToSave").css("display", "none");
        $("#ToCancel").css("display", "none");
    });

    PersonalInfo.LoadInfo();
    setup();//加载省市区下拉框 path="JsAddress/geo.js" 


    //修改头像
    $(".modified_img").click(function () {
        alert();
    });
});
var PersonalInfo = {
    LoadInfo: function () {

        $.ajax({
            type: "GET",
            url: "userInfo.aspx?action=LoadInfo&r=" + Math.random(),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data != null && data != undefined) {
                    var jsonObj;
                    try {
                        jsonObj = JSON.parse(data);
                    } catch (e) {
                        jsonObj = eval("(" + data + ")");
                    }
                    $("#spNickName").text(jsonObj.NickName);
                    $("#spRealName").text(jsonObj.RealName);
                    $("#spSex").text(jsonObj.Sex);
                    $("#spAge").text(jsonObj.Age);
                    $("#spBirth").text(jsonObj.Birth);
                    $("#spCale").text(jsonObj.Cale);
                    $("#spConst").text(jsonObj.Const);
                    $("#spBooldType").text(jsonObj.BooldType);
                    $("#spEmail").text(jsonObj.Email);
                    $("#spEmotState").text(jsonObj.EmotState);
                    $("#spContactWay").text(jsonObj.ContactWay);
                    $("#spEducation").text(jsonObj.Education);
                    $("#spSchool").text(jsonObj.School);
                    $("#spPaperType").text(jsonObj.PaperType);
                    $("#spPaperNumber").text(jsonObj.PaperNumber);
                    $("#spWorker").text(jsonObj.Worker);
                    $("#spHomeTown").text(jsonObj.HomeTown);
                    $("#spNowPlace").text(jsonObj.NowPlace);
                    $("#spAddress").text(jsonObj.Address);
                    $("#spRemark").text(jsonObj.Remark);
                    $("#spSchool").text(jsonObj.School);
                    $("#spEducation").text(jsonObj.Education);
                    $("#spCompany").text(jsonObj.Company);
                    //var address = jsonObj.Address;
                    //var arrAddress = new Array('', '', '');
                    //if (address != undefined) {
                    //    arrAddress = address.split(',');
                    //}

                    //if (jsonObj.realName.length > 0) {
                    //    $("#spRealNameOk").show();
                    //    $("#spDocumentCodeOk").show();
                    //    $("#spDocumentCodeNo").hide();
                    //    $("#spRealNameNo").hide();
                    //}
                    //else {
                    //    $("#spDocumentCodeOk").hide();
                    //    $("#spRealNameOk").hide();
                    //    $("#spRealNameNo").show();
                    //    $("#spDocumentCodeNo").show();
                    //}


                }
                else if (data.d == "0") {
                    window.location.href = "../Login.aspx";
                }

            },
            error: function (err) {

            }
        });

        $("#divEdit").css("display", "none");
        $("#ToSave").css("display", "none");
        $("#ToCancel").css("display", "none");
    },
    AlterInfo: function () {
        //$("#inputInfo").show();
        //$("#showInfo").hide();

        $("#txtNickName").val($("#spNickName").text());
        $("#txtRealName").val($("#spRealName").text());
        $("#selSex").val($("#spSex").text());
        $("#txtAge").val($("#spAge").text());
        $("#datBirth").val($("#spBirth").text());
        $("#selCale").val($("#spCale").text());
        $("#selConst").val($("#spConst").text());
        $("#selBooldType").val($("#spBooldType").text());
        $("#txtEmail").val($("#spEmail").text());
        $("#selEmotState").val($("#spEmotState").text());
        $("#txtContactWay").val($("#ContactWay").text());
        $("#selEducation").val($("#spEducation").text());
        $("#txtSchool").val($("#spSchool").text());
        $("#selPaperType").val($("#spPaperType").text());
        $("#txtPaperNumber").val($("#spPaperNumber").text());
        $("#txtCompany").val($("#spCompany").text());
        $("#selWorker").val($("#spWorker").text());
        //$("#txtHomeTown").val($("#spHomeTown").text());
        $("#txtNowPlace").val($("#spNowPlace").text());
        $("#txtAddress").val($("#spAddress").text());
        $("#txtRemark").val($("#spRemark").text());

        if ($("#spHomeTown").text().split(',').lenth > 2) {
            $("#s1").val($("#spHomeTown").text().split(',')[0]);
            $("#s1").prepend("<option selected='selected' value='" + $("#spHomeTown").text().split(',')[0] + "'>" + $("#spHomeTown").text().split(',')[0] + "</option>");
            $("#s2").prepend("<option selected='selected' value='" + $("#spHomeTown").text().split(',')[1] + "'>" + $("#spHomeTown").text().split(',')[1] + "</option>");
            $("#s3").prepend("<option selected='selected' value='" + $("#spHomeTown").text().split(',')[2] + "'>" + $("#spHomeTown").text().split(',')[2] + "</option>");

        }


        //$("#s1").val($("#spProvince").text());
        //$("#s1").prepend("<option selected='selected' value='" + $("#spProvince").text() + "'>" + $("#spProvince").text() + "</option>");
        //$("#s2").prepend("<option selected='selected' value='" + $("#spCity").text() + "'>" + $("#spCity").text() + "</option>");
        //$("#s3").prepend("<option selected='selected' value='" + $("#spTown").text() + "'>" + $("#spTown").text() + "</option>");
        //$("#txtGraduationSchool").val($("#spnGraduationSchool").text());
        //$("#selEducation").val($("#spnEducation").text());
        //$("#txtCompanyIndustry").val($("#spnCompanyIndustry").text());
        //$("#selCompanySize").val($("#spnCompanySize").text());
        //$("#txtPosition").val($("#spnPosition").text());
        //$("#selMonthIncome").val($("#spnMonthIncome").text());
        //$("#txtEmergencyContactor").val($("#spnEmergencyContactor").text());
        //$("#txtEmergencyPhoneNumber").val($("#spnEmergencyPhoneNumber").text());
        //$("#spnSex").val($("#spnSex").text());
        //$("#txtNickName").val($("#spNickName").text());
        //if ($("#spnMarry").text() == "未婚") {
        //    $("#selMarry").val("false");
        //}
        //else {
        //    $("#selMarry").val("true");
        //}
        //if ($("#spnHaveHouse").text() == "无房") {
        //    $("#selHaveHouse").val("false");
        //}
        //else {
        //    $("#selHaveHouse").val("true");
        //}
        //if ($("#spnHaveCar").text() == "无车") {
        //    $("#selHaveCar").val("false");
        //}
        //else {
        //    $("#selHaveCar").val("false");
        //}
        return false;
    },
    Cancel: function () {
        //$("#showInfo").show();
        //$("#inputInfo").hide();
        //$("#s1").val($("#spProvince").text());
        //$("#s2").val($("#spCity").text());
        //$("#s3").val($("#spTown").text());
        //$("#txtGraduationSchool").val($("#spnGraduationSchool").text());
        //$("#selEducation").val($("#spnEducation").text());
        //$("#txtCompanyIndustry").val($("#spnCompanyIndustry").text());
        //$("#selCompanySize").val($("#spnCompanySize").text());
        //$("#selMonthIncome").val($("#spnMonthIncome").text());
        //$("#selMarry").val($("#spnMarry").text());
        //$("#selHaveHouse").val($("#spnHaveHouse").text());
        //$("#selHaveCar").val($("#spnHaveCar").text());
        //$("#txtEmergencyContactor").val($("#spnEmergencyContactor").text());
        //$("#txtEmergencyPhoneNumber").val($("#spnEmergencyPhoneNumber").text());

        $("#txtNickName").val($("#spNickName").text());
        $("#txtRealName").val($("#spRealName").text());
        $("#selSex").val($("#spSex").text());
        $("#txtAge").val($("#spAge").text());
        $("#datBirth").val($("#spBirth").text());
        $("#selCale").val($("#spCale").text());
        $("#selConst").val($("#spConst").text());
        $("#selBooldType").val($("#spBooldType").text());
        $("#txtEmail").val($("#spEmail").text());
        $("#selEmotState").val($("#spEmotState").text());
        $("#txtContactWay").val($("#ContactWay").text());
        $("#selEducation").val($("#spEducation").text());
        $("#txtSchool").val($("#spSchool").text());
        $("#selPaperType").val($("#spPaperType").text());
        $("#txtPaperNumber").val($("#spPaperNumber").text());
        $("#txtCompany").val($("#spCompany").text());
        $("#selWorker").val($("#spWorker").text());
        //$("#txtHomeTown").val($("#spHomeTown").text());
        $("#txtNowPlace").val($("#spNowPlace").text());
        $("#txtAddress").val($("#spAddress").text());
        $("#txtRemark").val($("#spRemark").text());

        return false;
    },
    Add: function () {
        var shen = $("#s1").val();
        var shi = $("#s2").val();
        var qu = $("#s3").val();

        if (shen == "" || shen == "省份") {
            alert("请选择省份");
            $("#s1")[0].focus();
            return false;
        }
        if (shi == "" || shi == "地级市") {
            alert("请选择地级市");
            $("#s2")[0].focus();
            return false;
        }
        if (qu == "市、县级市、县") {
            qu = "";
        }
        var address = shen + "," + shi + "," + qu;
        //var graduationSchool = $("#txtGraduationSchool").val();
        //var position = $("#txtPosition").val();
        //var education = $("#selEducation").val();
        //var companyIndustry = $("#txtCompanyIndustry").val();
        //var companySize = $("#selCompanySize").val();
        //var monthIncome = $("#selMonthIncome").val();
        //var marry = $("#selMarry").val();
        //var haveHouse = $("#selHaveHouse").val();
        //var haveCar = $("#selHaveCar").val();
        //var sex = $("#spnSex").val();
        //var emergencyContactor = $("#txtEmergencyContactor").val();
        //var emergencyPhoneNumber = $("#txtEmergencyPhoneNumber").val();
        //var NickName = $("#txtNickName").val();

        //var param = {};
        //param["action"] = "Add";
        //param["address"] = address;
        //param["position"] = position;
        //param["graduationSchool"] = graduationSchool;
        //param["education"] = education;
        //param["companyIndustry"] = companyIndustry;
        //param["companySize"] = companySize;
        //param["monthIncome"] = monthIncome;
        //param["marry"] = marry;
        //param["haveHouse"] = haveHouse;
        //param["haveCar"] = haveCar;
        //param["sex"] = sex;
        //param["emergencyContactor"] = emergencyContactor;
        //param["NickName"] = NickName;
        //param["emergencyPhoneNumber"] = emergencyPhoneNumber;

        //=========================================================
        var NickName = $("#txtNickName").val();
        var RealName = $("#txtRealName").val();
        var Sex = $("#selSex").val();
        var Age = $("#txtAge").val();
        var Birth = $("#datBirth").val();
        var Cale = $("#selCale").val();
        var Const = $("#selConst").val();
        var BooldType = $("#selBooldType").val();
        var Email = $("#txtEmail").val();
        var EmotState = $("#selEmotState").val();
        var ContactWay = $("#txtContactWay").val();
        var Education = $("#selEducation").val();
        var School = $("#txtSchool").val();
        var PaperType = $("#selPaperType").val();
        var PaperNumber = $("#txtPaperNumber").val();
        var Company = $("#txtCompany").val();
        var Worker = $("#selWorker").val();
        var HomeTown = address;//$("#txtHomeTown").val();
        var NowPlace = $("#txtNowPlace").val();
        var Address = $("#txtAddress").val();
        var Remark = $("#txtRemark").val();

        var param = {};
        param["action"] = "Add";
        param["NickName"] = NickName;
        param["RealName"] = RealName;
        param["Sex"] = Sex;
        param["Age"] = Age;
        param["Birth"] = Birth;
        param["Cale"] = Cale;
        param["Const"] = Const;
        param["BooldType"] = BooldType;
        param["Email"] = Email;
        param["EmotState"] = EmotState;
        param["ContactWay"] = ContactWay;
        param["Education"] = Education;
        param["School"] = School;
        param["PaperType"] = PaperType;
        param["PaperNumber"] = PaperNumber;
        param["Company"] = Company;
        param["Worker"] = Worker;
        param["HomeTown"] = HomeTown;
        param["NowPlace"] = NowPlace;
        param["Address"] = Address;
        param["Remark"] = Remark;


        $.ajax({
            type: "POST",
            url: "userInfo.aspx",
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            dataType: "json",
            data: param,
            success: function (data) {
                PersonalInfo.LoadInfo();
                //$("#inputInfo").hide();
                //$("#showInfo").show();
            },
            error: function (err) {

            }
        });

        return true;
    }
}

