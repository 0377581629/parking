(function () {
    $(function () {
        let _$CardTypeTable = $('#CardTypeTable');
        let _$CardTypeTableFilter = $('#CardTypeTableFilter');
        let _$CardTypeFormFilter = $('#CardTypeFormFilter');

        let _$refreshButton = _$CardTypeFormFilter.find("button[name='RefreshButton']");
        let _createSingleButton = $('#CreateNewButton');

        let _CardTypeService = abp.services.app.cardType;

        let _scriptUrl = abp.appPath + 'view-resources/Areas/Park/CardType/';
        let _viewUrl = abp.appPath + 'Park/CardType/';

        const _permissions = {
            create: abp.auth.hasPermission('Park.CardType.Create'),
            edit: abp.auth.hasPermission('Park.CardType.Edit'),
            'delete': abp.auth.hasPermission('Park.CardType.Delete')
        };

        const _createOrEditModal = new app.ModalManager({
            viewUrl: _viewUrl + 'CreateOrEditModal',
            scriptUrl: _scriptUrl + '_CreateOrEditModal.js',
            modalClass: 'CreateOrEditCardTypeModal'
        });

        let getFilter = function () {
            return {
                filter: _$CardTypeTableFilter.val()
            };
        };

        let dataTable = _$CardTypeTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            deferLoad: 0,
            listAction: {
                ajaxFunction: _CardTypeService.getAll,
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
                                    _createOrEditModal.open({id: data.record.cardType.id});
                                }
                            },
                            {
                                icon: 'la la-trash text-danger',
                                text: app.localize('Delete'),
                                visible: function (data) {
                                    return _permissions.delete;
                                },
                                action: function (data) {
                                    baseHelper.Delete(data.record.cardType, _CardTypeService, getCardType);
                                }
                            }]
                    }
                },
                {
                    targets: 2,
                    data: "cardType.name",
                    name: "name"
                },
                {
                    targets: 3,
                    data: "cardType.note",
                    name: "note"
                }
            ]
        });

        function getCardType() {
            dataTable.ajax.reload();
        }

        if (_$refreshButton) {
            _$refreshButton.on('click', getCardType);
        }

        if (_createSingleButton) {
            _createSingleButton.click(function () {
                _createOrEditModal.open();
            });
        }

        abp.event.on('app.createOrEditCardTypeModalSaved', getCardType);

        $(document).keypress(function (e) {
            let sideBar = $('#kt_quick_sidebar');
            if (e.which === 13 && (sideBar === undefined || !sideBar.hasClass('kt-quick-panel--on'))) {
                getCardType();
            }
        });
    });
})();