$(function () {
    let _$dashboardService = abp.services.app.dashboard;
    let _$chart = $('#DashboardChart-WeeklyParkingRevenue');
    let _$totalParkingRevenueByWeek = 0.0;

    _$dashboardService.getParkingRevenueByWeek().done(function (data) {
        $.each(data, function (index, value) {
            _$totalParkingRevenueByWeek += value.parkingRevenue;
        });
        let totalParkingRevenueByWeekStr = baseHelper.ShowNumber(_$totalParkingRevenueByWeek, 0);
        $('#TotalParkingRevenueByWeek').text(totalParkingRevenueByWeekStr + " " + app.localize('Vehicle'));
        chartAreaParkingRevenue(data)
    });

    let chartAreaParkingRevenue = function (result) {
        _$chart.kendoChart({
            title: {
                text: app.localize('WeeklyParkingRevenue')
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
                    name: app.localize('TotalParkingRevenueByDay'),
                    data: result,
                    field: "parkingRevenue",
                    categoryField: "day",
                }],
            valueAxis: {
                title: {
                    text: app.localize("TotalParkingRevenue")
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