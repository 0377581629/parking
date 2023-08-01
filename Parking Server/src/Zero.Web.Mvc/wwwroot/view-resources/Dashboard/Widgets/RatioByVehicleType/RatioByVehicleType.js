$(function () {
    let _dashboardService = abp.services.app.dashboard;

    _dashboardService.getRatioByVehicleType().done(function(data) {
        ratioByVehicleType(data)
    })

    let ratioByVehicleType = function (data) {
        $("#ratioByVehicleTypeChart").kendoChart({
            dataSource: {data: data},
            title: {
                text: app.localize('RatioByVehicleType')
            },
            legend: {
                position: "top",
            },
            theme : "silver",
            seriesDefaults: {
                labels: {
                    template: "#= category # - #= kendo.format('{0:P}', percentage)#",
                    position: "outsideEnd",
                    visible: true,
                    background: "transparent"
                }
            },
            series: [{
                type: "pie",
                field: "ratio",
                categoryField: "vehicleTypeName",
                autoFit: true,
                labels: {
                    color: "#000",
                    position: "outsideEnd",
                    template: "#= category # - #= kendo.format('{0:P}', percentage)#",
                    visible: true
                }
            }],
            tooltip: {
                visible: true,
                template: "#= category # - #= kendo.format('{0:P}', percentage) #"
            }
        });
    }
});
