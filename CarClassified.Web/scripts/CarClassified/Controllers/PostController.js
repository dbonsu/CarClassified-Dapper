var CarClassified = CarClassified || {};

/*
 * Global object
 */
CarClassified.Controllers = CarClassified.Controllers || {};

/*
 * @class
 */
CarClassified.Controllers.PostController = function (postService) {
    var postService = postService;
    var self = this;
    var bodySelect = $('#bodystyle');
    var colorSelect = $('#color');
    var cylinderSelect = $('#cylinder');
    var fuelSelect = $('#fuelType');
    var makeSelect = $('#make');
    var modelSelect = $('#model');
    var transmissionSelect = $('#transmission');
    var conditionSelect = $('#condition');
    var posting_form = $('#posting_form');
    var wantImage = false;
    var postingBtn = $('#postingBtn');
    var payload = {};

    /*
     * Initializes object
     */
    self.start = function () {
        postService.getAssests(sucessCallback, failureCallBack);
        rebuildMake();
        this.sendPost();
    }

    /*
     * Generates model
     */
    var generateModelWithText = function () {
        var postModel = {};
        postModel.id = $('#userId').val();
        postModel.firstName = $('#firstName').val().trim();
        postModel.lastName = $('#lastName').val().trim();
        postModel.phone = $('#phone').val().trim();
        postModel.location = $('#location').val().trim();
        postModel.bodyStyle = $('#bodystyle option:selected').text().trim();
        postModel.color = $('#color option:selected').text().trim();
        postModel.cylinder = $('#cylinder option:selected').text().trim();
        postModel.make = $('#make option:selected').text().trim();
        postModel.fuel = $('#fuelType option:selected').text().trim();
        postModel.model = $('#model option:selected').text().trim();
        postModel.transmission = $('#transmission option:selected').text().trim();
        postModel.condition = $('#condition option:selected').text().trim();
        postModel.details = $("#details").val().trim();
        postModel.title = $("#title").val().trim();
        postModel.year = $('#year').val();
        postModel.miles = $('#miles').val();
        postModel.price = $('#price').val();
        postModel.email = $('#username').text().trim();
        return postModel;
    };

    /*
     * Sends model
     */
    this.sendPost = function () {
        posting_form.submit(function (event) {
            event.preventDefault();
            var generatedModel = generateModelWithText();
            if (isYearValid(generatedModel.year)) {
                generateModal(generatedModel);
            } else {
                $('#yearError').removeClass("hidden");
                $('#yearError').html("Please enter a valid year (1900-2999)")
            }
        });
    };

    /*
     * Generates modal
     */
    var generateModal = function (model) {
        $('#checkImageModal').modal();
        $('#confirm_image').click(function () {
            postService.completePost(model, postSuccess, postFail, true);
            $('#checkImageModal').modal('hide');
        });
        $('#deny_image').click(function () {
            $('#checkImageModal').modal('hide');
            postService.completePost(model, postSuccess, postFail, false);
        });
    };

    /*
     * Process successful account creation
     */
    var postSuccess = function (d, x, t) {
        //201 redirect to created success page
        //200 post is stored, redirect to image

        if (t.status == 201) {
            window.location = "/Post/Ok";
        } else {
            window.sessionStorage.setItem("tempEmail", d);
            window.location = "/Post/Image";
        }
    };

    /*
     * Validates year field
     * @param yearValue <value to validate>
     */
    var isYearValid = function (yearValue) {
        var re = /^(?:19|20)\d{2}$/;

        if (re.test(yearValue)) {
            return true;
        }
        return false;
    };

    var postFail = function () {
        console.log("failed to post")
    }

    /*
     * Builds a dropdowns from object
     * @param data <object to build>
     */
    var sucessCallback = function (data) {
        payload = data;

        buildSelectDropdown(payload.bodyStyles, bodySelect);
        buildSelectDropdown(payload.colors, colorSelect);
        buildSelectDropdown(payload.cylinders, cylinderSelect);
        buildSelectDropdown(payload.fuelTypes, fuelSelect);
        buildSelectDropdown(payload.makes, makeSelect);
        buildSelectDropdown(payload.makes[0].models, modelSelect);
        buildSelectDropdown(payload.transmissions, transmissionSelect);
        buildSelectDropdown(payload.conditions, conditionSelect);
    }
    var failureCallBack = function () {
        console.log('failed to retrieve all assests');
    }

    /*
     * Builds a dropdown
     * @param item <data to build from>
     * @param element <element to attach>
     */
    var buildSelectDropdown = function (item, element) {
        $.each(item, function (key, value) {
            element.append($('<option></option>').val(value.id).html(value.name || value.number));
        });
    };

    /*
     * Rebuilds the make and model fields
     */
    var rebuildMake = function () {
        makeSelect.change(function () {
            modelSelect.html('');
            var thisId = parseInt((makeSelect).val());
            var thisAr = [];
            for (var i = 0; i < payload.makes.length; i++) {
                if (payload.makes[i].id === thisId) {
                    thisAr = payload.makes[i].models;
                    buildSelectDropdown(thisAr, modelSelect);
                    return;
                }
            }
        })
    };
    self.start();
    return self;
}
