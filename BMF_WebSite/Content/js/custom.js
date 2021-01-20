(function ($) {

    "use strict";

    //preloader part js
    $(window).on('load', function () {
        $('.main-preloader').delay(1000).fadeOut(1000);
    });
    // countdown timer js 
    $('.coundown_res').countdown('2020/12/25', function (event) {
        var $this = $(this);
        $('#day').html(event.strftime('<span>%D</span>'));
        $('#hour').html(event.strftime('<span>%H</span>'));
        $('#month').html(event.strftime('<span>%M</span>'));
        $('#second').html(event.strftime('<span>%S</span>'));
    });
    // gallery slide js
    $('.gallery-main').slick({
        slidesToShow: 5,
        slidesToScroll: 1,
        autoplay: true,
        autoplaySpeed: 3000,
        speed: 2000,
        arrows: false,
        centerMode: true,
        centerPadding: '0px',
        focusOnSelect: true,
        responsive: [
            {
                breakpoint: 576,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1,
                }
    },
            {
                breakpoint: 768,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 1,
                }
    },
            {
                breakpoint: 992,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 1,
                }
    },

  ]
    });

    // Review slide js
    $('.review-main').slick({
        slidesToShow: 2,
        slidesToScroll: 1,
        autoplay: true,
        autoplaySpeed: 3000,
        speed: 2000,
        arrows: false,
        centerMode: true,
        centerPadding: '0px',
        focusOnSelect: true,
        responsive: [
            {
                breakpoint: 576,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1,
                }
    },
            {
                breakpoint: 768,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1,
                }
    },
            {
                breakpoint: 992,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1,
                }
    },

  ]
    });
    // Blog slide js
    $('.blog-main').slick({
        slidesToShow: 3,
        slidesToScroll: 1,
        autoplay: true,
        autoplaySpeed: 3000,
        speed: 2000,
        arrows: false,
        centerMode: true,
        centerPadding: '0px',
        focusOnSelect: true,
        responsive: [
            {
                breakpoint: 576,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1,
                }
    },
            {
                breakpoint: 768,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1,
                }
    },
            {
                breakpoint: 992,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 1,
                }
    },

  ]
    });
    // scrollber js
    $(".scrollarea").niceScroll({
        cursorcolor: "#fd9696",
        cursorheight: "5px",
    });
    //smooth scroll js
    $(window).scroll(function () {
        var scrolling = $(this).scrollTop();
        var sticky = $('.sticky-top');
        if (scrolling >= 100) {
            $(sticky).addClass("nav-bg");
        } else {
            $(sticky).removeClass("nav-bg");
        }
        if (scrolling >= 50) {
            $('.backtotop').fadeIn(500);
        } else {
            $('.backtotop').fadeOut(500);
        }
    });


    //animation scroll js
    var html_body = $('html, body');
    $('.navbar a , .scroll-down a , .backtotop a, .price-btn a').on('click', function () {
        if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && location.hostname == this.hostname) {
            var target = $(this.hash);
            target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
            if (target.length) {
                html_body.animate({
                    scrollTop: target.offset().top - 80
                }, 1500);
                return false;
            }
        }
    });

    // Closes responsive menu when a scroll link is clicked
    $('.nav-link').on('click', function () {
        $('.navbar-collapse').collapse('hide');
    });
    //scorllspy js
    $('body').scrollspy({
        target: ".navbar",
        offset: 100,
    });
    //lightbox js
    $('.venobox').venobox();
    // counter part js
    $('.counter').counterUp();
    // video player js
    jQuery("#bgndVideo").YTPlayer();
}(jQuery));
