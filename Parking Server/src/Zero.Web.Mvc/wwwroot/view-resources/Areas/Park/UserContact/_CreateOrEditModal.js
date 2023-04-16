(function ($) {
    app.modals.CreateOrEditUserContactModal = function () {

        const _UserContactsService = abp.services.app.userContact;

        let _modalManager;
        let _$UserContactInformationForm = null;

        let modal;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            modal = _modalManager.getModal();

            _modalManager.initControl();

            _$UserContactInformationForm = _modalManager.getModal().find('form[name=UserContactInformationsForm]');
            _$UserContactInformationForm.validate();
        };

        this.save = function () {
            if (!_$UserContactInformationForm.valid()) {
                return;
            }

            const UserContact = _$UserContactInformationForm.serializeFormToObject();

            _modalManager.setBusy(true);
            _UserContactsService.createOrEdit(
                UserContact
            ).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditUserContactModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);