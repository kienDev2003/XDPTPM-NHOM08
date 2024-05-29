$(document).ready(function() {
    var resizeCheck = "small";
    var main_w = $(".has-sidebar").width();
    $('.zoom').click(function() {
        if (resizeCheck == "small") {
            $(".zoom").html("<i class=\"fa fa-compress\"></i> Zoom-");
            $(".has-sidebar").animate({
                "width": "100%"
            });
            $('.sidebar').css("display", "none");
            resizeCheck = "large";
        } else if (resizeCheck == "large") {
            $(".zoom").html("<i class=\"fa fa-compress\"></i> Zoom+");
            $('.has-sidebar').width(main_w);
            $('.sidebar').css("display", "inherit");
            resizeCheck = "small";
        }
    });
    var data_id = $('#video').attr('data-id');
    var server_id = $('#video').attr('data-sv');
    if (data_id) {
        server(server_id, data_id);
    }
    $("#server1").click(function() {
        $('li.bt_active').removeClass('bt_active');
        $('#server1').addClass('bt_active');
        $('#video').attr('data-sv', '1');
    });
    $("#server2").click(function() {
        $('li.bt_active').removeClass('bt_active');
        $('#server2').addClass('bt_active');
        $('#video').attr('data-sv', '2');
    });
    $('.btn-like').click(function() {
        var id = $(this).data('id');
        $('span.vote-count').removeClass('fa-thumbs-o-up');
        $('span.vote-count').addClass('fa-thumbs-up');
        if (id) {
            $.post(ajaxurl, {
                like_post: 1,
                id: id
            }, function(data) {
                if (data == 0)
                    data = '1';
                $('.vote-count').html(data);
            });
        }
    });
    function activeTab(obj) {
        $('.change-tab ul li').removeClass('l_active');
        $(obj).addClass('l_active');
        var id = $(obj).find('a').attr('href');
        $('.popular-post').hide();
        $(id).show();
    }
    $('.dp-popular-tab li').click(function() {
        activeTab(this);
        return false;
    });
    activeTab($('.dp-popular-tab li:first-child'));
    $(".button-menu").click(function() {
        if (!$('.button-menu').hasClass('active') || !$('.body.row').hasClass('blur')) {
            $(".body.row").addClass("blur");
        } else {
            $(".body.row").removeClass("blur");
        }
        $(this).toggleClass("active");
        $(".list-menu").toggleClass("active");
    });
    $(".button-search").click(function() {
        if (!$('.button-search').hasClass('active') || !$('.body.row').hasClass('blur')) {
            $(".body.row").addClass("blur");
        } else {
            $(".body.row").removeClass("blur");
        }
        $(this).toggleClass("active");
        $(".form-search").toggleClass("active");
    });
    $(".list-menu, .button-menu, .form-search, .button-search, .sub-menu>li").click(function(e) {
        e.stopPropagation();
    });
    $('html, .button-search, .form-search').click(function() {
        if ($('.button-menu').hasClass('active') && !$('.button-search').hasClass('active'))
            $(".body.row").removeClass("blur");
        $('.list-menu').removeClass('active');
        $('.button-menu').removeClass('active');
    });
    $('html, .button-menu, .list-menu').click(function() {
        if ($('.button-search').hasClass('active') && !$('.button-menu').hasClass('active'))
            $(".body.row").removeClass("blur");
        $('.form-search').removeClass('active');
        $('.button-search').removeClass('active');
    });
    $("li.item-menu").click(function() {
        $(this).toggleClass("active");
    });
    $('.item-content-toggle').click(function() {
        if ($(this).find('.show-more').html() == 'Xem thêm')
            $(this).find('.show-more').html('Thu gọn');
        else
            $(this).find('.show-more').html('Xem thêm');
        $('article').toggleClass('toggled');
    });
});
var reloadedCount = {};
function reloadCurrentserver() {
    var data_id = $('#video').attr('data-id');
    var server_id = $('#video').attr('data-sv');
    if (data_id) {
        server(server_id, data_id);
    }
}
function del_cache() {
    var data_id = $('#video').attr('data-id');
    var server_id = $('#video').attr('data-sv');
    $.post(ajaxurl, {
        delcache: 1,
        server: server_id,
        videoid: data_id
    });
}
function errorHandler() {
    var time = 2;
    var data_id = $('#video').attr('data-id');
    var server_id = $('#video').attr('data-sv');
    if (typeof reloadedCount[server_id] == "undefined") {
        reloadedCount[server_id] = 1;
    }
    if (reloadedCount[server_id] < time) {
        setTimeout(function() {
            reloadCurrentserver();
        }, 100);
        reloadedCount[server_id]++;
    } else {
        $.post(ajaxurl, {
            reloadError: 1,
            server: server_id,
            videoid: data_id
        });
        $('#video .video-player').html("<p style='background:#333; color: #fff; text-align:center; padding: 10px;'>Server này là lỗi và tự động tải lại trong " + time + " lần." + "<br />Vui lòng chọn một server #2 để xem. </p>");
    }
}
var cookie_notice = !1
  , error_thispage = false;
function server(server, id) {
    var id = parseInt(id);
    var server = parseInt(server);
    $.post(ajaxurl, {
        vlxx_server: 1,
        id: id,
        server: server
    }, function(data) {
        var jsons = JSON.parse(data);
        $('#video').html(jsons.player);
        $('.download-button').html(jsons.download);
        if (server == 1) {
            $('#server1').addClass('bt_active');
        }
    });
    return false;
}
