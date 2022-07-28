module ConfigAjax {
    var getUrl = window.location;
    var baseUrl = getUrl.protocol + "//" + getUrl.host + "/" + getUrl.pathname.split('/')[1];//lets just get the url from browser

    export class ConfigAPI {

        public getConfig = (onSuccess: Function, onError?: Function): void => {
            try {
                $.ajax(
                    {
                        type: "GET",
                        url: baseUrl + "/GetConfigList",
                        async: true,
                        data: {},
                        success: function (result)//this will be a response stating success or whatever future adam decided to return...
                        {
                            onSuccess(result); //send it back!
                        },
                        error: function (result) //this will be a error message but lets just hope it never comes to this...
                        {
                            onError(result);//send it back!
                        }
                    }

                ).responseJSON;
            } catch (e) {
                console.log(e);//our last line of defence against those pesky mistakes!
            }
        }
    }
}