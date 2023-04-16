(function () {
    $(function () {

        let _$UserContactsTable = $('#UserContactsTable');
        let _$UserContactsTableFilter = $('#UserContactsTableFilter');
        let _$UserContactsFormFilter = $('#UserContactsFormFilter');

        let _$refreshButton = _$UserContactsFormFilter.find("button[name='RefreshButton']");
        let _createSingleButton = $('#CreateNewButton');

        let _userContactsService = abp.services.app.userContact;

        let _scriptUrl = abp.appPath + 'view-resources/Areas/Park/UserContact/';
        let _viewUrl = abp.appPath + 'Park/UserContact/';

        const _permissions = {
            create: abp.auth.hasPermission('Park.UserContact.Create'),
            edit: abp.auth.hasPermission('Park.UserContact.Edit'),
            'delete': abp.auth.hasPermission('Park.UserContact.Delete')
        };

        const _createOrEditModal = new app.ModalManager({
            viewUrl: _viewUrl + 'CreateOrEditModal',
            scriptUrl: _scriptUrl + '_CreateOrEditModal.js',
            modalClass: 'CreateOrEditUserContactModal'
        });

        let getFilter = function () {
            return {
                filter: _$UserContactsTableFilter.val()
            };
        };

        let dataTable = _$UserContactsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            deferLoad: 0,
            listAction: {
                ajaxFunction: _userContactsService.getAll,
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
                                    _createOrEditModal.open({id: data.record.userContact.id});
                                }
                            },
                            {
                                icon: 'la la-trash text-danger',
                                text: app.localize('Delete'),
                                visible: function (data) {
                                    return _permissions.delete;
                                },
                                action: function (data) {
                                    baseHelper.Delete(data.record.userContact, _userContactsService, getUserContacts);
                                }
                            }]
                    }
                },
                {
                    targets: 2,
                    data: "userContact.code",
                    name: "code"
                },
                {
                    targets: 3,
                    data: "userContact.name",
                    name: "name"
                },
                {
                    targets: 4,
                    data: "userContact.email",
                    name: "email"
                },
                {
                    targets: 5,
                    data: "userContact.content",
                    name: "content"
                },
                {
                    targets: 6,
                    data: "userContact.note",
                    name: "note"
                },
                {
                    targets: 7,
                    data: "userContact.isActive",
                    name: "isActive",
                    class: "text-center",
                    width: 80,
                    render: function (isActive) {
                        return baseHelper.ShowActive(isActive);
                    }
                }
            ]
        });

        function getUserContacts() {
            dataTable.ajax.reload();
        }

        if (_$refreshButton) {
            _$refreshButton.on('click', getUserContacts);
        }

        if (_createSingleButton) {
            _createSingleButton.click(function () {
                _createOrEditModal.open();
            });
        }

        abp.event.on('app.createOrEditUserContactModalSaved', getUserContacts);

        $(document).keypress(function (e) {
            let sideBar = $('#kt_quick_sidebar');
            if (e.which === 13 && (sideBar === undefined || !sideBar.hasClass('kt-quick-panel--on'))) {
                getUserContacts();
            }
        });

    });
})();