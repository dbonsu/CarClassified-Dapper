var CarClassified = CarClassified || {};
/*
 * global object
 */
CarClassified.Controllers = CarClassified.Controllers || {};

/*
 * Initializes listing controller
 * @class
 */
CarClassified.Controllers.ListingController = function (listingService) {
    var self = this;
    var listingService = listingService;
    var table = $('#listing_table');

    /*
     * Initializes functions
     */
    self.start = function () {
        getDefaultListing();
        onStateSelect();
        onCancelButton();
        onContactSeller();
        onHover();
    };

    /*
     * Retrieves default listing -top 20
     */
    var getDefaultListing = function () {
        listingService.getDefault(defaultSuccess, failCallback);
    };

    /*
     * Gets list for a state
     * @param payload
     */
    var getStateListing = function (payload) {
        table.bootstrapTable('load', payload);
    }

    /*
     * Sends seller contact
     */
    var onContactSeller = function () {
        $('#contact_seller_form').submit(function (event) {
            event.preventDefault();

            var buyer = {};
            buyer.postId = parseInt($('#postId').text());
            buyer.name = $('#buyer_name').val();
            buyer.email = $('#buyer_email').val().trim();
            if (isEmailValid(buyer.email)) {
                listingService.contactSeller(buyer, successContact)
            } else {
                $('#buyer_email_error').removeClass('hidden');
                $('#buyer_email_error').html('<p>Please enter a valid email  </p>');
            }
        });
    }

    /*
     *Response for successfull contact
     */
    var successContact = function () {
        $('#detail_modal').modal('hide');
        window.location = '/Listing/Success';
    }

    /*
     * Validates email field
     * @param email <string to validate>
     */
    var isEmailValid = function (email) {
        var reg = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return reg.test(email);
    };

    /*
     * Loads initial car list
     * @param payload <car list>
     */
    var defaultSuccess = function (payload) {
        table.bootstrapTable({
            data: payload,
        });
    };

    var failCallback = function () {
        console.log('failed to retrieve data');
    };

    /*
     *Loads modal on success
     * @param payload <detail object>
     */
    var buildModal = function (payload) {
        var result = payload.images;
        buildDetails(payload);
        buildImages(result, raiseModal);
    }

    /*
     * Raises modal
     */
    var raiseModal = function () {
        $('#detail_modal').modal();
    }

    /*
     * Hides modal on cancel
     */
    var onCancelButton = function () {
        $("#deny_list").click(function () {
            $('#detail_modal').modal('hide');
        });
    };

    /*
     * Creates pointer on hover
     */
    var onHover = function () {
        $("#deny_list").hover(function () {
            $(this).css('cursor', 'pointer');
        });
    };

    /*
     * Builds detail from server object
     * @param detailObject <server object>
     */
    var buildDetails = function (detailObject) {
        $('#postId').html(detailObject.id);
        $('#detail_title').html(detailObject.title);
        $('#detail_year').html(detailObject.year);
        $('#detail_location').html(detailObject.location);
        $('#detail_make').html(detailObject.make);
        $('#detail_model').html(detailObject.model);
        $('#detail_postDate').html(detailObject.postDate);
        $('#detail_phone').html(detailObject.phone);
        $('#detail_title').html(detailObject.title);
        $('#detail_name').html(detailObject.name);
        $('#detail_miles').html(detailObject.miles);
        $('#detail_price').html(detailObject.price);
        $('#detail_bodyStyle').html(detailObject.bodyStyle);
        $('#detail_color').html(detailObject.color);
        $('#detail_transmission').html(detailObject.transmission);
        $('#detail_cylinder').html(detailObject.cylinder);
        $('#detail_condition').html(detailObject.condition);
        $('#detail_fuel').html(detailObject.fuel);
        $('#detail_detail').html(detailObject.body);
    };

    /*
     * Builds base64 images
     * @param images <list of images>
     * @callback <method to call after building images>
     */
    var buildImages = function (images, callback) {
        var html = [];
        var im = "";
        $.each(images, function (key, value) {
            im = '<img src="' + "data:image/" + value.extension + ";base64," + value.body + '" height="150" width="200" alt="vehicle_image" />'
            html.push(im);
        });
        $('#detail_images').html(html);
        callback();
    }

    /*
     * attached to bootrapTable object
     */
    window.detailEvents = {
        'click .details_button': function () {
            var id = $(this).closest('tr').data('uniqueid');
            listingService.getListingDetail(id, buildModal, failCallback);
        }
    };

    /*
     * Gets state id
     */
    var onStateSelect = function () {
        $('#states').change(function () {
            var id = $(this).val();
            listingService.getStateListing(id, getStateListing, failCallback)
        });
    }

    self.start();
    return self;
}
