var CarClassified = CarClassified || {};

CarClassified.Controllers = CarClassified.Controllers || {};

CarClassified.Controllers.ImageController = function (postService) {
    var self = this;
    var postService = postService;

    self.start = function () {
        self.sendImages();
        onFileUpload();
    }

    self.sendImages = function () {
        $('#image_form').submit(function (event) {
            event.preventDefault();

            var formData = new FormData();
            formData.append('image_one', $('#image_one')[0].files[0]);
            formData.append('image_two', $('#image_two')[0].files[0]);
            formData.append('image_three', $('#image_three')[0].files[0]);
            postService.PostImage(formData, postSuccess);
        });
    };

    var postSuccess = function () {
        window.location = "/Post/Ok";
    }

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
