(function () {
    $(function () {
        let _$FareTable = $('#FareTable');
        let _$FareTableFilter = $('#FareTableFilter');
        let _$FareFormFilter = $('#FareFormFilter');

        let _$refreshButton = _$FareFormFilter.find("button[name='RefreshButton']");
        let _createSingleButton = $('#CreateNewButton');

        let _FareService = abp.services.app.fare;

        let _scriptUrl = abp.appPath + 'view-resources/Areas/Park/Fare/';
        let _viewUrl = abp.appPath + 'Park/Fare/';

        const _permissions = {
            create: abp.auth.hasPermission('Park.Fare.Create'),
            edit: abp.auth.hasPermission('Park.Fare.Edit'),
            'delete': abp.auth.hasPermission('Park.Fare.Delete')
        };

        const _createOrEditModal = new app.ModalManager({
            viewUrl: _viewUrl + 'CreateOrEditModal',
            scriptUrl: _scriptUrl + '_CreateOrEditModal.js',
            modalClass: 'CreateOrEditFareModal'
        });

        let getFilter = function () {
            return {
                filter: _$FareTableFilter.val()
            };
        };

        let dataTable = _$FareTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            deferLoad: 0,
            listAction: {
                ajaxFunction: _FareService.getAll,
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
                                    _createOrEditModal.open({id: data.record.fare.id});
                                }
                            },
                            {
                                icon: 'la la-trash text-danger',
                                text: app.localize('Delete'),
                                visible: function (data) {
                                    return _permissions.delete;
                                },
                                action: function (data) {
                                    baseHelper.Delete(data.record.fare, _FareService, getFare);
                                    console.log('fare = ',data.record.fare)
                                }
                            }]
                    }
                },
                {
                    targets: 2,
                    data: "fare.vehicleTypeName",
                    name: "vehicleTypeName"
                },
                {
                    targets: 3,
                    data: "fare.cardTypeName",
                    name: "cardTypeName"
                },
                {
                    targets: 4,
                    data: "fare.price",
                    name: "price",
                    class: "text-right px-5x",
                    render: function (price) {
                        return baseHelper.ShowNumber(price, 0);
                    }
                },
                {
                    targets: 5,
                    width: 80,
                    data: "fare.type",
                    name: "type",
                    class: "text-center",
                    render: function (type) {
                        switch (type) {
                            case 1:
                                return app.localize("FareType_Day")
                            case 2:
                                return app.localize("FareType_Night")
                        }
                    }
                }
            ]
        });

        function getFare() {
            dataTable.ajax.reload();
        }

        if (_$refreshButton) {
            _$refreshButton.on('click', getFare);
        }

        if (_createSingleButton) {
            _createSingleButton.click(function () {
                _createOrEditModal.open();
            });
        }

        abp.event.on('app.createOrEditFareModalSaved', getFare);

        $(document).keypress(function (e) {
            let sideBar = $('#kt_quick_sidebar');
            if (e.which === 13 && (sideBar === undefined || !sideBar.hasClass('kt-quick-panel--on'))) {
                getFare();
            }
        });
    });
})();