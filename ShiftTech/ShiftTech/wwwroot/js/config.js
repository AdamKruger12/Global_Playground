getConfig();
function getConfig() {
    var configAjax = new ConfigAjax.ConfigAPI;
    return configAjax.getConfig(onConfigSuccess, onFailure);
}
//TODO:make this editable and look nice
function onConfigSuccess(result) {
    var jsonObjects = JSON.parse(result).companies;
    for (var i = 0; i < jsonObjects.length; i++) {
        $("#config-field").append($("<div/>", { class: ".config-field" })[0].innerHTML = jsonObjects[i].name);
    }
}
function onFailure(result) {
    alert(result[0]);
}
$("body").append($("<div/>").append($('<input type="button" value="Add" />').on("click", function () {
    alert("Pressed!");
})));
//# sourceMappingURL=config.js.map