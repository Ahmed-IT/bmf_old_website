$(document).ready(function () {
    setTimeout(LoadAllPagePartials(), 1000);
});

// Functions
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

function LoadAllPagePartials() {
    $('.partial').each(function (k, v) {
        setTimeout(LoadPartial(v), 500, v);
    });
}

function SetLoadingDisplay(target, box) {
    if (box) {
        $(target).html('<div class="box no-border"><div class="box-body"><div class="overlay progress progress-striped active text-center" style="overflow:visible;"><h4>Loading, please wait...</h4><div class="progress-bar" style="width: 100%"></div></div></div></div>');
    }
    else {
        $(target).html('<h6 class="text-muted text-center">Loading, please wait...</h6><div class="progress mb-3"><div class="progress-bar bg-primary progress-bar-striped progress-bar-animated w-100" role="progressbar" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100"><span class="sr-only">Loading, please wait ....</span></div></div>');
    }
}

var isImageFile = function (file) {
    if (file.type) {
        return /^image\/\w+$/.test(file.type);
    } else {
        return /\.(jpg|jpeg|png|gif)$/.test(file);
    }
};

var oldExportAction = function (self, e, dt, button, config) {
    if (button[0].className.indexOf('buttons-excel') >= 0) {
        if ($.fn.dataTable.ext.buttons.excelHtml5.available(dt, config)) {
            $.fn.dataTable.ext.buttons.excelHtml5.action.call(self, e, dt, button, config);
        }
        else {
            $.fn.dataTable.ext.buttons.excelFlash.action.call(self, e, dt, button, config);
        }
    } else if (button[0].className.indexOf('buttons-print') >= 0) {
        $.fn.dataTable.ext.buttons.print.action(e, dt, button, config);
    }
};

var newExportAction = function (e, dt, button, config) {
    var self = this;
    var oldStart = dt.settings()[0]._iDisplayStart;

    dt.one('preXhr', function (e, s, data) {
        // Just this once, load all data from the server...
        data.start = 0;
        data.length = 2147483647;

        dt.one('preDraw', function (e, settings) {
            // Call the original action function 
            oldExportAction(self, e, dt, button, config);

            dt.one('preXhr', function (e, s, data) {
                // DataTables thinks the first item displayed is index 0, but we're not drawing that.
                // Set the property to what it was before exporting.
                settings._iDisplayStart = oldStart;
                data.start = oldStart;
            });

            // Reload the grid with the original page. Otherwise, API functions like table.cell(this) don't work properly.
            setTimeout(dt.ajax.reload, 0);

            // Prevent rendering of the full data to the DOM
            return false;
        });
    });

    // Requery the server with the new one-time export settings
    dt.ajax.reload();
};

// Custom Implementations
$(document).on('click', '.btn-deleteparent', function () {
    var _this = $(this), dlvl = $(this).data('dlvl');
    if (dlvl != null) { while (dlvl != 0) { _this = _this.parent(); dlvl--; } if (_this != null) { _this.remove(); } }
});