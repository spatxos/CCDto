
(function ($) {
    var date1 = new Date();
    console.log("本次加载：" + date1.getMinutes() + ":" + date1.getSeconds() + " " + date1.getMilliseconds());

    //备份jquery的ajax方法   
    var _ajax = $.ajax;

    var token = $.token;
    //重写jquery的ajax方法   
    $.ajax = function (opt) {
        if (opt.url.indexOf("javascript") <= -1) {
            if (!opt.beforeSubmit) {
                //备份opt中error和success方法   
                var fn = {
                    beforeSend: function (XMLHttpRequest) { },
                    error: function (XMLHttpRequest, textStatus, errorThrown) { },
                    //success: function (data, textStatus) { }
                    complete: function (data, textStatus) { },
                    beforeSubmit: function (formData, jqForm, options) { }
                };
                if (opt.beforeSend) {
                    fn.beforeSend = opt.beforeSend;
                }
                if (opt.error) {
                    fn.error = opt.error;
                }
                //if (opt.success) {
                //    fn.success = opt.success;
                //}
                if (opt.complete) {
                    fn.complete = opt.complete;
                }
                var isotherurl = false;
                if (opt.url.indexOf("kuaidi100.com") > -1) {
                    isotherurl = true;
                }
                //扩展增强处理   
                var _opt = $.extend(opt, {
                    beforeSend: function (XMLHttpRequest) {
                        date1 = new Date();
                        try {
                            //发送ajax请求之前向http的head里面加入验证信息
                            XMLHttpRequest.setRequestHeader('Authorization', 'Bearer ' + token);
                            XMLHttpRequest.setRequestHeader('content-type', 'application/x-www-form-urlencoded; charset=utf-8');

                            fn.beforeSend(XMLHttpRequest);
                            //return true;
                        } catch (e) {
                            console.log(e);
                        }
                    },
                    beforeSubmit: function (formData, jqForm, options) {
                        try {
                            fn.beforeSubmit(formData, jqForm, options);
                            //return true;
                        } catch (e) {
                            console.log(e);
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        //错误方法增强处理  
                        if (XMLHttpRequest.status !== 0) {
                            toastr.clear();
                            toastr["error"]("请求出现错误！请稍后重试", "系统提示");
                        }
                        //if (layer) {
                        //    layer.msg("出错了,请联系管理员!", 2, 3, null, true);
                        //} else {
                        //    alert("出错了,请联系管理员!");
                        //}
                        fn.error(XMLHttpRequest, textStatus, errorThrown);
                    },
                    //success: function (data, textStatus) {
                    //    //成功回调方法增强处理   
                    //    var error = data.error;
                    //    if (error != undefined && error == true) {
                    //        if (layer) {
                    //            layer.msg(data.reason, 2, 3, null, true);
                    //        } else {
                    //            alert(data.reason);
                    //        }
                    //        return;
                    //    }
                    //    fn.success(data, textStatus);
                    //},
                    complete: function (data, textStatus) {
                        try {
                            var json = JSON.parse(data.responseText);
                            setTimeout(function () {
                                //$('.fancybox-inner').height($($('.fancybox-inner').context).height() + 20);
                                //$('.fancybox-inner').attr("height", $($('.fancybox-inner').context).height() + 20 + "px");
                            }, 2000);
                            if (json.error === 1) {
                                toastr.clear();
                                toastr["error"]("当前登陆已过期！请重新登录", "系统提示");
                                location.href = "/Account/Index";
                            }
                            if (json.code === 1) {
                                //location.reload();
                                //$("#myUnauthorizedModal").modal('show');
                            }
                        } catch (e) {
                            console.log(e);
                        }
                        var date2 = new Date();
                        console.log("本次加载：" + date2.getMinutes() + ":" + date2.getSeconds() + " " + date2.getMilliseconds());

                        var dateDiff = date2.getTime() - date1.getTime();
                        $("#footer").prepend("本次加载：" + dateDiff + "毫秒；");

                        fn.complete(data, textStatus);
                    }
                });
                return _ajax(_opt);
            } else {
                var fn1 = {
                    headers: {},
                    beforeSubmit: function (formData, jqForm, options) { }
                };
                if (opt.headers) {
                    fn1.headers = opt.headers;
                }
                if (opt.beforeSubmit) {
                    fn1.beforeSubmit = opt.beforeSubmit;
                }
                var _opt1 = $.extend(opt, {
                    headers: {
                        "Authorization": 'Bearer ' + token,
                        //'content-type': 'multipart/form-data',
                    }
                });
                return _ajax(_opt1);
            }
        }
        return _ajax(opt);
    };
})(jQuery);