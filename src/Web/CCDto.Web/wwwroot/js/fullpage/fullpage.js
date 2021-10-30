/*
* version:fullPage.js-v1.0 / v1.1 / v1.2
* author:wangzhihong
* email:guailaoma@qq.com
* time:2017.12.19 / 2017.12.25 / 2018.1.19
*
* 一、功能介绍：
* 1、头部和页脚（可取舍），
* 2、滚轮翻页，
* 3、键盘翻页，
* 4、可配置背景(背景色，背景图片可混用)，
* 5、嵌套full，
* 6、导航tips，
* 7、回调函数，
* 8、自适应屏幕
*
* 2018.1.19：add header footer（样式都在fullpage.css里修改）
*
* 二、参数配置：
* fullBg:[],           // 背景配置，可嵌套（背景img和背景色，可以混用）
*                      例：["image/1.png","#B934FF",["#45FAFF","image/2.png","#542936"],"#1437FF"]
*                      img支持的格式为：jpg、png、jpeg、bmp、gif、svg
*
* navTip:[],           // 例：["第一屏","第二屏","第三屏"]
* nav:true,            // 左边nav 是否显示
* nestNav:true,        // 嵌套nav 是否显示
* nestBtn:true         // 嵌套btn 是否显示
* autoTime:300,        // full翻页速度
* startCallback:false, // 进入full回调函数
* endCallback:false    // 离开full回调函数
*
* 为了世界和平，请勿删掉注释。哈哈哈 0.0
*
* */
(function($){
    $.fn.fullScreen = function(obj){
        var setting={
            fullBg:['#542936','#4373FF',['#B934FF','#1437FF','#45FAFF','#17FF5B'],'#45FAFF','#542936'],
            navTip:[],
            autoTime:300,
            nav:true,
            nestNav:true,
            nestBtn:true,
            startCallback:false,
            endCallback:false
        };
        var footer_h = "";
        if(obj){
            $.extend(setting,obj);
        }
        // fullBg set
        var img_suffix = "";// img的后缀名
        for(var i = 0 ; i < setting.fullBg.length ; i++){
            if(typeof setting.fullBg[i] != "object"){ // 如果没有嵌套的
                if(setting.fullBg[i].indexOf(".") >= 0 ){ // 如果是图片
                    img_suffix = setting.fullBg[i].split(".")[setting.fullBg[i].split(".").length-1];
                    if( img_suffix === "jpg" || img_suffix === "png" || img_suffix === "gif" || img_suffix === "bmp" || img_suffix === "jpeg" || img_suffix === "svg" ){
                        $(".full").eq(i).css({
                            "background":'url('+setting.fullBg[i]+')',
                            "background-position":"center center",
                            "background-repeat":"no-repeat",
                            "background-size":"100% 100%"
                        });
                    }
                }else{
                    $(".full").eq(i).css({"background":setting.fullBg[i]});
                }
            }else{
                for(var j = 0 ; j < setting.fullBg[i].length ; j++){ // 如果有嵌套
                    if(setting.fullBg[i][j].indexOf(".") >= 0 ){ // 如果是图片
                        img_suffix = setting.fullBg[i][j].split(".")[setting.fullBg[i][j].split(".").length-1];
                        if( img_suffix === "jpg" || img_suffix === "png" || img_suffix === "gif" || img_suffix === "bmp" || img_suffix === "jpeg" || img_suffix === "svg" ){
                            $(".full").eq(i).find(".nest-full").eq(j).css({
                                "background":'url('+setting.fullBg[i][j]+')',
                                "background-position":"center center",
                                "background-repeat":"no-repeat",
                                "background-size":"100% 100%"
                            });
                        }
                    }else{
                        $(".full").eq(i).find(".nest-full").eq(j).css({"background":setting.fullBg[i][j]});
                    }
                }
            }
        }
        // 计算full的高度
        function load_obj_h(){
            var window_w = $(window).width();
            var window_h = $(window).height();
            $(".fullPage,.full,.nest-full-box,.nest-full").height(window_h);
            $(".nest-full").width(window_w);
            $(".full").each(function(){
                if($(this).find(".nest-full-box").length > 0){
                    var nest_full_len = $(this).find(".nest-full-box>.nest-full").length;// 用处：计算nest-full-box的宽度。
                    $(this).find(".nest-full-box").width(nest_full_len*window_w);
                }
            });
        }
        load_obj_h();
        if($(".myfooter").length>0){
            footer_h = $(".myfooter").height();
            $(".myfooter").css({bottom:-footer_h});
        }
        function objMove(index){
            var full_h = $(".full").height();
            // 判断是否处于动画中...
            if(!$(".full-box").is(":animated")){
                if(setting.endCallback){   // 离开full时的回调函数
                    setting.endCallback(reserveNow()+1,nestNow(reserveNow())+1);
                }
                $(".full-box").animate({
                    top:'-' + full_h * index + 'px'
                },setting.autoTime,function(){
                    if(setting.startCallback){// 进入full时的回调函数
                        setting.startCallback(reserveNow()+1,nestNow(reserveNow())+1);
                    }
                });
                $(".full-nav>li").eq(index).addClass("active").siblings().removeClass("active");
                // footer
                if($(".myfooter").length > 0){
                    if((index+1) >= $(".full").length){
                        $(".myfooter").animate({bottom:0},setting.autoTime+200);
                    }else{
                        $(".myfooter").animate({bottom:-footer_h},setting.autoTime+200);
                    }
                }
            }

        }
        function nestObjMove(full_now,nest_full_now){
            // 参数1 右边小点的 下标
            // 参数2 嵌套小点的 下标
            if(!$(".full").eq(full_now).find(".nest-full-box").is(":animated")){
                if(setting.endCallback){   // 离开full时的回调函数
                    setting.endCallback(reserveNow()+1,nestNow(reserveNow())+1);
                }
                $(".full").eq(full_now).find(".nest-full-box").animate({
                    left:'-'+(nest_full_now*100)+'%'
                },setting.autoTime,function(){
                    if(setting.startCallback){// 进入full时的回调函数
                        setting.startCallback(reserveNow()+1,nestNow(reserveNow())+1);
                    }
                });
                $(".full").eq(full_now).find(".nest-full-nav>li").eq(nest_full_now).addClass("active").siblings().removeClass("active");
            }

        }
        // 储存当前下标 - 右边小点
        function reserveNow(){
            var number = "";
            for(var i = 0; i < $(".full-nav>li").length ; i++){
                if($(".full-nav>li")[i].getAttribute("class") == "active"){
                    number = i;
                }
            }
            return number;
        }
        // 储存当前下标 - 嵌套的小点
        function nestNow(now){
            var number = "";
            if($(".full").eq(now).find(".nest-full-nav>li").length !=0){
                for(var i = 0; i < $(".full").eq(now).find(".nest-full-nav>li").length ; i++){
                    if($(".full").eq(now).find(".nest-full-nav>li")[i].getAttribute("class") == "active"){
                        number = i;
                    }
                }
            }
            return number;
        }
        var s_ol = "";
        var active = "";
        var nowNum = 0;     // 当前显示的 下标
        var nest_nowNum = 0;// 当前嵌套显示的 下标
        var tips = "";
        //---------------------------------------------------遍历full ol li---
        $(".full").each(function(i){
            if(i === 0){
                active = "active";
            }else{
                active = "";
            }
            if(setting.navTip.length > 0){
                tips = '<div class="tip-box">' +
                            '<div class="san"></div>' +
                            '<div class="tip-content">'+setting.navTip[i]+'</div>' +
                        '</div>';

            }
            s_ol += '<li class="'+active+'">'+tips+'</li>';
            if($(this).find(".nest-full-box").length > 0){
                var nest_ol = "";
                $(this).find(".nest-full-box .nest-full").each(function(j){
                    if(j === 0){
                        active = "active";
                    }else{
                        active = "";
                    }
                    nest_ol += '<li class="'+active+'"></li>';
                });
                $(this).find(".nest-full-nav").html(nest_ol);
            }
        });
        $(".full-nav").html(s_ol);

        if(setting.navTip.length > 0){
            $(".full-nav>li").hover(function(){
                $(this).find(".tip-box").fadeIn(100);
            },function () {
                $(this).find(".tip-box").fadeOut(100);
            });
        }
        if(setting.nav){
            $(".full-nav").show();
        }
        if(setting.nestNav){
            $(".nest-full-nav").show();
        }
        if(setting.nestBtn){
            $(".full-btn").show();
        }
        //-------------------------------------------------定位 右小点 上下居中-----
        var f_nav_h = parseFloat($(".full-nav").height())/2;
        var n_f_nav_h = parseFloat($(".nest-full-nav").width())/2;
        $(".full-nav").css({"marginTop":-f_nav_h});
        $(".nest-full-nav").css({"marginLeft":-n_f_nav_h});


        //-------------------------------------------------右边 小点 控制翻页-----
        $(".full-nav>li").click(function(){
            var index = $(".full-nav>li").index(this);
            objMove(index);
        });
        //----键盘事件----
        $(window).keyup(function(e){
            var ev = e || window.e || arguments.callee.caller.arguments[0];
            nowNum = reserveNow();
            nest_nowNum = nestNow(nowNum);
            if(ev.keyCode == 38){ // 上
                nowNum--;
                if(nowNum < 0){
                    nowNum = 0;
                    return false;
                }
                objMove(nowNum);
            }
            if(ev.keyCode == 40){ // 下
                nowNum++;
                if(nowNum >= $(".full").length){
                    nowNum = $(".full").length-1;
                    return false;
                }
                objMove(nowNum);
            }

            if(ev.keyCode == 37){ // 左
                //nest_nowNum = reserveNow(true);
                nest_nowNum--;
                if(nest_nowNum < 0){
                    nest_nowNum = 0;
                    return false;
                }
                nestObjMove(nowNum,nest_nowNum);
            }
            if(ev.keyCode == 39){ // 右
                nest_nowNum++;
                if(nest_nowNum >= $(".full").eq(nowNum).find(".nest-full").length){
                    nest_nowNum = $(".full").eq(nowNum).find(".nest-full").length-1;
                    return false;
                }
                nestObjMove(nowNum,nest_nowNum);
            }
        });
        //------------------------------------------------------
        /***********************
         6 * 函数：判断滚轮滚动方向
         7 * 作者：王志红
         8 * 参数：event
         9 * 返回：滚轮方向
         10* IE/Opera/Chrome----正数：向上 负数：向下
         11* Firefox        ----正数：向下 负数：向上
         12 *************************/
        var scrollFunc=function(event){
            var e = event || window.event;
            nowNum = reserveNow();
            nest_nowNum = nestNow(nowNum);
            if(e.wheelDelta){// IE/Opera/Chrome
                if(e.wheelDelta > 0){
                    nowNum--;
                    if(nowNum < 0){
                        nowNum = 0;
                        return false;
                    }
                    objMove(nowNum);
                }else{
                    nowNum++;
                    if(nowNum >= $(".full").length){
                        nowNum = $(".full").length-1;
                        return false;
                    }
                    objMove(nowNum);
                }
            }else if(e.detail){// Firefox
                if(e.detail > 0){
                    nowNum++;
                    if(nowNum >= $(".full").length){
                        nowNum = $(".full").length-1;
                        return false;
                    }
                    objMove(nowNum);
                }else{
                    nowNum--;
                    if(nowNum < 0){
                        nowNum = 0;
                        return false;
                    }
                    objMove(nowNum);

                }
            }
        };
        /*注册事件*/
        if(document.addEventListener){
            document.addEventListener('DOMMouseScroll',scrollFunc,false);
        }
        //W3C
        window.onmousewheel=document.onmousewheel=scrollFunc;//IE/Opera/Chrome/Safari
        //--------------------------------------------嵌套full 左右按钮----------
        $(".btn-left").click(function(){
            nowNum = reserveNow();
            nest_nowNum = nestNow(nowNum);
            nest_nowNum--;
            if(nest_nowNum < 0 ){
                nest_nowNum = 0;
                return false;
            }
            nestObjMove(nowNum,nest_nowNum);
        });
        $(".btn-right").click(function(){
            nowNum = reserveNow();
            nest_nowNum = nestNow(nowNum);
            nest_nowNum++;
            if(nest_nowNum >= $(this).parents(".full").find(".nest-full").length ){
                nest_nowNum =  $(this).parents(".full").find(".nest-full").length-1;
                return false;
            }
            nestObjMove(nowNum,nest_nowNum);
        });
        $(".nest-full-nav>li").click(function(){
            nest_nowNum = $(this).parents(".full").find(".nest-full-nav>li").index(this);
            nowNum = reserveNow();
            nestObjMove(nowNum,nest_nowNum);
        });

        // 浏览器大小变化时，重新计算full的高度。
        $(window).resize(function(){
            setTimeout(function(){
                load_obj_h();
            },500);
        })

    }
})(jQuery);
