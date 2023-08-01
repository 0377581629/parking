$(function () {
    let _dashboardService = abp.services.app.dashboard;

    _dashboardService.getRatioByCardType().done(function(data) {
        ratioByCardType(data)
    })

    let ratioByCardType = function (data) {
        $("#ratioByCardTypeChart").kendoChart({
            dataSource: {data: data},
            title: {
                text: app.localize('RatioByCardType')
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
                categoryField: "cardTypeName",
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
