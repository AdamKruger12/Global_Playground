//some global properties
var company = "";
$("#input-field").bind("change keyup input focus", function () {
    var value = $(this).val().toString().replace(/[^0-9\.]/g, '');
    //primitive validation for the card here.
    if (value.length > 12) {
        var response = checkCardNumber(value);
        if (!response.success) {
            $(".field-submit").prop("disabled", true);
            $(".error")[0].innerHTML = response.message;
            $(".img-company").attr("src", "/card_icons/card-logo-unknown.svg");
        }
        else {
            $(".field-submit").prop("disabled", false);
            $(".error")[0].innerHTML = "";
            company = response.type;
            $(".img-company").attr("src", selectImage(response.type));
        }
    }
    else {
        $(".field-submit").prop("disabled", true);
    }
});
//this changes the company logo on the digital card.
//i would use a lib or add more cards myself but lets not waste time on adding too much
function selectImage(value) {
    switch (value) {
        case "AmEx":
            return "/card_icons/card-logo-amex.svg";
        case "MasterCard":
            return "/card_icons/card-logo-mastercard.svg";
        case "Visa":
            return "/card_icons/card-logo-visa.svg";
        default:
            return "/card_icons/card-logo-unknown.svg";
    }
}
//what to do when the button is clicked.
$(".field-submit").on("click", function () {
    var value = $("#input-field").val().toString().replace(/[^0-9\.]/g, '');
    var save = new Ajax.API;
    save.saveCard(value, company, onSuccess, onError);
});
//our callback if the save was successful
function onSuccess(result) {
    var jsonObject = JSON.parse(result);
    if (jsonObject.responseCode == "00") {
        alert(jsonObject.message);
    }
    else {
        alert(jsonObject.message);
    }
}
//our callback if we made a mistake
function onError(resut) { }
//implementing numeric validation here
function validated(number) {
    //as per luhn's algorithm check if value is numeric and between 13 to 19 degits.
    var regex = new RegExp("^[0-9]{13,19}$");
    if (!regex.test(number)) {
        return false;
    }
    return luhnCheck(number);
}
//implementing luhn's algorithm here.
var luhnCheck = function (val) {
    var checksum = 0;
    var j = 1;
    //lets loop starting from the end.
    for (var i = val.length - 1; i >= 0; i--) {
        var calc = 0;
        calc = Number(val.charAt(j)) * j; //calc will be a multiple of i and (1 or 2) on alternating digits. If that makes sense...
        if (calc > 9) {
            checksum = checksum + 1; //if true add 1 to checksum to be used further on.
            calc = calc - 10;
        }
        checksum = checksum + calc; //add calucalted units to checksum total. this will be used to check validity of card number.
        if (j == 1) {
            j = 2;
        }
        else {
            j = 1;
        }
    }
    //Check if it is divisible by 10 or not.
    return (checksum % 10) == 0;
};
var checkCardNumber = function (cardNumber) {
    //following are card types and data found on the internet FYI.
    //Error messages
    var ccErrors = [];
    ccErrors[0] = "Unknown card type";
    ccErrors[1] = "No card number provided";
    ccErrors[2] = "Credit card number is in invalid format";
    ccErrors[3] = "Credit card number is invalid";
    ccErrors[4] = "Credit card number has an inappropriate number of digits";
    ccErrors[5] = "Warning! This credit card number is associated with a scam attempt";
    //Response format
    var response = function (success, message, type) {
        if (message === void 0) { message = null; }
        if (type === void 0) { type = null; }
        return ({
            message: message,
            success: success,
            type: type
        });
    };
    // Define the cards we support. You may add additional card types as follows.
    //  Name:         As in the selection box of the form - must be same as user's
    //  Length:       List of possible valid lengths of the card number for the card
    //  prefixes:     List of possible prefixes for the card
    //  checkdigit:   Boolean to say whether there is a check digit
    var cards = [];
    cards[0] = {
        name: "Visa",
        length: "13,16",
        prefixes: "4",
        checkdigit: true
    };
    cards[1] = {
        name: "MasterCard",
        length: "16",
        prefixes: "51,52,53,54,55",
        checkdigit: true
    };
    cards[2] = {
        name: "DinersClub",
        length: "14,16",
        prefixes: "36,38,54,55",
        checkdigit: true
    };
    cards[3] = {
        name: "CarteBlanche",
        length: "14",
        prefixes: "300,301,302,303,304,305",
        checkdigit: true
    };
    cards[4] = {
        name: "AmEx",
        length: "15",
        prefixes: "34,37",
        checkdigit: true
    };
    cards[5] = {
        name: "Discover",
        length: "16",
        prefixes: "6011,622,64,65",
        checkdigit: true
    };
    cards[6] = {
        name: "JCB",
        length: "16",
        prefixes: "35",
        checkdigit: true
    };
    cards[7] = {
        name: "enRoute",
        length: "15",
        prefixes: "2014,2149",
        checkdigit: true
    };
    cards[8] = {
        name: "Solo",
        length: "16,18,19",
        prefixes: "6334,6767",
        checkdigit: true
    };
    cards[9] = {
        name: "Switch",
        length: "16,18,19",
        prefixes: "4903,4905,4911,4936,564182,633110,6333,6759",
        checkdigit: true
    };
    cards[10] = {
        name: "Maestro",
        length: "12,13,14,15,16,18,19",
        prefixes: "5018,5020,5038,6304,6759,6761,6762,6763",
        checkdigit: true
    };
    cards[11] = {
        name: "VisaElectron",
        length: "16",
        prefixes: "4026,417500,4508,4844,4913,4917",
        checkdigit: true
    };
    cards[12] = {
        name: "LaserCard",
        length: "16,17,18,19",
        prefixes: "6304,6706,6771,6709",
        checkdigit: true
    };
    if (cardNumber.length == 0) {
        return response(false, ccErrors[1]); // why array? because its easy on the RAM
    }
    cardNumber = cardNumber.replace(/\s/g, "");
    //lets luhn it out!
    if (!validated(cardNumber)) {
        return response(false, ccErrors[2]);
    }
    //internet has some spam cards i can add here but i think i will include that in the config screen.
    //lets partake in some card checks...
    var lengthValid = false;
    var prefixValid = false;
    var company = "";
    for (var i = 0; i < cards.length; i++) {
        var prefix = cards[i].prefixes.split(",");
        for (var j = 0; j < prefix.length; j++) {
            var exp = new RegExp("^" + prefix[j]);
            if (exp.test(cardNumber)) {
                prefixValid = true;
            }
        }
        if (prefixValid) {
            var length_1 = cards[i].length.split(",");
            for (var j = 0; j < length_1.length; j++) {
                if (cardNumber.length == length_1[j]) {
                    lengthValid = true;
                }
            }
        }
        if (lengthValid && prefixValid) {
            company = cards[i].name;
            return response(true, null, company);
        }
    }
    // If it isn't a valid prefix there's no point at looking at the length
    if (!prefixValid) {
        return response(false, ccErrors[3]);
    }
    // See if all is OK by seeing if the length was valid
    if (!lengthValid) {
        return response(false, ccErrors[4]);
    }
    ;
    // The credit card is in the required format.
    return response(true, null, company);
};
//# sourceMappingURL=validation.js.map