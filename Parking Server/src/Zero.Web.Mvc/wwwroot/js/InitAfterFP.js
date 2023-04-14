var baseHelper = baseHelper || {};

var select2ViLanguage = {
    inputTooLong: function (args) {
        var overChars = args.input.length - args.maximum;

        var message = 'Vui lòng xóa bớt ' + overChars + ' ký tự';

        return message;
    },
    inputTooShort: function (args) {
        var remainingChars = args.minimum - args.input.length;

        var message = 'Vui lòng nhập thêm từ ' + remainingChars +
            ' ký tự trở lên';

        return message;
    },
    loadingMore: function () {
        return 'Đang lấy thêm kết quả…';
    },
    maximumSelected: function (args) {
        var message = 'Chỉ có thể chọn được ' + args.maximum + ' lựa chọn';

        return message;
    },
    noResults: function () {
        return 'Không tìm thấy kết quả';
    },
    searching: function () {
        return 'Đang tìm…';
    },
    removeAllItems: function () {
        return 'Xóa tất cả các mục';
    }
}

function whatDecimalSeparator() {
    let n = 1.1;
    n = new Intl.NumberFormat(abp.localization.currentLanguage.name).format(n).substring(1, 2);
    return n;
}

function whatThousandSeparator() {
    let decimalSeparator = whatDecimalSeparator();
    if (decimalSeparator === ',')
        return '.';
    return ',';
}

function capitalizeFirstLetter(string) {
    return string.charAt(0).toUpperCase() + string.slice(1);
}

