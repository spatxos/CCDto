jQuery(function ($) {
    //创建DOM
    var
	quickHTML = document.querySelector("div.quick_link_mian"),
	quickShell = $(document.createElement('div')).html(quickHTML).addClass('quick_links_wrap'),
	quickLinks = quickShell.find('.quick_links');
    quickPanel = quickLinks.next();
    quickShell.appendTo('.mui-mbar-tabs');

    //具体数据操作 
    var
	quickPopXHR,
	loadingTmpl = '<div class="loading" style="padding:30px 80px"><i></i><span>Loading...</span></div>',
	popTmpl = '<a href="javascript:;" class="ibar_closebtn" title="关闭"><i class="icon-close"></i></a><div class="ibar_plugin_title"><h3><%=title%></h3></div><div class="pop_panel"><%=content%></div><div class="arrow"><i></i></div><div class="fix_bg"></div>',
	historyListTmpl = '<ul><%for(var i=0,len=items.length; i<5&&i<len; i++){%><li><a href="<%=items[i].productUrl%>" target="_blank" class="pic"><img alt="<%=items[i].productName%>" src="<%=items[i].productImage%>" width="60" height="60"/></a><a href="<%=items[i].productUrl%>" title="<%=items[i].productName%>" target="_blank" class="tit"><%=items[i].productName%></a><div class="price" title="单价"><em>&yen;<%=items[i].productPrice%></em></div></li><%}%></ul>',
	newMsgTmpl = '<ul><li><a href="#"><span class="tips">新回复<em class="num"><b><%=items.commentNewReply%></b></em></span>商品评价/晒单</a></li><li><a href="#"><span class="tips">新回复<em class="num"><b><%=items.consultNewReply%></b></em></span>商品咨询</a></li><li><a href="#"><span class="tips">新回复<em class="num"><b><%=items.messageNewReply%></b></em></span>我的留言</a></li><li><a href="#"><span class="tips">新通知<em class="num"><b><%=items.arrivalNewNotice%></b></em></span>到货通知</a></li><li><a href="#"><span class="tips">新通知<em class="num"><b><%=items.reduceNewNotice%></b></em></span>降价提醒</a></li></ul>',
	quickPop = quickShell.find('#quick_links_pop'),
	quickDataFns = {
	    ////购物信息
	    //message_list: {
	    //    title: '购物车',
	    //    content: '<div class="ibar_plugin_content"><div class="ibar_cart_group ibar_cart_product"><ul><li class="cart_item clearfix"><div class="cart_item_pic"><a href="#"><img src="#" /></a></div><div class="cart_item_desc"><a href="#" class="cart_item_name">1.随机内舱家庭房(部分景观有遮挡)</a><div class="cart_item_sku"><span>可住：2,3人</span></div><div class="cart_item_price"><a class="btn btn-xs red pull-right">删除</a></div></div>	</li><li class="cart_item clearfix"><div class="cart_item_pic"><a href="#"><img src="#" /></a></div><div class="cart_item_desc"><a href="#" class="cart_item_name">1.随机内舱家庭房(部分景观有遮挡)</a><div class="cart_item_sku"><span>可住：2,3人</span></div><div class="cart_item_price"><a class="btn btn-xs red pull-right">删除</a></div></div>	</li><li class="cart_item clearfix"><div class="cart_item_pic"><a href="#"><img src="#" /></a></div><div class="cart_item_desc"><a href="#" class="cart_item_name">1.随机内舱家庭房(部分景观有遮挡)</a><div class="cart_item_sku"><span>可住：2,3人</span></div><div class="cart_item_price"><a class="btn btn-xs red pull-right">删除</a></div></div>	</li><li class="cart_item clearfix"><div class="cart_item_pic"><a href="#"><img src="#" /></a></div><div class="cart_item_desc"><a href="#" class="cart_item_name">1.随机内舱家庭房(部分景观有遮挡)</a><div class="cart_item_sku"><span>可住：2,3人</span></div><div class="cart_item_price"><a class="btn btn-xs red pull-right">删除</a></div></div>	</li><li class="cart_item clearfix"><div class="cart_item_pic"><a href="#"><img src="#" /></a></div><div class="cart_item_desc"><a href="#" class="cart_item_name">1.随机内舱家庭房(部分景观有遮挡)</a><div class="cart_item_sku"><span>可住：2,3人</span></div><div class="cart_item_price"><a class="btn btn-xs red pull-right">删除</a></div></div>	</li><li class="cart_item clearfix"><div class="cart_item_pic"><a href="#"><img src="#" /></a></div><div class="cart_item_desc"><a href="#" class="cart_item_name">1.随机内舱家庭房(部分景观有遮挡)</a><div class="cart_item_sku"><span>可住：2,3人</span></div><div class="cart_item_price"><a class="btn btn-xs red pull-right">删除</a></div></div>	</li><li class="cart_item clearfix"><div class="cart_item_pic"><a href="#"><img src="#" /></a></div><div class="cart_item_desc"><a href="#" class="cart_item_name">1.随机内舱家庭房(部分景观有遮挡)</a><div class="cart_item_sku"><span>可住：2,3人</span></div><div class="cart_item_price"><a class="btn btn-xs red pull-right">删除</a></div></div>	</li><li class="cart_item clearfix"><div class="cart_item_pic"><a href="#"><img src="#" /></a></div><div class="cart_item_desc"><a href="#" class="cart_item_name">1.随机内舱家庭房(部分景观有遮挡)</a><div class="cart_item_sku"><span>可住：2,3人</span></div><div class="cart_item_price"><a class="btn btn-xs red pull-right">删除</a></div></div>	</li></ul></div><div class="cart_handler"><a href="#" class="btn green btn-block">继续选舱</a><a href="#" class="btn red btn-block">提交订单</a></div></div>',
	    //    init: $.noop
	    //},

	    //用户中心
	    history_list: {
	        title: '用户中心',
	        content: '<div class="ibar_plugin_content"><div class="avatar_box"><p class="avatar_imgbox"><img src="/Images/4.png"></p><ul class="user_info"><li class="user_name">XXX</li><li>普通会员</li><li class="user_company">杭州灰企鹅邮轮票务代理有限公司</li></ul></div><div class="user_function"><a class="btn red-haze btn-block btn-lg">用户管理</a><a class="btn yellow-crusta btn-block btn-lg">公司信息</a><a class="btn blue btn-block btn-lg">合同下载</a><a class="btn green btn-block btn-lg">修改密码</a></div></div>',
	        init: $.noop
	    },
	    //消息中心
	    mpbtn_wdsc: {
	        title: '消息中心',
	        content: '<div class="ibar_plugin_content"><div class="ibar_cart_group ibar_cart_product"><ul><li class="cart_item clearfix user_news"><label class="label label-info">2015-04-25</label> <span>灰企鹅邮轮同业平台更新啦更新啦更新啦更新啦</span></li></ul></div></div>',
	        init: $.noop
	    },
        //客服中心
	    mpbtn_recharge: {
	        title: '客服中心',
	        content: '<div class="ibar_plugin_content"><div class="ia-head-list kefu_tel"><i class="icon-call-in"></i><span class="kefu_num">400-711-1255</span></div><div class="ga-expiredsoon"><div class="es-head">客服</div><div class="ia-none">XXX：<a href="http://wpa.qq.com/msgrdv=3&amp;uin=2853221639&amp;site=qq&amp;menu=yes" target="_blank"><img border="0" src="http://wpa.qq.com/pa?p=2:2853221639:41" alt="点击这里给我发消息" title="点击这里给我发消息"></a></div></div><div class="ga-expiredsoon"><div class="es-head">供应商合作</div><div class="ia-none">飞飞：<a href="http://wpa.qq.com/msgrdv=3&amp;uin=2853221639&amp;site=qq&amp;menu=yes" target="_blank"><img src="http://wpa.qq.com/pa?p=2:2853221639:41" style="vertical-align: middle;" alt="点此联系我"></a></div></div><div class="ga-expiredsoon"><div class="es-head">投诉</div><div class="ia-none">小蒋：<a href="http://wpa.qq.com/msgrdv=3&amp;uin=397273386&amp;site=qq&amp;menu=yes" target="_blank"><img src="http://wpa.qq.com/pa?p=2:397273386:41" style="vertical-align: middle;" alt="点此联系我"></a></div><hr class="margin-top-40"/><img src="/Images/Weixin/1.jpg" class="img-responsive" /></div>',
	        init: $.noop
	    }
	};

    //showQuickPop
    var
	prevPopType,
	prevTrigger,
	doc = $(document),
	popDisplayed = false,
	hideQuickPop = function () {
	    if (prevTrigger) {
	        prevTrigger.removeClass('current');
	    }
	    popDisplayed = false;
	    prevPopType = '';
	    quickPop.hide();
	    quickPop.animate({ right: -240, queue: true });
	},
	showQuickPop = function (type) {
	    if (quickPopXHR && quickPopXHR.abort) {
	        quickPopXHR.abort();
	    }
	    if (type !== prevPopType) {
	        var fn = quickDataFns[type];
	        quickPop.html(ds.tmpl(popTmpl, fn));
	        fn.init.call(this, fn);
	    }
	    doc.unbind('click.quick_links').one('click.quick_links', hideQuickPop);

	    quickPop[0].className = 'quick_links_pop quick_' + type;
	    popDisplayed = true;
	    prevPopType = type;
	    quickPop.show();
	    quickPop.animate({ right: 40, queue: true });
	};
    quickShell.bind('click.quick_links', function (e) {
        e.stopPropagation();
    });
    quickPop.delegate('a.ibar_closebtn', 'click', function () {
        quickPop.hide();
        quickPop.animate({ right: -240, queue: true });
        if (prevTrigger) {
            prevTrigger.removeClass('current');
        }
    });

    //通用事件处理
    var
	view = $(window),
	quickLinkCollapsed = !!ds.getCookie('ql_collapse'),
	getHandlerType = function (className) {
	    return className.replace(/current/g, '').replace(/\s+/, '');
	},
	showPopFn = function () {
	    var type = getHandlerType(this.className);
	    if (popDisplayed && type === prevPopType) {
	        return hideQuickPop();
	    }
	    showQuickPop(this.className);
	    if (prevTrigger) {
	        prevTrigger.removeClass('current');
	    }
	    prevTrigger = $(this).addClass('current');
	},
	quickHandlers = {
	    //购物车，最近浏览，商品咨询
	    my_qlinks: showPopFn,
	    message_list: showPopFn,
	    history_list: showPopFn,
	    mpbtn_recharge: showPopFn,
	    mpbtn_wdsc: showPopFn,
	    //返回顶部
	    return_top: function () {
	        ds.scrollTo(0, 0);
	        hideReturnTop();
	    }
	};
    quickShell.delegate('a', 'click', function (e) {
        var type = getHandlerType(this.className);
        if (type && quickHandlers[type]) {
            quickHandlers[type].call(this);
            e.preventDefault();
        }
    });


    //Return top
    var scrollTimer, resizeTimer, minWidth = 1350;

    function resizeHandler() {
        clearTimeout(scrollTimer);
        scrollTimer = setTimeout(checkScroll, 160);
    }

    function checkResize() {
        quickShell[view.width() > 1340 ? 'removeClass' : 'addClass']('quick_links_dockright');
    }
    function scrollHandler() {
        clearTimeout(resizeTimer);
        resizeTimer = setTimeout(checkResize, 160);
    }
    function checkScroll() {
        view.scrollTop() > 100 ? showReturnTop() : hideReturnTop();
    }
    function showReturnTop() {
        quickPanel.addClass('quick_links_allow_gotop');
    }
    function hideReturnTop() {
        quickPanel.removeClass('quick_links_allow_gotop');
    }
    view.bind('scroll.go_top', resizeHandler).bind('resize.quick_links', scrollHandler);
    quickLinkCollapsed && quickShell.addClass('quick_links_min');
    resizeHandler();
    scrollHandler();
});