(function ($) {
    app.modals.CreateOrEditFareModal = function () {

        const _FareService = abp.services.app.fare;

        let _modalManager;
        let _$FareInformationForm = null;

        let modal;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            modal = _modalManager.getModal();

            _modalManager.initControl();    

            let vehicleTypeSelector = modal.find('#VehicleTypeId');
            baseHelper.SimpleSelector(vehicleTypeSelector, app.localize('NoneSelect'), 'Park/GetPagedVehicleTypes');

            let cardTypeSelector = modal.find('#CardTypeId');
            baseHelper.SimpleSelector(cardTypeSelector, app.localize('NoneSelect'), 'Park/GetPagedCardTypes');

            _$FareInformationForm = _modalManager.getModal().find('form[name=FareInformationsForm]');
            _$FareInformationForm.validate();
        };

        this.save = function () {
            if (!_$FareInformationForm.valid()) {
                return;
            }

            const Fare = _$FareInformationForm.serializeFormToObject();

            _modalManager.setBusy(true);
            _FareService.createOrEdit(
                Fare
            ).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditFareModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);