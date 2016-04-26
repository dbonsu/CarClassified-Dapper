var CarClassified = CarClassified || {};

CarClassified.Services = CarClassified.Services || {};

CarClassified.Services.PostService = function () {
    var ASSESTS = "/api/assests"
    var POST_LISTING = "/api/post?hasImage="
    var POST_IMAGE = "/api/post/image"
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

    self.completePost = function (model, successCallback, failureCallback, hasImage) {
        $.ajax({
            url: POST_LISTING + hasImage,
            data: model,
            type: 'POST',
        }).success(function (d, x, t) {
            successCallback();
        }).fail(function (x, t, e) {
            failureCallback();
        });
    }

    self.PostImage = function (formData, successCallBack, failureCallBack) {
        $.ajax({
            url: POST_IMAGE,
            data: formData,
            type: 'POST',
            cache: false,
            contentType: false,
            processData: false
        }).success(function (d, x, t) {
            successCallback();
        });
    }

    return self;
}
