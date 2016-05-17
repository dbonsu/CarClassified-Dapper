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
        var errorMessage = "<div id=\"typeerror\" class=\"alert alert-danger\">" +
                               "<p>Please choose a valid file type(e.g. jpeg,png)</p>" +
                               "</div>";
        var fileSize = "<div id=\"sizerror\" class=\"alert alert-danger\">" +
                               "<p>Please limit the size of image to 2MB</p>" +
                               "</div>";

        var reg = /(.*?)\.(tif|tiff|gif|jpeg|jpg|jif|png)$/i;
        $(':file').change(function (e) {
            var parent = $(this).closest('.errordiv');
            $('.alert.alert-danger').remove();

            if (!reg.test($(this).val())) {
                
                (parent).append(errorMessage);
                $(this).val('');
                return;
            } else {
                $('#typeerror').remove();
            }
            if (isSizeOk()) {
                $('#sizeerror').remove();
            } else {
                $(this).val('');
            }
          
            function isSizeOk(){
                var imageSize = e.target.files[0].size;
                if (imageSize > 2048000) {
                    (parent).append(fileSize);
                    return false
                }
                return true;
            }
        });
    }

    self.start();
    return self;
}
