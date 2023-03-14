var code; //在全局定义验证码
var imgCodeEnable = true;
var guestListIndex; //左侧登记状态当前点击序号（从0开始）
var isSubmitStick;//目前为止生成船票的人数

var BookingId;//在全局定义BookingId
var GuestId;//在全局定义GuestId
var BookingAccessToken;//在全局定义BookingAccessToken
var BpassID;//在全局定义BpassID
var Barcode;//在全局定义Barcode 
var passPortDateNum;//在全局定义passPortDateNum 护照有效日期

var stepOneResp // 在全局保存第一步的返回值

//GetGuestInfo 数据结构 { GuestId: "",  FirstName: "", LastName: "", DateOfBirth: "", CountryOfCitizenship: "", Gender:"",Telephone :"",PassPortNumber :"",PassPortDate :""}
var GetGuestInfo = [];

var GetStickInfo = [];

var passengerOrder = 0;//当前操作的乘客序号

var directCustomer = null;//是否是非直销客，  true--是  false--不是
var hasPayed = null;//是否已付款，  true--已付款  false--未付款

var IsDomestic = true;//true境外    false null或者境内

var passportAdditional = "";//签发次数



var birthOfDate = null;//出生年月

$(function () {

    agreementDialog();//第三步消费方式中的“用户协议”

    var countryListArray = [{ "code": "CHN", "name": "中国" }, { "code": "HKG", "name": "中国香港" }, { "code": "MAC", "name": "中国澳门" }, { "code": "TWN", "name": "中国台湾" }, { "code": "JPN", "name": "日本" }, { "code": "KOR", "name": "韩国" }, { "code": "THA", "name": "泰国" }, { "code": "VNM", "name": "越南" }, { "code": "SGP", "name": "新加坡" }, { "code": "IND", "name": "印度" }, { "code": "RUE", "name": "俄罗斯" }, { "code": "BLR", "name": "白俄罗斯" }, { "code": "IDN", "name": "印度尼西亚" }, { "code": "PHL", "name": "菲律宾" }, { "code": "MYS", "name": "马来西亚" }, { "code": "MDV", "name": "马尔代夫" }, { "code": "BUR", "name": "缅甸" }, { "code": "LAO", "name": "老挝" }, { "code": "PAK", "name": "巴基斯坦" }, { "code": "PSE", "name": "巴勒斯坦" }, { "code": "TUR", "name": "土耳其" }, { "code": "IRQ", "name": "伊拉克" }, { "code": "ISR", "name": "以色列" }, { "code": "NPL", "name": "尼泊尔" }, { "code": "BGD", "name": "孟加拉国" }, { "code": "BTN", "name": "不丹" }, { "code": "LKA", "name": "斯里兰卡" }, { "code": "AFG", "name": "阿富汗" }, { "code": "OMN", "name": "阿曼" }, { "code": "LBN", "name": "黎巴嫩" }, { "code": "ARM", "name": "亚美尼亚" }, { "code": "ALB", "name": "阿尔巴尼亚" }, { "code": "DZA", "name": "阿尔及利亚" }, { "code": "ASM", "name": "美国萨摩亚" }, { "code": "AND", "name": "安道尔" }, { "code": "AGO", "name": "安哥拉" }, { "code": "AIA", "name": "安圭拉" }, { "code": "ATA", "name": "南极洲" }, { "code": "ATG", "name": "安提瓜和巴布达" }, { "code": "ARG", "name": "阿根廷" }, { "code": "ABW", "name": "阿鲁巴" }, { "code": "AUS", "name": "澳大利亚" }, { "code": "AUT", "name": "奥地利" }, { "code": "AZE", "name": "阿塞拜疆" }, { "code": "BHS", "name": "巴哈马" }, { "code": "BHR", "name": "巴林" }, { "code": "BRB", "name": "巴巴多斯" }, { "code": "BEL", "name": "比利时" }, { "code": "BLZ", "name": "伯利兹" }, { "code": "BEN", "name": "贝宁" }, { "code": "BMU", "name": "百慕大群岛" }, { "code": "BOL", "name": "玻利维亚" }, { "code": "BON", "name": "博内尔岛" }, { "code": "BIH", "name": "波斯尼亚和黑塞哥维那" }, { "code": "BWA", "name": "博茨瓦纳" }, { "code": "BVT", "name": "布维岛" }, { "code": "BRA", "name": "巴西" }, { "code": "IOT", "name": "英属印度洋" }, { "code": "BRN", "name": "文莱达鲁萨兰国" }, { "code": "BGR", "name": "保加利亚" }, { "code": "BFA", "name": "布基纳法索" }, { "code": "BDI", "name": "布隆迪" }, { "code": "KHM", "name": "柬埔寨" }, { "code": "CMR", "name": "喀麦隆" }, { "code": "CAN", "name": "加拿大" }, { "code": "CPV", "name": "佛得角群岛" }, { "code": "CYM", "name": "开曼群岛" }, { "code": "CAF", "name": "中非共和国" }, { "code": "TCD", "name": "乍得共和国" }, { "code": "CHL", "name": "智利" }, { "code": "CXR", "name": "圣诞岛" }, { "code": "CCK", "name": "科科斯岛" }, { "code": "COL", "name": "哥伦比亚" }, { "code": "COM", "name": "科摩罗" }, { "code": "COK", "name": "库克群岛" }, { "code": "CRI", "name": "哥斯达黎加" }, { "code": "CIV", "name": "科特迪瓦" }, { "code": "HRV", "name": "克罗地亚" }, { "code": "CUR", "name": "库拉索岛" }, { "code": "CYP", "name": "塞浦路斯" }, { "code": "CZE", "name": "捷克" }, { "code": "DNK", "name": "丹麦" }, { "code": "DJI", "name": "吉布提" }, { "code": "DMA", "name": "多米尼加" }, { "code": "DOM", "name": "多明尼加共和国" }, { "code": "UAE", "name": "迪拜" }, { "code": "TMP", "name": "东帝汶" }, { "code": "ECU", "name": "厄瓜多尔" }, { "code": "EGY", "name": "埃及" }, { "code": "SLV", "name": "萨尔瓦多" }, { "code": "GNQ", "name": "赤道几内亚" }, { "code": "ERI", "name": "厄立特里亚" }, { "code": "EST", "name": "爱沙尼亚" }, { "code": "ETH", "name": "埃塞俄比亚" }, { "code": "FRO", "name": "法罗群岛" }, { "code": "FLK", "name": "福克兰群岛" }, { "code": "FJI", "name": "斐济群岛" }, { "code": "FIN", "name": "芬兰" }, { "code": "FRA", "name": "法国" }, { "code": "GUF", "name": "法属圭亚那" }, { "code": "PYF", "name": "法属波利尼西亚" }, { "code": "ATF", "name": "法国南部。领土" }, { "code": "GAB", "name": "加蓬" }, { "code": "GMB", "name": "冈比亚" }, { "code": "GEO", "name": "格鲁吉亚" }, { "code": "DEU", "name": "德国" }, { "code": "GHA", "name": "加纳" }, { "code": "GIB", "name": "直布罗陀" }, { "code": "GRC", "name": "希腊" }, { "code": "GRL", "name": "格陵兰岛" }, { "code": "GRD", "name": "格林纳达" }, { "code": "GLP", "name": "瓜德罗普岛" }, { "code": "GUM", "name": "关岛" }, { "code": "GTM", "name": "瓜地马拉" }, { "code": "GIN", "name": "几内亚" }, { "code": "GNB", "name": "几内亚比绍" }, { "code": "GUY", "name": "圭亚那" }, { "code": "HTI", "name": "海地" }, { "code": "HND", "name": "洪都拉斯" }, { "code": "HUN", "name": "匈牙利" }, { "code": "ISL", "name": "冰岛" }, { "code": "IRL", "name": "爱尔兰" }, { "code": "ITA", "name": "意大利" }, { "code": "JAM", "name": "牙买加" }, { "code": "JOR", "name": "乔丹" }, { "code": "KAZ", "name": "哈萨克斯坦" }, { "code": "KEN", "name": "肯尼亚" }, { "code": "KIR", "name": "基里巴斯" }, { "code": "KWT", "name": "科威特" }, { "code": "KGZ", "name": "吉尔吉斯斯坦" }, { "code": "LVA", "name": "拉脱维亚" }, { "code": "LSO", "name": "莱索托" }, { "code": "LBR", "name": "利比里亚" }, { "code": "LBY", "name": "利比亚" }, { "code": "LIE", "name": "列支敦士登" }, { "code": "LTU", "name": "立陶宛" }, { "code": "LUX", "name": "卢森堡" }, { "code": "MCD", "name": "马其顿" }, { "code": "MDG", "name": "马达加斯加" }, { "code": "MWI", "name": "马拉维" }, { "code": "MLI", "name": "马里共和国" }, { "code": "MLT", "name": "马耳他" }, { "code": "MHL", "name": "马绍尔群岛" }, { "code": "MTO", "name": "马提尼克" }, { "code": "MRT", "name": "毛里塔尼亚" }, { "code": "MUS", "name": "毛里求斯" }, { "code": "MYT", "name": "马约特岛" }, { "code": "MEX", "name": "墨西哥" }, { "code": "FSM", "name": "密克罗尼西亚" }, { "code": "MDA", "name": "摩尔多瓦" }, { "code": "MCO", "name": "摩纳哥" }, { "code": "MNG", "name": "蒙古人民共和国" }, { "code": "MNE", "name": "黑山" }, { "code": "MSR", "name": "蒙特塞拉特岛" }, { "code": "MAR", "name": "摩洛哥" }, { "code": "MOZ", "name": "莫桑比克" }, { "code": "MMR", "name": "缅甸" }, { "code": "NAM", "name": "纳米比亚" }, { "code": "NRU", "name": "瑙鲁" }, { "code": "NLD", "name": "荷兰" }, { "code": "ANT", "name": "荷兰antillesnes" }, { "code": "NCL", "name": "新喀里多尼亚" }, { "code": "NZL", "name": "新西兰" }, { "code": "NIC", "name": "尼加拉瓜" }, { "code": "NGA", "name": "尼日利亚" }, { "code": "NIU", "name": "纽埃" }, { "code": "NFK", "name": "诺福克岛" }, { "code": "MNP", "name": "北马里亚纳群岛" }, { "code": "NOR", "name": "挪威" }, { "code": "OTH", "name": "其他" }, { "code": "PLW", "name": "帕劳" }, { "code": "PAN", "name": "巴拿马" }, { "code": "PNG", "name": "巴布亚新几内亚" }, { "code": "PRY", "name": "巴拉圭" }, { "code": "PER", "name": "秘鲁" }, { "code": "PCN", "name": "皮特开恩群岛" }, { "code": "POL", "name": "波兰" }, { "code": "PRT", "name": "葡萄牙" }, { "code": "PRI", "name": "波多黎各" }, { "code": "QAT", "name": "卡塔尔" }, { "code": "COG", "name": "刚果共和国" }, { "code": "YMD", "name": "也门共和国" }, { "code": "REU", "name": "留尼旺岛" }, { "code": "ROM", "name": "罗马尼亚" }, { "code": "RWA", "name": "卢旺达" }, { "code": "SGS", "name": "s.georgia和s.sandwich岛" }, { "code": "SHN", "name": "圣海伦娜" }, { "code": "KNA", "name": "圣基茨和尼维斯" }, { "code": "LCA", "name": "圣露西亚" }, { "code": "SPM", "name": "圣彼埃尔和密克隆岛" }, { "code": "WSM", "name": "萨摩亚（西印度）" }, { "code": "SMR", "name": "圣马力诺" }, { "code": "STP", "name": "圣多美和普林西比" }, { "code": "SAU", "name": "沙乌地阿拉伯" }, { "code": "SEN", "name": "塞内加尔共和国" }, { "code": "SRB", "name": "塞尔维亚" }, { "code": "SYC", "name": "塞舌尔群岛" }, { "code": "SLE", "name": "塞拉利昂" }, { "code": "SVK", "name": "斯洛伐克" }, { "code": "SVN", "name": "斯洛文尼亚" }, { "code": "SLB", "name": "所罗门群岛" }, { "code": "SOM", "name": "索马里" }, { "code": "ZAF", "name": "南非" }, { "code": "ESP", "name": "西班牙" }, { "code": "VCT", "name": "圣文森特和格林纳丁斯" }, { "code": "STK", "name": "圣基茨" }, { "code": "STM", "name": "圣马丁" }, { "code": "SUR", "name": "苏里南" }, { "code": "SJM", "name": "斯瓦尔巴群岛和扬马延岛我" }, { "code": "SWZ", "name": "斯威士兰" }, { "code": "SWE", "name": "瑞典" }, { "code": "CHS", "name": "瑞士" }, { "code": "TJK", "name": "塔吉克斯坦" }, { "code": "TZA", "name": "坦桑尼亚" }, { "code": "TGO", "name": "多哥" }, { "code": "TKL", "name": "托克劳群岛" }, { "code": "TON", "name": "汤加群岛" }, { "code": "TTO", "name": "特立尼达和多巴哥" }, { "code": "TUN", "name": "突尼斯" }, { "code": "TKM", "name": "土库曼斯坦" }, { "code": "TCA", "name": "特克斯和凯科斯岛" }, { "code": "TUV", "name": "图瓦卢" }, { "code": "UMI", "name": "美国小出岛" }, { "code": "UGA", "name": "乌干达" }, { "code": "UKR", "name": "乌克兰" }, { "code": "ARE", "name": "阿拉伯联合酋长国" }, { "code": "GBR", "name": "英国" }, { "code": "USA", "name": "美国" }, { "code": "URY", "name": "乌拉圭" }, { "code": "UZB", "name": "乌兹别克斯坦" }, { "code": "VUT", "name": "瓦努阿图" }, { "code": "VAT", "name": "梵蒂冈城" }, { "code": "VEN", "name": "委内瑞拉" }, { "code": "VGB", "name": "维尔京群岛（英国）" }, { "code": "VIR", "name": "维尔京群岛（美国）" }, { "code": "WLF", "name": "沃利斯及富图纳群岛" }, { "code": "ESH", "name": "西撒哈拉" }, { "code": "YEM", "name": "也门" }, { "code": "YUG", "name": "南斯拉夫" }, { "code": "ZAR", "name": "扎伊尔" }, { "code": "ZMB", "name": "赞比亚" }, { "code": "ZWE", "name": "津巴布韦" }];
    var testCHN = /^([\u4E00-\u9FA5|\u9FA6-\u9FCB|\u3400-\u4DB5|\u2F00-\u2FD5|\u2E80-\u2EF3|\uF900-\uFAD9|\uE815-\uE86F|\uE400-\uE5E8|\uE600-\uE6CF|\u2FF0-\u2FFB])*$/;

    // 日历控件初始化
    var calendarB = RcclCalendarPicker({
        allowPreYear: true,
        initDate: "1980/01",
        id: 'birthCalendar',
        onDateClick: function (date) {
            //console.log("birth", date);
            $("#birthCalendar").addClass('f-dn');
            $("input[name='Birthday']").val(date);
            birthCheck();
        },
        onMonthChange: function (month) {
            //console.log(month)
        },
        onInit: function () {
            //console.log("Init")
        }
    });

    var calendarP = RcclCalendarPicker({
        allowPreYear: false,
        id: 'passCalendar',
        onDateClick: function (date) {
            //console.log("sailDate", date);
            $("#passCalendar").addClass('f-dn');
            $("input[name='sailDate']").val(date);
            $("input[name='sailDate']").attr("cdate", date);
            sailDateCheck();
        },
        onMonthChange: function (month) {
            //console.log(month)
        },
        onInit: function () {
            //console.log("Init")
        }
    });

    var calendarP1 = RcclCalendarPicker({
        allowPreYear: false,
        id: 'passportCalendar',
        onDateClick: function (date) {
            $("input[name='passPortDate']").attr("numDate", date);
            var date = new Date(date);
            var year = date.getFullYear();
            var month = date.getMonth();
            var day = date.getDate();
            switch (month) {
                case 0:
                    month = "JAN"
                    break;
                case 1:
                    month = "FEB"
                    break;
                case 2:
                    month = "MAR"
                    break;
                case 3:
                    month = "APR"
                    break;
                case 4:
                    month = "MAY"
                    break;
                case 5:
                    month = "JUN"
                    break;
                case 6:
                    month = "JULY"
                    break;
                case 7:
                    month = "AUG"
                    break;
                case 8:
                    month = "SEP"
                    break;
                case 9:
                    month = "OCT"
                    break;
                case 10:
                    month = "NOV"
                    break;
                case 11:
                    month = "DEC"
                    break;
                default:

            }

            $("#passportCalendar").addClass('f-dn');
            $("input[name='passPortDate']").val(day + " " + month + " " + year);
        },
        onMonthChange: function (month) {
            //console.log(month)
        },
        onInit: function () {
            //console.log("Init")
        }
    });

    $("input[name='Birthday']").click(function (e) {
        e.stopPropagation();
        $(".calendar").addClass('f-dn');
        $("#birthCalendar").removeClass('f-dn');
    })

    $("input[name='sailDate']").click(function (e) {
        e.stopPropagation();
        $(".calendar").addClass('f-dn');
        $("#passCalendar").removeClass('f-dn');
    })

    $("input[name='passPortDate']").click(function (e) {
        e.stopPropagation();
        $(".calendar").addClass('f-dn');
        $("#passportCalendar").removeClass('f-dn');
    })

    //日期数字转英文
    function dateEnglish(date) {
        var date = new Date(date);
        var year = date.getFullYear();
        var month = date.getMonth();
        var day = date.getDate();
        switch (month) {
            case 0:
                month = "JAN"
                break;
            case 1:
                month = "FEB"
                break;
            case 2:
                month = "MAR"
                break;
            case 3:
                month = "APR"
                break;
            case 4:
                month = "MAY"
                break;
            case 5:
                month = "JUN"
                break;
            case 6:
                month = "JULY"
                break;
            case 7:
                month = "AUG"
                break;
            case 8:
                month = "SEP"
                break;
            case 9:
                month = "OCT"
                break;
            case 10:
                month = "NOV"
                break;
            case 11:
                month = "DEC"
                break;
            default:

        }
        $("#passPortDate").val(day + " " + month + " " + year);
    }

    //国籍的选择
    $("#countryCode").click(function () {
        $("#countrySelCon").show();
        getCountryName();
    })

    $("#countrySelCon a").live("click", function () {
        $("#countrySelCon").css("display", "none");
        $("#countryCode").val($(this).html());
        var countryno = $(this).attr("countryno");
        $("#countryCode").attr("countryno", countryno);
        identityShow($(this).html());
        $(".passPortNumber_promt").addClass("dis");

        //通行证提示/签发次数
        $(".identityCard_hkgmacPromt,.identityCard_twnPromt,.identityCard_hkgmacPromtTxt,.identityCard_twnPromtTxt").addClass("dis");
        $(".checkIN__signNumber").addClass("dis");
        $("#signNumberTxt").val("");

        if ($(this).html() != "中国") {
            $(".residenceAddress").addClass("dis");
            $(".sensitive").addClass("dis");
        } else {
            var province = $("#province").find("option:selected").text();
            var city = $("#city").find("option:selected").text();
            var area = $("#area").find("option:selected").text();
            isSensitive(province, city, area);
            $(".residenceAddress").removeClass("dis");
        }
    })

    function getCountryName() {
        for (var i = 0; i < countryListArray.length; i++) {
            $("#countrySelCon").append("<a countryno='" + countryListArray[i].code + "'>" + countryListArray[i].name + "</a>");
        }
    }

    //国籍中英文转换
    function countryChinese(countryName) {
        for (var i = 0; i < countryListArray.length; i++) {
            if (countryName == countryListArray[i].code) {
                countryName = countryListArray[i].name;
                return countryName;
            } else {
                if (countryName == countryListArray[i].name) {
                    countryName = countryListArray[i].code;
                    return countryName;
                }
            }
        }
    }

    //选择游轮
    $("#shipCode").click(function () {
        $("#shipSelCon").show();
        shipCodeCheck();
    })

    $("#shipSelectImg").click(function () {
        $("#shipSelCon").show();
        shipCodeCheck();
    })
    $("#shipSelCon").mouseleave(function () {
        $("#shipSelCon").css("display", "none");
    })
    $("#shipSelCon>a").bind("click", function () {
        SelectShip($(this).html(), $(this).attr("shipno"))
        shipCodeCheck();
    })

    if ($("#shipCode").attr("shipno") != null && $("#shipCode").val() != null) {
        SelectShip($("#shipCode").val(), $("#shipCode").attr("shipno"))
    }

    function SelectShip(shipName, shipCode) {
        $("#shipSelCon").css("display", "none");
        $("#shipCode").val(shipName);
        var shipno = shipCode;
        $("#shipCode").attr("shipno", shipno);
    }

    //第一步页面的校验绑定onblur事件
    $("#lastName").blur(function () {
        lastNameCheck();
    });
    $("#BookingNum").blur(function () {
        bookingNumCheck();
    })
    $("#stateroomNumber").blur(function () {
        stateroomNumberCheck();
    });
    $("#BookingIdNumber").blur(function () {
        BookingIdNumberCheck();
    });

    //出生日期
    $("#dateOfBirth").click(function () {
        birthCheck();
    })
    //出航日期
    $("#sailDate").click(function () {
        sailDateCheck();
    })

    var bookingNum = $(".input--selectBooking input[name='bookingNum']:checked").val();//booking=0,填写的是订单号    booking=1,填写的是舱房号

    //通过radio来选择舱房号还是订单号
    $(".input--selectBooking input[name='bookingNum']").click(function () {
        bookingNum = $(this).val();
        if (bookingNum == 0) {
            $("#bookingIdTip").show()
        } else {
            $("#bookingIdTip").hide()
        }
    })
    //第一步  提交资料
    $(".save_validData").click(function () {

        //舱房号和订单号二选一
        if (bookingNum == 1) {
            var stateroomNumber = $("#BookingNum").val().trim();
            var BookingIdNumber = "";
        } else {
            var stateroomNumber = "";//舱房号
            var BookingIdNumber = $("#BookingNum").val().trim();//订单号
        }

        var lastName = $("#lastName").val().trim();
        lastName = lastName.replace(/[ ]/g, "");
        var dateOfBirth = $("#dateOfBirth").val();
        var shipCode = $("#shipCode").attr("shipno");
        //var stateroomNumber = $("#stateroomNumber").val().trim();
        //var BookingIdNumber=$("#BookingIdNumber").val().trim();
        var sailDate = $("#sailDate").val();

        //判断当前登录乘客是否小于6个月，是，无法进行登记；否，可进行下一步
        try {
            var birthDate = new Date(dateOfBirth.replace("-", "/").replace("-", "/"));
            var sailDateTime = new Date(sailDate.replace("-", "/").replace("-", "/"));
        } catch (e) {
            console.log(e)
        }
        //6个月以下儿童不能进行在线登记
        var differTime = Math.ceil(sailDateTime.getTime() - birthDate.getTime()) / (60 * 60 * 1000 * 24);
        if (differTime < 183) {
            rdpAlert("年龄小于6个月的旅客无法登轮，请联系客服：" + $("#PcTelephone").val());
            return false;
        }

        //乘客合同勾选
        var isAgree = $(".isAgree").is(":checked");

        var isSubmit = submitValidData(isAgree, lastName, dateOfBirth, shipCode, stateroomNumber, BookingIdNumber, sailDate)

        //提交成功
        if (isSubmit == true) {
            startLoading("Step1");
            $.ajax({
                type: 'post',
                url: 'https://www.rcclchina.com.cn/api/Rccl.Olci/Olci/Step1',
                dataType: 'json',
                data: {
                    LastName: lastName,
                    DateOfBirth: dateOfBirth,
                    ShipCode: shipCode,
                    StateroomNumber: stateroomNumber,
                    SailDate: sailDate,
                    BookingId: BookingIdNumber,
                    Source: 'PCNEW'
                },
                success: function (response) {
                    stopLoading("Step1");
                    if (response.Success == true) {
                        BookingAccessToken = response.Data.BookingAccessToken;
                        directCustomer = response.Data.DirectCustomer;
                        hasPayed = response.Data.HasPayed;

                        //若是非直销客且没有支付，不能生成船票
                        if (directCustomer && !hasPayed) {
                            $(".printStick,.printStickShow").addClass("f-dn");
                            $(".printStickHide").removeClass("f-dn");
                        } else {
                            $(".printStick,.printStickShow").removeClass("f-dn");
                            $(".printStickHide").addClass("f-dn");
                        }

                        $("#guestListUL li").remove();
                        GetGuestInfo = [];
                        GetStickInfo = [];
                        isSubmitStick = 0;
                        LowAgeNum = 0;
                        stepOneResp = response;
                        retrieveGuestInformation();

                        //二维码弹窗显示
                        wxPicShow();

                        $("#step_form_2 .CheckIN__twoInfo div").eq(0).removeClass("f-dn");

                        if (response.Warning && response.Warning.length > 0) {
                            rdpAlert(response.Warning);
                        }

                    } else {
                        var error = response.ErrMsg;
                        if (error == "Locked") {
                            rdpAlert("您的预定现在被锁定，请稍后重试，或联系客服：" + $("#PcTelephone").val());
                        } else if (error == "Used") {
                            rdpAlert("您刚刚退出在线登记，请稍后重试");
                        }
                        else if (error == "TransmissionDone") {
                            $("#BookingNum").val("");
                            $("#BookingNumInfo").empty();
                            bookingNum = 0;
                            $(".input--selectBooking select option:first").attr("selected", true);
                            rdpAlert("距离出发时间小于48小时, 请使用订单号进行登记");
                        }
                        else if (error == "NotPaid") {
                            rdpAlert("您的预定未完成付款");
                        }
                        else if (error == "NotRight") {
                            rdpAlert("您输入的资料有误，请重新输入");
                        }
                        else if (error == "NotBooked") {
                            rdpAlert("您的订单付款未确认，请稍后重试");
                        }
                        else if (response.Code && response.Code > 0) {
                            rdpAlert(error)
                        }
                        else {
                            rdpAlert("您输入的资料有误，请重新输入");
                        }
                    }
                }
            })
        }

    })

    //第二步  返回上一步
    $(".return__previousStep").click(function () {
        updateGrantList = [];
        GranteeGuestList = [];
        clearInterval(timeInterval);
        $("#counttime").html("15分钟");
        clearInfo();
        $("#step_form_234").addClass("dis");
        $("#step_form_2").addClass("dis");
        $("#step_form_1").removeClass("dis");
        $(".step_li li").removeClass("curr");
        $(".step_li li").eq(0).addClass("curr").addClass("step_1");
    })

    var picValidateNum; //在全局定义验证码
    var picId;//在全局定义验证码id
    //产生图形验证码
    function createCode() {
        $.ajax({
            type: 'post',
            url: 'https://www.rcclchina.com.cn/api/Orchard.Rccl.Users/Util/PictureVerificationCode',
            success: function (resp) {
                if (resp.Success) {
                    code = resp.Data.ValidateNum;
                    picValidateNum = resp.Data.ValidateNum;
                    picId = resp.Data.Id;
                    $("#code").attr("src", "data:image/png;base64," + resp.Data.ImageBase64);
                } else {
                    //console.log(resp)
                    alert(resp.errMsg);
                }
            }
        })
    }
    createCode();
    $(".changeCode").click(function () {
        createCode();
        return false;
    })

    var selectTelNum = 0;//selectTelNum=0,境内    selectTelNum=1,境外

    $(".input--selectTel select").change(function () {
        selectTelNum = $(".input--selectTel select").get(0).selectedIndex;
        if (selectTelNum == 0) {
            IsDomestic = false;
            $(".input--smsValid").removeClass("dis");
            $(".addEmailPromt").addClass("f-dn");
            //if (isChangeTel) {
            //    $(".input--smsValid").removeClass("dis");
            //}
        } else if (selectTelNum == 1) {
            IsDomestic = true;
            $(".input--Captcha,.input--smsValid").addClass("dis");
            $(".addEmailPromt").removeClass("f-dn");
        }
    })
    //第二步  提交资料
    $("#submitstep2").click(function () {
        $("#granteelist").empty();
        getcheckInStick(GuestId);
        var nameEnglish;
        nameEnglish = $("#guestName").text();
        var localLastName = $("#localeLastName").val().trim();
        var localFirstName = $("#localeFirstName").val().trim();
        var gender = $("input[name='gender']:checked").val();
        var countryCode = $("#countryCode").attr("countryno");
        var passPortNumber = $("#passPortNumber").val().trim();
        var passPortDate = $("#passPortDate").val();
        var passPortDateNum = $("#passPortDate").attr("numDate");
        var vipNum = $("#vipNum").val().trim();
        var province = $("#province").find("option:selected").text();
        var city = $("#city").find("option:selected").text();
        var area = $("#area").find("option:selected").text();
        var identityCardNumber = $("#identityCardNumber").val().trim();

        //高危资料
        var sensitiveResidence = $("#sensitiveResidence").val();
        var sensitiveAddress = $("#sensitiveAddress").val();
        var sensitiveCA = $("#sensitiveCA").val();
        var sensitiveportDate = $("#sensitiveportDate").val();
        var sensitiveOccupation = $("#sensitiveOccupation").val();
        var sensitiveFirstGo = $("input[name='firstArrival']:checked").val();
        var scanningImgs = [];
        for (var i = 0; i < $(".sensitiveScanningPic").length; i++) {
            scanningImgs[i] = $(".sensitiveScanningPic").eq(i).find("img").attr("src");
        }


        //本人联络人信息
        var telephone = $("#telephone").val().trim();
        var valid = $("#valid").val().trim().toUpperCase();//图形验证码
        var smsValid = $("#smsValid").val().trim();//短信验证码
        var contactProvince = $("#ContactProvince").find("option:selected").text();
        var contactCity = $("#ContactCity").find("option:selected").text();
        //var contactArea = $("#ContactArea").find("option:selected").text();
        var contactArea = $("#ContactAddress").val();

        var contactEmail = $("#ContactEmail").val();//邮箱

        //紧急联络人信息
        var emergencyTelephone = $("#emergencyTelephone").val().trim();

        //出航日期 游轮
        var shipCode = $("#shipCode").attr("shipno");
        var sailDate = $("#sailDate").val();

        //checkbox
        var isAgreeEmail = $(".agreeCb").is(':checked');
        var agreeInfo = $(".agreeInfo").is(':checked');

        passportAdditional = $("#signNumberTxt").val();//签发次数

        var isSubmit = guestInfoFun(passportAdditional, localLastName, localFirstName, gender, countryCode, passPortNumber, passPortDate, vipNum, telephone, province, city, area,
                                emergencyTelephone, identityCardNumber, contactProvince, contactCity, contactArea, smsValid, contactEmail, agreeInfo);
        if (isSubmit) {
            if (!($(".sensitive").hasClass("dis"))) {
                isSubmit = dangerInfo(sensitiveResidence, sensitiveAddress, sensitiveCA, sensitiveportDate, sensitiveOccupation, sensitiveFirstGo, scanningImgs);
            }
        } else {
            return false;
        }

        //在线登记护照时间限制
        //护照日期
        var goTimeCon = $("#passPortDate").attr("numdate");
        goTimeCon = new Date(goTimeCon);
        //出航日期
        //var nowTime = $("#sailDate").attr("cdate");
        var nowTime = $("#sailDate").val();
        nowTime = new Date(nowTime);
        var passDueTime = (goTimeCon.getTime() - nowTime.getTime()) / (60 * 60 * 1000 * 24);
        if (passDueTime < 180) {
            rdpAlert("护照有效期时间距离开航日期应大于6个月，请尽快办理有效的护照");
            isSubmit = false;
            return false;
        } else {
            var dateOfBirth = $("#dateOfBirth").val();
            if (!ageInfoSHow(dateOfBirth)) {
                isSubmit = false;
                return false;
            }
        }

        if (isSubmit) {
            var data = {
                BookingId: BookingId, GuestId: GuestId, BookingAccessToken: BookingAccessToken,
                LocalLastName: localLastName, LocalFirstName: localFirstName,
                PassPortNumber: passPortNumber, PassPortDate: passPortDateNum,
                CountryCode: countryCode, Gender: gender, MemeberShipId: vipNum,
                SensitiveResidence: sensitiveResidence, SensitiveAddress: sensitiveAddress, SensitiveCA: sensitiveCA,
                SensitiveportDate: sensitiveportDate, SensitiveOccupation: sensitiveOccupation,
                SensitiveFirstGo: sensitiveFirstGo, ContactTelephone: telephone, VerifyCode: smsValid,
                Province: province, City: city, Area: area, ScanningImgs: scanningImgs,
                IdCard: identityCardNumber, Telephone: emergencyTelephone, IsAgreeEmail: isAgreeEmail,
                ContactProvince: contactProvince, ContactCity: contactCity, ContactAddress: contactArea,
                IsDomestic: IsDomestic, SailDate: sailDate, ShipCode: shipCode, ContactEmail: contactEmail,
                OutCHNTel: IsDomestic, PassportAdditional: passportAdditional
            }
            startLoading();
            try {
                $.ajax({
                    type: 'post',
                    url: 'https://www.rcclchina.com.cn/api/Rccl.Olci/Olci/UpsertGuestInformationV2',
                    dataType: 'json',
                    data: data,
                    success: function (response) {
                        stopLoading();
                        if (response.Success) {
                            $(".stepFour__name").text(nameEnglish + " " + localLastName + localFirstName);
                            if (gender == "F") {
                                $(".stepFour__sex").text("女");
                            } else {
                                $(".stepFour__sex").text("男");
                            }
                            $(".stepFour__countryName").text(countryChinese(countryCode));
                            if (countryChinese(countryCode) != "中国") {
                                $(".stepFour__identityCard").addClass("dis");
                                if (countryChinese(countryCode) == "中国香港" || countryChinese(countryCode) == "中国澳门") {
                                    $(".stepFour__identityCardTxt").text("港澳居民来往内地通行证号码：");
                                    $(".stepFour__identityCard").removeClass("dis");
                                } else if (countryChinese(countryCode) == "中国台湾") {
                                    $(".stepFour__identityCardTxt").text("台湾居民来往大陆通行证号码：");
                                    $(".stepFour__identityCard").removeClass("dis");
                                }
                            } else {
                                $(".stepFour__identityCardTxt").text("身份证：");
                                $(".stepFour__identityCard").removeClass("dis");
                            }
                            $(".stepFour__identityCardNum").text(identityCardNumber);
                            $(".stepFour__passportNumber").text(passPortNumber);
                            $(".stepFour__passportData").text(passPortDate);
                            $(".stepFour__birthDate").text($("#birthday").text());
                            $(".stepFour__shipName").text($("#shipCode").val());
                            $(".stepFour__sailDate").text($("#sailDate").attr("cdate"));
                            $(".stepFour__ownTelephone").text(telephone);
                            $(".stepFour__telephone").text(emergencyTelephone);

                            //支付关联人信息(可选或已选)
                            for (var i = 0; i < GetStickInfo.length; i++) {
                                var saveNameEnglish = GetStickInfo[i].LastName + " " + GetStickInfo[i].FirstName;
                                var cardList = "<div class='input' GuestId='" + GetStickInfo[i].GuestId + "' BookingId='" + GetStickInfo[i].BookingId + "' style='height:40px;border:2px solid #0073bb;margin-bottom:10px;cursor:poniter;position:relative;' ischecked='1'>"
                                             + "<p><span class='CardFirstName'>" + GetStickInfo[i].FirstName + "&nbsp;</span><span class='CardLastName'>" + GetStickInfo[i].LastName + "<i class='img' style='position:absolute;top:11px;right:15px;'></i></p>"
                                             + "</div>";
                                $("#granteelist").append(cardList);
                            }
                            getCardGuestInfo();

                        } else {
                            $("#valid").val("");
                            $("#smsValid").val("");
                            createCode();
                            if (response.Code == 20) {
                                rdpAlert("短信验证码输入错误！");
                                //$(".input--smsValid").removeClass("dis");
                                return;
                            } else if (response.Code == 15) {
                                rdpAlert("本人联系方式和紧急联系人不能相同!");
                                return;
                            }
                            rdpAlert(response.ErrMsg);
                        }
                    },
                    error: function (error) {
                        //console.log(error)
                        stopLoading();
                    }
                })
            } catch (e) {
                //console.log(e)
            }
        }
    })

    function getCardGuestInfo() {
        startLoading();
        try {
            $.ajax({
                type: 'get',
                url: 'https://www.rcclchina.com.cn/api/Rccl.Olci/Olci/OnboardPayment',
                dataType: 'json',
                data: { BookingId: BookingId, GuestId: GuestId, BookingAccessToken: BookingAccessToken },
                success: function (response) {

                    if (response.Success) {
                        if (response.Data != null) {
                            //PaymentType 如果是5, 代表信用卡  如果是10, 代表现金    如果是15, 代表不选择
                            if (response.Data.PaymentType == 10 || response.Data.PaymentType == 15) {
                                isGrantee = false;
                                $("#cardUserName").val("");
                                $("#cardNumber").val("");
                                $(".stepFour__paymethod").text("现金");
                                chooseCardType(2);//现金                                     
                            } else if (response.Data.PaymentType == 5 || response.Data.PaymentType == 0) {
                                //判断是true持卡人, false是被关联人
                                if (response.Data.IsGrantor) {
                                    $("#cardUserName").val(response.Data.CardHolderName);
                                    $("#cardNumber").val("xxxxxxxxxxxx" + response.Data.CardPartialNumber);
                                    $("#cardMonSelect").val(response.Data.CardExpirationMonth);
                                    $("#cardYearSelect").val(response.Data.CardExpirationYear);
                                    isGrantee = false;
                                    xykValid = true;
                                    $("#cardNumberInfo").removeClass("dis");//输入正确信用卡卡号提示

                                } else {
                                    for (var i = 0; i < GetStickInfo.length; i++) {
                                        if (response.Data.GrantorGuestId == GetStickInfo[i].GuestId) {
                                            $(".cardholder").text(GetStickInfo[i].LocalLastName + " " + GetStickInfo[i].LocalFirstName);
                                        }
                                    }
                                    isGrantee = true;
                                    xykValid = false;
                                }
                                $(".stepFour__paymethod").text("信用卡");
                                chooseCardType(1);//信用卡   
                            }
                            //else if (response.Data.PaymentType == 15) {
                            //    isGrantee = false;
                            //    $(".stepFour__paymethod").text("暂不选择任何支付方式");
                            //    chooseCardType(3);//暂不选择 
                            //}
                        } else {
                            isGrantee = false;
                            xykValid = true;
                            $("#cardUserName").val("");
                            $("#cardNumber").val("");
                            $(".stepFour__paymethod").text("信用卡");
                            chooseCardType(1);
                        }

                        $("#step_form_2,#step_form_4").addClass("dis");
                        $("#step_form_3").removeClass("dis");
                        $(".step_li li").removeClass("step_1").removeClass("curr");
                        $(".step_li li").eq(2).addClass("curr");
                        stopLoading();
                        xyCardTime();
                    }
                },
                error: function (error) {
                    stopLoading();
                }
            })
        } catch (e) {
            //console.log(e)
        }
    }

    //选择共用信用卡的同行人员
    $("#granteelist .input").live("click", function () {
        var isCurr = $(this).hasClass("curr");
        if (isCurr) {
            $(this).removeClass("curr");
        } else {
            $(this).addClass("curr");
        }
    })

    //全部生成船票按钮
    $(".btn_ticket").click(function () {
        //var telephone = $("#telephone").val().trim();
        //var valid = $("#valid").val().trim();
        //var validCode = $("#code").val().trim();//随机图形验证码
        //var smsValid = $("#smsValid").val().trim();//短信验证码
        //var isSubmit = submitStickInfo(telephone, valid, validCode, smsValid);
        if ($(".btn_ticket").hasClass("invalid")) {
            rdpAlert("请先全部完成登记");
            return false;
        }
        startLoading();
        //生成全部船票
        $.ajax({
            type: 'post',
            url: 'https://www.rcclchina.com.cn/api/Rccl.Olci/Olci/CreateBoardingPassAll',
            data: { BookingId: BookingId, BookingAccessToken: BookingAccessToken },
            success: function (response) {
                if (response.Success) {
                    //rdpAlert("成功生成全部船票！");
                    //console.log(response.Data)
                    for (var i = 0; i < response.Data.length; i++) {
                        var guest = response.Data[i];
                        var bpassId = guest.BpassID
                        window.open("../../Rccl.ESLAPI/PassNew/Pdf?boardingPassId=" + bpassId);
                    }
                }
                stopLoading();
            }
        })
    })

    // 第一步  验证资料
    function submitValidData(isAgree, lastName, dateOfBirth, shipCode, stateroomNumber, BookingIdNumber, sailDate) {
        //拼音
        if (lastName == "") {
            rdpAlert("请输入姓氏拼音", "", function () {
                $("#lastName").focus();
            });
            return false;
        } else if (!lastNameCheck()) {
            rdpAlert("请输入正确的姓氏拼音", "", function () {
                $("#lastName").focus();
            });
            return false;
        }
        //出生日期
        if (dateOfBirth == "") {
            rdpAlert("请输入出生日期", "", function () {
                $("#dateOfBirth").focus();
            });
            return false;
        } else if (!birthCheck()) {
            rdpAlert("请输入正确的出生日期", "", function () {
                $("#dateOfBirth").focus();
            });
            return false;
        }
        //所乘游轮
        if (shipCode == "" || shipCode == undefined) {
            rdpAlert("请选择所乘游轮", "", function () {
                $("#shipCode").focus();
            });
            return false;
        } else if (!shipCodeCheck()) {
            rdpAlert("请选择所乘游轮", "", function () {
                $("#shipCode").focus();
            });
            return false;
        }
        //舱房号和订单号验证
        if (stateroomNumber == "") {
            if (BookingIdNumber == "") {
                rdpAlert("舱房号和订单号二选一填写！");
                return false;
            } else {
                if (!bookingNumCheck()) {
                    rdpAlert("请输入正确的订单号", "", function () {
                        $("#BookingIdNumber").focus();
                    });
                    return false;
                }
            }
        } else {
            if (!bookingNumCheck()) {
                rdpAlert("请输入正确的舱房号", "", function () {
                    $("#stateroomNumber").focus();
                });
                return false;
            }
        }

        //出航日期
        if (sailDate == "") {
            rdpAlert("请选择出航日期", "", function () {
                $("#sailDate").focus();
            });
            return false;
        } else if (!sailDateCheck()) {
            rdpAlert("请选择出航日期", "", function () {
                $("#sailDate").focus();
            });
            return false;
        }
        if (!isAgree) {
            rdpAlert("请勾选同意乘客票据合同");
            return false;
        }

        return true;
    }


    // 第二步  验证资料
    function guestInfoFun(passportAdditional, localLastName, localFirstName, gender, countryCode,
                            passPortNumber, passPortDate, vipNum, telephone,
                            province, city, area, emergencyTelephone,
                            identityCardNumber,
                            contactProvince, contactCity, contactArea, smsValid, contactEmail, agreeInfo) {
        //中文姓
        if (localLastName == "") {
            rdpAlert("请输入中文姓", "", function () {
                $("#localeLastName").focus();
            });
            return false;
        } else {
            if (countryCode == "CHN") {
                if (!localNameCheck("localeLastName", "中文姓")) {
                    rdpAlert("请输入正确的中文姓", "", function () {
                        $("#localeLastName").focus();
                    });
                    return false;
                }
            } else {
                if (!localNameCheck("localeLastName", "中文姓") || !pyNameCheck("localeLastName", "中文姓")) {
                    $("#localeLastNameInfo").text("");
                } else {
                    rdpAlert("请输入正确的中文姓", "", function () {
                        $("#localeLastName").focus();
                    });
                    return false;
                }
            }

        }
        //中文名
        if (localFirstName == "") {
            rdpAlert("请输入中文名", "", function () {
                $("#localeFirstName").focus();
            });
            return false;
        } else {
            if (countryCode == "CHN") {
                if (!localNameCheck("localeFirstName", "中文名")) {
                    rdpAlert("请输入正确的中文名", "", function () {
                        $("#localeFirstName").focus();
                    });
                    return false;
                }
            } else {
                if (!localNameCheck("localeFirstName", "中文名") || !pyNameCheck("localeFirstName", "中文名")) {
                    $("#localeFirstNameInfo").text("");
                } else {
                    rdpAlert("请输入正确的中文名", "", function () {
                        $("#localeFirstName").focus();
                    });
                    return false;
                }
            }

        }
        //性别
        if (gender == "" || gender == undefined) {
            rdpAlert("请选择性别", "", function () {
                $(".checkIN__gender").focus();
            });
            return false;
        }
        //国籍
        if (countryCode == "") {
            rdpAlert("请选择所属国籍", "", function () {
                $("#countryCode").focus();
            });
            return false;
        } else if (!countryCodeCheck()) {
            rdpAlert("请选择所属国籍", "", function () {
                $("#countryCode").focus();
            });
            return false;
        }
        if (!($(".residenceAddress").hasClass("dis"))) {
            //省
            if (province == "请选择" || province == "") {
                rdpAlert("请选择户籍地的省");
                return false;
            }
            //市
            if (city == "请选择" || city == "") {
                rdpAlert("请选择户籍地的市");
                return false;
            }
            //区
            if (area == "请选择" || area == "") {
                rdpAlert("请选择户籍地的区");
                return false;
            }
        }

        //身份证   台湾身份证样本：A234567890  香港身份证样本：C668668（9）
        if (countryCode == "CHN") {
            if (identityCardNumber == "") {
                rdpAlert("请输入身份证", "", function () {
                    $("#identityCardNumber").focus();
                });
                return false;
            } else if (countryCode == "CHN" && identityCardNumber.length > 18) {
                rdpAlert("请输入正确的身份证", "", function () {
                    $("#identityCardNumber").focus();
                });
                return false;
            }
        } else if (countryCode == "HKG" || countryCode == "MAC" || countryCode == "TWN") {
            if (identityCardNumber == "") {
                rdpAlert("请输入通行证", "", function () {
                    $("#identityCardNumber").focus();
                });
                return false;
            } else if (identityCardNumber.length > 12) {
                rdpAlert("请输入正确的通行证", "", function () {
                    $("#identityCardNumber").focus();
                });
                return false;
            }
        }
        //护照号码
        if (passPortNumber == "") {
            rdpAlert("请输入护照号码", "", function () {
                $("#passPortNumber").focus();
            });
            return false;
        } else if (!passPortNumberCheck()) {
            rdpAlert("请输入正确的护照号码", "", function () {
                $("#passPortNumber").focus();
            });
            return false;
        }
        //护照有效期
        if (passPortDate == "") {
            rdpAlert("请选择护照有效期", "", function () {
                $("#passPortDate").focus();
            });
            return false;
        } else if (!passPortDateCheck()) {
            rdpAlert("请选择护照有效期", "", function () {
                $("#passPortDate").focus();
            });
            return false;
        }

        if (vipNum != "") {
            if (!isVIP(vipNum)) {
                rdpAlert("请输入正确格式的会员号码");
                return false;
            }
        }
        //紧急联络人电话
        if (emergencyTelephone == "") {
            rdpAlert("请输入紧急联络人电话", "", function () {
                $("#emergencyTelephone").focus();
            });
            return false;
        } else if (!checkEmergencyPhone(emergencyTelephone)) {
            rdpAlert("请输入正确的紧急联络人电话", "", function () {
                $("#emergencyTelephone").focus();
            });
            return false;
        }

        //联系电话
        if (telephone == "") {
            rdpAlert("请输入乘客本人电话", "", function () {
                $("#telephone").focus();
            });
            return false;
        } else if (!checkPhone(telephone)) {
            if (!IsDomestic) {
                rdpAlert("请输入正确的乘客本人电话", "", function () {
                    $("#telephone").focus();
                });
                return false;
            }
        }

        //邮箱
        if (contactEmail != "") {
            if (!IsMail(contactEmail)) {
                rdpAlert("请输入正确的邮箱", "", function () {
                    $("#ContactEmail").focus();
                });
                return false;
            }
        } else {
            if (IsDomestic) {
                rdpAlert("请输入邮箱", "", function () {
                    $("#ContactEmail").focus();
                });
                return false;
            }
        }

        if (countryCode == "CHN") {
            //省
            if (contactProvince == "请选择" || contactProvince == "") {
                rdpAlert("请选择联系人地址中的省");
                return false;
            }
            //市
            if (contactCity == "请选择" || contactCity == "") {
                rdpAlert("请选择联系人地址中的市");
                return false;
            }
            //详细地址
            if (contactArea == "请选择" || contactArea == "") {
                rdpAlert("请将联系人地址填写完整");
                return false;
            }
        }
        //图形验证码
        //if (valid == "") {
        //    rdpAlert("请输入图形验证码");
        //    $("#valid").focus();
        //    return false;
        //} else {
        //    if (valid != picValidateNum.toUpperCase()) {
        //        rdpAlert("请输入正确的图形验证码");
        //        createCode();
        //        $("#valid").focus();
        //        return false;
        //    }
        //}
        //短信验证码
        if (!IsDomestic) {
            if (smsValid == "" || smsValid == null) {
                rdpAlert("请输入短信验证码");
                return false;
            }
        }

        //是否同意
        if (!agreeInfo) {
            rdpAlert("请勾选确认拼音姓名正确");
            return false;
        }
        //签发次数为纯数字
        if (countryCode == "HKG" || countryCode == "MAC" || countryCode == "TWN") {
            if (passportAdditional != "") {
                if (!pureDigital(passportAdditional)) {
                    rdpAlert("请输入正确的签发次数");
                    return false;
                }
            }
        }

        return true;
    }

    //高危资料验证
    function dangerInfo(sensitiveResidence, sensitiveAddress, sensitiveCA,
                        sensitiveportDate, sensitiveOccupation, sensitiveFirstGo, scanningImgs) {
        if (sensitiveResidence == "") {
            rdpAlert("出生地不能为空！");
            return false;
        }
        if (sensitiveAddress == "") {
            rdpAlert("户籍地址不能为空！");
            return false;
        }
        if (sensitiveCA == "") {
            rdpAlert("发证机关不能为空！");
            return false;
        }
        if (sensitiveportDate == "") {
            rdpAlert("护照签发日期不能为空！");
            return false;
        }
        if (sensitiveOccupation == "") {
            rdpAlert("职业现状不能为空！");
            return false;
        }
        if (sensitiveFirstGo == undefined || sensitiveFirstGo == "") {
            rdpAlert("是否首次出镜不能为空！");
            return false;
        }
        if (scanningImgs == null || scanningImgs == "") {
            rdpAlert("请上传户口本、出生证扫描件！");
            return false;
        }
        return true;
    }

    //生成船票  联系人资料验证
    function submitStickInfo(telephone, valid, smsValid) {
        //联系电话
        if (telephone == "") {
            rdpAlert("请输入联系电话", "", function () {
                $("#telephone").focus();
            });
            return false;
        } else if (!checkPhone(telephone)) {
            rdpAlert("请输入正确的联系电话", "", function () {
                $("#telephone").focus();
            });
            return false;
        }
        //图形验证码
        if (valid == "") {
            rdpAlert("请输入图形验证码");
            $("#valid").focus();
            return false;
        } else {
            if (valid != picValidateNum.toUpperCase()) {
                rdpAlert("请输入正确的图形验证码");
                $("#valid").focus();
                return false;
            }
        }
        //短信验证码
        if (smsValid == "" || smsValid == null) {
            rdpAlert("请输入短信验证码");
            return false;
        }
        return true;
    }

    //英文姓名
    function lastNameCheck() {
        var value = $("#lastName").val().trim().replace(/[ ]/g, "");
        var name = "lastNameInfo";
        // 非空验证
        if (value == "") {
            $("#" + name).text("姓氏拼音不能为空");
            $("#" + name).css('color', 'red');
            return false;
        }
        // 纯英文验证
        if (!/^[A-Za-z]*$/.test(value)) {
            $("#" + name).text("请输入正确的姓氏拼音");
            $("#" + name).css('color', 'red');
            return false;
        }
        $("#" + name).empty();
        $("#" + name).append("<i class='okimg'></i>");
        return true;
    }

    //生日检查
    function birthCheck() {
        var birthId = "dateOfBirth";
        var name = "dateOfBirthInfo";
        var msg = "出生日期";
        var flag = dateCheck(birthId, msg);
        if (!flag) {
            return false;
        }
        $("#" + name).empty();
        $("#" + name).append("<i class='okimg'></i>");
        return true;
    }

    //出航日期验证
    function sailDateCheck() {
        var id = "sailDate";
        var name = "sailDateInfo";
        var msg = "出航日期";
        var flag = dateCheck(id, msg);
        if (!flag) {
            return false;
        }
        $("#" + name).empty();
        $("#" + name).append("<i class='okimg'></i>");
        return true;
    }

    //游轮验证
    function shipCodeCheck() {
        var shipCode = $("#shipCode").attr("shipno");

        if (shipCode == "") {
            $("#shipCodeInfo").empty();
            $("#shipCodeInfo").text("所乘游轮不能为空");
            $("#shipCodeInfo").css('color', 'red');
            return false;
        }
        $("#shipCodeInfo").empty();
        $("#shipCodeInfo").append("<i class='okimg'></i>");
        return true;
    }

    //舱房号与订单号验证
    function bookingNumCheck() {
        var bookingNumber = $("#BookingNum").val().trim();
        var name = "BookingNumInfo";
        if (bookingNumber == "") {
            return false;
        }
        if (parseInt(bookingNum) == 0) {
            if (!/^[0-9]*$/.test(bookingNumber)) {
                $("#" + name).text("请输入正确的订单号");
                $("#" + name).css('color', 'red');
                return false;
            }
        }
        $("#" + name).empty();
        $("#" + name).append("<i class='okimg'></i>");
        return true;
    }
    //房间号验证
    //function stateroomNumberCheck() {
    //    var stateroomNumber = $("#stateroomNumber").val().trim();
    //    var name = "stateroomNumberInfo";
    //    if (stateroomNumber == "") {
    //    //    $("#" + name).text("舱房号不能为空");
    //    //    $("#" + name).css('color', 'red');
    //        return false;
    //    }
    //    if (!/^[0-9]*$/.test(stateroomNumber)) {
    //        $("#" + name).text("请输入正确的舱房号");
    //        $("#" + name).css('color', 'red');
    //        return false;
    //    }
    //    $("#" + name).empty();
    //    $("#" + name).append("<i class='okimg'></i>");
    //    return true;
    //}

    //订单号验证
    //function BookingIdNumberCheck() {
    //    var BookingIdNumber = $("#BookingIdNumber").val().trim();
    //    var name = "BookingIdNumberInfo";
    //    if (BookingIdNumber == "") {
    //    //    $("#" + name).text("订单号不能为空");
    //    //    $("#" + name).css('color', 'red');
    //        return false;
    //    }
    //    if (!/^[0-9]*$/.test(BookingIdNumber)) {
    //        $("#" + name).text("请输入正确的订单号");
    //        $("#" + name).css('color', 'red');
    //        return false;
    //    }
    //    $("#" + name).empty();
    //    $("#" + name).append("<i class='okimg'></i>");
    //    return true;
    //}

    //日期的验证共通  msg：提示信息
    function dateCheck(id, msg) {
        var dateObj = $("#" + id).val();
        var dateInfo = id + "Info";
        // 非空验证
        if (dateObj == "") {

            $("#" + dateInfo).text(msg + "不能为空");
            $("#" + dateInfo).css('color', 'red');
            return false;
        }
        return true;
    }

    //中文姓名
    function localNameCheck(id, msg) {
        var value = $("#" + id).val().trim();
        if (value == "") {
            $("#" + id + "Info").text(msg + "不能为空");
            $("#" + id + "Info").css("color", "red");
            return false;
        }
        var len = value.length;
        if (len >= 5) {
            $("#" + id + "Info").text(msg + "长度只能在5个字以内");
            $("#" + id + "Info").css("color", "red");
            return false;
        }
        //中文验证
        if (!testCHN.test(value)) {
            $("#" + id + "Info").text("请输入正确的" + msg);
            $("#" + id + "Info").css('color', 'red');
            return false;
        }
        $("#" + id + "Info").empty();
        //$("#" + id + "Info").append("<i class='okimg'></i>");
        return true;
    }

    function pyNameCheck(id, msg) {
        var value = $("#" + id).val().trim();
        if (value == "") {
            $("#" + id + "Info").text(msg + "不能为空");
            $("#" + id + "Info").css("color", "red");
            return false;
        }
        //中文验证
        if (!/^[A-Za-z]+$/.test(value)) {
            $("#" + id + "Info").text("请输入正确的" + msg);
            $("#" + id + "Info").css('color', 'red');
            return false;
        }
        $("#" + id + "Info").empty();
        return true;
    }


    //国籍验证
    function countryCodeCheck() {
        var id = "countryCode";
        var value = $("#" + id).val();

        if (value == "") {
            $("#" + id + "Info").text("所属国籍不能为空");
            $("#" + id + "Info").css("color", "red");
            return false;
        }

        $("#" + id + "Info").empty();
        //$("#" + id + "Info").append("<i class='okimg'></i>");
        return true;
    }

    //护照验证
    function passPortNumberCheck() {
        var id = "passPortNumber";
        var value = $("#" + id).val().trim();
        //护照为"TBA"时 ，设为空
        if (value.trim() == "TBA") {
            $("#" + id).val("");
            value = "";
        }

        if (value == "") {
            $("#" + id + "Info").text("护照号码不能为空");
            $("#" + id + "Info").css("color", "red");
            return false;
        }
        var len = value.length;
        if (len >= 18) {
            $("#" + id + "Info").text("护照号码长度只能在12位以内");
            $("#" + id + "Info").css("color", "red");
            return false;
        }
        if (!/^[A-Za-z0-9]+$/.test(value)) {
            $("#" + id + "Info").text("请输入正确的护照号码");
            $("#" + id + "Info").css('color', 'red');
            return false;
        }

        $("#" + id + "Info").empty();
        //$("#" + id + "Info").append("<i class='okimg'></i>");
        return true;
    }

    //护照日期验证
    function passPortDateCheck() {
        var id = "passPortDate";
        var value = $("#" + id).val();

        if (value == "") {
            $("#" + id + "Info").text("护照有效期不能为空");
            $("#" + id + "Info").css("color", "red");
            return false;
        }

        $("#" + id + "Info").empty();
        //$("#" + id + "Info").append("<i class='okimg'></i>");
        return true;

    }

    //会员号码数字化判断
    $("#vipNum").blur(function () {
        var vipNumber = $("#vipNum").val().trim();
        if (vipNumber != "") {
            if (!isVIP(vipNumber)) {
                rdpAlert("请输入正确格式的会员号码");
            }
        }
    })

    //会员号码
    function vipNumCheck() {
        var id = "vipNum";
        var vipNum = $("#" + id).val().trim();
        if (vipNum == "123456") {
            return true;
        } else {
            rdpAlert("该会员号不存在");
            return false;
        }
    }

    //手机号判断
    function checkPhone(phone) {
        var phoneRex = /^(13[0-9]{9})|(14[0-9]{9})|(15[0-9]{9})|(18[0-9]{9})|(17[0-9]{9})$/;
        if (phone == "" || phoneRex.test(phone) == false || phone.length > 11) {
            return false;
        }
        return true;
    }
    //紧急联系号码判断包括数字和下划线
    function checkEmergencyPhone(phone) {
        var phoneRex = /^[0-9\-\—]*$/;
        if (phone == "" || phoneRex.test(phone) == false) {
            return false;
        }
        return true;
    }

    //纯数字判断
    function pureDigital(number) {
        var phoneRex = /^[0-9]*$/;
        if (number == "" || phoneRex.test(number) == false) {
            return false;
        }
        return true;
    }

    var isFirstSMS = true;//是否是第一次发送短信 true 第一次
    //短信验证码
    $("#getValid").click(function (e) {
        var phone = $("input[name='telephone']").val().trim();
        if (imgCodeEnable) {

            //联系电话
            if (phone == "") {
                rdpAlert("请输入乘客本人电话", "", function () {
                    $("#telephone").focus();
                });
                imgCodeEnable = true;
                return;
            } else if (!checkPhone(phone)) {
                rdpAlert("请输入正确的乘客本人电话", "", function () {
                    $("#telephone").focus();
                });
                imgCodeEnable = true;
                return;
            }
            //图形验证码
            if (!isFirstSMS) {
                $(".input--Captcha").removeClass("dis");
                var valid = $("input[name='valid']").val().trim().toUpperCase();//随机图形验证码
                if (valid == "") {
                    rdpAlert("请输入图形验证码");
                    imgCodeEnable = true;
                    return;
                } else {
                    if (valid != picValidateNum.toUpperCase()) {
                        rdpAlert("请输入正确的图形验证码");
                        imgCodeEnable = true;
                        return;
                    }
                }
            }
            $.ajax({
                url: "https://www.rcclchina.com.cn/api/Orchard.Rccl.Users/VerifyCodeApi/Get?phone=" + phone + "&picValidateNumber=" + picValidateNum + "&picId=" + picId,
                type: 'GET',
                success: function (resp) {
                    imgCodeEnable = false;
                    isFirstSMS = false;
                    if (resp.Success) {
                        rdpAlert("短信发送成功！如果您没有收到短信验证，请一分钟后重新发送。");
                        //$(".input--Captcha").removeClass("dis");
                        isFirstSMS = false;
                    } else {
                        rdpAlert("短信发送失败！请稍后再发送");
                        // console.log(resp)
                    }
                    $("#getValid").html("验证码(" + "<span>60</span>" + "s)");
                    var codeInterval = setInterval(function () {
                        var sendCodeTxt = $("#getValid span").text();
                        if (sendCodeTxt <= 0) {
                            $("#getValid").html("获取验证码");
                            imgCodeEnable = true;
                            clearInterval(codeInterval);
                        } else {
                            sendCodeTxt--;
                            $("#getValid span").text(sendCodeTxt);
                        }
                    }, 1000)
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    imgCodeEnable = true;
                }
            });
        }
        return false;
    });


    //左侧登记状态点击事件
    $("#guestListUL li").live("click", function () {

        //通行证提示/签发次数
        $(".identityCard_hkgmacPromt,.identityCard_twnPromt,.identityCard_hkgmacPromtTxt,.identityCard_twnPromtTxt").addClass("dis");
        $(".checkIN__signNumber").addClass("dis");
        $("#signNumberTxt").val("");

        passengerOrder = $(this).index();
        passengerOrder = parseInt(passengerOrder) - 1;

        createCode();
        getcheckInStick(GuestId);
        clearInfo();
        GuestId = $(this).find(".guest").attr("guestid");
        BookingId = $(this).find(".guest").attr("bookingid");

        //标志左侧被选择的乘客图标
        $("#guestListUL li").find(".gimage_focus").removeClass("gimage_focus").addClass("gimage");
        $("#guestListUL li").each(function () {
            var index = $(this).index();
            index = parseInt(index) - 1;
            var guestDoingId = $(this).find(".guest").attr("guestid");
            if (GuestId == guestDoingId) {

                birthOfDate = GetStickInfo[index].DateOfBirth;

                //当前用户已进行到哪一步  5---第一步验证  10---完成填写资料  15---已生成船票
                var checkProgress = GetStickInfo[index].CheckProgress;
                if (checkProgress == 20) {
                    $(".button--regenerateTicket").removeClass("f-dn");
                } else {
                    $(".button--regenerateTicket").addClass("f-dn");
                }
                $(this).find(".gimage").removeClass("gimage").addClass("gimage_focus");
                if ($(this).find(".guest").hasClass("complete")) {
                    //停止填写资料倒计时
                    $(".step_speed .step_prompt").removeClass("dis");
                    getBoardingPass(BookingId, BookingAccessToken, GuestId);
                } else {
                    getStepNum(checkProgress);
                }
                return;
            }
        })



    })

    //保存正在登记的用户船票信息
    function getcheckInStick(GuestId) {

        //新增信息
        var identityCardNumber = $("#identityCardNumber").val();
        var emergencyTelephone = $("#emergencyTelephone").val();
        var ContactProvince = $("#ContactProvince").find("option:selected").text();
        var ContactCity = $("#ContactCity").find("option:selected").text();
        //var ContactArea = $("#ContactArea").find("option:selected").text();
        var ContactAddress = $("#ContactAddress").val();
        var isAgreeEmail = $(".agreeCb").is(':checked');

        var contactEmail = $("#ContactEmail").val();//邮箱

        var localLastName = $("#localeLastName").val().trim();
        var localFirstName = $("#localeFirstName").val().trim();
        var gender = $("input[name='gender']:checked").val();
        var passPortNumber = $("#passPortNumber").val().trim();
        var passPortDate = $("#passPortDate").attr("numDate");
        var vipNum = $("#vipNum").val().trim();
        var countryno = $("#countryCode").attr("countryno");
        var province = $("#province").find("option:selected").text();
        var city = $("#city").find("option:selected").text();
        var area = $("#area").find("option:selected").text();

        //高危资料
        var sensitiveResidence = $("#sensitiveResidence").val().trim();
        var sensitiveAddress = $("#sensitiveAddress").val().trim();
        var sensitiveCA = $("#sensitiveCA").val().trim();
        var sensitiveportDate = $("#sensitiveportDate").val().trim();
        var sensitiveOccupation = $("#sensitiveOccupation").val().trim();
        var sensitiveFirstGo = $("input[name='firstArrival']:checked").val();
        var scanningImgs = [];
        for (var j = 0; j < $(".sensitiveScanningPic").length; j++) {
            scanningImgs[j] = $(".sensitiveScanningPic").eq(j).find("img").attr("src");
        }
        var telephone = $("#telephone").val().trim();
        for (var i = 0; i < GetStickInfo.length; i++) {
            if ((GuestId == GetStickInfo[i].GuestId)) {
                GetStickInfo[i].LocalLastName = localLastName;
                GetStickInfo[i].LocalFirstName = localFirstName;
                GetStickInfo[i].Gender = gender;
                GetStickInfo[i].CountryCode = countryno;
                GetStickInfo[i].PassportNumber = passPortNumber;
                GetStickInfo[i].PassPortExpirationDate = passPortDate;
                GetStickInfo[i].MemberShipId = vipNum;

                GetStickInfo[i].Province = province;
                GetStickInfo[i].City = city;
                GetStickInfo[i].Area = area;

                GetStickInfo[i].SensitiveResidence = sensitiveResidence;
                GetStickInfo[i].SensitiveAddress = sensitiveAddress;
                GetStickInfo[i].SensitiveCA = sensitiveCA;
                GetStickInfo[i].SensitiveportDate = sensitiveportDate;
                GetStickInfo[i].SensitiveOccupation = sensitiveOccupation;
                GetStickInfo[i].SensitiveFirstGo = sensitiveFirstGo;
                GetStickInfo[i].ScanningImgs = scanningImgs;

                GetStickInfo[i].ContactTelephone = telephone;
                GetStickInfo[i].IdCard = identityCardNumber;
                GetStickInfo[i].Telephone = emergencyTelephone;
                GetStickInfo[i].ContactProvince = ContactProvince;
                GetStickInfo[i].ContactCity = ContactCity;
                //GetStickInfo[i].ContactAddress = ContactArea;
                GetStickInfo[i].ContactAddress = ContactAddress;
                GetStickInfo[i].IsAgreeEmail = isAgreeEmail;
                GetStickInfo[i].ContactEmail = contactEmail;

                //保存境内外电话
                GetStickInfo[i].OutCHNTel = IsDomestic;

                //签发次数
                GetStickInfo[i].PassportAdditional = $("#signNumberTxt").val();

            }
        }

    }

    //左侧正在登记图片及文字修改,并显示正在登记用户信息
    function GuestDoing(GuestId) {
        //显示当前用户信息
        for (var i = 0; i < GetStickInfo.length; i++) {
            if (GuestId == GetStickInfo[i].GuestId) {
                $("#guestName").text(GetStickInfo[i].LastName + " " + GetStickInfo[i].FirstName);
                $("#birthday").text(GetStickInfo[i].DateOfBirth);
                $("#localeLastName").val(GetStickInfo[i].LocalLastName);
                $("#localeFirstName").val(GetStickInfo[i].LocalFirstName);
                if (GetStickInfo[i].Gender == "M") {
                    $("input:radio[name='gender']").eq(0).attr("checked", true);
                } else if (GetStickInfo[i].Gender == "F") {
                    $("input:radio[name='gender']").eq(1).attr("checked", true);
                }
                $("#countryCode").attr("countryno", GetStickInfo[i].CountryCode);
                $("#countryCode").val(countryChinese(GetStickInfo[i].CountryCode));
                identityShow((countryChinese(GetStickInfo[i].CountryCode)));
                if ((countryChinese(GetStickInfo[i].CountryCode)) !== "中国") {
                    $(".residenceAddress").addClass("dis");
                } else {
                    $(".residenceAddress").removeClass("dis");
                }

                $("#passPortNumber").val(GetStickInfo[i].PassportNumber);
                if (GetStickInfo[i].PassPortExpirationDate == "" || GetStickInfo[i].PassPortExpirationDate == null) {
                    $("#passPortDate").val("");
                    $("#passPortDate").attr("numdate", "");
                } else {
                    $("#passPortDate").val(dateEnglish(GetStickInfo[i].PassPortExpirationDate));
                    $("#passPortDate").attr("numdate", GetStickInfo[i].PassPortExpirationDate);
                }
                if (GetStickInfo[i].MemberShipId != "0") {
                    $("#vipNum").val(GetStickInfo[i].MemberShipId);
                }

                var province = GetStickInfo[i].Province;
                var city = GetStickInfo[i].City;
                var area = GetStickInfo[i].Area;
                var addressType = 1;
                setAddress(province, city, area, addressType);


                //高危资料
                $("#sensitiveResidence").val(GetStickInfo[i].SensitiveResidence);
                $("#sensitiveAddress").val(GetStickInfo[i].SensitiveAddress);
                $("#sensitiveCA").val(GetStickInfo[i].SensitiveCA);
                $("#sensitiveportDate").val(GetStickInfo[i].SensitiveportDate);
                $("#sensitiveOccupation").val(GetStickInfo[i].SensitiveOccupation);
                if (GetStickInfo[i].SensitiveFirstGo == "true") {
                    $("input:radio[name='firstArrival']").eq(0).attr("checked", true);
                } else if (!(GetStickInfo[i].SensitiveFirstGo)) {
                    $("input:radio[name='firstArrival']").eq(1).attr("checked", true);
                }
                var scanningImgs = [];
                scanningImgs = GetStickInfo[i].ScanningImgs;
                $(".sensitiveScanningPic").remove();
                if (scanningImgs != null) {
                    for (var j = 0; j < scanningImgs.length; j++) {
                        $(".sensitiveScanningImgs").append("<div class='sensitiveScanningPic'>"
                                                                 + "<img src='" + scanningImgs[j] + "' id='sensitiveScanningPic'/>"
                                                                 + "<div class='sensitiveScanning__delete dis'>删除</div>"
                                                             + "</div>"
                                                     );

                    }
                    var scannLeft = (scanningImgs.length) * 90;
                    $("#sensitiveScanning").css("left", scannLeft + "px");
                    $(".sensitiveScanningImg").css("left", scannLeft + "px");
                } else {
                    $("#sensitiveScanning").css("left", "0px");
                    $(".sensitiveScanningImg").css("left", "0px");
                }
                var telephone = $("#telephone").val(GetStickInfo[i].ContactTelephone);
                //if (GetStickInfo[i].ContactTelephone != "" && GetStickInfo[i].ContactTelephone != null) {
                //    $(".input--smsValid").addClass("dis");
                //} else {
                //    $(".input--smsValid").removeClass("dis");
                //}

                //新增信息
                $("#identityCardNumber").val(GetStickInfo[i].IdCard);
                if (GetStickInfo[i].IsAgreeEmail) {
                    $(".agreeCb").attr("checked", true);
                } else {
                    $(".agreeCb").attr("checked", false);
                }
                $("#signNumberTxt").val(GetStickInfo[i].PassportAdditional);

                //联系人地址和紧急联系人电话, 如果另外一个人是空的, 默认带上
                if (GetStickInfo[i].ContactProvince != "请选择" && GetStickInfo[i].ContactProvince != null && GetStickInfo[i].ContactProvince != "") {
                    var ContactProvince = GetStickInfo[i].ContactProvince;
                    var ContactCity = GetStickInfo[i].ContactCity;
                    var ContactAddress = GetStickInfo[i].ContactAddress;
                    var addressType = 2;
                    setAddress(ContactProvince, ContactCity, ContactAddress, addressType);
                    $("#ContactAddress").val(GetStickInfo[i].ContactAddress);
                }
                if (GetStickInfo[i].Telephone != "" && GetStickInfo[i].Telephone != null && GetStickInfo[i].Telephone != "") {
                    $("#emergencyTelephone").val(GetStickInfo[i].Telephone);
                }

                $("#ContactEmail").val(GetStickInfo[i].ContactEmail);//邮箱 

                //true--境外
                if (GetStickInfo[i].OutCHNTel) {
                    $(".input--selectTel select option:last").attr("selected", true);
                    $(".input--Captcha,.input--smsValid").addClass("dis");
                    $(".addEmailPromt").removeClass("f-dn");
                } else {
                    $(".input--selectTel select option:first").attr("selected", true);
                    $(".input--smsValid").removeClass("dis");
                    $(".addEmailPromt").addClass("f-dn");
                }
                IsDomestic = GetStickInfo[i].OutCHNTel;
                var dateOfBirth = GetStickInfo[i].DateOfBirth;
                ageInfoSHow(dateOfBirth);

            }
        }
        //返回第二步填写登船信息页面
        $("#step_form_4,#step_form_3,.step__stickContent").addClass("dis");
        $("#step_form_2").removeClass("dis");
        $(".step_li li").removeClass("curr");
        $(".step_li li").eq(1).addClass("curr");

        //判断出航前---大前天凌晨01分之后和前天可修改乘客信息
        var sailDate = $("#sailDate").val();//出航日期
        var nowHours = (new Date()).getHours();
        var nowMinu = (new Date()).getMinutes();
        //相差的天数
        var differDays = Math.ceil(new Date(sailDate).getTime() - new Date().getTime()) / (1000 * 60 * 60 * 24);

        //if (0 <differDays && differDays< 2 && nowHours >= 12 && nowMinu >= 1) {
        //    rdpAlert("在线值船已经关闭，无法修改信息，请联系客服4008850277");
        //}
    }

    //本人联络人电话时间(境内需要填写验证码)
    var isChangeTel = false;//false电话号码没修改
    //$("#telephone").change(function () {
    //    if (selectTelNum == 0) {
    //        isChangeTel = true;
    //        $(".input--smsValid").removeClass("dis");
    //    }
    //})

    var isLowEighteen;//低于18岁为false， 不低于18岁为true
    var LowAgeNum = 0;//低于18岁的总人数
    //未满18周岁显示高亮提示以及6个月以下儿童不能进行在线登记
    function ageInfoSHow(dateOfBirth) {
        //出航日期
        var sailDateTime = $("#sailDate").val();
        try {
            //生日
            var birthDate = new Date(dateOfBirth.replace("-", "/").replace("-", "/"));
            sailDateTime = new Date(sailDateTime.replace("-", "/").replace("-", "/"));
        } catch (e) {
            console.log(e)
        } finally {

        }
        //6个月以下儿童不能进行在线登记
        var differTime = Math.ceil(sailDateTime.getTime() - birthDate.getTime()) / (60 * 60 * 1000 * 24);
        if (differTime < 183) {
            rdpAlert("年龄小于6个月的旅客无法登轮，请联系客服:" + $("#PcTelephone").val());
            $(".stepTwo__bottomTxt").removeClass("dis");
            $(".stepTwo__topTxt").addClass("dis");
            isLowEighteen = false;
            return false;
        } else {
            $(".stepTwo__bottomTxt").addClass("dis");
        }

        //未满18岁提示       
        var passDueTime = Math.ceil(sailDateTime.getTime() - birthDate.getTime()) / (60 * 60 * 1000 * 24 * 365);
        if (passDueTime < 18) {
            $(".stepTwo__topTxt").removeClass("dis");
            isLowEighteen = false;
        } else {
            $(".stepTwo__topTxt").addClass("dis");
            isLowEighteen = true;
        }
        return true;
    }


    //当左侧乘客状态是COMPLETED时, 点击直接进入船票页面, 调用接口(不使用)
    function completedStatus(bookingId, guestId) {
        startLoading();
        $.ajax({
            type: 'get',
            url: 'https://www.rcclchina.com.cn/api/Rccl.Olci/Olci/GetBoardingPassById',
            dataType: 'json',
            data: {
                bookingId: bookingId,
                guestId: guestId,
                bookingAccessToken: BookingAccessToken
            },
            success: function (response) {
                if (response.Success) {

                    //$(".txt__laterTime").text(response.Data.EndBoardingTime);
                    if (response.Data.Deck != null) {
                        $(".txt__floorNum").text(response.Data.Deck);
                    }

                    $(".txt__roomNum").text(response.Data.Stateroom);
                    $(".txt__StateroomCategory").text(response.Data.StateroomCategory);

                    //$(".txt__MemberCredit").text(response.Data.MemberCredit);
                    $(".txt__stickNameE").text(response.Data.LastName + " " + response.Data.FirstName);
                    $(".txt__stickNameC").text(response.Data.LocalLastName + " " + response.Data.LocalFirstName);
                    $(".txt__shipNameE").text(response.Data.ShipENName);
                    $(".txt__shipNameC").text(response.Data.ShipCNName);
                    $(".txt__startTime span").text(response.Data.SailDateBegin);
                    $(".txt__endTime span").text(response.Data.SailDateEnd);
                    $(".txt__portAddress").text(response.Data.DeparturePort);
                    $(".txt__orderNum").text(response.Data.bookingId);
                    if (response.Data.MusterStation != null) {
                        $(".txt__EscapeZone").text(response.Data.MusterStation);
                    }


                    //下载pdf
                    BpassID = response.Data.BpassID;
                    //console.log(BpassID);
                    $(".button--loaddown").attr("href", "https://www.rcclchina.com.cn/Rccl.ESLAPI/PassNew/Pdf?boardingPassId=" + BpassID);
                    //条形码
                    Barcode = response.Data.Barcode;
                    $(".img--barCode").attr("src", "https://www.rcclchina.com.cn/Rccl.ESLAPI/Pass/barcode?barcode=" + Barcode);

                    $("#step_form_4").addClass("dis");
                    $("#step_form_2").addClass("dis");
                    $(".step__stickContent").removeClass("dis");
                    $(".step_li li").removeClass("step_1").removeClass("curr");
                    $(".step_li li").eq(2).removeClass("step_4").addClass("curr");
                }
                stopLoading();
            },
            error: function (error) {
                //console.log(error)
            }
        })
    }

    //船票页面修改信息
    $(".button--modifyInfo").click(function () {
        GuestDoing(GuestId);
    })

    //第三步  修改
    $(".changeStick").click(function () {
        createCode();
        $("#valid").val("");
        $("#smsValid").val("");

        $("#granteelist").empty();
        //支付关联人信息(可选或已选)
        for (var i = 0; i < GetStickInfo.length; i++) {
            var saveNameEnglish = GetStickInfo[i].LastName + " " + GetStickInfo[i].FirstName;
            var cardList = "<div class='input' GuestId='" + GetStickInfo[i].GuestId + "' BookingId='" + GetStickInfo[i].BookingId + "' style='height:40px;border:2px solid #0073bb;margin-bottom:10px;cursor:poniter;position:relative;' ischecked='1'>"
                         + "<p><span class='CardFirstName'>" + GetStickInfo[i].FirstName + "&nbsp;</span><span class='CardLastName'>" + GetStickInfo[i].LastName + "<i class='img' style='position:absolute;top:11px;right:15px;'></i></p>"
                         + "</div>";
            $("#granteelist").append(cardList);
        }
        getCardGuestInfo();
        //$("#step_form_4").addClass("dis");
        //$("#step_form_3").removeClass("dis");
        //$(".step_li li").removeClass("curr");
        //$(".step_li li").eq(2).addClass("curr");
        //$(".step_li li").eq(3).addClass("step_4");              
    })

    //第一步提交资料调用ajax
    function retrieveGuestInformation() {
        startLoading("retrieveGuestInformation");
        appendGuestList(stepOneResp.Data.GuestInfos, 0, function (isSuccess) {

            stopLoading("retrieveGuestInformation")

            isSubmitStick = parseInt(isSubmitStick);
            if (isSuccess) {
                for (var i = 0; i < GetGuestInfo.length; i++) {
                    var guest = GetGuestInfo[i];
                    var status = guest.status;
                    var FirstName = guest.FirstName;
                    var LastName = guest.LastName;
                    GuestId = guest.GuestId;
                    BookingId = guest.BookingId;
                    if (status == "IN_PROGRESS" || status == null) {
                        $("#guestListUL").append(
                            "<li>" +
                                "<div class='guest' GuestId='" + GuestId + "' BookingId='" + BookingId + "'>" +
                                    "<div class='gimage'>" +
                                    "</div>" +
                                    "<p class='guestName'>" + FirstName + "&nbsp;" + LastName + "</p>" +
                                    "<p class='guestStatus'>未登记</p>" +
                                "</div>" +
                            "</li>"
                            );
                    } else if (status == "REPRINT" || status == "COMPLETED") {
                        $("#guestListUL").append(
                            "<li>" +
                                "<div class='guest complete' GuestId='" + GuestId + "' BookingId='" + BookingId + "'>" +
                                    "<div class='gimage'><i class='yes'>" +
                                    "</i></div>" +
                                    "<p class='guestName'>" + FirstName + "&nbsp;" + LastName + "</p>" +
                                    "<p class='guestStatus'>已登记</p>" +
                                "</div>" +
                            "</li>"
                            );
                    }

                    if (GetGuestInfo[i].BpassID != null) {
                        isSubmitStick++;
                    }
                    ageInfoSHow(GetGuestInfo[i].DateOfBirth);
                    if (!isLowEighteen) {
                        LowAgeNum++;
                    }
                }

                GetStickInfo = GetGuestInfo;
                GuestId = GetStickInfo[0].GuestId;
                IsDomestic = GetStickInfo[0].OutCHNTel;
                $(".guest").eq(0).find("div").removeClass("gimage").addClass("gimage_focus");
                $("#guestName").text(GetStickInfo[0].LastName + " " + GetStickInfo[0].FirstName);
                $("#birthday").text(GetStickInfo[0].DateOfBirth);

                GuestId = GetStickInfo[0].GuestId;

                $("#guestListUL .button--reload").addClass("dis");//左侧加载成功--左侧“重新加载”按钮隐藏   

                var callcenterTel = $("#callcenterTel").val();

                birthOfDate = GetStickInfo[0].DateOfBirth;

                //当前用户已进行到哪一步  5---第一步验证  10---完成填写资料  15---已生成船票
                var checkProgress = GetStickInfo[0].CheckProgress;
                if (checkProgress == 20) {
                    $(".button--regenerateTicket").removeClass("f-dn");
                } else {
                    $(".button--regenerateTicket").addClass("f-dn");
                }
                //直接跳转到生成船票
                if (LowAgeNum == GetGuestInfo.length) {
                    rdpAlert("您的订单至少需要一名成年人, 如需修改订单, 请即刻联系客服:" + callcenterTel);
                } else {
                    //如果所有的乘客都已生成船票或者当前登录乘客已生成船票，就直接跳转到生成船票页
                    if (GetGuestInfo.length != 0 && isSubmitStick == GetGuestInfo.length) {
                        getBoardingPass(BookingId, BookingAccessToken, GuestId);
                    } else {
                        if (GetGuestInfo[0].status == "REPRINT" || GetGuestInfo[0].status == "COMPLETED") {
                            getBoardingPass(GetGuestInfo[0].BookingId, BookingAccessToken, GetGuestInfo[0].GuestId);
                        } else {
                            getStepNum(checkProgress);
                        }
                    }
                }
            } else {
                // 显示重新加载， 点击 后重新调用retrieveGuestInformation（）
                $("#step_form_2").addClass("dis");//左侧加载失败--右侧内容隐藏
                $("#guestListUL .button--reload").removeClass("dis");//左侧加载失败--左侧“重新加载”按钮显示
            }

        })
    }

    //身份证一栏是否显示
    function identityShow(countryCName) {
        if (countryCName != "中国") {
            $(".checkIN__identityCardNumber").addClass("dis");
            $("#identityCardNumber").val("");
            $(".residenceAddress").addClass("dis");
            $(".sensitive").addClass("dis");
            if (countryCName == "中国香港" || countryCName == "中国澳门") {
                $(".identityCardTxtp").css("width", "178px");
                $("#identityCardNumber").css("width", "62%");
                $(".identityCardTxt").text("港澳居民来往内地通行证号码：");
                $(".checkIN__identityCardNumber").removeClass("dis");
            } else if (countryCName == "中国台湾") {
                $(".identityCardTxtp").css("width", "178px");
                $("#identityCardNumber").css("width", "62%");
                $(".identityCardTxt").text("台湾居民来往大陆通行证号码：");
                $(".checkIN__identityCardNumber").removeClass("dis");
            }
        } else {
            $(".identityCardTxtp").css("width", "122px");
            $("#identityCardNumber").css("width", "73%");
            $(".identityCardTxt").text("身份证：");
            $(".checkIN__identityCardNumber").removeClass("dis");
            $(".residenceAddress").removeClass("dis");
        }
    }

    $(".button--reload").click(function () {
        GetGuestInfo = [];
        GetStickInfo = [];
        $("#guestListUL li").remove();
        retrieveGuestInformation();
    })

    function appendGuestList(guests, index, callback) {
        if (guests.length == index) {
            return callback(true);
        }
        var guestInfo = guests[index];
        var status = guestInfo.OlciStatus;
        var FirstName = guestInfo.FirstName;
        var LastName = guestInfo.LastName;
        var DateOfBirth = guestInfo.DateOfBirth;
        GuestId = guestInfo.GuestLanguage.GuestId;
        BookingId = guestInfo.GuestLanguage.BookingId;

        GetGuestInformation(BookingId, GuestId, BookingAccessToken, function (isSuccess, guest) {
            if (!isSuccess) {
                _.remove(GetGuestInfo, function (n) {
                    return true;
                })
                return callback(false)
            }
            guest.status = status;
            guest.BookingId = BookingId;
            guest.GuestId = GuestId;
            GetGuestInfo.push(guest)
            index++;
            return appendGuestList(guests, index, callback)
        });

    }

    function GetGuestInformation(BookingId, GuestId, BookingAccessToken, callback) {
        var shipCode = $("#shipCode").attr("shipno");
        var sailDate = $("#sailDate").val();
        $.ajax({
            type: 'get',
            url: 'https://www.rcclchina.com.cn/api/Rccl.Olci/Olci/GetGuestInformation',
            data: { BookingId: BookingId, GuestId: GuestId, BookingAccessToken: BookingAccessToken, shipCode: shipCode, sailDate: sailDate },
            success: function (response) {

                if (response.Success && response.Data) {
                    callback(true, response.Data[0]);
                } else {
                    //console.log(response)
                    callback(false)
                }
            },
            error: function (error) {
                // console.log(error)

                callback(false);
            }
        })

        //获取已有的支付信息
        $.ajax({
            type: 'get',
            url: 'https://www.rcclchina.com.cn/api/Rccl.Olci/Olci/OnboardPayment',
            dataType: 'json',
            data: { BookingId: BookingId, GuestId: GuestId, BookingAccessToken: BookingAccessToken },
            success: function (response) {
                if (response.Success) {
                    if (response.Data != null) {
                        if (response.Data.GranteeGuestList != null) {
                            for (var i = 0; i < response.Data.GranteeGuestList.length; i++) {
                                var GranteeGuest = { GrantorGuestId: "", GranteeGuestId: "" };
                                GranteeGuest.GranteeGuestId = response.Data.GranteeGuestList[i].GuestId;
                                GranteeGuest.GrantorGuestId = response.Data.GrantorGuestId;
                                updateGrantList.push(GranteeGuest);
                            }
                        }
                    }
                }
            }
        })
    }

    //自动设置地址
    function setAddress(province, city, area, addressType) {
        if (addressType == 1) {
            var provinceId = "province";
            var cityId = "city";
            var areaId = "area";
            isSensitive(province, city, area);
        } else if (addressType == 2) {
            var provinceId = "ContactProvince";
            var cityId = "ContactCity";
            var areaId = "ContactArea";
        }
        $("#" + provinceId).val(0)
        $("#" + cityId).val(0)
        $("#" + areaId).val(0)
        $("#" + provinceId + " option").each(function () {
            if ($(this).text() == province) {
                var provinceName = $(this).val();
                $("#" + provinceId).val(provinceName);
                getCity(provinceName, addressType);
                $("#" + cityId + " option").each(function () {
                    if ($(this).text() == city) {
                        var cityName = $(this).val();
                        $("#" + cityId).val(cityName);
                        getArea(cityName, addressType)
                        $("#" + areaId + " option").each(function () {
                            if ($(this).text() == area) {
                                var areaName = $(this).val();
                                $("#" + areaId).val(areaName);
                            }
                        })
                    }
                })
            } else {
                return;
            }
        })
    }
    //获取城市
    function getCity(selValue, addressType) {
        if (addressType == 1) {
            var cityId = "city";
        } else {
            var cityId = "ContactCity";
        }
        $("#" + cityId + " option:gt(0)").remove();
        $.each(cityJson, function (k, p) {
            if (p.id == selValue || p.parent == selValue) {
                var option = "<option value='" + p.id + "'>" + p.city + "</option>";
                $("#" + cityId).append(option);
            }
        });
    }
    //获取区
    function getArea(selValue, addressType) {
        if (addressType == 1) {
            var cityId = "area";
        } else {
            var cityId = "ContactArea";
        }
        $("#" + cityId + " option:gt(0)").remove();
        $.each(countyJson, function (k, p) {
            if (p.id == selValue || p.parent == selValue) {
                var option = "<option value='" + p.id + "'>" + p.county + "</option>";
                $("#" + cityId).append(option);
            }
        });
    }
    //第二步省市区显示以及判断是否是高危
    function isSensitive(province, city, area) {

        //调用ajax判断是否是高危地区
        $.ajax({
            type: 'post',
            url: 'https://www.rcclchina.com.cn/api/Rccl.Olci/Olci/CheckHighRiskArea',
            data: { Province: province, City: city, Area: area },
            success: function (resp) {
                if (resp.Success) {
                    if (resp.Data) {
                        $(".sensitive").removeClass("dis");
                    } else {
                        //不是高危
                        $(".sensitive").addClass("dis");
                    }
                }
            }
        })
    }

    var timeInterval;
    //资料填写时间倒计时
    function countdown() {
        var time = 60;
        var counttime = 14;
        timeInterval = setInterval(function () {
            time--;
            if (time < 10) {
                time = "0" + time;
            }
            if (time == "00") {
                time = 59;
                counttime--;
            }
            if (counttime <= 0) {
                counttime = "0";
                time = "00";
                rdpAlert("您没有在规定时间内延长录入时间，点击确认重新录入。", "在线录入超时提醒", function () {
                    $("#counttime").html("15分钟");
                    window.location.reload();
                });
                clearInterval(timeInterval);
            }
            $("#counttime").html(counttime + ":" + time + "分钟");
        }, 1000)
    }

    //清除第二步右侧填写信息（不包括联系人）
    function clearInfo() {
        $("#guestName").text("");
        $("#birthday").text("");
        $("#localeLastName").val("");
        $("#localeFirstName").val("");
        $("#countryCode").val("");
        $("#passPortNumber").val("");
        $("#passPortDate").val("");
        $("#passPortDate").attr("numdate", "");
        $("#vipNum").val();
        $("input[name='gender']").attr("checked", false);

        $("#valid").val("");
        $("#smsValid").val("");

        $("#province").find("option:first").attr("selected", true);
        $("#city").find("option:first").attr("selected", true);
        $("#area").find("option:first").attr("selected", true);


        //高危地区
        $("#sensitiveResidence").val("");
        $("#sensitiveAddress").val("");
        $("#sensitiveCA").val("");
        $("#sensitiveportDate").val("");
        $("#sensitiveOccupation").val("");
        $("input[name='firstArrival']").attr("checked", false);

        $(".sensitiveScanningPic").remove();
    }

    //生成电子船票
    $(".printStick").click(function () {
        //电子船票调用ajax
        setStickInfo(BookingId, BookingAccessToken, GuestId);
    })
    //生成电子船票调用ajax(第一次生成船票)
    function setStickInfo(BookingId, BookingAccessToken, GuestId) {
        startLoading();
        $.ajax({
            type: 'post',
            url: 'https://www.rcclchina.com.cn/api/Rccl.Olci/Olci/CreateBoardingPassV2',
            dataType: 'json',
            data: { BookingAccessToken: BookingAccessToken, BookingId: BookingId, GuestId: GuestId },
            success: function (response) {
                if (response.Success) {
                    //停止填写资料倒计时
                    $(".step_speed .step_prompt").addClass("dis");
                    clearInterval(timeInterval);
                    //修改后的电子船票
                    $(".txt__laterTime").text(response.Data.EndBoardingTime.replace("/", ".").replace("/", "."));
                    if (response.Data.Deck != null) {
                        $(".txt__floorNum").text(response.Data.Deck);
                    }
                    if (response.Data.StateroomCategory == null || response.Data.StateroomCategory == "") {
                        $(".stickOne__roomNumIcon").addClass("f-dn");
                    } else if (response.Data.StateroomCategory.indexOf("SUIT") > -1) {
                        $(".stickOne__roomNumIcon").removeClass("f-dn");
                    }
                    $(".txt__roomNum").text(response.Data.Stateroom);
                    //$(".txt__StateroomCategory").text(response.Data.StateroomCategory);
                    $(".stickOne__MemberCreditIcon").removeClass("f-dn");
                    if (response.Data.MemberCredit == null || response.Data.MemberCredit == "") {
                        $(".stickOne__MemberCreditIcon").addClass("f-dn");
                    } else if (response.Data.MemberCredit == "GOLD") {
                        $(".stickOne__MemberCreditIcon img").attr("src", "https://www.rcclchina.com.cn/Modules/Rccl.ESLAPI/image/G.png");
                    } else if (response.Data.MemberCredit == "PLATINUM") {
                        $(".stickOne__MemberCreditIcon img").attr("src", "https://www.rcclchina.com.cn/Modules/Rccl.ESLAPI/imageP.png");
                    } else if (response.Data.MemberCredit == "EMERALD") {
                        $(".stickOne__MemberCreditIcon img").attr("src", "https://www.rcclchina.com.cn/Modules/Rccl.ESLAPI/image/E.png");
                    } else if (response.Data.MemberCredit == "DIAMOND") {
                        $(".stickOne__MemberCreditIcon img").attr("src", "https://www.rcclchina.com.cn/Modules/Rccl.ESLAPI/image/D.png");
                    } else if (response.Data.MemberCredit == "DIAMOND PLUS") {
                        $(".stickOne__MemberCreditIcon img").attr("src", "https://www.rcclchina.com.cn/Modules/Rccl.ESLAPI/image/DP.png");
                    } else if (response.Data.MemberCredit == "PINNACLE CLUB") {
                        $(".stickOne__MemberCreditIcon img").attr("src", "https://www.rcclchina.com.cn/Modules/Rccl.ESLAPI/image/PC.png");
                    }

                    //$(".txt__MemberCredit").text(response.Data.MemberCredit);
                    $(".txt__stickNameE").text(response.Data.LastName + " " + response.Data.FirstName);
                    $(".txt__stickNameC").text(response.Data.LocalLastName + " " + response.Data.LocalFirstName);
                    var SailDateBegin = response.Data.SailDateBegin;
                    SailDateBegin = new Date(SailDateBegin);
                    var DateOfBirth = response.Data.DateOfBirth;
                    DateOfBirth = new Date(DateOfBirth);
                    var passDueTime = Math.ceil(SailDateBegin.getTime() - DateOfBirth.getTime()) / (60 * 60 * 1000 * 24 * 365);
                    if (passDueTime > 18) {
                        $(".txt__stickbirthDate").html(response.Data.DateOfBirth.replace("/", ".").replace("/", "."));
                    } else {
                        var DateOfBirth = response.Data.DateOfBirth.substring(0, 10).replace("/", ".").replace("/", ".");
                        $(".txt__stickbirthDate").html(DateOfBirth + "&nbsp;<span style='color:blue;font-weight:bold;'>Minor</span>");
                    }
                    if (response.Data.Gender == "F") {
                        $(".txt__stickSex").text("F/女");
                    } else if (response.Data.Gender == "M") {
                        $(".txt__stickSex").text("M/男");
                    }
                    $(".txt__stickPassport").text(response.Data.PassportNumber);
                    $(".txt__stickNationality").text(response.Data.CountryCode);
                    if (response.Data.PassPortExpirationDate != null && response.Data.PassPortExpirationDate != "") {
                        $(".txt__stickDateExpiry").text(response.Data.PassPortExpirationDate.replace("/", ".").replace("/", "."));
                    } else {
                        $(".txt__stickDateExpiry").text("");
                    }
                    $(".txt__stickShipE").text(response.Data.ShipENName);
                    $(".txt__stickShipC").text(response.Data.ShipCNName);
                    $(".txt__stickNumber").text(response.Data.BookingId);

                    var stickItinenary = "";
                    var stickItinenarySec = "";
                    var count = response.Data.RouteEvents.length;
                    if (count >= 6) {
                        count = 6;
                    }
                    for (var i = 0; i < count; i++) {
                        stickItinenary = stickItinenary + " " + response.Data.RouteEvents[i].Date.replace("/", ".").replace("/", ".") + " " + response.Data.RouteEvents[i].HarborCNName;
                    }
                    if (response.Data.RouteEvents.length > 6) {
                        for (var j = 6; j < response.Data.RouteEvents.length ; j++) {
                            stickItinenarySec = stickItinenarySec + " " + response.Data.RouteEvents[j].Date.replace("/", ".").replace("/", ".") + " " + response.Data.RouteEvents[j].HarborCNName;
                        }
                        $(".stickCon__txtContent .stickCon__itinenary").css({ "left": "118px", "width": "320px" });
                    } else {
                        $(".stickCon__txtContent .stickCon__itinenary").css({ "left": "179px", "width": "124px" });
                    }
                    $(".txt__stickItinenary").text(stickItinenary);
                    $(".txt__stickItinenarySecList").text(stickItinenarySec);

                    $(".txt__stickClassify").text(response.Data.AgencyType);
                    if (response.Data.MusterStation != null) {
                        $(".txt__EscapeZone").text(response.Data.MusterStation);
                    }
                    $(".txt__sailDate").text(response.Data.SailDateBegin.replace("/", ".").replace("/", "."));
                    $(".txt__hangbanhao").text(response.Data.ShipCode + response.Data.SailDateBegin.replace("/", "").replace("/", ""));
                    var idCard = response.Data.IdCard;
                    if (idCard != null && idCard != "") {
                        var idCard1 = idCard.substring(0, 3);
                        var idCard2 = idCard.substring(6, 14);
                        idCard = idCard1 + "***" + idCard2 + "****";
                    }
                    $(".txt__idCard").text(idCard);
                    $(".txt__telephone").text(response.Data.Telephone);
                    if (response.Data.DeparturePortEname = 'BAO') {
                        $(".txt__departurePort").text("吴淞口国际邮轮港");
                    } else {
                        $(".txt__departurePort").text(response.Data.DeparturePortEname);
                    }

                    //下载pdf
                    BpassID = response.Data.BpassID;
                    $(".button--loaddown").attr("href", "https://www.rcclchina.com.cn/Rccl.ESLAPI/PassNew/Pdf?boardingPassId=" + BpassID);
                    $(".txt_barCode").text(response.Data.Barcode);

                    //条形码
                    Barcode = response.Data.Barcode;
                    $(".img--barCode").attr("src", "https://www.rcclchina.com.cn/Rccl.ESLAPI/Pass/barcode?barcode=" + Barcode);

                    //左侧 未登记状态改为已登记状态
                    $("#guestListUL li").each(function () {
                        var index = $(this).index();
                        index = parseInt(index) - 1;
                        if (GuestId == $(this).find(".guest").attr("guestid")) {
                            $(this).find(".guest").addClass("complete");
                            $(this).find(".gimage_focus").append("<i class='yes'></i>");
                            $(this).find(".guestStatus").text("已登记");
                            isSubmitStick++;

                            //第一次生成船票将checkProgress修改为20
                            GetStickInfo[index].CheckProgress = 20;
                            $(".button--regenerateTicket").removeClass("f-dn");
                        }
                    })

                    $("#step_form_234").removeClass("dis");
                    $("#step_form_1,#step_form_2,#step_form_3,#step_form_4").addClass("dis");
                    $(".step__stickContent").removeClass("dis");
                    $(".step_li li").removeClass("step_1").removeClass("curr");
                    $(".step_li li").eq(3).removeClass("step_4").addClass("curr");

                }
                else {
                    rdpAlert(response.ErrMsg)
                }
                stopLoading()
            },
            error: function (error) {
                // console.log(error)
            }
        })
    }

    //生成电子船票调用ajax
    function getBoardingPass(bookingId, BookingAccessToken, guestId) {
        startLoading("getBoardingPass");
        $.ajax({
            type: 'get',
            url: 'https://www.rcclchina.com.cn/api/Rccl.Olci/Olci/GetBoardingPassById',
            dataType: 'json',
            data: {
                bookingId: bookingId,
                guestId: guestId,
                bookingAccessToken: BookingAccessToken
            },
            success: function (response) {
                if (response.Success) {
                    //停止填写资料倒计时
                    $(".step_speed .step_prompt").addClass("dis");
                    clearInterval(timeInterval);
                    //修改后的电子船票
                    $(".txt__laterTime").text(response.Data.EndBoardingTime.replace("/", ".").replace("/", "."));
                    if (response.Data.Deck != null) {
                        $(".txt__floorNum").text(response.Data.Deck);
                    }
                    if (response.Data.StateroomCategory == null || response.Data.StateroomCategory == "") {
                        $(".stickOne__roomNumIcon").addClass("f-dn");
                    } else if (response.Data.StateroomCategory.indexOf("SUIT") > -1) {
                        $(".stickOne__roomNumIcon").removeClass("f-dn");
                    }
                    $(".txt__roomNum").text(response.Data.Stateroom);
                    //$(".txt__StateroomCategory").text(response.Data.StateroomCategory);
                    $(".stickOne__MemberCreditIcon").removeClass("f-dn");
                    if (response.Data.MemberCredit == null || response.Data.MemberCredit == "") {
                        $(".stickOne__MemberCreditIcon").addClass("f-dn");
                    } else if (response.Data.MemberCredit == "GOLD") {
                        $(".stickOne__MemberCreditIcon img").attr("src", "https://www.rcclchina.com.cn/Modules/Rccl.ESLAPI/image/G.png");
                    } else if (response.Data.MemberCredit == "PLATINUM") {
                        $(".stickOne__MemberCreditIcon img").attr("src", "https://www.rcclchina.com.cn/Modules/Rccl.ESLAPI/imageP.png");
                    } else if (response.Data.MemberCredit == "EMERALD") {
                        $(".stickOne__MemberCreditIcon img").attr("src", "https://www.rcclchina.com.cn/Modules/Rccl.ESLAPI/image/E.png");
                    } else if (response.Data.MemberCredit == "DIAMOND") {
                        $(".stickOne__MemberCreditIcon img").attr("src", "https://www.rcclchina.com.cn/Modules/Rccl.ESLAPI/image/D.png");
                    } else if (response.Data.MemberCredit == "DIAMOND PLUS") {
                        $(".stickOne__MemberCreditIcon img").attr("src", "https://www.rcclchina.com.cn/Modules/Rccl.ESLAPI/image/DP.png");
                    } else if (response.Data.MemberCredit == "PINNACLE CLUB") {
                        $(".stickOne__MemberCreditIcon img").attr("src", "https://www.rcclchina.com.cn/Modules/Rccl.ESLAPI/image/PC.png");
                    }

                    //$(".txt__MemberCredit").text(response.Data.MemberCredit);
                    $(".txt__stickNameE").text(response.Data.LastName + " " + response.Data.FirstName);
                    $(".txt__stickNameC").text(response.Data.LocalLastName + " " + response.Data.LocalFirstName);
                    var SailDateBegin = response.Data.SailDateBegin;
                    SailDateBegin = new Date(SailDateBegin);
                    var DateOfBirth = response.Data.DateOfBirth;
                    DateOfBirth = new Date(DateOfBirth);
                    var passDueTime = Math.ceil(SailDateBegin.getTime() - DateOfBirth.getTime()) / (60 * 60 * 1000 * 24 * 365);
                    if (passDueTime > 18) {
                        $(".txt__stickbirthDate").html(response.Data.DateOfBirth.replace("/", ".").replace("/", "."));
                    } else {
                        var DateOfBirth = response.Data.DateOfBirth.substring(0, 10).replace("/", ".").replace("/", ".");
                        $(".txt__stickbirthDate").html(DateOfBirth + "&nbsp;<span style='color:blue;font-weight:bold;'>Minor</span>");
                    }
                    if (response.Data.Gender == "F") {
                        $(".txt__stickSex").text("F/女");
                    } else if (response.Data.Gender == "M") {
                        $(".txt__stickSex").text("M/男");
                    }
                    $(".txt__stickPassport").text(response.Data.PassportNumber);
                    $(".txt__stickNationality").text(response.Data.CountryCode);
                    if (response.Data.PassPortExpirationDate != null && response.Data.PassPortExpirationDate != "") {
                        $(".txt__stickDateExpiry").text(response.Data.PassPortExpirationDate.replace("/", ".").replace("/", "."));
                    } else {
                        $(".txt__stickDateExpiry").text("");
                    }
                    $(".txt__stickShipE").text(response.Data.ShipENName);
                    $(".txt__stickShipC").text(response.Data.ShipCNName);
                    $(".txt__stickNumber").text(response.Data.BookingId);

                    var stickItinenary = "";
                    var stickItinenarySec = "";
                    var count = response.Data.RouteEvents.length;
                    if (count >= 6) {
                        count = 6;
                    }
                    for (var i = 0; i < count; i++) {
                        stickItinenary = stickItinenary + " " + response.Data.RouteEvents[i].Date.replace("/", ".").replace("/", ".") + " " + response.Data.RouteEvents[i].HarborCNName;
                    }
                    if (response.Data.RouteEvents.length > 6) {
                        for (var j = 6; j < response.Data.RouteEvents.length ; j++) {
                            stickItinenarySec = stickItinenarySec + " " + response.Data.RouteEvents[j].Date.replace("/", ".").replace("/", ".") + " " + response.Data.RouteEvents[j].HarborCNName;
                        }
                        $(".stickCon__txtContent .stickCon__itinenary").css({ "left": "118px", "width": "320px" });
                    } else {
                        $(".stickCon__txtContent .stickCon__itinenary").css({ "left": "179px", "width": "124px" });
                    }
                    $(".txt__stickItinenary").text(stickItinenary);
                    $(".txt__stickItinenarySecList").text(stickItinenarySec);

                    $(".txt__stickClassify").text(response.Data.AgencyType);
                    if (response.Data.MusterStation != null) {
                        $(".txt__EscapeZone").text(response.Data.MusterStation);
                    }
                    $(".txt__sailDate").text(response.Data.SailDateBegin.replace("/", ".").replace("/", "."));
                    $(".txt__hangbanhao").text(response.Data.ShipCode + response.Data.SailDateBegin.replace("/", "").replace("/", ""));
                    var idCard = response.Data.IdCard;
                    if (idCard != null && idCard != "") {
                        var idCard1 = idCard.substring(0, 3);
                        var idCard2 = idCard.substring(6, 14);
                        idCard = idCard1 + "***" + idCard2 + "****";
                    }
                    $(".txt__idCard").text(idCard);
                    $(".txt__telephone").text(response.Data.Telephone);
                    if (response.Data.DeparturePortEname = 'BAO') {
                        $(".txt__departurePort").text("吴淞口国际邮轮港");
                    } else {
                        $(".txt__departurePort").text(response.Data.DeparturePortEname);
                    }

                    //下载pdf
                    BpassID = response.Data.BpassID;
                    $(".button--loaddown").attr("href", "https://www.rcclchina.com.cn/Rccl.ESLAPI/PassNew/Pdf?boardingPassId=" + BpassID);
                    $(".txt_barCode").text(response.Data.Barcode);
                    //条形码
                    Barcode = response.Data.Barcode;
                    $(".img--barCode").attr("src", "https://www.rcclchina.com.cn/Rccl.ESLAPI/Pass/barcode?barcode=" + Barcode);

                    $("#step_form_234").removeClass("dis");
                    $("#step_form_1,#step_form_2,#step_form_3,#step_form_4").addClass("dis");
                    $(".step__stickContent").removeClass("dis");
                    $(".step_li li").removeClass("step_1").removeClass("curr");
                    $(".step_li li").eq(3).removeClass("step_4").addClass("curr");

                }
                else {
                    rdpAlert(response.ErrMsg)
                }
                stopLoading("getBoardingPass")
            },
            error: function (error) {
                // console.log(error)
            }
        })
    }


    var stickIndex;
    //左右箭头滑动
    $(".button--arrowLeft").click(function () {
        stickIndex = 1;
        stickPage(stickIndex);
    })
    $(".button--arrowRight").click(function () {
        stickIndex = 0;
        stickPage(stickIndex);
    })

    //1页和2页的切换
    $(".button--pageLi li").click(function () {
        stickIndex = $(this).index();
        stickPage(stickIndex);
    })
    function stickPage(stickIndex) {
        //alert(stickIndex);
        if (stickIndex == 0) {
            $(".step__stickOne").removeClass("dis");
            $(".step__stickTwo").addClass("dis");
            $(".button--arrowRight").removeClass("arrowActive");
            $(".button--arrowLeft").addClass("arrowActive");
            $(".button--pageLi li").removeClass("active");
            $(".button--pageLi li").eq(0).addClass("active");
        } else {
            $(".step__stickOne").addClass("dis");
            $(".step__stickTwo").removeClass("dis");
            $(".button--arrowRight").addClass("arrowActive");
            $(".button--arrowLeft").removeClass("arrowActive");
            $(".button--pageLi li").removeClass("active");
            $(".button--pageLi li").eq(1).addClass("active");
        }
    }

    //发送到邮箱
    $(".button--sendToEmail").click(function () {
        var nameE = $(".txt__stickNameE").text();
        var nameC = $(".txt__stickNameC").text().trim();
        $("#sendTicketGuest").text(nameE + " " + nameC);
        $("#sendEmailDialog").removeClass("dis");
    })
    //确定发送
    $("#sendEmailBtn").click(function () {
        var email = $("#email").val();
        if (IsMail(mail)) {
            $.ajax({
                type: 'post',
                url: 'https://www.rcclchina.com.cn/api/Rccl.Olci/olci/SendBoardPassEmail',
                dataType: 'json',
                data: { BPassId: BpassID, Email: email },
                success: function (resp) {
                    if (resp.Success) {
                        rdpAlert("已成功发送到邮箱！");
                    } else if (resp.Code == 5) {
                        rdpAlert("请先生成船票！");
                    } else if (resp.Code == 10) {
                        rdpAlert("邮件发送失败，请检查邮箱是否正确！");
                    }
                }
            })
            $("#sendEmailDialog").addClass("dis");
            $("#checkInLoading").removeClass("dis");
        } else {
            rdpAlert("请输入正确的邮箱");
            $("#email").focus();
        }
    })
    $(".sendEmail__closed,.remainingNum--closed,.cancelClosedOlci,.submitCard").click(function () {
        $("#sendEmailDialog,#remainingNum,#xyCardTime").addClass("dis");
    })
    //邮箱验证
    function IsMail(mail) {
        var reyx = /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
        return (reyx.test(mail));
    }


    //收件地址初始化省市区
    $.each(provinceJson, function (k, p) {
        var option = "<option value='" + p.id + "'>" + p.province + "</option>";
        $("#province").append(option);
        $("#ContactProvince").append(option);
    });

    $("#province").change(function () {
        var selValue = $(this).val();
        $("#city option:gt(0)").remove();
        $.each(cityJson, function (k, p) {
            if (p.id == selValue || p.parent == selValue) {
                var option = "<option value='" + p.id + "'>" + p.city + "</option>";
                $("#city").append(option);
            }
        });
    });
    $("#ContactProvince").change(function () {
        var selValue = $(this).val();
        $("#ContactCity option:gt(0)").remove();
        $.each(cityJson, function (k, p) {
            if (p.id == selValue || p.parent == selValue) {
                var option = "<option value='" + p.id + "'>" + p.city + "</option>";
                $("#ContactCity").append(option);
            }
        });
    });

    $("#city").change(function () {
        var selValue = $(this).val();
        $("#area option:gt(0)").remove();
        $.each(countyJson, function (k, p) {
            if (p.id == selValue || p.parent == selValue) {
                var option = "<option value='" + p.id + "'>" + p.county + "</option>";
                $("#area").append(option);
            }
        });
        var cityName = $("#city").find("option:selected").text();
        if ($("#area option").size() < 2) {
            var option = "<option value='1'>" + cityName + "</option>";
            $("#area").append(option);
        }
    });
    $("#ContactCity").change(function () {
        var selValue = $(this).val();
        $("#ContactArea option:gt(0)").remove();
        $.each(countyJson, function (k, p) {
            if (p.id == selValue || p.parent == selValue) {
                var option = "<option value='" + p.id + "'>" + p.county + "</option>";
                $("#ContactArea").append(option);
            }
        });
        var cityName = $("#ContactCity").find("option:selected").text();
        if ($("#ContactArea option").size() < 2) {
            var option = "<option value='1'>" + cityName + "</option>";
            $("#ContactArea").append(option);
        }
    });
    $("#area").change(function () {
        var province = $("#province").find("option:selected").text();
        var city = $("#city").find("option:selected").text();
        var area = $("#area").find("option:selected").text();
        //调用ajax判断是否是高危地区
        $.ajax({
            type: 'post',
            url: 'https://www.rcclchina.com.cn/api/Rccl.Olci/Olci/CheckHighRiskArea',
            data: { Province: province, City: city, Area: area },
            success: function (resp) {
                if (resp.Success) {
                    if (resp.Data) {
                        rdpAlert("您的户籍所在地处于敏感地区！");
                        $(".sensitive").removeClass("dis");
                    } else {
                        //不是高危
                        $(".sensitive").addClass("dis");
                    }
                }
            }
        })
    })

    // 日历控件初始化
    var calendarB = RcclCalendarPicker({
        allowPreYear: true,
        id: 'sensitiveBirthCalendar',
        onDateClick: function (date) {
            //console.log("birth", date);
            $("#sensitiveBirthCalendar").addClass('f-dn');
            $("input[name='sensitiveBirth']").val(date);
        },
        onMonthChange: function (month) {
            // console.log(month)
        },
        onInit: function () {
            // console.log("Init")
        }
    });
    $("input[name='sensitiveBirth']").click(function (e) {
        e.stopPropagation();
        $(".calendar").addClass('f-dn');
        $("#sensitiveBirthCalendar").removeClass('f-dn');
    })

    var calendarB = RcclCalendarPicker({
        allowPreYear: false,
        id: 'sensitiveportDateCalendar',
        onDateClick: function (date) {
            //console.log("birth", date);
            $("#sensitiveportDateCalendar").addClass('f-dn');
            $("input[name='sensitiveportDate']").val(date);
        },
        onMonthChange: function (month) {
            // console.log(month)
        },
        onInit: function () {
            // console.log("Init")
        }
    });
    $("input[name='sensitiveportDate']").click(function (e) {
        e.stopPropagation();
        $(".calendar").addClass('f-dn');
        $("#sensitiveportDateCalendar").removeClass('f-dn');
    })

    //上传扫描件
    $(".sensitiveScanningImgs .sensitiveScanningPic").live('mouseover', function () {
        $(this).find(".sensitiveScanning__delete").removeClass("dis");
    })
    $(".sensitiveScanningImgs .sensitiveScanningPic").live('mouseout', function () {
        $(this).find(".sensitiveScanning__delete").addClass("dis");
    })
    var scannLeft;//上传控件的left值
    $(".sensitiveScanningImgs .sensitiveScanning__delete").live('click', function () {
        if (($(".sensitiveScanningPic").length) == 5) {
            scannLeft = 450;
        } else {
            scannLeft = $("#sensitiveScanning").position().left;
        }
        $(".sensitiveScanning__delete").each(function () {
            if (!($(this).hasClass("dis"))) {
                $(this).parent().remove();
            }
        })
        scannLeft = scannLeft - 90;
        $("#sensitiveScanning").css("left", scannLeft + "px");
        $(".sensitiveScanningImg").css("left", scannLeft + "px");
        $("#sensitiveScanning").removeClass("dis");
        $(".sensitiveScanningImg").removeClass("dis");
    })

    //二维码弹窗隐藏
    $(".button--closed").click(function () {
        $("#weixinModal").hide();
        $("#agreementTxt").addClass("dis");
    })

    //退出在线登记
    $("#prevStep1").click(function () {
        leaveUrlNum = 1;
        if (getcheckInNum() != 0) {
            $(".remainingNumber").text(checkInNum);
            $("#remainingNum").removeClass("dis");
        }
        window.location.href = "../../Rccl.Olci/Olci/CheckInIndex";
    })
    //关闭浏览器
    $(window).bind('beforeunload', function () {
        if ($(".step_form_1").hasClass("dis")) {
            if (getcheckInNum() != 0) {
                $(".remainingNumber").text(checkInNum);
                leaveUrlNum = 2;
                $("#remainingNum").removeClass("dis");
            }
            return false;
        }
    });

    //暂不更改支付方式(出航日期与当前时间相比小于48小时)
    $(".cancelCard").click(function () {
        $("#step_form_3").addClass("dis");
        $("#step_form_4").removeClass("dis");
        $(".step_li li").removeClass("curr");
        $(".step_li li").eq(3).removeClass("step_4").addClass("curr");
        $("#xyCardTime").addClass("dis");
        $("#weixinModal").show();
    })

    //获取当前乘客的进行到哪一步
    function getStepNum(checkProgress) {
        GuestDoing(GuestId);
        if (checkProgress == 5 || checkProgress == null) {
            //isChangeTel = false;
            $("#step_form_1,.step_form_3,.step_form_4").addClass("dis");
            $("#step_form_234").removeClass("dis");
            $(".step_form_2").removeClass("dis");
            $(".step_li li").removeClass("curr");
            $(".step_li li").eq(1).addClass("curr");
            //资料填写时间倒计时
            countdown();
        } else if (checkProgress == 10) {
            $("#granteelist").empty();
            //支付关联人信息(可选或已选)
            for (var i = 0; i < GetStickInfo.length; i++) {
                var saveNameEnglish = GetStickInfo[i].LastName + " " + GetStickInfo[i].FirstName;
                var cardList = "<div class='input' GuestId='" + GetStickInfo[i].GuestId + "' BookingId='" + GetStickInfo[i].BookingId + "' style='height:40px;border:2px solid #0073bb;margin-bottom:10px;cursor:poniter;position:relative;' ischecked='1'>"
                             + "<p><span class='CardFirstName'>" + GetStickInfo[i].FirstName + "&nbsp;</span><span class='CardLastName'>" + GetStickInfo[i].LastName + "<i class='img' style='position:absolute;top:11px;right:15px;'></i></p>"
                             + "</div>";
                $("#granteelist").append(cardList);
            }
            getCardGuestInfo();
            $("#step_form_1,.step_form_2,.step_form_4").addClass("dis");
            $("#step_form_234").removeClass("dis");
        } else if (checkProgress == 15) {
            $(".stepFour__name").text(GetStickInfo[passengerOrder].LastName + " " + GetStickInfo[passengerOrder].FirstName + " " + GetStickInfo[passengerOrder].LocalLastName + "" + GetStickInfo[passengerOrder].LocalFirstName);
            if (GetStickInfo[passengerOrder].Gender == "F") {
                $(".stepFour__sex").text("女");
            } else {
                $(".stepFour__sex").text("男");
            }
            $(".stepFour__countryName").text(countryChinese(GetStickInfo[passengerOrder].CountryCode));
            if (countryChinese(GetStickInfo[passengerOrder].CountryCode) != "中国") {
                $(".stepFour__identityCard").addClass("dis");
                if (countryChinese(GetStickInfo[passengerOrder].CountryCode) == "中国香港" || countryChinese(GetStickInfo[passengerOrder].CountryCode) == "中国澳门") {
                    $(".stepFour__identityCardTxt").text("港澳居民来往内地通行证：");
                    $(".stepFour__identityCard").removeClass("dis");
                } else if (countryChinese(GetStickInfo[passengerOrder].CountryCode) == "中国台湾") {
                    $(".stepFour__identityCardTxt").text("台湾居民来往大陆通行证：");
                    $(".stepFour__identityCard").removeClass("dis");
                }
            } else {
                $(".stepFour__identityCardTxt").text("身份证：");
                $(".stepFour__identityCard").removeClass("dis");
            }
            $(".stepFour__identityCardNum").text(GetStickInfo[passengerOrder].IdCard);
            $(".stepFour__passportNumber").text(GetStickInfo[passengerOrder].PassportNumber);
            $(".stepFour__passportData").text(GetStickInfo[passengerOrder].PassPortExpirationDate);
            $(".stepFour__birthDate").text($("#birthday").text());
            $(".stepFour__shipName").text($("#shipCode").val());
            $(".stepFour__sailDate").text($("#sailDate").attr("cdate"));
            $(".stepFour__ownTelephone").text(GetStickInfo[passengerOrder].ContactTelephone);
            $(".stepFour__telephone").text(GetStickInfo[passengerOrder].Telephone);
            if (GetStickInfo[passengerOrder].PaymentType == "NO") {
                $(".stepFour__paymethod").text("暂不选择任何支付方式");
            } else if (GetStickInfo[passengerOrder].PaymentType == "CA") {
                $(".stepFour__paymethod").text("现金");
            } else {
                $(".stepFour__paymethod").text("信用卡");
            }
            $("#step_form_1,.step_form_2,.step_form_3").addClass("dis");
            $("#step_form_234").removeClass("dis");
            $(".step_form_4").removeClass("dis");
            $(".step_li li").removeClass("curr");
            $(".step_li li").eq(3).removeClass("step_4").addClass("curr");
        }
    }


    //重新生成船票
    $(".button--regenerateTicket").click(function () {
        startLoading();
        $.ajax({
            type: 'post',
            url: 'https://www.rcclchina.com.cn/api/Rccl.Olci/Olci/SendBoardPassEmailV2?guestId=' + GuestId,
            success: function (resp) {
                if (resp.Success) {
                    rdpAlert("船票信息已发送到您的手机，请注意查收。");
                    stopLoading();
                } else {
                    rdpAlert(resp.ErrMsg);
                    stopLoading();
                }
            },
            error: function (error) {
                rdpAlert(error);
                stopLoading();
            }
        })
    })

    //护照提示
    $("#passPortNumber").focus(function () {
        var countryCode = $("#countryCode").val();
        if (countryCode == '中国') {
            $(".passPortNumber_promt").removeClass("dis");
        } else {
            $(".passPortNumber_promt").addClass("dis");
        }
    })

    //通行证提示
    $("#identityCardNumber").focus(function () {
        var countryCode = $("#countryCode").val();
        $(".identityCard_hkgmacPromt,.identityCard_twnPromt,.identityCard_hkgmacPromtTxt,.identityCard_twnPromtTxt").addClass("dis");
        if (countryCode == '中国香港' || countryCode == '中国澳门') {
            $(".identityCard_hkgmacPromt,.identityCard_hkgmacPromtTxt").removeClass("dis");
            $(".checkIN__signNumber").removeClass("dis");
        } else if (countryCode == '中国台湾') {
            $(".identityCard_twnPromt,.identityCard_twnPromtTxt").removeClass("dis");
            $(".checkIN__signNumber").removeClass("dis");
        }

    })

});

