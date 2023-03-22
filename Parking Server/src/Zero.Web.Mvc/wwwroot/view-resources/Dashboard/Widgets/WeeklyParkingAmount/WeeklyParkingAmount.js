$(function () {
    let _$dashboardService = abp.services.app.dashboard;
    let _$chart = $('#DashboardChart-WeeklyParkingAmount');
    let _$totalParkingAmountByWeek = 0.0;

    _$dashboardService.getParkingAmountByWeek().done(function (data) {
        $.each(data, function (index, value) {
            _$totalParkingAmountByWeek += value.parkingAmount;
        });
        let totalParkingAmountByWeekStr = baseHelper.ShowNumber(_$totalParkingAmountByWeek, 0);
        $('#TotalParkingAmountByWeek').text(totalParkingAmountByWeekStr + " " + app.localize('Vehicle'));
        chartAreaParkingAmount(data)
    });

    let chartAreaParkingAmount = function (result) {
        _$chart.kendoChart({
            title: {
                text: app.localize('WeeklyParkingAmount')
            },
            legend: {
                position: "bottom"
            },
            theme: "sass",
            seriesDefaults: {
                type: "area",
                area: {
                    line: {
                        style: "smooth"
                    }
                }
            },
            series: [
                {
                    name: app.localize('TotalParkingAmountByDay'),
                    data: result,
                    field: "parkingAmount",
                    categoryField: "day",
                }],
            valueAxis: {
                title: {
                    text: app.localize("TotalParkingAmount")
                },
                labels: {
                    format: "{0:n0}"
                },
                line: {
                    visible: false
                },
            },
            categoryAxis: {
                majorGridLines: {
                    visible: false
                },
                labels: {
                    rotation: "auto"
                }
            },
            tooltip: {
                visible: true,
                format: "$ {0:0,000}",
                template: "#= series.name #: #= kendo.toString(value, 'n0') #"
            }
        })
    }
});