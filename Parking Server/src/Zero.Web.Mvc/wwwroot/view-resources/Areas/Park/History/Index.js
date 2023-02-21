(function () {
    $(function () {
        let _$HistoryTable = $('#HistoryTable');
        let _$HistoryTableFilter = $('#HistoryTableFilter');
        let _$HistoryFormFilter = $('#HistoryFormFilter');

        let _$refreshButton = _$HistoryFormFilter.find("button[name='RefreshButton']");
        let _createSingleButton = $('#CreateNewButton');

        let _HistoryService = abp.services.app.history;

        let _scriptUrl = abp.appPath + 'view-resources/Areas/Park/History/';
        let _viewUrl = abp.appPath + 'Park/History/';

        let fromDateSelector = $('#FromDateSelector');
        let toDateSelector = $('#ToDateSelector');

        const _permissions = {
            create: abp.auth.hasPermission('Park.History.Create'),
            edit: abp.auth.hasPermission('Park.History.Edit'),
            'delete': abp.auth.hasPermission('Park.History.Delete')
        };

        const _createOrEditModal = new app.ModalManager({
            viewUrl: _viewUrl + 'CreateOrEditModal',
            scriptUrl: _scriptUrl + '_CreateOrEditModal.js',
            modalClass: 'CreateOrEditHistoryModal'
        });

        const _viewModal = new app.ModalManager({
            viewUrl: _viewUrl + 'ViewModal',
            scriptUrl: _scriptUrl + '_ViewModal.js',
            modalClass: 'ViewModal'
        });

        let getFilter = function () {
            return {
                filter: _$HistoryTableFilter.val(),
                fromDate: fromDateSelector.val() !== '' ? moment(fromDateSelector.val(), 'L').format('YYYY-MM-DDT00:00:00Z') : null,
                toDate: toDateSelector.val() !== '' ? moment(toDateSelector.val(), 'L').format('YYYY-MM-DDT23:59:59Z') : null,
            };
        };

        let dataTable = _$HistoryTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            deferLoad: 0,
            listAction: {
                ajaxFunction: _HistoryService.getAll,
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
                    width: 120,
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
                                icon: baseHelper.SimpleTableIcon('view'),
                                text: app.localize('View'),
                                action: function (data) {
                                    _viewModal.open({id: data.record.history.id});
                                }
                            },
                            {
                                icon: 'la la-edit text-primary',
                                text: app.localize('Edit'),
                                visible: function (data) {
                                    return _permissions.edit;
                                },
                                action: function (data) {
                                    _createOrEditModal.open({id: data.record.history.id});
                                }
                            },
                            {
                                icon: 'la la-trash text-danger',
                                text: app.localize('Delete'),
                                visible: function (data) {
                                    return _permissions.delete;
                                },
                                action: function (data) {
                                    baseHelper.Delete(data.record.history, _HistoryService, getHistory);
                                }
                            }]
                    }
                },
                {
                    targets: 2,
                    data: "history.cardCode",
                    name: "cardCode"
                },
                {
                    targets: 3,
                    data: "history.licensePlate",
                    name: "licensePlate"
                },
                {
                    targets: 4,
                    data: "history.price",
                    name: "price",
                    class: "text-right px-5x",
                    render: function (price) {
                        return baseHelper.ShowNumber(price, 0);
                    }
                },
                {
                    targets: 5,
                    width: 80,
                    data: "history.inTime",
                    name: "inTime",
                    class: "text-center",
                    render: function (date) {
                        return moment(date).format('DD-MM-YYYY HH:mm:ss');
                    }
                },
                {
                    targets: 6,
                    width: 80,
                    data: "history.outTime",
                    name: "outTime",
                    class: "text-center",
                    render: function (date) {
                        return moment(date).format('DD-MM-YYYY HH:mm:ss');
                    }
                }
            ]
        });

        function getHistory() {
            dataTable.ajax.reload();
        }

        if (_$refreshButton) {
            _$refreshButton.on('click', getHistory);
        }

        if (_createSingleButton) {
            _createSingleButton.click(function () {
                _createOrEditModal.open();
            });
        }

        abp.event.on('app.createOrEditHistoryModalSaved', getHistory);

        $(document).keypress(function (e) {
            let sideBar = $('#kt_quick_sidebar');
            if (e.which === 13 && (sideBar === undefined || !sideBar.hasClass('kt-quick-panel--on'))) {
                getHistory();
            }
        });
    });
})();