//二维码弹窗显示
function wxPicShow() {
    var shipCode = $("#shipCode").attr("shipno");
    var sailDate = $("#sailDate").val();
    startLoading();
    $.ajax({
        type: 'get',
        url: 'https://www.rcclchina.com.cn/api/Rccl.Olci/Olci/GetQrCode?bookingId=' + BookingId + '&sailDate=' + sailDate + '&shipCode=' + shipCode,
        success: function (resp) {
            stopLoading();
            if (resp.Success) {
                $("#QRcode_container img").attr("src", resp.Data);
                $(".step_stickQRCode img").attr("src", resp.Data);
            }
        },
        error: function (error) {
            stopLoading();
        }
    })
}

var checkInNum;//剩余需要做在线登记人数
function getcheckInNum() {
    checkInNum = 0;
    for (var i = 0; i < GetStickInfo.length; i++) {
        if (GetStickInfo[i].status == "IN_PROGRESS" || GetStickInfo[i].status == null) {
            checkInNum++;
        }
    }
    return checkInNum;
}

var leaveUrlNum = 2;//离开跳转地址 1:退出在线登记  2：后退 ,关闭浏览器
//剩余需要做在线登记人数的提醒
function submitClosedOlci() {
    $("#remainingNum").addClass("dis");
}

