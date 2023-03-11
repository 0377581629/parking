(function () {
    $(function () {
        let _$ResidentTable = $('#ResidentTable');
        let _$ResidentTableFilter = $('#ResidentTableFilter');
        let _$ResidentFormFilter = $('#ResidentFormFilter');

        let _$refreshButton = _$ResidentFormFilter.find("button[name='RefreshButton']");
        let _createSingleButton = $('#CreateNewButton');

        let _ResidentService = abp.services.app.resident;

        let _scriptUrl = abp.appPath + 'view-resources/Areas/Park/Resident/';
        let _viewUrl = abp.appPath + 'Park/Resident/';

        const _permissions = {
            create: abp.auth.hasPermission('Park.Resident.Create'),
            edit: abp.auth.hasPermission('Park.Resident.Edit'),
            'delete': abp.auth.hasPermission('Park.Resident.Delete')
        };

        const _createOrEditModal = new app.ModalManager({
            viewUrl: _viewUrl + 'CreateOrEditModal',
            scriptUrl: _scriptUrl + '_CreateOrEditModal.js',
            modalClass: 'CreateOrEditResidentModal'
        });

        let getFilter = function () {
            return {
                filter: _$ResidentTableFilter.val()
            };
        };

        let dataTable = _$ResidentTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            deferLoad: 0,
            listAction: {
                ajaxFunction: _ResidentService.getAll,
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
                                    _createOrEditModal.open({id: data.record.resident.id});
                                }
                            },
                            {
                                icon: 'la la-trash text-danger',
                                text: app.localize('Delete'),
                                visible: function (data) {
                                    return _permissions.delete;
                                },
                                action: function (data) {
                                    baseHelper.Delete(data.record.resident, _ResidentService, getResident);
                                }
                            }]
                    }
                },
                {
                    targets: 2,
                    data: "resident.apartmentNumber",
                    name: "apartmentNumber"
                },
                {
                    targets: 3,
                    data: "resident.ownerFullName",
                    name: "ownerFullName"
                },
                {
                    targets: 4,
                    data: "resident.ownerPhone",
                    name: "ownerPhone"
                },
                {
                    targets: 5,
                    data: "resident.ownerEmail",
                    name: "ownerEmail"
                },
                {
                    targets: 6,
                    data: "resident.isPaid",
                    name: "isPaid",
                    class: "text-center",
                    width: 80,
                    render: function (isPaid) {
                        return baseHelper.ShowActive(isPaid);
                    }
                },
                {
                    targets: 7,
                    data: "resident.isActive",
                    name: "isActive",
                    class: "text-center",
                    width: 80,
                    render: function (isActive) {
                        return baseHelper.ShowActive(isActive);
                    }
                }
            ]
        });

        function getResident() {
            dataTable.ajax.reload();
        }

        if (_$refreshButton) {
            _$refreshButton.on('click', getResident);
        }

        if (_createSingleButton) {
            _createSingleButton.click(function () {
                _createOrEditModal.open();
            });
        }

        abp.event.on('app.createOrEditResidentModalSaved', getResident);

        $(document).keypress(function (e) {
            let sideBar = $('#kt_quick_sidebar');
            if (e.which === 13 && (sideBar === undefined || !sideBar.hasClass('kt-quick-panel--on'))) {
                getResident();
            }
        });
    });
})();