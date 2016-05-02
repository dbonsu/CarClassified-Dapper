var CarClassified = CarClassified || {};

/*
 * Global object
 */
CarClassified.Services = CarClassified.Services || {};

/*
 * @class
 */
CarClassified.Services.PostService = function () {
    var ASSESTS = "/api/assests"
    var POST_LISTING = "/api/post?hasImage="
    var POST_IMAGE = "/api/post/image?email="
    var self = this;

    /*
     * Gets assests (states, colors, etc)
     * @callback successCallback <success>
    * @callback failureCallback <failure>
     */
    self.getAssests = function (successCallback, failureCallback) {
        return $.get(ASSESTS, {})
             .done(function (data) {
                 successCallback(data);
             })
         .fail(function () {
             failureCallback();
         });
    };

    /*
     * Gets listing for a state
     * @param model <form object>
     * @callback successCallback <success>
     * @callback failureCallback <failure>
     * @param hasImage <bool to specify if user want to load image>
     */
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
    /*
     * Gets listing for a state
     * @param formData <form object>
     * @callback successCallback <success>
     * @param email <email of poster>
     */
    self.PostImage = function (formData, successCallback, email) {
        $.ajax({
            url: POST_IMAGE + email,
            data: formData,
            type: 'POST',
            cache: false,
            contentType: false,
            processData: false
        }).success(function (d, x, t) {
            successCallback(d, x, t);
        });
    }

    return self;
}
