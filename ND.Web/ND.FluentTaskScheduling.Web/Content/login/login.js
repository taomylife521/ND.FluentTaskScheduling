$(function () {
   // var working = false;
    $("body").keydown(function() {
        if (event.keyCode == "13") {//keyCode=13是回车键
            $('#btnLogin').trigger("click");
        }
    }); 

    $('#btnLogin').on('click', function (e) {
        var $this = $(".login");
        var $state = $this.find('button > .state');
        var username = $("#txtUserName").val();
        var password = $("#txtPassword").val();
        if (username.length <= 0 || username == undefined)
        {
            swal("登录失败","用户名不能为空", "error");
            return;
        }
        if (password.length <=0 || password == undefined)
        {
            swal("登录失败", "密码不能为空", "error");
            return;
        }
        password = md5(password);
       
        $this.addClass('loading');
        $state.html('登录中');
        $.ajax({
            url: 'Index',
            type: "post",
            data: {
                username: username,
                password: password.toUpperCase()

            },
            success: function (data) {
                if (parseInt(data.Status) != 1)
                {
                    setTimeout(function () {
                        $this.addClass('error');
                        $state.html("登录失败:" + data.Msg);
                        setTimeout(function () {
                            $this.removeClass('ok loading');
                            $state.html('Log in');
                        }, 2000);
                    }, 1000);
                    return;
                }
                setTimeout(function () {
                    $this.addClass('ok');
                    $state.html('欢迎回来!');
                    setTimeout(function () {
                        location.href = "../Home/Index";
                    }, 1000);
                }, 500);
               
               
            },
            error: function (XMLHttpRequest, textStatus, errorThrown)
            {
                setTimeout(function () {
                    $this.addClass('error');
                    $state.html("登录失败:"+errorThrown);
                    setTimeout(function () {
                        $this.removeClass('ok loading');
                        $state.html('Log in');
                    }, 2000);
                }, 1000);
               
               
            }
        });
        //e.preventDefault();
        //if (working) return;
        //working = true;
        //var $this = $(this),
        //  $state = $this.find('button > .state');
        //$this.addClass('loading');
        //$state.html('Authenticating');
        //setTimeout(function () {
        //    $this.addClass('ok');
        //    $state.html('Welcome back!');
        //    setTimeout(function () {
        //        $state.html('Log in');
        //        $this.removeClass('ok loading');
        //        working = false;
        //    }, 4000);
        //}, 3000);
    });
})