var ConfigAjax;
(function (ConfigAjax) {
    var getUrl = window.location;
    var baseUrl = getUrl.protocol + "//" + getUrl.host + "/" + getUrl.pathname.split('/')[1]; //lets just get the url from browser
    var ConfigAPI = /** @class */ (function () {
        function ConfigAPI() {
            this.getConfig = function (onSuccess, onError) {
                try {
                    $.ajax({
                        type: "GET",
                        url: baseUrl + "/GetConfigList",
                        async: true,
                        data: {},
                        success: function (result) {
                            onSuccess(result); //send it back!
                        },
                        error: function (result) {
                            onError(result); //send it back!
                        }
                    }).responseJSON;
                }
                catch (e) {
                    console.log(e); //our last line of defence against those pesky mistakes!
                }
            };
        }
        return ConfigAPI;
    }());
    ConfigAjax.ConfigAPI = ConfigAPI;
})(ConfigAjax || (ConfigAjax = {}));
//# sourceMappingURL=api_config.js.map