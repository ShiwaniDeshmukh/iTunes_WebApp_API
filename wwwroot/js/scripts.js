$(document).ready(function () {
    $('.track-link').on('click', function (e) {
        e.preventDefault();
        var trackId = $(this).data('track-id');
        var clickCountElement = $('#click-count-' + trackId);

        $.ajax({
            url: '/Songs/TrackClick',
            type: 'POST',
            data: { trackId: trackId },
            success: function () {
                var clickCount = parseInt(clickCountElement.text()) + 1;
                clickCountElement.text(clickCount);
            }
        });
    });
});
