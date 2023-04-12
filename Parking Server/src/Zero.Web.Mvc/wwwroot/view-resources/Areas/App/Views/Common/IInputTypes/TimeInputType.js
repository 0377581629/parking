var TimeInputType = (function () {
    return function () {
        var _options;
        function init(inputTypeInfo, options) {
            _options = options;
        }

        var $timeInput;
        function getView(selectedValues, allItems) {
            let $div = $('<div class="input-group">');
            $timeInput = $('<input type="text" class="form-control time-picker text-center"/>').appendTo($div);
            let $divAppend = $('<div class="input-group-append"></div>').appendTo($div);
            let $span = $('<span class="input-group-text">').appendTo($divAppend);
            $('<i class="la la-clock"></i>').appendTo($span);
            $timeInput
                .on('change', function () {
                    if (_options && typeof (_options.onChange) === "function") {
                        _options.onChange($timeInput.val());
                    }
                });

            if (selectedValues && selectedValues.length > 0) {
                $timeInput.attr("init-value", selectedValues[0]);
            }
            return $div[0];
        }

        function getSelectedValues() {
            return [moment($timeInput.val(),'LT').format('HH:mm')];
        }
        
        function afterViewInitialized() {
            $timeInput.datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'LT'
            })
            if ($timeInput.attr('init-value')) {
                let initDate = moment($timeInput.attr('init-value'),'HH:mm').format('LT');
                $timeInput.val(initDate);
            }else {
                let initDate = moment().format('LT');
                $timeInput.val(initDate);
            }
        }

        return {
            name: "TIME",
            init: init,
            getSelectedValues: getSelectedValues,
            getView: getView,
            hasValues: false,//is that input type need values to work. For example dropdown need values to select.
            afterViewInitialized: afterViewInitialized
        };
    };
})();

(function () {
    var TimeInputTypeProvider = new function () {
        this.name = "TIME";
        this.get = function () {
            return new TimeInputType();
        }
    }();

    abp.inputTypeProviders.addInputTypeProvider(TimeInputTypeProvider);
})();
