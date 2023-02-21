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

            let vehicleTypeSelector = modal.find('#VehicleTypeId');
            baseHelper.SimpleSelector(vehicleTypeSelector, app.localize('NoneSelect'), 'Park/GetPagedVehicleTypes');

            let cardTypeSelector = modal.find('#CardTypeId');
            baseHelper.SimpleSelector(cardTypeSelector, app.localize('NoneSelect'), 'Park/GetPagedCardTypes');

            _$HistoryInformationForm = _modalManager.getModal().find('form[name=HistoryInformationsForm]');
            _$HistoryInformationForm.validate();
        };

        this.save = function () {
            if (!_$HistoryInformationForm.valid()) {
                return;
            }

            const History = _$HistoryInformationForm.serializeFormToObject();

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