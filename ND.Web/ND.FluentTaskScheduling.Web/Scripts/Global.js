$(function () {
    var General = function () {
        var _self = this;
      
        ///获取状态描述
        _self.StatusDiscription = function (optionArr, status) {
            var result = status;
            $.each(optionArr, function (k,v) {
                if($(this).val() == status)
                {
                    result = $(this).text();
                }
            });
            return result;
        }
       
    };

    /* 通用函数实例 */
    _G = new General();
  //  _G.Init();
    
});

String.prototype.replaceAll = function (s1, s2) {
    return this.replace(new RegExp(s1, "gm"), s2);
}