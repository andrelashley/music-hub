var RelationshipsController = function (relationshipsService) {
    var button;

    var init = function (el) {
        $(el).click(toggleRelationship);
    }

    var toggleRelationship = function (e) {
        button = $(e.target);
        var artistId = button.attr("data-artist-id");

        if (button.hasClass("btn-default")) {
            relationshipsService.follow(artistId, done, fail);
        } else {
            relationshipsService.unfollow(artistId, done, fail);
        }
    };

    var done = function () {
        var text = (button.text() === "Follow") ? "Following" : "Follow";
        button.toggleClass("btn-primary").toggleClass("btn-default").text(text);
    };

    var fail = function () {
        alert("Something failed");
    }

    return {
        init: init
    };
}(RelationshipsService);