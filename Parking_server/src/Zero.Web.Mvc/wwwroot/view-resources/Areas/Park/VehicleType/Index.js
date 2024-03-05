(function () {
    $(function () {
        let _$VehicleTypeTable = $('#VehicleTypeTable');
        let _$VehicleTypeTableFilter = $('#VehicleTypeTableFilter');
        let _$VehicleTypeFormFilter = $('#VehicleTypeFormFilter');

        let _$refreshButton = _$VehicleTypeFormFilter.find("button[name='RefreshButton']");
        let _createSingleButton = $('#CreateNewButton');

        let _VehicleTypeService = abp.services.app.vehicleType;

        let _scriptUrl = abp.appPath + 'view-resources/Areas/Park/VehicleType/';
        let _viewUrl = abp.appPath + 'Park/VehicleType/';

        const _permissions = {
            create: abp.auth.hasPermission('Park.VehicleType.Create'),
            edit: abp.auth.hasPermission('Park.VehicleType.Edit'),
            'delete': abp.auth.hasPermission('Park.VehicleType.Delete')
        };

        const _createOrEditModal = new app.ModalManager({
            viewUrl: _viewUrl + 'CreateOrEditModal',
            scriptUrl: _scriptUrl + '_CreateOrEditModal.js',
            modalClass: 'CreateOrEditVehicleTypeModal'
        });

        let getFilter = function () {
            return {
                filter: _$VehicleTypeTableFilter.val()
            };
        };

        let dataTable = _$VehicleTypeTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            deferLoad: 0,
            listAction: {
                ajaxFunction: _VehicleTypeService.getAll,
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
                                    _createOrEditModal.open({id: data.record.vehicleType.id});
                                }
                            },
                            {
                                icon: 'la la-trash text-danger',
                                text: app.localize('Delete'),
                                visible: function (data) {
                                    return _permissions.delete;
                                },
                                action: function (data) {
                                    baseHelper.Delete(data.record.vehicleType, _VehicleTypeService, getVehicleType);
                                }
                            }]
                    }
                },
                {
                    targets: 2,
                    data: "vehicleType.name",
                    name: "name"
                },
                {
                    targets: 3,
                    data: "vehicleType.note",
                    name: "note"
                }
            ]
        });

        function getVehicleType() {
            dataTable.ajax.reload();
        }

        if (_$refreshButton) {
            _$refreshButton.on('click', getVehicleType);
        }

        if (_createSingleButton) {
            _createSingleButton.click(function () {
                _createOrEditModal.open();
            });
        }

        abp.event.on('app.createOrEditVehicleTypeModalSaved', getVehicleType);

        $(document).keypress(function (e) {
            let sideBar = $('#kt_quick_sidebar');
            if (e.which === 13 && (sideBar === undefined || !sideBar.hasClass('kt-quick-panel--on'))) {
                getVehicleType();
            }
        });
    });
})();