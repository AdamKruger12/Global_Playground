//this is just to add the card look and feel to the screen
$(".card-wrapper").append($("<div/>", { class: "credit-card-outer" }).append($("<div/>", { class: "credit-card-company" }).append($("<img/>", { class: "img-company", src: "/card_icons/card-logo-unknown.svg" })), $("<div/>", { class: "credit-card-number credit-card-font" }), $("<div/>", { class: "credit-card-date-prefix", html: "Expires End" }), $("<div/>", { class: "credit-card-date credit-card-font", html: "01/18" }), $("<div/>", { class: "credit-card-person credit-card-font", html: "MR JOHN SMITH" })));
//this is a little listener to take the card input and add it to the screen
$("#input-field").bind("change keyup input focus", function () {
    var value = $(this).val().toString().replace(/[^0-9\.]/g, '');
    $(".credit-card-number").html(cc_format(value));
});
//stackoverflow ftw! this adds some formatting to the card number to look like you would expect.
function cc_format(value) {
    var v = value.replace(/\s+/g, '').replace(/[^0-9]/gi, '');
    var matches = v.match(/\d{4,16}/g);
    var match = matches && matches[0] || '';
    var parts = [];
    for (var i = 0, len = match.length; i < len; i += 4) {
        parts.push(match.substring(i, i + 4));
    }
    if (parts.length) {
        return parts.join(' ');
    }
    else {
        return value;
    }
}
//# sourceMappingURL=card.js.map