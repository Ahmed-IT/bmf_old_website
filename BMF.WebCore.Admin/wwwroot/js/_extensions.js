$(document).ready(function () {
    // Validator
    if (jQuery.validator != null) {
        // empty Space
        jQuery.validator.addMethod("maxWords", function (value, element, params) {
            //console.log(value.trim().length);
            return this.optional(element) || value.trim().length != 0;
            //return value.trim().length != 0;
        }, "This field cannot be blank space(s).");
    }
});

// Datatable Filters
function DTFilter_Initialize(TheDataTable, FilterContainer, Columns, FilterTypes, TableName) {
    if (TheDataTable != null && Columns != null && FilterContainer != null && FilterContainer != "" && String(FilterContainer).trim().length != 0) {
        var FilterSettings = [];
        var FilterDiv = $("#" + FilterContainer);
        FilterDiv.empty();
        if (FilterDiv.length != 0) {
            var FilterCard_Header = $('<div>').addClass('card-header bg-secondary')
                .append('<h3 class="card-title"><i class="mr-1 icon fas fa-search"></i>Search</h3>')
                .append('<div class="card-tools"><button type="button" class="btn btn-tool" data-card-widget="collapse"><i class="fas fa-plus"></i></button></div>');
            var FilterCard_Body_Row = $('<div>').addClass('row');
            var FilterCard_Body = $('<div>').addClass('card-body').append(FilterCard_Body_Row);
            var FilterCard_Footer = $('<div>').addClass('card-footer');
            var FilterCard = $('<div>').addClass('card collapsed-card').append(FilterCard_Header).append(FilterCard_Body).append(FilterCard_Footer);

            // Body
            $.each(Columns, function (key, val) {
                if (val != null) {
                    var FieldName = val;
                    var ParsedFieldName = key;
                    FilterCard_Body_Row.append("<div class='col-md-4'><label>" + FieldName + ":</label> <div id='" + FilterContainer + "filter_" + ParsedFieldName + "'></div></div>");
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
            FilterCard_Footer.append(
                $("<div class='row no-padding no-margin'>")
                    .append("<div class='col-md-6'><button type='button' class='w-100 btn btn-danger' id='" + FilterContainer + "_Clear' onclick='yadcf.exResetAllFilters(window[\"" + TableName + "\"]);'>Clear All</button></div>")
                    .append("<div class='col-md-6'><button type='button' class='w-100 btn btn-success' id='" + FilterContainer + "_FilterBtn' onclick='yadcf.exFilterExternallyTriggered(window[\"" + TableName + "\"]);'>Search</button></div>")
            );
            FilterDiv.append(FilterCard);

            // Filter Initialization
            //console.log(TheDataTable);
            yadcf.init(TheDataTable, FilterSettings, { externally_triggered: true });
        }
    }
}