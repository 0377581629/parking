(function ($) {
    app.modals.ViewModal = function () {
        let _modalManager;
        let modal;
        this.init = function (modalManager) {
            _modalManager = modalManager;
            modal = _modalManager.getModal();
            _modalManager.initControl();
            _modalManager.bigModal();
        };
    };
})(jQuery);
