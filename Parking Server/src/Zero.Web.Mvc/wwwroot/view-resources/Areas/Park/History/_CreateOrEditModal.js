(function ($) {
    app.modals.CreateOrEditHistoryModal = function () {

        const _HistoryService = abp.services.app.history;

        let _modalManager;
        let _$HistoryInformationForm = null;

        let modal;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            modal = _modalManager.getModal();

            _modalManager.initControl();

            let vehicleTypeName = modal.find('#History_VehicleTypeName');
            let cardTypeName = modal.find('#History_CardTypeName');
            let price = modal.find('#History_Price');

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
                                vehicleTypeName: item.vehicleTypeName,
                                cardTypeName: item.cardTypeName,
                                price: item.price,
                            }
                        });

                        if (data.result.totalCount === 0) {
                            res.splice(0, 0, {
                                text: app.localize('NotFound'),
                                id: null,
                                vehicleTypeName: '',
                                cardTypeName: '',
                            });
                        }

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
            }).on('select2:select', function (e) {
                let data = e.params.data;
                vehicleTypeName.val(data.vehicleTypeName);
                cardTypeName.val(data.cardTypeName);
                price.val(data.price);
            })

            _$HistoryInformationForm = _modalManager.getModal().find('form[name=HistoryInformationsForm]');
            _$HistoryInformationForm.validate();
        };

        this.save = function () {
            if (!_$HistoryInformationForm.valid()) {
                return;
            }

            const History = _$HistoryInformationForm.serializeFormToObject();
            console.log('history = ', History);

            _modalManager.setBusy(true);
            _HistoryService.createOrEdit(
                History
            ).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditHistoryModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);