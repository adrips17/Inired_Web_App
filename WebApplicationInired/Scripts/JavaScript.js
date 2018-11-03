
    var lat;
    var lon;
    var path;
    var url;

    function initMap() {
        var map = new google.maps.Map(document.getElementById('map'), {
            zoom: 14,
            center: { lat: 39.4561165, lng: -0.3545661 }
        });

        map.addListener('click', function (e) {
            placeMarkerAndPanTo(e.latLng, map);
            lat = e.latLng.lat();
            lon = e.latLng.lng();
        });
    }

function placeMarkerAndPanTo(latLng, map) {
    var marker = new google.maps.Marker({
        position: latLng,
        map: map
    });
    map.panTo(latLng);
}

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('[data-label="UploadImage"]').children('img').attr('src', e.target.result);

        }


        reader.readAsDataURL(input.files[0]);
    }
}

$('#UploadInput').change(function () {
    readURL(this);

    url = this.value;
    url = url.split("\\");
    path = url[2];
    $("#UploadPath").text(path);
});

function enviarUbi(){
//$("#btnCrearUbi").click(function () {

    //Datos del usuario
    var nombre = $('#nombreMarker').val();
    var ubicacion = $('#ubicacionMarker').val();
    var nota = $('#notaMarker').val();
    //var comentario = $('#comentarioMarker').val();
    var descripcion = $('#descripcionMarker').val();



    $.ajax({
        type: "POST",
        url: "/Markers/CrearNuevoMarker",
        data: {
            nombreRecibido: nombre,
            ubicacionRecibida: ubicacion,
            notaRecibida: nota,
            //comentarioRecibido: comentario,
            descripcionRecibida: descripcion,
            latRecibida: lat,
            lonRecibida: lon,


        },
        success: function (data) {
            //var nombre = $('#nombreMarker').val();

            //var pathImagen = path;

            //$.ajax({
            //    type: "POST",
            //    url: "/Images/ImagenesMarkers",
            //    data: {
            //        pathRecibido: pathImagen,
            //        //markerRecibido: id_marker,
            //        nombreRecibido: nombre,

            //    },
            //    success: function (data) {

            //    }
            //})
            alert('Registrada!')
        }

    })
}



