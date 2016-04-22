var CarClassified = CarClassified || {};

CarClassified.Controllers = CarClassified.Controllers || {};

CarClassified.Controllers.PostController = function (postService) {
    var postService = postService;
    var self = this;
    var bodySelect = $('#bodystyle');
    var colorSelect = $('#color');
    var cylinderSelect = $('#cylinder');

    self.start = function () {
        postService.getAssests(sucessCallback, failureCallBack);
    }
    var sucessCallback = function (data) {
        var all = data;
        var bodystles = all.bodyStyles;
    }
    var failureCallBack = function () {
        console.log('failed to retrieve all assessts');
    }

    self.start();
    return self;
}
