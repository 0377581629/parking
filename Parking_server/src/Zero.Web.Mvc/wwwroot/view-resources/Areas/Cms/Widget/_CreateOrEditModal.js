(function ($) {
    app.modals.CreateOrEditWidgetModal = function () {

        const _WidgetsService = abp.services.app.widget;
        const _CmsService = abp.services.app.cms;

        let _modalManager;
        let _$WidgetInformationForm = null;

        let modal;
        let asyncLoad;
        let contentType;
        let contentCount;

        let jsPlainText;
        let cssPlainText;

        this.init = function (modalManager) {
            _modalManager = modalManager;
            modal = _modalManager.getModal();
            _modalManager.initControl();
            
            contentType = modal.find('#ContentType');
            contentCount = modal.find('#Widget_ContentCount');
            contentType.on('change', function () {
                if (contentType.val() === '1')
                    contentCount.attr('readonly', true);
                else
                    contentCount.attr('readonly', false);
            });
            asyncLoad = modal.find('#Widget_AsyncLoad');
            asyncLoad.on('change', function () {
                if ($(this).prop('checked') === true) {
                    modal.find('.asyncLoadGroup').removeClass('hidden');
                } else {
                    modal.find('.asyncLoadGroup').addClass('hidden');
                }
            });
            
            jsPlainText = CodeMirror.fromTextArea(document.getElementById("Widget_JsPlain"), {
                mode: "javascript",
                styleActiveLine: true,
                lineNumbers: true,
                matchBrackets: true,
                scrollbarStyle: 'simple',
                autoRefresh: true
            });

            cssPlainText = CodeMirror.fromTextArea(document.getElementById("Widget_CssPlain"), {
                mode: "css",
                styleActiveLine: true,
                lineNumbers: true,
                matchBrackets: true,
                scrollbarStyle: 'simple',
                autoRefresh: true
            });

            _$WidgetInformationForm = _modalManager.getModal().find('form[name=WidgetInformationsForm]');
            _$WidgetInformationForm.validate();
        };

        this.save = function () {
            if (!_$WidgetInformationForm.valid()) {
                return;
            }

            const Widget = _$WidgetInformationForm.serializeFormToObject();
            Widget.jsPlain = jsPlainText.getValue();
            Widget.cssPlain = cssPlainText.getValue();
            
            _modalManager.setBusy(true);
            _WidgetsService.createOrEdit(
                Widget
            ).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditWidgetModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);