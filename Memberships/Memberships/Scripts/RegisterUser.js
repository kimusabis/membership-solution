$(function () {
    var registerUserButton = $(".register-user-panel button").click(onRegisterUserClick);

    var registerUserCheckBox = $("#AcceptUserAgreement").click(onToggleRegisterUserDisabledClick);

    //User agreement Check Box
    function onToggleRegisterUserDisabledClick() {
        $(".register-user-panel button").toggleClass("disabled");
    }

    //Register User Button
    function onRegisterUserClick() {
        var url = "/Account/RegisterUserAsync";
        var antiforgery = $('[name="__RequestVerificationToken"]').val();
        var name = $('.register-user-panel .first-name').val();
        var email = $('.register-user-panel .email').val();
        var password = $('.register-user-panel .password').val();

        $.post(url, { __RequestVerificationToken: antiforgery, email: email, name: name, password: password, acceptUserAgreement: true },
            function (data) {
                var parsed = $.parseHTML(data);
                hasErrors = $(parsed).find("[data-valmsg-summary]").text().replace(/\n|\r/g).length > 0;

                if (hasErrors == true) {
                    $(".register-user-panel").html(data);

                    registerUserButton = $(".register-user-panel button").click(onRegisterUserClick);

                    registerUserCheckBox = $("#AcceptUserAgreement").click(onToggleRegisterUserDisabledClick);

                    $(".register-user-panel button").removeClass("disabled");

                }
                else {
                    //registerUserButton = $(".register-user-panel button").click(onRegisterUserClick);
                    //registerUserCheckBox = $("#AcceptUserAgreement").click(onToggleRegisterUserDisabledClick);
                    location.href = "/Home/Index";
                }
            }).fail(function (xhr, status, error) {
                // error handling code goes here
            });
    }
});