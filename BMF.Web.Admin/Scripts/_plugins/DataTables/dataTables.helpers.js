function DT_Initialize(TableName, Plain) {
    Plain = Plain || false;
    var TableElement = $("#" + TableName);
    // Validate if Element is a Table
    if (TableElement.is("table")) {
        TableElement.addClass("table table-bordered table-condensed table-hover table-striped");
        var Settings = {
            responsive: true, autoWidth: false, bJQueryUI: true, scrollX: '100%',
            dom: '<"pull-left"B><"pull-right clearfix"l>rt<"bottom"<"pull-left"i><"pull-right"p>>',
            buttons: [
                {
                    extend: 'colvis',
                    columns: ':not(:first-child):not(:last-child)',
                    text: "<i class='margin-r-5 fa fa-columns'></i>Table Columns"
                }
            ],
            columnDefs: [{ targets: -1, orderable: false, searchable: false }]
        };
        if (Plain) {
            Settings = {
                responsive: true, autoWidth: false, bJQueryUI: true, scrollX: '100%',
                dom: '<"pull-left"B><"pull-right clearfix"l>rt<"bottom"<"pull-left"i><"pull-right"p>>',
                buttons: [
                    {
                        extend: 'colvis',
                        columns: ':not(:first-child):not(:last-child)',
                        text: "<i class='margin-r-5 fa fa-columns'></i>Table Columns"
                    }
                ],
                columnDefs: []
            };
        }
        var DT = TableElement.DataTable(Settings);
        //$(".dataTables_scrollBody").floatingScroll();
        //$(".dataTables_scrollBody").floatingScroll("update");
        //setTimeout(function () {
        //    $(".dataTables_scrollBody").floatingScroll("update");
        //}, 2000);
        return DT;
    } else {
        DisplayError("Could not initialize DataTable. HTML Element must be a table.", TableElement, true);
    }
    return null;
}

function DTFilter_Initialize(TheDataTable, FilterContainer, Columns, FilterTypes) {
    if (TheDataTable != null && Columns != null && FilterContainer != null && FilterContainer != "" && String(FilterContainer).trim().length != 0) {
        var FilterSettings = [];
        var FilterBox = $("#" + FilterContainer);
        FilterBox.empty();
        if (FilterBox.length != 0) {
            FilterBox.addClass("box no-border box-withshadow collapsed-box " + FilterContainer);
            // Header
            var FilterBox_Header = $("<div />");
            FilterBox_Header.addClass("box-header with-border").attr("style", "display:none");
            FilterBox_Header.html("<h3 class='box-title text-bold' data-widget='collapse' style='cursor:pointer;'><i class='margin-r-5 fa fa-filter'></i>Set Filters</h3><div class='box-tools'><button type='button' class='btn btn-box-tool' data-widget='collapse'><i class='fa fa-minus'></i></button></div>");
            // Body
            var FilterBox_Body = $("<div />");
            FilterBox_Body.addClass("box-body");
            $.each(Columns, function (key, val) {
                if (val != null) {
                    var FieldName = val;
                    var ParsedFieldName = key;
                    FilterBox_Body.append("<div class='col-md-4'><label>" + FieldName + ":</label> <div id='" + FilterContainer + "filter_" + ParsedFieldName + "'></div></div>");
                    if (FilterTypes[key] != null) {
                        if (FilterTypes[key] == "select") {
                            FilterSettings.push({ column_number: key, /*column_data_type: "html",*/ html_data_type: "text", filter_type: FilterTypes[key], filter_container_id: FilterContainer + "filter_" + ParsedFieldName, filter_delay: 750 });
                        } else {
                            FilterSettings.push({ column_number: key, filter_type: FilterTypes[key], filter_container_id: FilterContainer + "filter_" + ParsedFieldName, filter_delay: 750 });
                        }
                    } else {
                        FilterSettings.push({ column_number: key, filter_type: "text", filter_container_id: FilterContainer + "filter_" + ParsedFieldName, filter_delay: 750 });
                    }
                }
            });
            // Footer
            var FilterBox_Footer = $("<div />");
            FilterBox_Footer.addClass("box-footer no-padding");
            // Clear Button
            //FilterBox_Footer.html("<button class='w-100 btn btn-flat btn-danger' id='" + FilterContainer + "_Clear' onclick='yadcf.exResetAllFilters(DT);'>Clear All</button>");
            FilterBox_Footer.html("<div class='row no-padding no-margin'><div class='col-md-6 no-padding no-margin'><button type='button' class='w-100 btn btn-flat btn-danger' id='" + FilterContainer + "_Clear' onclick='yadcf.exResetAllFilters(DT);'>Clear All</button></div><div class='col-md-6 no-padding no-margin'><button type='button' class='w-100 btn btn-flat btn-success' id='" + FilterContainer + "_FilterBtn' onclick='yadcf.exFilterExternallyTriggered(DT);'>Apply Filter</button></div></div>");
            FilterBox.append(FilterBox_Header);
            FilterBox.append(FilterBox_Body);
            FilterBox.append(FilterBox_Footer);
            FilterBox.boxWidget();
            // Filter Initialization
            //console.log(TheDataTable);
            yadcf.init(TheDataTable, FilterSettings, { externally_triggered: true });
        }
    }
}

function DT_AddRecord(TheDataTable, NewRecord) {
    TheDataTable.row.add(NewRecord).draw(false);
}