var carousel2 = function (_this ,opt) {
     this.bNextPrev = opt.bNextPrev;
     this.$this = $(_this);
     this.event = opt.event;
     this.tr = opt.tr;
     this.limit = opt.limit;
     this.animateTime = opt.animateTime;
     this.setIntervalTime = opt.setIntervalTime;
     this.bSetInterval = opt.bSetInterval;
     this.direction = opt.direction;
     this.table = opt.table;
     this.index = 0;
     this.init();
}

carousel2.prototype = {
  constructor: carousel2
, nTimer: 0
, init: function(){
    var self = this;
        self.$table =self.$this.find(self.table);
        self.$trs = self.$this.find(self.tr);
        self.nTr = self.$trs.length;
        // console.log(self.nTr);
        if(self.nTr <= self.limit){             //如果当前条数小于显示的最大条数所有就不用轮播
            return false;
        }
        self.$trs.clone().insertAfter(self.$trs.slice(-1));
    if (self.direction == 'vertical'){
        self.margin = 'marginTop';
        self.nTrHeight = self.$trs.eq(0).height();
    }else{
        self.margin = 'marginLeft';
        self.nTrHeight = self.$trs.eq(0).width();
    }
    if(self.bNextPrev) {
        self.$this.find('[data-next], .next').length || self.$this.append(' <a href="" class="next" data-next="carousel2">next</a>');
        self.$this.find('[data-prev], .prev').length || self.$this.append(' <a href="" class="prev" data-prev="carousel2">prev</a>');
    }
    self.$next = self.$this.find('.next, [data-next]');
    self.$prev = self.$this.find('.prev, [data=prev]');
    self.bindEvent();
  }
, bindEvent: function(){
    var self = this;
     if(self.bSetInterval){
         self.timer = setInterval(function(){
             self.fNext();
         }, self.setIntervalTime);
         self.$this.on('mouseenter', function () {
             clearInterval(self.timer);
         });
         self.$this.on('mouseleave', function () {
             self.timer = setInterval(function(){
                 self.fNext();
             }, self.setIntervalTime);
         });
     }
    if(self.bNextPrev){
        self.$next.on(self.event,function(){
            self.fNext();
            return false;
        });
        self.$prev.on(self.event,function(){
            self.fPrev();
            return false;
        });
    }
  }
, fNext: function(){
        var self = this;
        self.index++;
        if (self.index >= self.nTr) {
            // console.log(self.index);
            self.fAnimate(function(){
                self.$table.css(self.margin, 0);
                self.index = 0;
            })
        } else {
            self.fAnimate();
        }
    return false;
  }
, fPrev: function(){
        var self = this;
        self.index--;
        if (self.index < 0) {
            self.index += self.nTr;
            self.$table.css(self.margin, self.nTrHeight * self.nTr);
            self.fAnimate(function(){

            })
        } else {
            self. fAnimate();
        }
    }
, fAnimate: function(callback){
    var self = this;
        var oAnimate={};
       if(self.margin == 'marginTop'){
           oAnimate ={marginTop : -(self.index * self.nTrHeight)};
       }else{
           oAnimate ={marginLeft : -(self.index * self.nTrHeight)};
       }
        self.$table.animate(oAnimate
        ,300,'linear',function(){
            // console.log(111);
            callback && callback();
        })
  }
}
$.fn.carousel2 = function (opt) {
  return this.each(function () {
      var $this = $(this);
      var opts = $.extend({}, $.fn.carousel2.defaults, opt);
      var data = $this.data('carousel2');
      if(!data){$this.data('carousel2',(data=new carousel2(this,opts)))}
  });
};
$.fn.carousel2.defaults ={
    bNextPrev: false,
    bSetInterval: true,
    event: 'click tap',
    table: 'table',
    tr: 'tr',
    limit: 10,
    setIntervalTime: 2000,
    animateTime: 400,
    direction: 'vertical'
};
$.fn.carousel2.Constructor = carousel2;
