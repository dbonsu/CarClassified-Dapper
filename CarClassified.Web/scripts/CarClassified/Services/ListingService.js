var CarClassified = CarClassified || {};

CarClassified.Services = CarClassified.Services || {};

CarClassified.Services.ListingService = function () {
    var self = this;
    var GET_ALL_LISTING = "api/listings";

    this.getAllListings = function (successCallback, failureCallback) {
        return $.get(GET_ALL_LISTING, {})
            .done(function (data) {
                successCallback(data);
            })
        .fail(function () {
            failureCallback();
        });
    };

    this.sample = function () {
        return [
  {
      "id": 8,
      "year": 1928,
      "title": "car2",
      "location": "there",
      "make": "vw",
      "model": "green",
      "postDate": "2015-07-24T11:57:33 +04:00"
  },
  {
      "id": 1,
      "year": 1933,
      "title": "car2",
      "location": "her",
      "make": "vw",
      "model": "green",
      "postDate": "2015-01-02T11:37:38 +05:00"
  },
  {
      "id": 2,
      "year": 1933,
      "title": "car4",
      "location": "her",
      "make": "audi",
      "model": "green",
      "postDate": "2014-01-29T02:47:53 +05:00"
  },
  {
      "id": 8,
      "year": 2006,
      "title": "car2",
      "location": "everywhere",
      "make": "bmw",
      "model": "blue",
      "postDate": "2016-04-16T05:20:21 +04:00"
  },
  {
      "id": 3,
      "year": 1981,
      "title": "car4",
      "location": "everywhere",
      "make": "vw",
      "model": "green",
      "postDate": "2015-02-20T05:22:54 +05:00"
  }
        ]
    };

    return self;
}
