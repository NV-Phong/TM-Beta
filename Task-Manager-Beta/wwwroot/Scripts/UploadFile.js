    $(function () {
        $('#btnUpload').click(function () {
            $('#fileUpload').trigger('click');
        });
            });
    $('#fileUpload').change(function () {
                if (window.FormData !== undefined) {
                    var fileUpload = $('#fileUpload').get(0);
    var files = fileUpload.files;
    var formData = new FormData();
    formData.append('file', files[0]);
    $.ajax(
    {
        type: 'POST',
    url: '/Sach/ProcessUpload',
    contentType: false,
    processData: false,
    data: formData,
    success: function (urlImage) {
        $('#pictureUpload').attr('src', urlImage);
    $('#hinh').val(urlImage);
                            },
    error: function (err) {
        alert('Error ', err.statusText);
                            }
                        });
                }
    });


document.getElementById("fileUpload").addEventListener("change", function () {
    var reader = new FileReader();

    reader.onload = function (e) {
        var selectedImage = document.getElementById("selectedImage");
        selectedImage.src = e.target.result;
        selectedImage.style.display = "block";
    };

    reader.readAsDataURL(this.files[0]);
});