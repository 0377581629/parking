(function ($) {
    app.modals.CreateOrEditVehicleTypeModal = function () {

        const _VehicleTypeService = abp.services.app.vehicleType;

        let _modalManager;
        let _$VehicleTypeInformationForm = null;

        let modal;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            modal = _modalManager.getModal();

            _modalManager.initControl();
            
            _$VehicleTypeInformationForm = _modalManager.getModal().find('form[name=VehicleTypeInformationsForm]');
            _$VehicleTypeInformationForm.validate();
        };

        this.save = function () {
            if (!_$VehicleTypeInformationForm.valid()) {
                return;
            }

            const VehicleType = _$VehicleTypeInformationForm.serializeFormToObject();

            _modalManager.setBusy(true);
            _VehicleTypeService.createOrEdit(
                VehicleType
            ).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditVehicleTypeModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);