var DateTimeInputType = (function () {
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
            return [moment($dateInput.val(),'L LT').format('DD/MM/YYYY HH:mm')];
        }
        
        function afterViewInitialized() {
            $dateInput.datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L LT',
            })
            
            if ($dateInput.attr('init-value') !== undefined) {
                let initDate = moment($dateInput.attr('init-value'),'DD/MM/YYYY HH:mm').format('L LT');
                $dateInput.val(initDate);
            } else {
                let initDate = moment().format('L LT');
                $dateInput.val(initDate);
            }
        }

        return {
            name: "DATE_TIME",
            init: init,
            getSelectedValues: getSelectedValues,
            getView: getView,
            hasValues: false,//is that input type need values to work. For example dropdown need values to select.
            afterViewInitialized: afterViewInitialized
        };
    };
})();

(function () {
    var DateTimeInputTypeProvider = new function () {
        this.name = "DATE_TIME";
        this.get = function () {
            return new DateTimeInputType();
        }
    }();

    abp.inputTypeProviders.addInputTypeProvider(DateTimeInputTypeProvider);
})();
