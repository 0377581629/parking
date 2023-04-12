var RadioInputType = (function () {
    return function () {
        let _options;
        function init(inputTypeInfo, options) {
            _options = options;
        }

        let radioName = app.normalGuid();
        function getView(selectedValues, allItems) {
            let $div = $('<div class="form-group radio-inline" style="justify-content: space-around;">');
            if (allItems && allItems.length > 0) {
                for (let i=0;i<allItems.length;i++) {
                    let targetItem = allItems[i];
                    let $label = $('<label class="radio radio-outline">').appendTo($div);
                    let $radio = $('<input type="radio" name="radios_' + radioName + '" value="' + targetItem.value + '"/>')
                        .appendTo($label);
                    $('<span></span>').appendTo($label);
                    $label.append(targetItem.value);
                    $radio
                        .on('change', function () {
                            if (_options && typeof (_options.onChange) === "function") {
                                _options.onChange($radio.val());
                            }
                        });

                    if (selectedValues && selectedValues.length > 0) {
                        if (selectedValues.indexOf(targetItem.value) > -1) 
                            $radio.prop("checked", true);
                    }
                }
            }
            return $div[0];
        }

        function getSelectedValues() {
            let selected = [];
            $('input[name=radios_' + radioName + ']').filter(':checked').each(function(){
                selected.push($(this).val());
            })
            return selected;
        }

        function afterViewInitialized() {
        }

        return {
            name: "RADIO",
            init: init,
            getSelectedValues: getSelectedValues,
            getView: getView,
            hasValues: true,//is that input type need values to work. For example dropdown need values to select.
            afterViewInitialized: afterViewInitialized
        };
    };
})();

(function () {
    var RadioInputTypeProvider = new function () {
        this.name = "RADIO";
        this.get = function () {
            return new RadioInputType();
        }
    }();

    abp.inputTypeProviders.addInputTypeProvider(RadioInputTypeProvider);
})();