function guid() {
    return 'a' + 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g,
        function (c) {
            let r = Math.random() * 16 | 0, v = c === 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
}

$.fn.exists = function () {
    return this.length !== 0;
}

baseHelper.Select2Language = function () {
    if (abp.localization.currentLanguage.name === 'vi')
        return select2ViLanguage;
    return null;
}

baseHelper.UpFirstChar = function (str) {
    return capitalizeFirstLetter(str);
}

baseHelper.ValidSelectors = function (element = null) {
    let res = true;
    if (element === null || element === undefined) {
        $('body').find('select.requiredSelect2').each(function () {
            let selectId = $(this).attr('id');
            if ($(this).hasClass('requiredSelect2') && ($(this).val() === null || $(this).val() === undefined || $(this).val() === '')) {
                $('body').find('#select2-' + selectId + '-container').parent().addClass('invalid');
                res = false;
            } else {
                $('body').find('#select2-' + selectId + '-container').parent().removeClass('invalid');
            }
        });
    } else {
        element.find('select.requiredSelect2').each(function () {
            let selectId = $(this).attr('id');
            if ($(this).hasClass('requiredSelect2') && ($(this).val() === null || $(this).val() === undefined || $(this).val() === '')) {
                $('body').find('#select2-' + selectId + '-container').parent().addClass('invalid');
                res = false;
            } else {
                $('body').find('#select2-' + selectId + '-container').parent().removeClass('invalid');
            }
        });
    }
    return res;
}

baseHelper.RefreshUI = function (element) {
    if (element) {
        let detailIndex = 1;
        element.find('.detailOrder').each(function () {
            $(this).html(detailIndex);
            detailIndex++;
        });

        element.find('.kt-select2').select2({
            width: '100%',
            dropdownParent: element,
            dropdownAutoWidth: true,
            language: baseHelper.Select2Language()
        });

        element.find('.kt-select2-non-search').select2({
            width: '100%',
            dropdownParent: element,
            dropdownAutoWidth: true,
            minimumResultsForSearch: -1,
            language: baseHelper.Select2Language()
        });

        element.find('.kt-select2-multi-select').select2({
            width: '100%',
            dropdownAutoWidth: true,
            multiple: true,
            closeOnSelect: false,
            language: baseHelper.Select2Language()
        });

        element.find('.number').number(true, 0, whatDecimalSeparator(), whatThousandSeparator());
        element.find('.number1').number(true, 1, whatDecimalSeparator(), whatThousandSeparator());
        element.find('.number2').number(true, 2, whatDecimalSeparator(), whatThousandSeparator());
        element.find('.number3').number(true, 3, whatDecimalSeparator(), whatThousandSeparator());
        element.find('.numberOther').number(true, 0, '', '');

        element.find('.date-picker').each(function () {
            $(this).datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            if ($(this).attr('init-value') !== undefined && $(this).attr('init-value').length > 0) {
                let initDate = moment($(this).attr('init-value'), 'DD/MM/YYYY').format('L');
                $(this).val(initDate);
            }
        });

        element.find('.datetime-picker').each(function () {
            $(this).datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L LT'
            });
            if ($(this).attr('init-value') !== undefined && $(this).attr('init-value').length > 0) {
                let initDate = moment($(this).attr('init-value'), 'DD/MM/YYYY hh:mm').format('L LT');
                $(this).val(initDate);
            }

        });

        element.find('.month-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'MM/YYYY'
        });

        element.find('.make-slug').focusout(function () {
            let originalString = $(this).val();
            if (originalString === '')
                return; // Do nothing if nothing in the MakeSlug input
            let slugInput = $('.slug-input[data-target=' + $(this).attr('id') + ']');
            if (!slugInput) {
                console.log('Error: MakeSlug has invalid SlugInput');
                return;
            }
            if (slugInput.val() !== '')
                return; // SlugInput isn't empty. It already generated or entered by user before
            let slug = baseHelper.MakeSlug(originalString);
            slugInput.val(slug);
        });

        element.find('.frSimpleEditor[initEditor="false"]').each(function () {
            let id = $(this).attr('id');
            if (id !== undefined && id !== null) {
                new FroalaEditor($(this).get(0), frEditorConfigSimple)
                $(this).removeAttr('initEditor');
            }
        });

        element.find('.imgPickerWrapper[init!="true"]').each(function () {
            let imgWrapper = $(this);
            let allows = imgWrapper.attr('allows');
            let imgChange = imgWrapper.find('.imgChange');
            let imgValue = imgWrapper.find('.imgValue');
            let imgHolder = imgWrapper.find('.imgHolder');
            let imgRemove = imgWrapper.find('.imgRemove');

            imgChange.on('click', function () {
                _fileManagerModal.open({
                    allowExtension: allows,
                    maxSelectCount: 1
                }, function (selected) {
                    if (selected !== undefined && selected.length >= 1) {
                        if (imgValue) imgValue.val(selected[selected.length - 1].path);
                        if (imgHolder) imgHolder.attr('src', selected[selected.length - 1].path);
                        imgWrapper.addClass('imgChanged');
                    }
                });
            });

            imgRemove.on('click', function () {
                imgHolder.attr('src', imgHolder.attr('default-src'));
                imgValue.val('');
                imgWrapper.removeClass('imgChanged');
            });

            $(this).attr('init', 'true');
        });
    }
}

baseHelper.Select2 = function (element, placeHolder, serviceUrl, options = {}) {
    let defaultOptions = {
        showWithCode: false,
        filterFunc: null,
        mapResultFunc: null,
        onSelectFunc: null,
        onClearFunc: null,
        parentElement: null,
        allowTag: false,
        createTagFunc: null,
        templateFunc: null
    };
    let settings = $.extend({}, defaultOptions, options);
    baseHelper.SimpleSelector(element, placeHolder, serviceUrl,
        settings.showWithCode,
        settings.filterFunc,
        settings.mapResultFunc,
        settings.onSelectFunc,
        settings.onClearFunc,
        settings.parentElement,
        settings.allowTag,
        settings.createTagFunc,
        settings.templateFunc);
}

