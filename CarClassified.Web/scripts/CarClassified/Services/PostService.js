var CarClassified = CarClassified || {};

CarClassified.Services = CarClassified.Services || {};

CarClassified.Services.PostService = function () {
    var ASSESTS = "/api/assests"

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
    }

    return self;
}
