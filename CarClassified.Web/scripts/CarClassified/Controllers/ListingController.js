var CarClassified = CarClassified || {};

CarClassified.Controllers = CarClassified.Controllers || {};

CarClassified.Controllers.ListingController = function (listingService) {
    var self = this;
    var listingService = listingService;
    self.start = function () {
        getListing();
    };

    var getListing = function () {
        var result = listingService.sample();
        $('#listing_table').bootstrapTable({
            data: result
        });
    };
    self.start();
}
