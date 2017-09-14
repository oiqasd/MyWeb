//获取浏览器信息
function BrowserInfo() {
    var bro = $.browser;

    var binfo = "";//浏览器信息
    if (bro.msie) { binfo = "Microsoft Internet Explorer " + bro.version; }
    else if (bro.mozilla) { binfo = "Mozilla Firefox " + bro.version; }
    else if (bro.safari) { binfo = "Apple Safari " + bro.version; }
    else if (bro.opera) { binfo = "Opera " + bro.version; }
    else if (bro.chrome) { binfo = "Google Chrome " + bro.version; }
    else {
        binfo = JSON.stringify(bro);//navigator.userAgent
    }
}

var CommonHelper = {
    //验证邮箱 true表示格式正确
    CheckEmail: function (str) {
        if (str.match(/[A-Za-z0-9_-]+[@](\S*)(net|com|cn|org|cc|tv|[0-9]{1,3})(\S*)/g) == null) {
            return false;
        }
        else {
            return true;
        }
    },
    //验证电话 true表示格式正确
    CheckMobilePhone: function (str) {
        if (str.match(/^(?:13\d|15[0-9]|17[0-9]|18[0123456789])-?\d{5}(\d{3}|\*{3})$/) == null) {
            return false;
        }
        else {
            return true;
        }
    },
    //true表示为数字
    CheckNum: function (str) {
        if (str.match(/\D/) == null) {
            return false;
        }
        else {
            return true;
        }
    },
    //true表示格式正确 检查输入的身份证号是否正确
    CheckCard: function (str) {
        //15位数身份证正则表达式
        var arg1 = /^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$/;
        //18位数身份证正则表达式
        var arg2 = /^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])((\d{4})|\d{3}[A-Z])$/;
        if (str.match(arg1) == null && str.match(arg2) == null) {
            return false;
        }
        else {
            return true;
        }
    },
    //检查字符串是否为空
    CheckEmpty: function (str) {
        if (str == undefined || str == "") {
            return true;
        } else {
            return false;
        }
    },
    //true只有数字和字母和下划线
    CheckNumOrLet: function (parVal) {
        if (parVal.match(/[^a-zA-Z0-9_]+/)) {
            return true;
        } else {
            return false;
        }
    },
    //true表示为小数
    CheckDecimal: function (str) {
        if (str.match(/^-?\d+(\.\d+)?$/g) == null) {
            return false;
        }
        else {
            return true;
        }
    },
    //整数
    CheckInteger: function (str) {
        if (str.match(/^[-+]?\d*$/) == null) {
            return false;
        }
        else {
            return true;
        }
    },
    //true表示为全部为字符 不包含汉字
    CheckStr: function (str) {
        if (/[^\x00-\xff]/g.test(str)) {
            return false;
        }
        else {
            return true;
        }
    },
    //true表示包含汉字
    CheckChinese: function (str) {
        if (escape(str).indexOf("%u") != -1) {
            return true;
        }
        else {
            return false;
        }
    },
    //验证Money，true表示正确
    CheckMoney: function (str) {
        if (str.match(/^[0-9][\d]{0,11}([\.][\d]{0,4})?$/)) {
            return true;
        } else {
            return false;
        }
    },
    //true表示开始时间大于或等于结束时间
    CompareTime: function (stime, etime) {
        var starttimes = stime.split('-');
        var endtimes = etime.split('-');
        var starttimeTemp = starttimes[0] + '/' + starttimes[1] + '/' + starttimes[2];
        var endtimesTemp = endtimes[0] + '/' + endtimes[1] + '/' + endtimes[2];
        if (Date.parse(new Date(starttimeTemp)) > Date.parse(new Date(endtimesTemp)) || Date.parse(new Date(starttimeTemp)) == Date.parse(new Date(endtimesTemp))) {
            return true;
        }
    },
    //true表示开始时间大于或等于结束时间,只有年月日
    CheckCompareTime: function (stime, etime) {
        var starttimes = stime.split('-');
        var endtimes = etime.split('-');
        var starttimeTemp = starttimes[0] + '/' + starttimes[1] + '/' + starttimes[2];
        var endtimesTemp = endtimes[0] + '/' + endtimes[1] + '/' + endtimes[2];
        if (Date.parse(new Date(starttimeTemp)) > Date.parse(new Date(endtimesTemp))) {
            return true;
        }
    },
    //检查输入的IP地址是否正确 true表示格式正确
    CheckIP: function (str) {
        var arg = /^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$/;
        if (str.match(arg) == null) {
            return false;
        }
        else {
            return true;
        }
    },
    //检查输入的URL地址是否正确 true表示格式正确
    CheckURL: function (str) {
        //if (str.match(/(http[s]?|ftp):\/\/[^\/\.]+?\..+\w$/i) == null) {
        if (str.match(/(http[s]?|ftp):\/\/([\w-]+\.)+[\w-]+(\/[\w- .\/?%&=]*)?/) == null) {
            return false
        }
        else {
            return true;
        }
    },
    //编码  注释：ECMAScript v3 反对使用该方法，应用使用 decodeURI() 和 decodeURIComponent() 替代它。
    Escape: function (value) {
        if (value != undefined) {
            return escape(value);
        }
        return undefined;
    },
    //解码
    UnEscape: function (value) {
        if (value != undefined) {
            return unescape(value);
        }
        return undefined;
    },
    //获取url参数值  name等于参数名
    GetQueryString: function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]); return null;
    },
    //截取字符串
    GetStrSub: function (str, lth) {
        var resultStr = "";
        if (str == undefined || str == "") {
            resultStr = "";
        }
        else {
            if (str.length > lth) {
                resultStr = str.substring(0, lth) + '...';
            }
            else {
                resultStr = str;
            }
        }
        return resultStr;
    },
    //.net Json后的日期转换
    JsonToDateTime: function (jsonDate) {
        if (jsonDate == undefined || jsonDate.length <= 0) return jsonDate;
        var date = new Date(parseInt(jsonDate.substr(6))).format("yyyy-MM-dd HH:mm:ss");
        return date;
    },
    //.net Json后的日期转换 只有年月日
    JsonToDateTimeymd: function (jsonDate) {
        if (jsonDate == undefined || jsonDate.length <= 0) return jsonDate;
        var date = new Date(parseInt(jsonDate.substr(6))).format("yyyy-MM-dd");
        return date;
    },
    //获得访问的路径/Home/Index
    GetPath: function () {
        return window.location.pathname;
    },
    //获得
    GetUrl: function () {
        return window.location.href;
    },
    //获得访问协议 http https
    GetProtocol: function () {
        return window.location.protocol;
    },
    //获取域名
    GetDomain: function () {
        return document.domain;
    },
    //获得端口
    GetPort: function () {
        return window.location.port;
    },
};

//KeyPress
function OnlyNumber() {
    if (window.event.keyCode < 48 || window.event.keyCode > 57) {
        window.event.keyCode = 0;
        window.event.returnValue = false;
    }
}

function KeyPressAll() {
    if ((window.event.keyCode < 48 || window.event.keyCode > 57) &&
        (window.event.keyCode < 65 || window.event.keyCode > 90) &&
        (window.event.keyCode < 97 || window.event.keyCode > 122)) {
        window.event.keyCode = 0;
        window.event.returnValue = false;
    }
}

//KeyUp
function KeyAll(obj) {
    obj.value = obj.value.replace(/[^\a-\z\A-\Z0-9\u4E00-\u9FA5\@\.\-]/g, '');
}
function KeyNumber(obj) {
    obj.value = obj.value.replace(/[^\d]/g, '');
}
function KeyHuman(obj) {
    obj.value = obj.value.replace(/[^\a-\z\A-\Z\0-9]/g, '');
}
//KeyDown
function textKeyDown(obj) {
    if (obj.value.length >= 200) {
        if (window.event.keyCode != 8 && window.event.keyCode != 127) {
            window.event.returnValue = false;
        }
    }
}


