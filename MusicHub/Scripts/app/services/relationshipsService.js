var RelationshipsService = function () {
    var follow = function (artistId, done, fail) {
        $.post("/api/relationships", { artistId: artistId })
                                            .done(done)
                                            .fail(fail);
    };

    var unfollow = function (artistId, done, fail) {
        $.ajax({
            url: "/api/relationships",
            data: { artistId: artistId },
            method: "DELETE"
        })
                .done(done)
                .fail(fail);
    };

    return {
        follow: follow,
        unfollow: unfollow
    };
}();