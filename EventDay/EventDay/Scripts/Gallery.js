/*
Obsługa galerii
*/

var data2 = [];
var data = new FormData();
var files;

$(document).ready(function () {

    $('#FileGalleryAdd').on('click', function () {

        var data = new FormData();
        var files = $("#FileGallerySearch").get(0).files;
        var Description = document.getElementById('FileGalleryDescription').value;

        var toSplit = location.href;     
        var split = toSplit.split('/');
        var id = split[split.length-1];

        if (files.length > 0) {

            data.append("UploadedImage", files[0]);
            data.append("Description", Description);
            data.append("IdEvent", id);
            data.append("Lp", data2.length);
            data2[data2.length] = data;


            var el = document.getElementById('Gallery');
            var index = data2.length - 1;

            if (el) {
                var HTML = "";
                HTML += '<div id="' + document.getElementById('FileGallerySearch').value + '">';
                HTML += '<p>' + Description + '</p>';
                //HTML += '<textarea rows="4" cols="50" maxlength="50" readonly >' + Description + '</textarea>'
                HTML += '<img src="' + document.getElementById("FileGallery").src + '" id="' + document.getElementById('FileGallerySearch').value + '"/>';
                HTML += '<input id="Delete" type="image" src="/Content/trash.png" alt="usuń" value="usuń" onclick=" $(this).parent().remove(); delete data2[' + index + ']" style=""/>';
                HTML += '</div>';

                el.innerHTML += HTML;
                //data2.splice('+usun22+', 1);
            }
        }
    });
});

function usun(usun) {
}

$(document).ready(function () {

    $('#GalleryAccept').on('click', function () {
        for (var id in data2) {

            var ajaxRequest = $.ajax({
                type: "POST",
                //url: "YourEvents/UploadFile",
                url: "/YourEvents/UploadFile",
                contentType: false,
                processData: false,
                data: data2[id]
            });
        }

        ajaxRequest.done(function (xhr, textStatus) {
            alert("Zakończono zapis");
        });
    });
});