var paymethod;//支付名称
var payType;//支付方式
var bankName;//发卡行
var xykValid = true;//信用卡是否可修改
var isGrantee = false;//是否是被关联的人  true被关联   false不被关联
var PaymentType;//如果是5, 代表信用卡  如果是10, 代表现金    如果是15, 代表不选择
$("#cardNumber").blur(function () {
    $("#cardNumberInfo").addClass("dis");//输入正确信用卡卡号提示
    bankName = "";
    var cardNumber = $("#cardNumber").val().trim();
    if (cardNumber == "") {
        rdpAlert("请输入信用卡卡号");
        return false;
    } else {
        if (!/^[0-9]*$/.test(cardNumber)) {
            rdpAlert("请输入正确的信用卡卡号");
            return false;
        }
    }
})

//关联人信息
function granteeContactShow() {
    var granteelistLen = $("#granteelist .input").length;
    for (var m = 0; m < granteelistLen; m++) {
        var granteelistGuestId = $("#granteelist .input").eq(m).attr("GuestId");
        if (updateGrantList.length > 0) {
            for (var n = 0; n < updateGrantList.length; n++) {
                if (granteelistGuestId == updateGrantList[n].GranteeGuestId) {
                    $("#granteelist .input").eq(m).addClass("dis");
                }
            }
        }
        if (granteelistGuestId == GuestId) {
            $("#granteelist .input").eq(m).addClass("curr").addClass("dis");
        }
    }

    for (var j = 0; j < updateGrantList.length; j++) {
        if (GuestId == updateGrantList[j].GrantorGuestId && GuestId != updateGrantList[j].GranteeGuestId) {
            for (var i = 0; i < granteelistLen; i++) {
                var granteelistGuestId = $("#granteelist .input").eq(i).attr("GuestId");
                if (granteelistGuestId == updateGrantList[j].GranteeGuestId) {
                    $("#granteelist .input").eq(i).addClass("curr").removeClass("dis");
                }
            }
        }
    }
}

