(function ($) {
    app.modals.CreateOrEditCardTypeModal = function () {

        const _CardTypeService = abp.services.app.cardType;

        let _modalManager;
        let _$CardTypeInformationForm = null;

        let modal;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            modal = _modalManager.getModal();

            _modalManager.initControl();

            _$CardTypeInformationForm = _modalManager.getModal().find('form[name=CardTypeInformationsForm]');
            _$CardTypeInformationForm.validate();
        };

        this.save = function () {
            if (!_$CardTypeInformationForm.valid()) {
                return;
            }

            const CardType = _$CardTypeInformationForm.serializeFormToObject();

            _modalManager.setBusy(true);
            _CardTypeService.createOrEdit(
                CardType
            ).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditCardTypeModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);