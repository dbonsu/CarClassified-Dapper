//globacl object
var CarClassified = CarClassified || {};

CarClassified.Controllers = CarClassified.Controllers || {};
/*
 * Handles image uploading
 *
 */
CarClassified.Controllers.ImageController = function (postService) {
    var self = this;
    var postService = postService;

    /**
     * initializes this contollers
     */
    self.start = function () {
        self.sendImages();
        onFileUpload();
    }

    /*
     * sends image
     */
    self.sendImages = function () {
        $('#image_form').submit(function (event) {
            event.preventDefault();

            var formData = new FormData();
            $.each($(':file'), function (key, val) {
                var file = $(this)[0].files[0];
                if (file !== null && file !== undefined) {
                    formData.append(val.id, file);
                }
            });
            var email = window.sessionStorage.getItem('tempEmail');

            postService.PostImage(formData, postSuccess, email);
        });
    };

    /*
     * Redirects to post/ok
     */
    var postSuccess = function () {
        window.location = "/Post/Ok";
    }

    /*
     * validates file uploads
     */
    var onFileUpload = function () {
        var errorMessage = "<div class=\"alert alert-danger\">" +
                               "<p>Please choose a valid file type(e.g. jpeg,png)</p>" +
                               "</div>";
        var reg = /(.*?)\.(tif|tiff|gif|jpeg|jpg|jif|png)$/i;
        $(':file').change(function () {
            var parent = $(this).closest('.error');

            if (reg.test($(this).val())) {
                $('.alert').remove();
                return;
            }
            (parent).append(errorMessage);
        });
    }

    self.start();
    return self;
}