//支付方式部分
//paymentType   1为信用卡  2为现金  3不选择任何支付方式
function chooseCardType(paymentType) {
    granteeContactShow();
    $(".xyk_minorPromt").addClass("dis");
    //是否是被关联的人  true被关联   false不被关联
    if (!isGrantee) {
        if (paymentType == "1") {
            paymethod = "信用卡";
            payType = 1;
            $("#noSelectCard").removeClass("curr");
            $("#debitCard").removeClass("curr");
            $("#creditCard").addClass("curr");
            $(".cxk_main").addClass("dis");
            //未成年人不可选择信用卡,显示提示
            var sailDateTime = $("#sailDate").val();
            try {
                var birthDate = new Date(birthOfDate.replace("-", "/").replace("-", "/"));
                sailDateTime = new Date(sailDateTime.replace("-", "/").replace("-", "/"));
            } catch (e) {
                console.log(e)
            }
            var passDueTime = Math.ceil(sailDateTime.getTime() - birthDate.getTime()) / (60 * 60 * 1000 * 24 * 365);
            if (passDueTime < 18) {
                $(".xyk_main").addClass("dis");
                $(".xyk_minorPromt").removeClass("dis");
                return false;
            }
            if (xykValid) {
                $(".xyk_main .submit").text("提交资料");
                $(".xyk_main").removeClass("dis");
                $(".form--xykInfo").removeClass("dis");
                $(".xyk_cardholder").addClass("dis");
                $(".noselect_main").addClass("dis");
            } else {
                $(".xyk_main .submit").text("确认");
                $(".xyk_cardholder").removeClass("dis");
                $(".xyk_main").removeClass("dis");
                $(".form--xykInfo").addClass("dis");
                $(".noselect_main").addClass("dis");
            }
        } else if (paymentType == "2") {
            paymethod = "现金";
            payType = 2;
            $("#noSelectCard").removeClass("curr");
            $("#creditCard").removeClass("curr");
            $("#debitCard").addClass("curr");

            $(".cxk_main").removeClass("dis");
            $(".xyk_main").addClass("dis");
            $(".noselect_main").addClass("dis");
        } else {
            paymethod = "暂不选择任何支付方式";
            $("#debitCard").removeClass("curr");
            $("#creditCard").removeClass("curr");
            $("#noSelectCard").addClass("curr");
            $(".noselect_main").removeClass("dis");
            $(".cxk_main").addClass("dis");
            $(".xyk_main").addClass("dis");
        }
    } else {
        $("#creditCard").addClass("curr");
        $(".cxk_main").addClass("dis");

        paymethod = "信用卡";
        payType = 1;
        $("#noSelectCard").removeClass("curr");
        $("#debitCard").removeClass("curr");
        $(".xyk_main .submit").text("确认");
        $(".xyk_cardholder").removeClass("dis");
        $(".xyk_main").removeClass("dis");
        $(".form--xykInfo").addClass("dis");
        $(".noselect_main").addClass("dis");
    }
}
//支付方式--返回上一步
function paymethodPrev() {

    $("#step_form_3").addClass("dis");
    $("#step_form_2").removeClass("dis");
    $(".step_li li").removeClass("curr");
    $(".step_li li").eq(1).addClass("curr");
}
var GranteeGuestList = [];//正在登记的信用卡关联信息
var updateGrantList = [];//信用卡关联与被关联人信息
//PaymentType 如果是5, 代表信用卡  如果是10, 代表现金    如果是15, 代表不选择
//支付方式--提交
function paymethodSubmit(paymentType) {
    GranteeGuestList = [];
    var IsCash;
    var CardHolderName = $("#cardUserName").val().trim().replace(/[ ]/g, "");
    var CardNumber = $("#cardNumber").val().trim().replace(/[ ]/g, "");
    var CardExpirationMonth = $("#cardMonSelect").val();
    var CardExpirationYear = $("#cardYearSelect").val();
    var cardEndDate = CardExpirationYear + "/" + CardExpirationMonth;
    //var IsAgree = $(".agreement").hasClass("curr");

    var granteelistLen = $("#granteelist .input").length;
    for (var i = 0; i < granteelistLen; i++) {
        var isCurr = $("#granteelist .input").eq(i).hasClass("curr");
        if (isCurr) {
            var GranteeGuest = { GuestId: "", LastName: "", FirstName: "" };
            GranteeGuest.GuestId = $("#granteelist .input").eq(i).attr("guestid");
            GranteeGuest.FirstName = $("#granteelist .input").eq(i).find(".CardFirstName").text();
            GranteeGuest.LastName = $("#granteelist .input").eq(i).find(".CardLastName").text();
            GranteeGuestList.push(GranteeGuest);
        }
    }
    if (paymentType == 2) {
        PaymentType = 10;
        IsCash = true;
        var data = {
            BookingId: BookingId, GuestId: GuestId, BookingAccessToken: BookingAccessToken,
            IsCash: IsCash, PaymentType: PaymentType, CardNumber: CardNumber, CardHolderName: CardHolderName, CardExpirationMonth: CardExpirationMonth,
            CardExpirationYear: CardExpirationYear, GranteeGuestList: GranteeGuestList
        }
        cardInfoSubmit(data);
    } else if (paymentType == 1) {
        PaymentType = 5;
        IsCash = false;
        var data = {
            BookingId: BookingId, GuestId: GuestId, BookingAccessToken: BookingAccessToken,
            IsCash: IsCash, PaymentType: PaymentType, CardNumber: CardNumber, CardHolderName: CardHolderName, CardExpirationMonth: CardExpirationMonth,
            CardExpirationYear: CardExpirationYear, GranteeGuestList: GranteeGuestList
        }
        if (xykValid) {
            if (cardValid(CardHolderName, CardNumber)) {
                cardInfoSubmit(data);
            }
        } else {
            $("#step_form_3").addClass("dis");
            $("#step_form_4").removeClass("dis");
            $(".step_li li").removeClass("curr");
            $(".step_li li").eq(3).removeClass("step_4").addClass("curr");
            //二维码弹窗显示
            $("#weixinModal").show();
            return false;
        }
    } else {
        PaymentType = 15;
        IsCash = false;
        var data = {
            BookingId: BookingId, GuestId: GuestId, BookingAccessToken: BookingAccessToken,
            IsCash: IsCash, PaymentType: PaymentType, CardNumber: CardNumber, CardHolderName: CardHolderName, CardExpirationMonth: CardExpirationMonth,
            CardExpirationYear: CardExpirationYear, GranteeGuestList: GranteeGuestList
        }
        cardInfoSubmit(data);
    }
}