baseHelper.SimpleSelector = function (element, placeHolder, serviceUrl,
                                      showWithCode = false,
                                      filterFunc = null,
                                      mapResultFunc = null,
                                      onSelectFunc = null,
                                      onClearFunc = null,
                                      parentElement = null,
                                      allowTag = false,
                                      createTagFunc = null,
                                      templateFunc = null) {
    if (serviceUrl !== undefined && serviceUrl[0] === '/')
        serviceUrl = serviceUrl.substring(1, serviceUrl.length);

    let selectorOptions = {
        dropdownParent: parentElement === null ? element.parent() : parentElement,
        placeholder: placeHolder,
        allowClear: true,
        width: '100%',
        language: baseHelper.Select2Language(),
        ajax: {
            url: abp.appPath + "api/services/app/" + serviceUrl,
            dataType: 'json',
            delay: 50,
            data: function (params) {
                let res = {
                    filter: params.term,
                    skipCount: ((params.page || 1) - 1) * 10
                };
                if (filterFunc !== null && filterFunc !== undefined)
                    return $.extend(filterFunc(), res);
                return res;
            },
            processResults: function (data, params) {
                params.page = params.page || 1;

                if (mapResultFunc !== null && mapResultFunc !== undefined)
                    return {
                        results: mapResultFunc(data.result.items),
                        pagination: {
                            more: (params.page * 10) < data.result.totalCount
                        }
                    }
                let res = $.map(data.result.items, function (item) {
                    return {
                        text: showWithCode ? item.code + ' - ' + item.name : item.name,
                        id: item.id
                    }
                });

                return {
                    results: res,
                    pagination: {
                        more: (params.page * 10) < data.result.totalCount
                    }
                };
            },
            cache: true
        },
        tags: allowTag
    };

    if (allowTag) {
        selectorOptions.createTag = createTagFunc;
    }

    if (element.is("[multiple]")) {
        selectorOptions.closeOnSelect = false;
    }

    if (templateFunc !== null) {
        selectorOptions.templateResult = templateFunc;
        selectorOptions.templateSelection = templateFunc;
    }

    element.select2(selectorOptions);

    if (onSelectFunc !== null && onSelectFunc !== undefined) {
        element.on('select2:select', function (e) {
            onSelectFunc(e.params.data, e);
        });
    }
    if (onClearFunc !== null && onClearFunc !== undefined) {
        element.on('select2:clear', function (e) {
            onClearFunc(e);
        });
    }
}

baseHelper.SimpleRequiredSelector = function (element, placeHolder, serviceUrl, showWithCode = false) {
    return baseHelper.SimpleSelector(element, placeHolder, serviceUrl, showWithCode);
}

baseHelper.ShowNumber = function (input, floatCount = 0) {
    if (input !== undefined && parseFloat(input) !== parseFloat("0"))
        return $.number(input, floatCount, whatDecimalSeparator(), whatThousandSeparator());
    return '';
}

baseHelper.SimpleTableIcon = function (funcName) {
    switch (funcName) {
        case 'send':
            return 'la la-send text-primary';
        case 'approve':
            return 'la la-check text-success';
        case 'return':
            return 'la la-reply  text-dark';
        case 'edit':
            return 'la la-edit text-primary';
        case 'delete':
            return 'la la-trash text-danger';
        case 'view':
            return 'la la-eye text-info';
        case 'attach':
            return 'la la-paperclip text-success'
    }
    return '';
};

baseHelper.ShowOrderStatus = function (status) {
    let $span = $("<span/>");
    if (status === 1) {
        $span.addClass("badge badge-success").text(app.localize('Order_Done'));
    } else if (status === 2) {
        $span.addClass("badge badge-danger").text(app.localize('Order_Fail'));
    }
    return $span[0].outerHTML;
}


$('.kt-select2').select2({
    width: '100%',
    dropdownAutoWidth: true,
    language: baseHelper.Select2Language()
});

$('.kt-select2-non-search').select2({
    width: '100%',
    dropdownAutoWidth: true,
    minimumResultsForSearch: -1,
    language: baseHelper.Select2Language()
});

$('.kt-select2-multi-select').select2({
    width: '100%',
    dropdownAutoWidth: true,
    multiple: true,
    closeOnSelect: false,
    language: baseHelper.Select2Language()
});

$('.number').number(true, 0, whatDecimalSeparator(), whatThousandSeparator());
$('.number1').number(true, 1, whatDecimalSeparator(), whatThousandSeparator());
$('.number2').number(true, 2, whatDecimalSeparator(), whatThousandSeparator());
$('.number3').number(true, 3, whatDecimalSeparator(), whatThousandSeparator());
$('.numberOther').number(true, 0, '', '');