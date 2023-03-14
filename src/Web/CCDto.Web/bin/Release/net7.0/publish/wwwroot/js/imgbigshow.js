(function($) {
            $.fn.preview = function() {
                $(this).each(function() {
                    $(this).hover(function(e) {
                        if (/.png$|.gif$|.jpg$|.bmp$/.test($(this).find("img").attr("src"))) {
                            $("body").append("<div id='preview'><img src='" + $(this).find("img").attr('src') + "' /><p>"  + "</p></div>");
                        } else {
                            $("body").append("<div id='preview'><p>"  + "</p></div>");
                        }
                        $("#preview").css({
                            position: "absolute",
                            top: "0px",
                            width: "500px",
                            height:"300px",
                            zIndex: 1000,
                            border: "1px solid #e7ecf1"
                        });
                        $("#preview > img").css({
                            width: "100%",height:"100%"
                        });
                        $("#preview > div > p").css({
                            textAlign: "center",
                            fontSize: "12px",
                            padding: "8px 0 3px",
                            margin: "0"
                        });

                    }, function() {
                        $("#preview").remove();
                    }).mousemove(function (e) {
                        var left = $(this).offset().left;
                        var top = $(this).offset().top;
                        var width = $(this).find("img").width();
                        var trueleft = left + width +15;
                        $("#preview").css("top",top+ "px");
                        $("#preview").css("left", trueleft + "px").css("right", "auto");
                        
                    });
                });
            };
        })(jQuery);