//如果出航日期与当前时间相比小于48小时, 信用卡和现金支付方式不可以提交
function xyCardTime() {
    //出航日期
    var sailDateTime = $("#sailDate").val();
    try {
        sailDateTime = new Date(sailDateTime.replace("-", "/").replace("-", "/"));
        var nowTime = new Date();
    } catch (e) {
        console.log(e)
    } finally {

    }
    //未满48小时提示       
    var xyCardTime = Math.ceil(sailDateTime.getTime() - nowTime.getTime()) / (60 * 60 * 1000);
    if (xyCardTime < (48 - 17)) {
        $("#xyCardTime").removeClass("dis");
        return false;
    }
    return true;
}

//提交信用卡信息
function cardInfoSubmit(data) {
    startLoading();
    $.ajax({
        type: 'post',
        url: 'https://www.rcclchina.com.cn/api/Rccl.Olci/Olci/OnboardPayment',
        data: data,
        success: function (resp) {
            stopLoading();
            if (resp.Success) {
                $(".stepFour__paymethod").text(paymethod);
                $("#step_form_3").addClass("dis");
                $("#step_form_4").removeClass("dis");
                $(".step_li li").removeClass("curr");
                $(".step_li li").eq(3).removeClass("step_4").addClass("curr");
                //二维码弹窗显示
                $("#weixinModal").show();
                updateGrant();//更新关联与被关联的联系
            } else {
                if (resp.ErrMsg == "更新支付信息失败") {

                    //0812 支付失败不跳下一步
                    rdpAlert("更新支付信息失败，请使用其他信用卡");
                } else {
                    rdpAlert(resp.ErrMsg);
                }
            }
        },
        error: function (error) {
            //console.log(error)
            stopLoading();
        }
    })
}

