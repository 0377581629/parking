var MultiLineStringInputType = (function () {
    return function () {
        var _inputTypeInfo;
        var _options;
        function init(inputTypeInfo, options) {
            _inputTypeInfo = inputTypeInfo;
            _options = options;
        }
        var $textArea;
        function getView(selectedValues, allItems) {
            
            $textArea = $('<textarea class="form-control" type="text" rows="3"/>')
                .on('change', function () {
                    if (_options && typeof (_options.onChange) === "function") {
                        _options.onChange($textArea.val());
                    }
                });

            if (selectedValues && selectedValues.length > 0) {
                $textArea.val(selectedValues[0]);
            }

            return $textArea[0];
        }

        function getSelectedValues() {
            return [$textArea.val()];
        }

        function afterViewInitialized() {

        }
        return {
            name: "MULTI_LINE_STRING",
            init: init,
            getSelectedValues: getSelectedValues,
            getView: getView,
            hasValues: false,//is that input type need values to work. For example dropdown need values to select.
            afterViewInitialized: afterViewInitialized
        };
    };
})();

(function () {
    var MultiLineStringInputTypeProvider = new function () {
        this.name = "MULTI_LINE_STRING";
        this.get = function () {
            return new MultiLineStringInputType();
        }
    }();

    abp.inputTypeProviders.addInputTypeProvider(MultiLineStringInputTypeProvider);
})();