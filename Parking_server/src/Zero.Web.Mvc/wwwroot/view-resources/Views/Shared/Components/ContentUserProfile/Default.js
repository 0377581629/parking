(function () {
    $(function () {
        const _parkService = abp.services.app.parkPublic;
        const _profileService = abp.services.app.profile;

        let accountInfoForm = $('form[id="accInfoForm"]');
        let updPasswordForm = $('form[id="updPasswordForm"]');

        let btnUpdAccInfo = $('#btnUpdAccountInfo');
        let btnUpdPasswordInfo = $('#btnUpdPassword');

        if (btnUpdAccInfo) {
            btnUpdAccInfo.on('click', function () {
                if (!accountInfoForm.valid())
                    return;
                abp.ui.setBusy(accountInfoForm);
                let accInfo = accountInfoForm.serializeFormToObject();
                _profileService.updateCurrentUserProfileSimple(
                    accInfo
                ).done(function () {
                    abp.notify.success(app.localize('FP_UpdateSuccessful'));
                }).always(function () {
                    abp.ui.clearBusy(accountInfoForm);
                })
            });
        }

        if (btnUpdPasswordInfo) {
            btnUpdPasswordInfo.on('click', function () {
                if (!updPasswordForm.valid())
                    return;
                abp.ui.setBusy(updPasswordForm);
                let accInfo = updPasswordForm.serializeFormToObject();
                _profileService.changePassword(
                    accInfo
                ).done(function () {
                    abp.notify.success(app.localize('FP_UpdateSuccessful'));
                }).always(function () {
                    abp.ui.clearBusy(updPasswordForm);
                })
            });
        }

        let myOrdersTable = $("#MyOrdersTable");

        myOrdersTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            deferLoad: 0,
            listAction: {
                ajaxFunction: _parkService.getMyOrders
            },
            columnDefs: [
                {
                    targets: 0,
                    data: "order.code",
                    name: "code",
                    width: 120,
                    class: "text-center"
                },
                {
                    targets: 1,
                    data: "order.cardNumber",
                    name: "cardNumber",
                    class: "text-center",
                    width: 120,
                },
                {
                    targets: 2,
                    data: "order.amount",
                    name: "amount",
                    class: "text-center",
                    width: 120,
                    render: function (total) {
                        return baseHelper.ShowNumber(total);
                    }
                },
                {
                    targets: 3,
                    data: "order.creationTime",
                    name: "creationTime",
                    class: "text-center",
                    width: 120,
                    render: function (creationTime) {
                        return moment(creationTime).format('L LT');
                    }
                },
                {
                    targets: 4,
                    data: "order.status",
                    name: "status",
                    class: "text-center",
                    width: 120,
                    render: function (status) {
                        return baseHelper.ShowOrderStatus(status);
                    }
                }
            ]
        });

        let myCardsTable = $("#MyCardsTable");

        myCardsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            deferLoad: 0,
            listAction: {
                ajaxFunction: _parkService.getMyCards
            },
            columnDefs: [
                {
                    targets: 0,
                    data: "card.cardNumber",
                    name: "cardNumber",
                    class: "text-center",
                    width: 120,
                },
                {
                    targets: 1,
                    data: "card.licensePlate",
                    name: "licensePlate",
                    class: "text-center",
                    width: 120,
                    render: function (total) {
                        return baseHelper.ShowNumber(total);
                    }
                },
                {
                    targets: 2,
                    data: "card.vehicleTypeName",
                    name: "vehicleTypeName",
                    class: "text-center",
                    width: 120,
                },
                {
                    targets: 3,
                    data: "card.cardTypeName",
                    name: "cardTypeName",
                    class: "text-center",
                    width: 120,
                },
                {
                    targets: 4,
                    data: "card.balance",
                    name: "balance",
                    class: "text-center",
                    width: 120,
                    render: function (balance) {
                        return baseHelper.ShowNumber(balance);
                    }
                },
                {
                    targets: 5,
                    data: "card.isActive",
                    name: "isActive",
                    class: "text-center",
                    width: 80,
                    render: function(isActive) {
                        return baseHelper.ShowActive(isActive);
                    }
                }
            ]
        });
        
    });
})();


