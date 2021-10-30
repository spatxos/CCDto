//文档：https://developer.mozilla.org/zh-CN/docs/Web/API/notification/Using_Web_Notifications
// Learn about this code on MDN: https://developer.mozilla.org/en-US/docs/Web/API/Notifications_API/Using_the_Notifications_API 

window.addEventListener('load', function () {
    // 首先，让我们检查我们是否有权限发出通知
    // 如果没有，我们就请求获得权限
    if (window.Notification && Notification.permission !== "granted") {
        Notification.requestPermission().then(function (result) {
            if (result === 'denied') {
                console.log('Permission wasn\'t granted. Allow a retry.');
                return;
            }
            if (result === 'default') {
                console.log('The permission request was dismissed.');
                return;
            }
            // Do something with the granted permission.
        });
    }
});

var Notify = function (title, body, tag, url) {
    try {
        if (window.Notification && Notification.permission !== "denied") {
            Notification.requestPermission(function (status) {
                if (status === 'granted') {
                    //弹出一个通知  
                    var n = new Notification(title,
                        { //标题  
                            body: body, //显示内容  
                            //以下是可选参数  
                            icon: 'http://static.ylsdai.com/images/favicon.ico',
                            //lang :  
                            onclick: function () {
                                window.open(url);
                                n.close();
                            },
                            //onclose:  
                            //onerror:  
                            //onshow:  
                            tag: tag
                        });
                    n.onclick = function () {
                        window.open(url);
                        n.close();
                    };
                    //两秒后关闭通知  
                    //setTimeout(function() {n.close();},5000);
                }
            });
        }
    } catch (e) {
        alert(e);
    }
}

