(function () {
    $(function () {
        const _parkService = abp.services.app.parkPublic;

        let currentOrderId;
        let btnCheckOut = $('#btnCheckOut');

        let btnConfirmOrder = $('#checkoutBtnConfirmOrder');
        let billingForm = $('form[id="billingDetailForm"]');

        let cardSelector = $('#CardId');
        cardSelector.select2({
            placeholder: app.localize('PleaseSelect'), allowClear: true, width: '100%', ajax: {
                url: abp.appPath + "api/services/app/Park/GetPagedCards",
                dataType: 'json',
                delay: 50,
                data: function (params) {
                    return {
                        filter: params.term, skipCount: ((params.page || 1) - 1) * 10,
                    };
                },
                processResults: function (data, params) {
                    params.page = params.page || 1;
                    let res = $.map(data.result.items, function (item) {
                        return {
                            id: item.id, text: item.code + '-' + item.cardNumber,
                        }
                    });

                    return {
                        results: res, pagination: {
                            more: (params.page * 10) < data.result.totalCount
                        }
                    };
                },
                cache: true
            }, language: abp.localization.currentLanguage.name
        })

        function getOrderInfo() {
            let order = {}

            order = $.extend(billingForm.serializeFormToObject(), order);

            return order;
        }

        if (btnConfirmOrder) {
            btnConfirmOrder.on('click', function () {
                let validSelectors = baseHelper.ValidSelectors();

                if (!billingForm.valid() || !validSelectors) {
                    abp.notify.error(app.localize('FP_InvalidData_PleaseCheck'));
                    return;
                }

                let order = getOrderInfo();
                abp.ui.setBusy();
                _parkService.createOrder(order)
                    .done(function (orderId) {
                        currentOrderId = orderId;
                        abp.notify.success(app.localize('FP_CreateOrderSuccessful'));
                        btnConfirmOrder.attr('disabled', 'disabled');
                        btnCheckOut.removeAttr('disabled');
                    })
                    .always(function () {
                        abp.ui.clearBusy();
                    })
            })
        }

        if (btnCheckOut) {
            btnCheckOut.click(function (e) {
                let typePayment = $('.form-check-input:checked').val();
                _parkService.urlPayment(typePayment, currentOrderId)
                    .done(function (paymentUrl) {
                        window.location = paymentUrl;
                    })
            })
        }


    });
})();


