var CarClassified = CarClassified || {};

CarClassified.Services = CarClassified.Services || {};

CarClassified.Services.ListingService = function () {
    var self = this;
    var GET_DEFAULT_LISTING = "/api/listings";
    var GET_STATE_LISTING = "/api/listings?stateId=";
    var GET_LISTING_DETAILS = "/api/listings/details?id=";
    var CONTACT_SELLER = "/api/listings/contact";

    this.getDefault = function (successCallback, failureCallback) {
        return $.get(GET_DEFAULT_LISTING, {})
            .done(function (data) {
                successCallback(data);
            })
        .fail(function () {
            failureCallback();
        });
    };

    this.getStateListing = function (stateId, successCallback, failureCallback) {
        return $.get(GET_STATE_LISTING + stateId, {})
            .done(function (data) {
                successCallback(data);
            })
        .fail(function () {
            failureCallback();
        });
    };

    this.getListingDetail = function (listingId, successCallback, failureCallback) {
        return $.ajax({
            type: 'GET',
            url: GET_LISTING_DETAILS + listingId,

            error: failureCallback,
            success: successCallback
        });
    };

    this.contactSeller = function (postObj, successCallback) {
        return $.ajax({
            type: 'POST',
            url: CONTACT_SELLER,
            data: postObj,
            success: successCallback
        });
    };
    return self;
}