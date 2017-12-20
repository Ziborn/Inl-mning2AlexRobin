$("#loginForm button").click(function () {
    $.ajax({
        url: 'check/login/' + $("#loginForm [name=Login]").val(),
        method: 'POST'
    })
        .done(function (result) {
            console.log("Success!", result);
        })
        .fail(function (xhr, status, error) {
            console.log("Fail", xhr);
        });
});

function addClaim() {
    $.ajax({
        url: 'check/addClaim',
        method: 'GET'
    })
        .done(function () {
            console.log("Claim success!");
        })
        .fail(function (xhr, status, error) {
            console.log("Fail", xhr);
        });
}

$("#addUsers").click(function () {
    $.ajax({
        url: "check/add",
        method: 'GET'
    })
        .done(function (result) {
            console.log("Success!", result);
            addClaim();
        })
        .fail(function (xhr, status, error) {
            console.log("Fail", xhr);
        });
});

$("#openNews").click(function () {
    $.ajax({
        url: "check/OpenNews",
        method: 'GET'
    })
        .done(function () {
            console.log("Success!");
        })
        .fail(function (xhr, status, error) {
            console.log("Fail");
        });
});

$("#hiddenNews").click(function () {
    $.ajax({
        url: "check/check",
        method: 'GET'
    })
        .done(function () {
            console.log("Success!");
        })
        .fail(function (xhr, status, error) {
            console.log("Fail");
        });
});

$("#minAgeNews").click(function () {
    $.ajax({
        url: "check/MinAgeNews",
        method: 'GET'
    })
        .done(function (result) {
            console.log("Success! ", result);
        })
        .fail(function (xhr, status, error) {
            console.log("Fail, get older dirty boy!");
        });
});