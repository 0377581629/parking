(function () {
    $(function () {
        const _parkService = abp.services.app.parkPublic;

        let cardSelector = $('#CardId');
        cardSelector.select2({
            placeholder: app.localize('PleaseSelect'),
            allowClear: true,
            width: '100%',
            ajax: {
                url: abp.appPath + "api/services/app/Park/GetPagedCards",
                dataType: 'json',
                delay: 50,
                data: function (params) {
                    return {
                        filter: params.term,
                        skipCount: ((params.page || 1) - 1) * 10,
                    };
                },
                processResults: function (data, params) {
                    params.page = params.page || 1;
                    let res = $.map(data.result.items, function (item) {
                        return {
                            id: item.id,
                            text: item.code + '-' + item.cardNumber,
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
            language: abp.localization.currentLanguage.name
        })

    });
})();


