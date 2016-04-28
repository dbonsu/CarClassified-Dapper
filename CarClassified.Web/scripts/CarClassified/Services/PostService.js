var CarClassified = CarClassified || {};

CarClassified.Services = CarClassified.Services || {};

CarClassified.Services.PostService = function () {
    var ASSESTS = "/api/assests"
    var POST_LISTING = "/api/post?hasImage="
    var POST_IMAGE = "/api/post/image?email="
    var self = this;

    self.test = function (name) {
        console.log(name);
    }
    self.getAssests = function (successCallback, failureCallback) {
        return $.get(ASSESTS, {})
             .done(function (data) {
                 successCallback(data);
             })
         .fail(function () {
             failureCallback();
         });
    };

    self.completePost = function (model, successCallback, failureCallback, hasImage) {
        $.ajax({
            url: POST_LISTING + hasImage,
            data: model,
            type: 'POST',
        }).success(function (d, x, t) {
            successCallback(d, x, t);
        }).fail(function (x, t, e) {
            failureCallback();
        });
    }

    self.PostImage = function (formData, successCallBack, email) {
        $.ajax({
            url: POST_IMAGE + email,
            data: formData,
            type: 'POST',
            cache: false,
            contentType: false,
            processData: false
        }).success(function (d, x, t) {
            successCallBack(d, x, t);
        });
    }

    return self;
}
