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

        let getReportFilter = function () {
            return {
                filter: "",
            };
        };

        reloadReport();

        function reloadReport() {
            abp.ui.setBusy(reportWrapper);
            _reportService[endPoint](
                getReportFilter()
            ).done(function (data) {
                console.log('data =', data);
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
    });
})();