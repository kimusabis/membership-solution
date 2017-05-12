$(function () {
    // Wire up the hover event
    var loginLinkHover = $("#loginLink").hover(onLoginLinkHover);

    function onLoginLinkHover() {
        $('div[data-login-user-area]').addClass('open');
    }

    // wire event to the click event
    var loginCloseButton = $("#close-login").click(onCloseLogin);

    function onCloseLogin() {
        $('div[data-login-user-area]').removeClass('open');
    }

    // login button events
    var loginButton = $("#login-button").click(onLoginClick);

    function onLoginClick() {
        var url = $('#login-button').attr('data-login-action');
        var return_url = $('#login-button').attr('data-login-return-url');
        var email = $('#Email').val();
        var pwd = $('#Password').val();
        var remember_me = $('#RememberMe').prop('checked');
        var antifogery = $('[name="__RequestVerificationToken"]').val();

        $.post(url, { __RequestVerificationToken: antifogery, email: email, password: pwd, RememberMe: remember_me },
            function (data) {
                var parsed = $.parseHTML(data);
                hasErrors = $(parsed).find("[data-valmsg-summary]").text().replace(/\n|'r/g).length > 0;

                if (hasErrors == true) {
                    $('div[data-login-panel-partial]').html(data);
                    $('div[data-login-user-area]').addClass('open');
                    $('#Email').addClass("data-login-error");
                    $('#Password').addClass("data-login-error");
                    alert('Invalid login attempt.');
                } else {
                    $('div[data-login-user-area]').removeClass('open');
                    $('#Email').removeClass("data-login-error");
                    $('#Password').removeClass("data-login-error");
                    location.href = return_url;
                }
                // wire up events
                loginButton = $("#login-button").click(onLoginClick);
                loginLinkHover = $("#loginLink").hover(onLoginLinkHover);
                loginCloseButton = $("#close-login").click(onCloseLogin);
            }).fail(function (xhr, status, error) {
                $('#Email').addClass("data-login-error");
                $('#Password').addClass("data-login-error");
                // wire up events
                loginButton = $("#login-button").click(onLoginClick);
                loginLinkHover = $("#loginLink").hover(onLoginLinkHover);
                loginCloseButton = $("#close-login").click(onCloseLogin);
            });
    }
});