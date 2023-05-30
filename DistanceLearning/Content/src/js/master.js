$(function () {


    $(".main-menu-list").addClass("left");
    $(".register").slideUp();
    $(".login").slideUp();


    $(".main-menu-btn, .main-menu-list-exit-btn").on("click", function () {
        $(".main-menu-list").toggleClass("left");
        $(".main-menu-list").toggleClass("right");

        if ($(".main-menu-list").hasClass("right")) {
            $(".register").slideUp();
            $(".login").slideUp();
            $(".site").addClass("opacity");
            $("footer").addClass("opacity");

        } else if ($(".main-menu-list").hasClass("left")) {
            $(".site").removeClass("opacity");
            $("footer").removeClass("opacity");
        }
    });


    $("").on("click", function () {
        $(".main-menu-list").toggleClass("left");
        $(".main-menu-list").toggleClass("right");
        if ($(".main-menu-list").hasClass("right")) {
            $(".register").slideUp();
            $(".login").slideUp();
            $(".site").addClass("opacity");
            $("footer").addClass("opacity");

        } else if ($(".main-menu-list").hasClass("left")) {
            $(".site").removeClass("opacity");
            $("footer").removeClass("opacity");
        }
    });


    $("#register-nav-btn").on("click", function () {
        $(".register").slideToggle();

        if ($('.login').is(':visible')) {
            $(".login").slideUp();

        }
        $(".main-menu-list").toggleClass("right");
        $(".main-menu-list").addClass("left");
        $(".site").toggleClass("opacity");
        $("footer").toggleClass("opacity");
    })


    $("#register-form-exit-btn").on("click", function () {
        $(".register").slideToggle();
    })


    $("#login-nav-btn").on("click", function () {
        $(".login").slideToggle();

        if ($('.register').is(':visible')) {

            $(".register").slideUp();
        }
        $(".main-menu-list").toggleClass("right");
        $(".main-menu-list").addClass("left");
        $(".site").toggleClass("opacity");
        $("footer").toggleClass("opacity");
    })

    $("#login-form-exit-btn").on("click", function () {
        $(".login").slideToggle();
    })



    $(".list-of-courses-in-cat-list").slideUp();
    $(".cat-item-list-btn").on("click", function () {
        $(this).next("ul").slideToggle();
    })
})










