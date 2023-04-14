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
                    class: "text-center",
                    // render: function (order) {
                    //     return '<a class="btnViewOrderDetail" data-id="' + order.id + '" href="#">' + order.code + '</a>';
                    // }
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

        let _scriptUrl = abp.appPath + 'view-resources/Views/Shared/Components/ContentUserProfile/';
        let _viewUrl = abp.appPath + 'UserProfile/';

        const _viewOrderDetailModal = new app.ModalManager({
            viewUrl: _viewUrl + 'ViewOrderDetailModal',
            scriptUrl: _scriptUrl + '_ViewOrderDetailModal.js',
            modalClass: 'ViewOrderDetailModal'
        });

        // myOrdersTable.on('click', '.btnViewOrderDetail', function (e) {
        //     e.preventDefault();
        //     _viewOrderDetailModal.open({id: $(this).data('id')});
        // });
    });
})();


