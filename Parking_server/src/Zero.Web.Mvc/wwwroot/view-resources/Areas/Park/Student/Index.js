(function () {
    $(function () {
        let _$StudentTable = $('#StudentTable');
        let _$StudentTableFilter = $('#StudentTableFilter');
        let _$StudentFormFilter = $('#StudentFormFilter');

        let _$refreshButton = _$StudentFormFilter.find("button[name='RefreshButton']");
        let _createSingleButton = $('#CreateNewButton');

        let _StudentService = abp.services.app.student;

        let _scriptUrl = abp.appPath + 'view-resources/Areas/Park/Student/';
        let _viewUrl = abp.appPath + 'Park/Student/';

        const _permissions = {
            create: abp.auth.hasPermission('Park.Student.Create'),
            edit: abp.auth.hasPermission('Park.Student.Edit'),
            'delete': abp.auth.hasPermission('Park.Student.Delete')
        };

        const _createOrEditModal = new app.ModalManager({
            viewUrl: _viewUrl + 'CreateOrEditModal',
            scriptUrl: _scriptUrl + '_CreateOrEditModal.js',
            modalClass: 'CreateOrEditStudentModal'
        });

        let getFilter = function () {
            return {
                filter: _$StudentTableFilter.val()
            };
        };

        let dataTable = _$StudentTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            deferLoad: 0,
            listAction: {
                ajaxFunction: _StudentService.getAll,
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
                                    _createOrEditModal.open({id: data.record.student.id});
                                }
                            },
                            {
                                icon: 'la la-trash text-danger',
                                text: app.localize('Delete'),
                                visible: function (data) {
                                    return _permissions.delete;
                                },
                                action: function (data) {
                                    baseHelper.Delete(data.record.student, _StudentService, getStudent);
                                }
                            }]
                    }
                },
                {
                    targets: 2,
                    data: "student.avatar",
                    name: "avatar",
                    orderable: false,
                    width: 52,
                    render: function (avatar){
                        return baseHelper.ShowAvatar(avatar);
                    }
                },
                {
                    targets: 3,
                    data: "student.code",
                    name: "code"
                },
                {
                    targets: 4,
                    data: "student.name",
                    name: "name"
                },
                {
                    targets: 5,
                    data: "student.phoneNumber",
                    name: "phoneNumber"
                },
                {
                    targets: 6,
                    data: "student.email",
                    name: "email"
                },
                {
                    targets: 7,
                    data: "student.isActive",
                    name: "isActive",
                    class: "text-center",
                    width: 80,
                    render: function (isActive) {
                        return baseHelper.ShowActive(isActive);
                    }
                }
            ]
        });

        function getStudent() {
            dataTable.ajax.reload();
        }

        if (_$refreshButton) {
            _$refreshButton.on('click', getStudent);
        }

        if (_createSingleButton) {
            _createSingleButton.click(function () {
                _createOrEditModal.open();
            });
        }

        abp.event.on('app.createOrEditStudentModalSaved', getStudent);

        $(document).keypress(function (e) {
            let sideBar = $('#kt_quick_sidebar');
            if (e.which === 13 && (sideBar === undefined || !sideBar.hasClass('kt-quick-panel--on'))) {
                getStudent();
            }
        });
    });
})();