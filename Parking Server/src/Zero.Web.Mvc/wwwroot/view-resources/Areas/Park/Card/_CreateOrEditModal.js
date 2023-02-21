(function ($) {
    app.modals.CreateOrEditCardModal = function () {

        const _CardService = abp.services.app.card;

        let _modalManager;
        let _$CardInformationForm = null;

        let modal;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            modal = _modalManager.getModal();

            _modalManager.initControl();

            let vehicleTypeSelector = modal.find('#VehicleTypeId');
            baseHelper.SimpleSelector(vehicleTypeSelector, app.localize('NoneSelect'), 'Park/GetPagedVehicleTypes');

            let cardTypeSelector = modal.find('#CardTypeId');
            baseHelper.SimpleSelector(cardTypeSelector, app.localize('NoneSelect'), 'Park/GetPagedCardTypes');

            _$CardInformationForm = _modalManager.getModal().find('form[name=CardInformationsForm]');
            _$CardInformationForm.validate();
        };

        this.save = function () {
            if (!_$CardInformationForm.valid()) {
                return;
            }

            const Card = _$CardInformationForm.serializeFormToObject();

            _modalManager.setBusy(true);
            _CardService.createOrEdit(
                Card
            ).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditCardModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);