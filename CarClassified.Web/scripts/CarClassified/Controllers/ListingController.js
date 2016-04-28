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
        listingService.getDefault(defaultSuccess, failCallback);
    };

    var getStateListing = function (payload) {
        table.bootstrapTable('load', payload);
    }

    var onDetailHover = function () {
        $('tr').hover(function () {
            $(this).css('cursor', 'pointer');
        });
    };

    var defaultSuccess = function (payload) {
        table.bootstrapTable({
            data: payload,
        });
        self.onListingSelection();
    };
    var failCallback = function () {
        console.log('failed to retrieve data');
    };

    var buildModal = function (payload) {
        //create modal with images
    }

    window.detailEvents = {
        'click .details_button': function () {
            var id = $(this).closest('tr').data('uniqueid');
            listtingService.getListingDetail(id, buildModal, failCallback);
        }
    };

    var onStateSelect = function () {
        $('#states').change(function () {
            var id = $(this).val();
            listingService.getStateListing(id, getStateListing, failCallback)
        });
    }

    self.start();
    return self;
}
