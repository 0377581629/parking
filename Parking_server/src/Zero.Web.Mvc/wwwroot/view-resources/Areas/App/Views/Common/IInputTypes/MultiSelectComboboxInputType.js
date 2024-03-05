var MultiSelectComboBoxInputType = (function () {
    return function () {
        let _options;
        function init(inputTypeInfo, options) {
            _options = options;
        }

        let $combobox;
        function getView(selectedValues, allItems) {
            $combobox = $('<select class="form-control w-100" multiple/>');

            if (allItems && allItems.length > 0) {
                for (let i = 0; i < allItems.length; i++) {
                    if (typeof(allItems[i]) === 'object' && allItems[i].value !== undefined) {
                        let $option = $('<option></option>')
                            .attr('value', allItems[i].value)
                            .text(allItems[i].value);

                        if (selectedValues && selectedValues.length > 0 && selectedValues.indexOf(allItems[i].value) !== -1) {
                            $option.attr("selected", "selected");
                        }

                        $option.appendTo($combobox);
                    }
                    else{
                        let $option = $('<option></option>')
                            .attr('value', allItems[i])
                            .text(allItems[i]);

                        if (selectedValues && selectedValues.length > 0 && selectedValues.indexOf(allItems[i]) !== -1) {
                            $option.attr("selected", "selected");
                        }

                        $option.appendTo($combobox);
                    }
                }
            }

            $combobox
                .on('change', function () {
                    if (_options && typeof (_options.onChange) === "function") {
                        _options.onChange($combobox.val());
                    }
                });
            return $combobox[0];
        }

        function getSelectedValues() {
            return $combobox.val();
        }

        function afterViewInitialized() {
            $combobox.select2({
                width: '100%',
                dropdownAutoWidth: true,
                multiple: true,
                closeOnSelect: false,
                language: baseHelper.Select2Language()
            });
        }

        return {
            name: "MULTISELECTCOMBOBOX",
            init: init,
            getSelectedValues: getSelectedValues,
            getView: getView,
            hasValues: true,//is that input type need values to work. For example dropdown need values to select.
            afterViewInitialized: afterViewInitialized
        };
    };
})();

(function () {
    var MultiSelectComboBoxInputTypeProvider = new function () {
        this.name = "MULTISELECTCOMBOBOX";
        this.get = function () {
            return new MultiSelectComboBoxInputType();
        }
    }();

    abp.inputTypeProviders.addInputTypeProvider(MultiSelectComboBoxInputTypeProvider);
})();