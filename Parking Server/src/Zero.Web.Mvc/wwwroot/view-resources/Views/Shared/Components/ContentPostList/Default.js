(function () {
    $(function () {
        let changeListingPostsPage = $('.changeListingPostsPage');

        changeListingPostsPage.on('click', function (e) {
            let viewPage = $(this).data('page');
            
            window.location = window.location.origin + window.location.pathname + '?PostViewingPage=' + viewPage;
            
            e.preventDefault();
        });
    });
})();


