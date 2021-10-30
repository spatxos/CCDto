var MyScript = {
    ///页面初始化
    PageInit: function () {
        $.ajaxSetup({ cache: false });

        bootbox.setDefaults({
            locale: 'zh_CN',
            title: "系统提示："
        });

        $.fn.editable.defaults.emptytext = '请输入';
        $.fn.editable.defaults.language = 'zh-CN';
        $.fn.editable.defaults.inputclass = 'form-control';

        toastr.options = {
            "closeButton": true,
            "debug": false,
            "positionClass": "toast-top-right",
            "progressBar": true,
            //"onclick": function() {alert('x');},
            "showDuration": "10000",
            "hideDuration": "10000",
            "timeOut": "50000",
            "extendedTimeOut": "10000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };

        $("a.fancyboximg").fancybox({
            'transitionIn': 'elastic',
            'transitionOut': 'elastic',
            'speedIn': 600,
            'speedOut': 200,
            'overlayShow': false
        });

        $("a.fancybox").fancybox({
            'transitionIn': 'elastic',
            'transitionOut': 'elastic',
            'speedIn': 600,
            'speedOut': 200,
            'overlayShow': false,
            'width': '95%',
            'height': '60%',
            'titleShow':true,
            'autoDimensions': false,
            'onComplete': function () {
                //$('.fancybox-iframe').load(function () {
                //    $('.fancybox-inner').height($($(this).context).height() + 20);
                //});
            }
        });

        $("a.fancyboxinit").click(function () {
            $(this).fancybox({
                'transitionIn': 'elastic',
                'transitionOut': 'elastic',
                'speedIn': 600,
                'speedOut': 200,
                'overlayShow': false,
                'width': '95%',
                'height': '60%',
                'titleShow': true,
                'autoDimensions': false,
                'onComplete': function () {
                    //$('.fancybox-iframe').load(function () {
                    //    $('.fancybox-inner').height($($(this).context).height() + 20);
                    //});
                },
            });
        })

        $("[data-toggle='popover-ajax']").webuiPopover({
            animation: 'pop',
            type: 'async',
            closeable: true
        });

        $("[data-toggle='popover']").webuiPopover({
            animation: 'pop',
            closeable: true
            //type: 'async'
        });
    },

    //普通下拉选择初始化
    BsSelectInit: function () {
        $('.bs-select').selectpicker({
            iconBase: 'fa',
            tickIcon: 'fa-check'
        });
    },

    //选择框初始化
    iCheckInit: function () {
        $('input').iCheck({
            checkboxClass: 'icheckbox_square-blue',
            radioClass: 'iradio_square-blue',
            increaseArea: '20%' // optional
        });
    },

    //选择框初始化
    classiCheckInit: function () {
        $('input.icheck').iCheck({
            checkboxClass: 'icheckbox_square-blue',
            radioClass: 'iradio_square-blue',
            increaseArea: '20%' // optional
        });
    },

    SlimScrollInit: function (el) {
        $(el).each(function () {
            if ($(this).attr("data-initialized")) {
                return; // exit
            }

            var height;

            if ($(this).attr("data-height")) {
                height = $(this).attr("data-height");
            } else {
                height = $(this).css('height');
            }

            $(this).slimScroll({
                allowPageScroll: true, // allow page scroll when the element scroll is ended
                size: '7px',
                color: ($(this).attr("data-handle-color") ? $(this).attr("data-handle-color") : '#bbb'),
                wrapperClass: ($(this).attr("data-wrapper-class") ? $(this).attr("data-wrapper-class") : 'slimScrollDiv'),
                railColor: ($(this).attr("data-rail-color") ? $(this).attr("data-rail-color") : '#eaeaea'),
                position: 'right',
                height: height,
                alwaysVisible: ($(this).attr("data-always-visible") === "1" ? true : false),
                railVisible: ($(this).attr("data-rail-visible") === "1" ? true : false),
                disableFadeOut: true
            });

            $(this).attr("data-initialized", "1");
        });
    },

    //日期选择初始化
    /*DatepickerInit: function () {
        $('.dateISO').datepicker({
            language: "zh-CN",
            autoclose: true
        });
    },*/

    //blockUI
    blockUI: function () {
        $.blockUI({
            message: "<div class='loading-message loading-message-boxed'><img src='../images/loading-spinner-grey" +
            "" +
            ".gif' align=''><span>&nbsp;&nbsp;加载中<img src='../assets/global/img/loading.gif' /></span></div>", css: { border: "0px", background: "none" } });
    },

    ModalInit: function () {
        // fix stackable modal issue: when 2 or more modals opened, closing one of modal will remove .modal-open class. 
        $('body').on('hide.bs.modal', function () {
            if ($('.modal:visible').size() > 1 && $('html').hasClass('modal-open') === false) {
                $('html').addClass('modal-open');
            } else if ($('.modal:visible').size() <= 1) {
                $('html').removeClass('modal-open');
            }
        });

        // fix page scrollbars issue
        $('body').on('show.bs.modal', '.modal', function () {
            if ($(this).hasClass("modal-scroll")) {
                $('body').addClass("modal-open-noscroll");
            }
        });

        // fix page scrollbars issue
        $('body').on('hidden.bs.modal', '.modal', function () {
            $('body').removeClass("modal-open-noscroll");
        });

        // remove ajax content and remove cache on modal closed 
        $('body').on('hidden.bs.modal', '.modal:not(.modal-cached)', function () {
            $(this).removeData('bs.modal');
        });
    },

    AllInit:function() {
        MyScript.PageInit();
        MyScript.ModalInit();
        MyScript.SlimScrollInit('.scroller');
        MyScript.BsSelectInit();

        $('.select2').select2({

        });

        $("img").error(function () {
        //    $(this).attr("src", "../images/no-image.png");
        });
    }
}

var producturl = "";

// Instance the tour

$(document).ready(function () {
    MyScript.AllInit();
    String.prototype.Contains = function (value) {
        'use strict';
        if (this !== "" && this !== null && this !== "undefined") {
            return this.indexOf(value) > -1;
        }
        return false;
    };
});

var destroyediter = function (name) {
    if (typeof (UE.getEditor(name)).container !== 'undefined') {
        UE.getEditor(name).destroy();
    }
};


function isJSON(str) {
    if (typeof str == 'string') {
        try {
            var obj = JSON.parse(str);
            if (typeof obj == 'object' && obj) {
                return obj;
            } else {
                return str;
            }

        } catch (e) {
            return str;
        }
    }
    return str;
}

