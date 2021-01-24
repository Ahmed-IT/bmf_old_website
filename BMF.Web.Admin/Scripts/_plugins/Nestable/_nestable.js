/* Create Nestable */
var CreateNestable = function () {

};

/* Populate Item */
var AddNestableItem = function (List, ID, Parent, Display, View, Edit, Delete) {
    var Item = $('<li/>')
        .addClass('dd-item dd3-item')
        .attr("data-id", ID)
        .data('ID', ID)
        //.data('Display', Display)
        //.data('Parent', Parent)
        .append($('<div/>').addClass('dd-handle dd3-handle').html("<i class='fa fa-bars'></i>"))
        .append($('<div/>').addClass('dd3-content').text(Display));
    var Item_Actions = $('<div/>')
        .addClass('dd-actions')
        .appendTo(Item);
    if (View)
        var Item_V = $('<span/>')
            .addClass('btn btn-xs pull-right btn-primary margin-l-5')
            .data('ID', ID)
            .html('<i class="fa fa-eye"></i>')
            .on('click', View)
            .appendTo(Item_Actions);
    if (Edit)
        var Item_E = $('<span/>')
            .addClass('btn btn-xs pull-right btn-primary margin-l-5')
            .data('ID', ID)
            .html('<i class="fa fa-pencil"></i>')
            .on('click', Edit)
            .appendTo(Item_Actions);
    if (Delete)
        var Item_D = $('<span/>')
            .addClass('btn btn-xs pull-right btn-danger margin-l-5')
            .data('ID', ID)
            .html('<i class="fa fa-remove"></i>')
            .on('click', Delete)
            .appendTo(Item_Actions);
    if (Parent == null) {
        $(List).append(Item);
    } else {
        var P = $(List).find($('[data-id="' + Parent + '"]'));
        if (P.find('.dd-list').length == 0)
            P.append($('<ol/>').addClass('dd-list'));
        $(P.find('.dd-list')).append(Item);
    }
};

/* Update Element */
var UpdateOutput = function (target, source) {
    var list = source.length ? source : $(source.target);
    if (window.JSON) {
        $(target).val(window.JSON.stringify(list.nestable('serialize')));
    } else {
        $(target).val('JSON browser support required.');
    }
};

/* Create Filter */
var InitFilter = function (target, label) {
    var Div = $('<div/>').addClass('form-group'),
        Label = $('<label/>').addClass('control-label').attr('for', 'dd-f').text('Filter ' + label).appendTo(Div),
        Filter = $('<input/>').addClass('form-control').attr('id', 'dd-f').attr('type', 'text').attr('placeholder', 'Filter ' + label).appendTo(Div).on('input', function () {
            var _F = $(this).val().toString().toLowerCase();
            $(target).find('.dd3-content').each(function () {
                var _This = $(this), _Text = $(this).text().toString().toLowerCase();
                if (_Text.indexOf(_F) == -1) _This.parent('li').hide();
                else {
                    _This.parent('li').show();
                    _This.parents('li').show();
                }
            });
        });
    Div.prepend($('<hr/>', { 'style': 'border-width:3px;margin-top:10px;margin-bottom:10px;' }).addClass('border-blue-bg'));
    Div.append($('<hr/>', { 'style': 'border-width:3px;margin-top:10px;margin-bottom:10px;' }).addClass('border-blue-bg'));
    $(target).prepend(Div);
};