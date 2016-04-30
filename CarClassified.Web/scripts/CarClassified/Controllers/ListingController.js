var CarClassified = CarClassified || {};

CarClassified.Controllers = CarClassified.Controllers || {};

CarClassified.Controllers.ListingController = function (listingService) {
    var self = this;
    var listingService = listingService;
    var table = $('#listing_table');
    self.start = function () {
        getDefaultListing();
        onStateSelect();
        onCancelButton();
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
    };
    var failCallback = function () {
        console.log('failed to retrieve data');
    };

    var buildModal = function (payload) {
        //create modal with images
        var result = payload.images;

        buildImages(result);
        $('#detail_modal').modal();
        //var xx = result[0];
        //$('#image_1').attr('src' , "data:image/" + xx.extension + ";base64," + xx.body);
    }

    var beforeSend = function (payload) {
        $('#detail_modal').modal();
        //create modal with images
        var result = payload.images;

        buildImages(result);
        //var xx = result[0];
        //$('#image_1').attr('src' , "data:image/" + xx.extension + ";base64," + xx.body);
    }
    var onCancelButton = function () {
        $("#deny_list").click(function () {
            $('#detail_modal').modal('hide');
        });
    };
    var buildDetails = function (detailObject) {
    };

    var buildImages = function (images) {
        var html = [];
        var im ="";
        $.each(images, function (key, value) {
                
               im  ='<img src=""' + "data:image/" + value.extension + ";base64" + value.body + ' height="200" width="250" alt="vehicle_image" />'
               html.push(im);
           
        });
        $('#detail_images').html(html);
    }

    window.detailEvents = {
        'click .details_button': function () {
            var id = $(this).closest('tr').data('uniqueid');
            listingService.getListingDetail(id, buildModal, failCallback, beforeSend);
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