//更新关联与被关联的联系
function updateGrant() {
    var grantee = [];
    for (var i = 0; i < updateGrantList.length; i++) {
        if (GuestId != updateGrantList[i].GrantorGuestId) {
            var GranteeGuest = { GrantorGuestId: "", GranteeGuestId: "" };
            GranteeGuest.GrantorGuestId = updateGrantList[i].GrantorGuestId;
            GranteeGuest.GranteeGuestId = updateGrantList[i].GranteeGuestId;
            grantee.push(GranteeGuest);
        }
    }
    updateGrantList = grantee;
    for (var j = 0; j < GranteeGuestList.length; j++) {
        if (PaymentType != 5) {
            if (GuestId != GranteeGuestList[j].GuestId) {
                continue;
            }
        }
        var GranteeGuest = { GrantorGuestId: "", GranteeGuestId: "" };
        GranteeGuest.GrantorGuestId = GuestId;
        GranteeGuest.GranteeGuestId = GranteeGuestList[j].GuestId;
        updateGrantList.push(GranteeGuest);
    }
}


//信用卡资料验证cardValid(CardHolderName, CardNumber, IsAgree)
function cardValid(cardUserName, cardNumber) {
    var xykName = "";
    for (var i = 0; i < GetStickInfo.length; i++) {
        if (GuestId == GetStickInfo[i].GuestId) {
            xykName = GetStickInfo[i].LastName + GetStickInfo[i].FirstName;
            xykName = xykName.replace(/[ ]/g, "").toUpperCase();
        }
    }
    if (cardUserName == "") {
        rdpAlert("请输入姓名拼音");
        return false;
    } else {
        if (!/^[a-zA-Z+\s]+$/.test(cardUserName)) {
            rdpAlert("请输入正确的姓名拼音");
            return false;
        } else if (cardUserName.toUpperCase() != xykName) {
            rdpAlert("信用卡持卡人必须和当前乘客名相同");
            return false;
        }
    }
    if (cardNumber == "") {
        rdpAlert("请输入信用卡卡号");
        return false;
    } else {
        if (!/^[0-9]*$/.test(cardNumber)) {
            rdpAlert("请输入正确的信用卡卡号");
            return false;
        }
    }
    //if (!IsAgree) {
    //    rdpAlert("请勾选用户协议");
    //    return false;
    //}
    return true;
}

