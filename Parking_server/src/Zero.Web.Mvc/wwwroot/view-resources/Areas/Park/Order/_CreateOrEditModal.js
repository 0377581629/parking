(function ($) {
    app.modals.CreateOrEditOrderModal = function () {

        const _OrderService = abp.services.app.order;

        let _modalManager;
        let _$OrderInformationForm = null;

        let modal;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            modal = _modalManager.getModal();

            _modalManager.initControl();

            let cardSelector = modal.find('#CardId');
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

            _$OrderInformationForm = _modalManager.getModal().find('form[name=OrderInformationsForm]');
            _$OrderInformationForm.validate();
        };

        this.save = function () {
            if (!_$OrderInformationForm.valid()) {
                return;
            }

            const Order = _$OrderInformationForm.serializeFormToObject();

            _modalManager.setBusy(true);
            _OrderService.createOrEdit(
                Order
            ).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditOrderModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);