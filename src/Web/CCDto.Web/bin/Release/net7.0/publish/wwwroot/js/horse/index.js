/* ==========================================================
 * horse.js v20140729
 * ==========================================================
 * Copyright 黄笑雯
 *
 * 跑马灯效果原生ＪＳ
 * ========================================================== */
$(function() { 
  var ui = {
    	$btnLottery: $('#btn-lottery')
    , $carousel2: $('[data-toggle=carousel2]')
    , $prizes: $('.prizebg')
  };

  var oConfig = window.oPageConfig;

  var prizeArr = [0,1,2,4,7,6,5,3]; // 奖品数组
  var prizeNum = 0;

  var oPage = {
    /**
     * 初始化
     */
    init: function() {
      this.view();
      this.listen();
    }
    /**
     * 视图显示
     */
  , view: function() {
      // 中奖播报
      ui.$carousel2.carousel2({bSetInterval: true, limit: 5});
    }
    /**
     * 绑定监听事件
     */
  , listen: function() {
      var self = this;

      // 鼠标点击按钮抽奖效果
	  	ui.$btnLottery.on('click',function(){	
        $.ajax({
          url: oConfig.oUrl.send
        , type: 'POST'
        , data: {id: 1}
        , dataType: 'json'
        }).done(function(msg) {
          if(msg.code === 0) {
            prizeNum = msg.data;
          }
        });     
        // 调用跑马灯效果
        lightChange(ui.$prizes, "active", prizeArr, prizeNum);     

	  	});
     
      
   }

  };

  oPage.init();
});