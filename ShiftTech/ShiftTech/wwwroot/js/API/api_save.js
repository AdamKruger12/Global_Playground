var Ajax;
(function (Ajax) {
    var getUrl = window.location;
    var baseUrl = getUrl.protocol + "//" + getUrl.host + "/" + getUrl.pathname.split('/')[1]; //lets just get the url from browser
    //save card ajax function. Response will be success or error.
    var API = /** @class */ (function () {
        function API() {
            this.saveCard = function (number, company, onSuccess, onError) {
                try {
                    $.ajax({
                        type: "POST",
                        url: baseUrl + "home/SaveCard",
                        async: true,
                        data: {
                            number: number,
                            company: company
                        },
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
        return API;
    }());
    Ajax.API = API;
})(Ajax || (Ajax = {}));
//# sourceMappingURL=api_save.js.map