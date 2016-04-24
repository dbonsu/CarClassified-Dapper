var CarClassified = CarClassified || {};

CarClassified.Services = CarClassified.Services || {};

CarClassified.Services.PostService = function () {
    var ASSESTS = "/api/assests"
    var POST_LISTING = "/api/post"
    var self = this;

    self.test = function (name) {
        console.log(name);
    }
    self.getAssests = function (successCallback, failureCallback) {
        return $.get(ASSESTS, {})
             .done(function (data) {
                 successCallback(data);
             })
         .fail(failureCallback);
    };

    self.completePost = function (model, successCallback, failureCallback) {
        $.ajax({
            url: POST_LISTING,
            data: model,
            type: 'POST',
        }).success(function (d, x, t) {
            successCallback();
        }).fail(function (x, t, e) {
            failureCallback();
        });
    }

    return self;
}