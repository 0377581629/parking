(function () {
    $(function () {
    const _postService = abp.services.app.post;
    let postForm = $('#PostForm');
    let saveButton = $('#SaveButton');
    let _imageHolder = $('#AvatarHolder');
    let CancelAvatar = $('#CancelAvatar');
    let ChangeAvatar = $('#ChangeAvatar');
    let _imageValue = $('#Avatar');
    let backToListingPage = $('#backToListingPage');

    let _$PostForm = null;
    _$PostForm = $('.form-validation-post');
    _$PostForm.validate();

    new FroalaEditor('#Description', frEditorConfig);

    ChangeAvatar.on('click', function () {
        _fileManagerModal.open({
            allowExtension: "*.jpg;*.png;*.jpeg",
            maxSelectCount: 1
        }, function (selected) {
            if (selected !== undefined && selected.length >= 1) {
                if (_imageValue) _imageValue.val(selected[selected.length - 1].path);
                if (_imageHolder) _imageHolder.attr('src', selected[selected.length - 1].path);
            }
        });
    });

    CancelAvatar.on('click', function () {
        _imageHolder.attr('src', '');
        _imageValue.val('')
    });

    backToListingPage.on('click', function () {
        window.location = '/Cms/Post';
    });

    saveButton.on('click', function () {
        let selectorsValid = baseHelper.ValidSelectors();
        
        if (!_$PostForm.valid() || !selectorsValid) {
            return;
        }
        
        let post = postForm.serializeFormToObject();
        _postService.createOrEdit(
            post
        ).done(function () {
            abp.notify.info(app.localize('SavedSuccessfully'));
            window.location = '/Cms/Post/Index';
        }).always(function () {
        });
    });
    });
})();

