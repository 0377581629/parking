(function ($) {
    app.modals.CreateOrEditStudentModal = function () {
        const _StudentService = abp.services.app.student;

        let _modalManager;
        let _$StudentInformationForm = null;

        let modal;

        let studentDetailTable;
        let addStudentDetailBtn;
        let studentDetail;

        let _imageWrap;
        let _imageHolder;
        let _changeImageButton;
        let _cancelImageButton;
        let _imageValue;
        let uploadFileInput;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            modal = _modalManager.getModal();

            _modalManager.initControl();

            _imageWrap = modal.find('#AvatarWrap');
            _imageHolder = modal.find('#AvatarHolder');
            _changeImageButton = modal.find('#ChangeAvatar');
            _cancelImageButton = modal.find('#CancelAvatar');
            _imageValue = modal.find('#Avatar');
            uploadFileInput = modal.find(".uploadFileInput");

            baseHelper.SelectSingleFile("*.jpg;*.png", _changeImageButton, _cancelImageButton, null,_imageValue, _imageHolder, _imageWrap,"kt-avatar--changed");

            _cancelImageButton.on("click",function (){
                uploadFileInput.val("");
                _imageValue.val("");
                _imageHolder.fadeIn("fast").attr('src',"/Common/Images/default-profile-picture.png");
            })

            studentDetail = modal.find('#Student_StudentDetail');

            studentDetailTable = modal.find('#StudentDetailTable');
            addStudentDetailBtn = modal.find('#btnAddDetailStudentDetail');

            if (addStudentDetailBtn) {
                addStudentDetailBtn.on('click', function () {
                    $.get(abp.appPath + 'Park/Student/NewStudentDetail').then(function (res) {
                        studentDetailTable.find('#LastDetailRowStudentDetail').before(res);
                        baseHelper.RefreshUI(studentDetailTable);
                        InitStudentDetailSelector();
                    });
                });
            }

            studentDetailTable.on('click', '.btnDeleteDetail', function () {
                let rowId = $(this).attr('rowId');
                studentDetailTable.find('.detailRow[rowId="' + rowId + '"]').remove();
                baseHelper.RefreshUI(studentDetailTable);
            });

            baseHelper.RefreshUI(studentDetailTable);

            InitStudentDetailSelector();

            _$StudentInformationForm = _modalManager.getModal().find('form[name=StudentInformationsForm]');
            _$StudentInformationForm.validate();
        };

        function InitStudentDetailSelector() {
            if (studentDetailTable) {
                studentDetailTable.find('.detailRow').each(function () {
                    let rowId = $(this).attr('rowId');

                    let cardSelector = studentDetailTable.find('.cardSelector[rowId="' + rowId + '"][initSelector="false"]');
                    cardSelector.select2({
                        placeholder: app.localize('PleaseSelect'),
                        allowClear: true,
                        width: '100%',
                        ajax: {
                            url: abp.appPath + "api/services/app/Park/GetPagedCards",
                            dataType: 'json',
                            delay: 50,
                            data: function (params) {
                                return {
                                    filter: params.term,
                                    skipCount: ((params.page || 1) - 1) * 10,
                                };
                            },
                            processResults: function (data, params) {
                                params.page = params.page || 1;
                                let res = $.map(data.result.items, function (item) {
                                    return {
                                        id: item.id,
                                        text: item.code + '-' + item.cardNumber,
                                    }
                                });

                                return {
                                    results: res,
                                    pagination: {
                                        more: (params.page * 10) < data.result.totalCount
                                    }
                                };
                            },
                            cache: true
                        },
                        language: abp.localization.currentLanguage.name
                    })
                    cardSelector.removeAttr('initSelector');
                });
            }
        }

        function GetStudentDetails() {
            let details = [];
            if (studentDetailTable) {
                studentDetailTable.find('.detailRow').each(function () {
                    let rowId = $(this).attr('rowId');
                    details.push({
                        id: studentDetailTable.find('.detailId[rowId="' + rowId + '"]').val(),
                        cardId: studentDetailTable.find('.cardSelector[rowId="' + rowId + '"]').val(),
                        note: studentDetailTable.find('.detailNote[rowId="' + rowId + '"]').val(),
                    })
                });
            }
            return details;
        }

        this.save = function () {
            if (!_$StudentInformationForm.valid()) {
                return;
            }

            const Student = _$StudentInformationForm.serializeFormToObject();
            Student.studentDetails = GetStudentDetails();

            _modalManager.setBusy(true);
            _StudentService.createOrEdit(
                Student
            ).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditStudentModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);