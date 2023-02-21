(function () {
    $(function () {
        let _configureParksService = abp.services.app.configurePark;
        let configureParkSettingsForm = $('#ConfigureParkSettingsForm');

        let ApplyDecreasePercent = $('#ApplyDecreasePercent');

        function toggleDecreasePercentConfigurePark() {
            if (ApplyDecreasePercent.is(':checked')) {
                $('#DecreasePercentConfigurePark').slideDown('fast');
            } else {
                $('#DecreasePercentConfigurePark').slideUp('fast');
            }
        }

        ApplyDecreasePercent.change(function () {
            toggleDecreasePercentConfigurePark();
        });

        toggleDecreasePercentConfigurePark();

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