//用户协议弹出框
function agreementDialog() {
    // TODO用户协议的内容
    var agreementContent = "创建船上消费帐户需填写信用卡信息。在启航前，我们不会获得该卡的授权或收取费用。 上述信用卡持有者特此授权游轮公司在航游期间从该信用卡帐户收取船上消费帐户消费的所有费用，并同意对此类收费承担个人责任；上述宾客保证，在航游结束时将以现金或旅行支票支付航游期间该消费帐户消费的所有费用。 信用卡持有者必须随船同行且列于该预订下。 您若想使用同一信用卡为多人付款，请确保在下列复选框内勾选他们的姓名，如果预订编号不同，请点击所提供的链接。";

    setDialogTopHeight("#agreementTxt .box");
    //打开并阅读用户协议
    $("#readAgreement").click(function () {
        $("#agreementContent").html(agreementContent);
        setDialogTopHeight("#agreementTxt .box");
        $("#agreementTxt").removeClass("dis");
    });
    //弹出框阅读完后点击确认同意用户协议
    $("#agreementBtn").click(function () {
        $("#agreementTxt").addClass("dis");
        $(".newcheckin .step_form_3 .xyk_main .agreement").addClass("curr");
    });

    //直接打钩同意，或不同意去除打钩
    $(".newcheckin .step_form_3 .xyk_main .agreement").click(function () {
        var className = $(this).attr("class");
        if (className == "agreement") {
            $(this).addClass("curr");
        } else {
            $(this).removeClass("curr");
        }
    });
}

//设置弹出窗到到页面顶部的高度
function setDialogTopHeight(id) {
    var height_screen = $(window).height();
    //var height_top = (height_screen - $(id).height()) / 2 + "px";
    var height_top = 100 + "px";
    $(id).css("top", height_top);

}