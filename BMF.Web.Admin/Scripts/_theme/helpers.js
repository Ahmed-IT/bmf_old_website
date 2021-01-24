// Ajax Loader
var $loading = $('.full-page-dim,.full-overlay,.full-overlay-loader').hide();
$(document).on({
    ajaxStart: function () {
        $loading.show();
    },
    ajaxStop: function () {
        $loading.hide();
    }
});
$(document).ajaxError(function (e, xhr) {
    if (xhr.status == 403) {
        console.log(e);
        console.log(xhr);
        location.reload();
    }
});
$(document).ready(function () {
    setTimeout(LoadAllPagePartials(), 1000);
    if (jQuery.validator != null) {
        jQuery.validator.addMethod("emptySpace", function (value, element) {
            return value.trim().length != 0;
        }, "This field cannot be blank space(s).");
    }
});
$(document).on('click', '.btn-deleteparent', function () {
    var _this = $(this), dlvl = $(this).data('dlvl');
    if (dlvl != null) { while (dlvl != 0) { _this = _this.parent(); dlvl--; } if (_this != null) { _this.remove(); } }
});
$(document).on('click', "button.refresh-partial,a.refresh-partial", function () {
    var $this = $(this);
    $this.find('i').addClass("fa-spin").delay(1500)
        .queue(function () {
            $this.find('i').removeClass("fa-spin");
            $this.find('i').dequeue();
        });
    var target = $this.data('target');
    var _target = $(target);
    _target.children().remove();
    SetLoadingDisplay(_target);
    LoadPartial(_target);
});
$(document).on('click', "button.collapse-box,a.collapse-box", function () {
    var $this = $(this);
    var target = $this.data('target');
    var _target_box = $(target);
    var box_content = _target_box.find("> .box-header, > .box-body, > .box-footer, > form  >.box-body, > form > .box-footer");
    if (!_target_box.hasClass("collapsed-box")) {
        box_content.slideUp(500, function () {
            box_content.css("box-shadow", "none");
            _target_box.addClass("collapsed-box");
        });
    } else {
        box_content.css("box-shadow", "none");
        box_content.slideDown(500, function () {
            box_content.css("box-shadow", "");
            _target_box.removeClass("collapsed-box");
        });
    }
});
$(document).on('click', ".btn-clearclosestinput", function () {
    var $this = $(this).parent(), $input = $this, C = 0;
    while (!$input.is('input')) {
        $input = $this.find('input');
        if (!$input.is('input')) { if ($input.is('body')) { return; } $this = $this.parent(); }
        else { $input.val(''); }
        if (C == 50) { return; }
        C++;
    }
});
function randomNumber(min, max) {
    return Math.floor(Math.random() * (max - min + 1) + min);
}
function LoadAllPagePartials() {
    $('.partial').each(function (k, v) {
        setTimeout(LoadPartial(v), 500, v);
    });
}
function LoadPartial(target, params, method, cache, processData, contentType, async, successFunction, errorFunction) {
    if (method == null || method == '') method = 'GET';
    if (cache == null) cache = false;
    if (async == null || async == '') async = true;
    if (processData == null || processData == '') processData = false;
    if (contentType == null || contentType == '') contentType = false;
    var u = $(target).data('u');
    if (u && u.length > 0) {
        $.ajax({
            url: u,
            type: method,
            data: params,
            async: async,
            cache: cache,
            processData: processData,
            contentType: contentType,
            dataType: "html",
            success: function (data) {
                $(target).html(data);
                if (successFunction != null && successFunction != undefined) successFunction();
            },
            error: function (data) {
                if (errorFunction != null && errorFunction != undefined) errorFunction();
            }
        });
    }
}
function SetLoadingDisplay(target, planes, box) {
    if (planes) {
        $(target).html('<div class="overlay"><div class="app-loader-animation-left color-tertiary"><i class="rightface app-loader-icon-top icon-xxl fa fa-plane"></i><i class="leftface app-loader-icon-bottom icon-xxl fa fa-plane"></i></div></div>');
    }
    else if (box) {
        $(target).html('<div class="box no-border"><div class="box-body"><div class="overlay progress progress-striped active text-center" style="overflow:visible;"><h4>Loading, please wait...</h4><div class="progress-bar" style="width: 100%"></div></div></div></div>');
    }
    else {
        $(target).html('<div class="box-body"><div class="overlay progress progress-striped active text-center" style="overflow:visible;"><h4>Loading, please wait...</h4><div class="progress-bar" style="width: 100%"></div></div></div>');
    }
}
function SetLoadingBarDisplay(target, percent) {
    if (percent == null || percent == '')
        percent = '100%';
    else
        percent = percent + '%';
    var ProgressBar = $(target).find('.progress-bar');
    if (ProgressBar.length != 0)
        $(ProgressBar).width(percent);
    else
        $(target).html('<div class="box-body"><div class="overlay progress progress-striped active text-center" style="overflow:visible;"><h4>Loading, please wait...</h4><div class="progress-bar" style="width: ' + percent + '"></div></div></div>');
}
function SetError(target, message, clear) {
    alert(message);
}