(function () {
    $(function () {
        let _$OrderTable = $('#OrderTable');
        let _$OrderTableFilter = $('#OrderTableFilter');
        let _$OrderFormFilter = $('#OrderFormFilter');

        let _$refreshButton = _$OrderFormFilter.find("button[name='RefreshButton']");
        let _createSingleButton = $('#CreateNewButton');

        let _OrderService = abp.services.app.order;

        let _scriptUrl = abp.appPath + 'view-resources/Areas/Park/Order/';
        let _viewUrl = abp.appPath + 'Park/Order/';

        const _permissions = {
            create: abp.auth.hasPermission('Park.Order.Create'),
            edit: abp.auth.hasPermission('Park.Order.Edit'),
            'delete': abp.auth.hasPermission('Park.Order.Delete')
        };

        const _createOrEditModal = new app.ModalManager({
            viewUrl: _viewUrl + 'CreateOrEditModal',
            scriptUrl: _scriptUrl + '_CreateOrEditModal.js',
            modalClass: 'CreateOrEditOrderModal'
        });

        let getFilter = function () {
            return {
                filter: _$OrderTableFilter.val()
            };
        };

        let dataTable = _$OrderTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            deferLoad: 0,
            listAction: {
                ajaxFunction: _OrderService.getAll,
                inputFilter: getFilter
            },
            columnDefs: [
                {
                    targets: 0,
                    className: 'control responsive',
                    orderable: false,
                    render: function () {
                        return '';
                    }
                },
                {
                    width: 80,
                    targets: 1,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: '',
                    rowAction: {
                        dropDownStyle: false,
                        cssClass: 'text-center',
                        items: [
                            {
                                icon: 'la la-edit text-primary',
                                text: app.localize('Edit'),
                                visible: function (data) {
                                    return _permissions.edit;
                                },
                                action: function (data) {
                                    _createOrEditModal.open({id: data.record.order.id});
                                }
                            },
                            {
                                icon: 'la la-trash text-danger',
                                text: app.localize('Delete'),
                                visible: function (data) {
                                    return _permissions.delete;
                                },
                                action: function (data) {
                                    baseHelper.Delete(data.record.order, _OrderService, getOrder);
                                }
                            }]
                    }
                },
                {
                    targets: 2,
                    data: "order.code",
                    name: "code"
                },
                {
                    targets: 3,
                    data: "order.cardNumber",
                    name: "cardNumber"
                },
                {
                    targets: 4,
                    data: "order.amount",
                    name: "amount"
                },
                {
                    targets: 5,
                    data: "order.vnpTransactionNo",
                    name: "vnpTransactionNo"
                },
                {
                    targets: 6,
                    data: "order.creationTime",
                    name: "creationTime",
                    class: "text-center",
                    width: 120,
                    render: function (creationTime) {
                        return moment(creationTime).format('L LT');
                    }
                },
                {
                    targets: 7,
                    data: "order.status",
                    name: "status",
                    render: function (status) {
                        switch (status) {
                            case 1:
                                return app.localize('OrderStatus_Success')
                            case 2:
                                return app.localize('OrderStatus_Fail')
                            default:
                                return ""
                        }
                    }
                }
            ]
        });

        function getOrder() {
            dataTable.ajax.reload();
        }

        if (_$refreshButton) {
            _$refreshButton.on('click', getOrder);
        }

        if (_createSingleButton) {
            _createSingleButton.click(function () {
                _createOrEditModal.open();
            });
        }

        abp.event.on('app.createOrEditOrderModalSaved', getOrder);

        $(document).keypress(function (e) {
            let sideBar = $('#kt_quick_sidebar');
            if (e.which === 13 && (sideBar === undefined || !sideBar.hasClass('kt-quick-panel--on'))) {
                getOrder();
            }
        });
    });
})();