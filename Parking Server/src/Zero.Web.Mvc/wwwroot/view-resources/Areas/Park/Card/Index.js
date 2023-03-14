(function () {
    $(function () {
        let _$CardTable = $('#CardTable');
        let _$CardTableFilter = $('#CardTableFilter');
        let _$CardFormFilter = $('#CardFormFilter');

        let _$refreshButton = _$CardFormFilter.find("button[name='RefreshButton']");
        let _createSingleButton = $('#CreateNewButton');

        let _CardService = abp.services.app.card;

        let _scriptUrl = abp.appPath + 'view-resources/Areas/Park/Card/';
        let _viewUrl = abp.appPath + 'Park/Card/';

        const _permissions = {
            create: abp.auth.hasPermission('Park.Card.Create'),
            edit: abp.auth.hasPermission('Park.Card.Edit'),
            'delete': abp.auth.hasPermission('Park.Card.Delete')
        };

        const _createOrEditModal = new app.ModalManager({
            viewUrl: _viewUrl + 'CreateOrEditModal',
            scriptUrl: _scriptUrl + '_CreateOrEditModal.js',
            modalClass: 'CreateOrEditCardModal'
        });

        let getFilter = function () {
            return {
                filter: _$CardTableFilter.val()
            };
        };

        let dataTable = _$CardTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            deferLoad: 0,
            listAction: {
                ajaxFunction: _CardService.getAll,
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
                                    _createOrEditModal.open({id: data.record.card.id});
                                }
                            },
                            {
                                icon: 'la la-trash text-danger',
                                text: app.localize('Delete'),
                                visible: function (data) {
                                    return _permissions.delete;
                                },
                                action: function (data) {
                                    baseHelper.Delete(data.record.card, _CardService, getCard);
                                }
                            }]
                    }
                },
                {
                    targets: 2,
                    data: "card.code",
                    name: "code"
                },
                {
                    targets: 3,
                    data: "card.cardNumber",
                    name: "cardNumber"
                },
                {
                    targets: 4,
                    data: "card.vehicleTypeName",
                    name: "vehicleTypeName"
                },
                {
                    targets: 5,
                    data: "card.cardTypeName",
                    name: "cardTypeName"
                },
                {
                    targets: 6,
                    data: "card.note",
                    name: "note"
                },
                {
                    targets: 7,
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

        function getCard() {
            dataTable.ajax.reload();
        }

        if (_$refreshButton) {
            _$refreshButton.on('click', getCard);
        }

        if (_createSingleButton) {
            _createSingleButton.click(function () {
                _createOrEditModal.open();
            });
        }

        abp.event.on('app.createOrEditCardModalSaved', getCard);

        $(document).keypress(function (e) {
            let sideBar = $('#kt_quick_sidebar');
            if (e.which === 13 && (sideBar === undefined || !sideBar.hasClass('kt-quick-panel--on'))) {
                getCard();
            }
        });

        //IMPORT
        let btnImport = $('#ImportFromExcelButton');
        if (btnImport) {
            btnImport.fileupload({
                url: abp.appPath + 'Park/Card/ImportFromExcel',
                dataType: 'json',
                maxFileSize: 1048576 * 100,
                done: function (e, response) {
                    let jsonResult = response.result;
                    if (jsonResult.success) {
                        abp.notify.info(app.localize('UploadImportFileSuccessful'));
                    } else {
                        abp.notify.warn(app.localize('UploadImportFileFailed'));
                    }
                }
            }).prop('disabled', !$.support.fileInput)
                .parent().addClass($.support.fileInput ? undefined : 'disabled');
        }
    });
})();