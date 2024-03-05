var DateInputType = (function () {
    return function () {
        var _options;
        function init(inputTypeInfo, options) {
            _options = options;
        }

        var $dateInput;
        function getView(selectedValues, allItems) {
            let $div = $('<div class="input-group">');
            $dateInput = $('<input type="text" class="form-control date-picker text-center"/>').appendTo($div);
            let $divAppend = $('<div class="input-group-append"></div>').appendTo($div);
            let $span = $('<span class="input-group-text">').appendTo($divAppend);
            $('<i class="la la-calendar"></i>').appendTo($span);
            $dateInput
                .on('change', function () {
                    if (_options && typeof (_options.onChange) === "function") {
                        _options.onChange($dateInput.val());
                    }
                });

            if (selectedValues && selectedValues.length > 0) {
                $dateInput.attr("init-value", selectedValues[0]);
            }
            return $div[0];
        }

        function getSelectedValues() {
            return [moment($dateInput.val(),'L').format('DD/MM/YYYY')];
        }
        
        function afterViewInitialized() {
            $dateInput.datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            })
            
            if ($dateInput.attr('init-value') !== undefined) {
                let initDate = moment($dateInput.attr('init-value'),'DD/MM/YYYY').format('L');
                $dateInput.val(initDate);
            } else {
                let initDate = moment().format('L');
                $dateInput.val(initDate);
            }
        }

        return {
            name: "DATE",
            init: init,
            getSelectedValues: getSelectedValues,
            getView: getView,
            hasValues: false,//is that input type need values to work. For example dropdown need values to select.
            afterViewInitialized: afterViewInitialized
        };
    };
})();

(function () {
    var DateInputTypeProvider = new function () {
        this.name = "DATE";
        this.get = function () {
            return new DateInputType();
        }
    }();

    abp.inputTypeProviders.addInputTypeProvider(DateInputTypeProvider);
})();
