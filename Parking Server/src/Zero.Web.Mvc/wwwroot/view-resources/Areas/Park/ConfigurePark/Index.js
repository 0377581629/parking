(function () {
    $(function () {
        let _configureParksService = abp.services.app.configurePark;
        let configureParkSettingsForm = $('#ConfigureParkSettingsForm');

        $('#SaveButton').click(function () {
            let configurePark = configureParkSettingsForm.serializeFormToObject();

            if (!configureParkSettingsForm.valid()) {
                return;
            }

            abp.ui.setBusy();
            _configureParksService.updateConfigurePark(
                configurePark
            ).done(function () {
                window.location.href = abp.appPath + 'Park/ConfigurePark';
            }).always(function () {
                abp.ui.clearBusy();
            });

        });
    });
})();