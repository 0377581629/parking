(function () {
    $(function () {
        let reportName = $('#ReportName').val();
        let leftHeader = $('#LeftHeader').val();
        let rightHeader = $('#RightHeader').val();
        let serviceName = $('#ServiceName').val();
        let endPoint = $('#EndPoint').val();

        let reportWrapper = $("#reportViewerContent");

        let reportViewer = reportHelper.InitReportViewer(reportWrapper, reloadReport);

        let _reportService = abp.services.app[serviceName];

        let refreshReport = $('#RefreshReport');
        let filter = $('#Filter');
        let startDateSelector = $('#StartDateSelector');
        let endDateSelector = $('#EndDateSelector');
        let cardTypeSelector = $('#CardTypeSelector');
        let vehicleTypeSelector = $('#VehicleTypeSelector');

        baseHelper.SimpleSelector(cardTypeSelector, app.localize('NoneSelect'), 'Park/GetPagedCardTypes');
        baseHelper.SimpleSelector(vehicleTypeSelector, app.localize('NoneSelect'), 'Park/GetPagedVehicleTypes');
        
        let getReportFilter = function () {
            return {
                filter: filter.val(),
                startDate: startDateSelector.val(),
                endDate: endDateSelector.val(),
                cardTypeId: cardTypeSelector.val(),
                vehicleTypeId: vehicleTypeSelector.val()
            };
        };

        function reloadReport() {
            abp.ui.setBusy(reportWrapper);
            _reportService[endPoint](
                getReportFilter()
            ).done(function (data) {
                console.log('data = ',data);
                reportViewer.reportSource({
                    report: reportName + ".trdx",
                    parameters: {
                        ReportData: JSON.stringify(data),
                        HeaderLeft: leftHeader,
                        HeaderRight: rightHeader,
                    }
                });
                reportViewer.refreshReport();
            }).always(function () {
                abp.ui.clearBusy(reportWrapper);
            });
        }

        refreshReport.on('click', reloadReport);
    });
})();