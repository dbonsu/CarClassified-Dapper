var CarClassified = CarClassified || {};

CarClassified.Controllers = CarClassified.Controllers || {};

CarClassified.Controllers.ListingController = function (listingService) {
    var self = this;
    var listingService = listingService;
    var table = $('#listing_table');
    self.start = function () {
        getDefaultListing();
        onStateSelect();
    };

    

    var getDefaultListing = function () {
        var result = listingService.getDefault(defaultSuccess, failCallback);
       
    };

    var getStateListing = function (payload) {
        table.bootstrapTable('load', payload);
    }

    var defaultSuccess = function (payload) {
        table.bootstrapTable({
            data: payload,
        });
    };
    var failCallback = function () {
        console.log('failed to retrieve data');
    };

    var onStateSelect = function () {

        $('#states').change(function () {
            var id = $(this).val();
            listingService.getStateListing(id,getStateListing, failCallback)
         
        });
        
    }
    self.start();
}
