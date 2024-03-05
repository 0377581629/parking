var ComboBoxInputType = (function () {
    return function () {
        var _options;
        function init(inputTypeInfo, options) {
            _options = options;
        }

        var $combobox;
        function getView(selectedValues, allItems) {
            $combobox = $('<select class="form-control" />');

            if (allItems && allItems.length > 0) {
                for (let i = 0; i < allItems.length; i++) {
                    if (typeof(allItems[i]) === 'object' && allItems[i].value !== undefined) {
                        $('<option></option>')
                            .attr('value', allItems[i].value)
                            .text(allItems[i].value)
                            .appendTo($combobox);
                    } else {
                        $('<option></option>')
                            .attr('value', allItems[i])
                            .text(allItems[i])
                            .appendTo($combobox);    
                    }
                }
            }

            $combobox
                .on('change', function () {
                    if (_options && typeof (_options.onChange) === "function") {
                        _options.onChange($combobox.val());
                    }
                });

            if (selectedValues && selectedValues.length > 0) {
                $combobox.val(selectedValues[0]);
            }

            return $combobox[0];
        }

        function getSelectedValues() {
            return [$combobox.val()];
        }

        /*
         * {
                name: "",//unique name of InputType (string)
                init: init,//initialize function
                getSelectedValues: getSelectedValues,//function that returns selected value(s) for returned view (returns list of string)
                getView: getView,//function that returns html view for input type (gets parameter named selectedValues)
                hasValues: false //is that input type need values to work. For example dropdown need values to select.
            }
         */

        function afterViewInitialized() {
            $combobox.select2({
                width: '100%',
                dropdownAutoWidth: true,
                language: baseHelper.Select2Language()
            });
        }

        return {
            name: "COMBOBOX",
            init: init,
            getSelectedValues: getSelectedValues,
            getView: getView,
            hasValues: true,//is that input type need values to work. For example dropdown need values to select.
            afterViewInitialized: afterViewInitialized
        };
    };
})();

(function () {
    var ComboBoxInputTypeProvider = new function () {
        this.name = "COMBOBOX";
        this.get = function () {
            return new ComboBoxInputType();
        }
    }();

    abp.inputTypeProviders.addInputTypeProvider(ComboBoxInputTypeProvider);
})();