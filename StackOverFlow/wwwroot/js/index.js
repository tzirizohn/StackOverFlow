$(() => {

    $("#like-question").on('click', function () {

        $("#like-question").attr('disabled', true);

        const like = {
            questionId: $("#like-question").data("questionid"),
            userId: $("#like-question").data("userid")
        };


        $.post("/home/addlike", like, function () {
            console.log('hi');
        });

    });

    setInterval(() => {
        const questionId = $('#like-question').data('questionid');
        $.get('/home/numberoflikes', { questionId }, function (likes) {
            document.getElementById('likes').innerHTML = likes;
        })
    }, 1000);
});