(function ($) {
    app.modals.CreateOrEditResidentModal = function () {
        const _ResidentService = abp.services.app.resident;

        let _modalManager;
        let _$ResidentInformationForm = null;

        let modal;

        let residentDetailTable;
        let addResidentDetailBtn;
        let residentDetail;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            modal = _modalManager.getModal();

            _modalManager.initControl();

            residentDetail = modal.find('#Resident_ResidentDetail');

            residentDetailTable = modal.find('#ResidentDetailTable');
            addResidentDetailBtn = modal.find('#btnAddDetailResidentDetail');

            if (addResidentDetailBtn) {
                addResidentDetailBtn.on('click', function () {
                    $.get(abp.appPath + 'Park/Resident/NewResidentDetail').then(function (res) {
                        residentDetailTable.find('#LastDetailRowResidentDetail').before(res);
                        baseHelper.RefreshUI(residentDetailTable);
                        InitResidentDetailSelector();
                    });
                });
            }

            residentDetailTable.on('click', '.btnDeleteDetail', function () {
                let rowId = $(this).attr('rowId');
                residentDetailTable.find('.detailRow[rowId="' + rowId + '"]').remove();
                baseHelper.RefreshUI(residentDetailTable);
            });

            baseHelper.RefreshUI(residentDetailTable);

            InitResidentDetailSelector();

            _$ResidentInformationForm = _modalManager.getModal().find('form[name=ResidentInformationsForm]');
            _$ResidentInformationForm.validate();
        };

        function InitResidentDetailSelector() {
            if (residentDetailTable) {
                residentDetailTable.find('.detailRow').each(function () {
                    let rowId = $(this).attr('rowId');

                    let cardSelector = residentDetailTable.find('.cardSelector[rowId="' + rowId + '"][initSelector="false"]');
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

        function GetResidentDetails() {
            let details = [];
            if (residentDetailTable) {
                residentDetailTable.find('.detailRow').each(function () {
                    let rowId = $(this).attr('rowId');
                    details.push({
                        id: residentDetailTable.find('.detailId[rowId="' + rowId + '"]').val(),
                        cardId: residentDetailTable.find('.cardSelector[rowId="' + rowId + '"]').val(),
                        note: residentDetailTable.find('.detailNote[rowId="' + rowId + '"]').val(),
                    })
                });
            }
            return details;
        }

        this.save = function () {
            if (!_$ResidentInformationForm.valid()) {
                return;
            }

            const Resident = _$ResidentInformationForm.serializeFormToObject();
            Resident.residentDetails = GetResidentDetails();

            _modalManager.setBusy(true);
            _ResidentService.createOrEdit(
                Resident
            ).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditResidentModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);