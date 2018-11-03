

    var map;
var locat;
var images;
var nombreMarker;


function initMap(latDb, longDb) {
    map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: latDb != null ? latDb : 39.4561165, lng: longDb != null ? longDb : -0.3545661 },
        zoom: 14
    });



}
function GoLocation(locationId) {
    $.get("/Markers/GetAllLocationById?id=" + locationId, function (data, status) {
        initMap(data.lat, data.lon);
        marker = new google.maps.Marker({
            position: { lat: data.lat, lng: data.lon },
            map: map
        });
        locat = locationId;
        nombreMarker = data.nombre;



        if (nombreMarker != null) {
            document.getElementById("nombreMarker").innerHTML = "" + data.nombre + "";
            document.getElementById("nombre").innerHTML = "NOMBRE DEL MARKER";
        }


        var ubicacionMarker = data.ubicacion;
        if (ubicacionMarker != null) {
            document.getElementById("ubicacionMarker").innerHTML = "" + data.ubicacion + "";
            document.getElementById("ubi").innerHTML = "UBICACIÓN DEL MARKER";
        }

        var notaMarker = data.nota;
        if (nota.toString() != null) {
            document.getElementById("notaMarker").innerHTML = "" + data.nota + "";
            document.getElementById("nota").innerHTML = "NOTA";
        }
    })
}

//MODAL COMENTARIOS

function btnNuevoComentario() {
    if (locat != null) {
        $(document).ready(function () {
            $("#modalComentario").modal("show");
        });
    } else {
        alert('Primero debes seleccionar una ubicación')
    }
}
//$("#btnNuevoComentario").click(function () {
//    if (locat != null) {
//        $(document).ready(function () {
//            $("#modalComentario").modal("show");
//        });
//    } else {
//        alert('Primero debes seleccionar una ubicación')
//    }
//});

function btnEnviarComentario() {
    var nombreMarkerr = nombreMarker;
    var comentario = $('#comentario_ubicacion').val();
    var usuLogueado = '@Session["usuarioUsu"]';


    $.ajax({
        type: "POST",
        url: "/Coments/EnviarComentario",
        data: {
            nombreMarkerRecibido: nombreMarkerr,
            comentarioRecibido: comentario,
            usuarioRecibido: usuLogueado,
        },
        success: function (data) {
            //location.href = "http://localhost:50381/";
            alert("Enviado!");
            $(document).ready(function () {
                $("#modalComentario").modal("hide");
            });
        }
    })
}
//$("#btnEnviarComentario").click(function () {
//    var nombreMarkerr = nombreMarker;
//    var comentario = $('#comentario_ubicacion').val();
//    var usuLogueado = '@Session["usuarioUsu"]';


//    $.ajax({
//        type: "POST",
//        url: "/Coments/EnviarComentario",
//        data: {
//            nombreMarkerRecibido: nombreMarkerr,
//            comentarioRecibido: comentario,
//            usuarioRecibido: usuLogueado,
//        },
//        success: function (data) {
//            //location.href = "http://localhost:50381/";
//            alert("Enviado!");
//            $(document).ready(function () {
//                $("#modalComentario").modal("hide");
//            });
//        }
//    })
//});


function verComentarios() {

    if (locat != null) {
        var nomMark = nombreMarker;
        $.ajax({
            type: "POST",
            url: "/Coments/ComentariosPorMarker",
            data: {
                nombreRecibido: nomMark,
            },
            success: function (listaComentarios) {

                $(document).ready(function () {
                    listaComents = listaComentarios;
                    if (listaComents.length == 0) {
                        var t = "No hay comentarios";
                        document.getElementById("body_card").innerHTML = "" + t + "";
                    } else {
                        var i = 0;
                        for (i; i < listaComents.length; i++) {
                            var text = "<div class='card' style='background-color:#c74a4a'><div class='container'style='margin-bottom:10px'>" +
                                       "<h4 style='color:#fff'><b>" + listaComents[i] + "</b></h4></div></div>";
                            document.getElementById("body_card").innerHTML = "" + text + "";
                        }

                        $("#modalComentarioPorMarker").modal("show");
                    }

                });
            }


        })
    } else {
        alert('Selecciona primero una ubicación')
    }
}
//$("#btnVerComentarios").click(function () {
//    var nomMark = nombreMarker;
//    $.ajax({
//        type: "POST",
//        url: "/Coments/ComentariosPorMarker",
//        data: {
//            nombreRecibido: nomMark,
//        },
//        success: function (listaComentarios) {

//            $(document).ready(function () {
//                listaComents = listaComentarios;
//                var i;
//                for (i = 0; i < listaComents.length; i++) {
//                    var text = text + "<div class='form-group'><div class='card' style='background-color:#c74a4a'><div class='container' style='margin-bottom:10px'>" +
//                "<h4 style='color:#fff'><b>" + listaComents[i] + "</b></h4></div></div></div>"
//                }
//                document.getElementById("body_card").innerHTML = "" + text + "";
//                $("#modalComentarioPorMarker").modal("show");

//            });

//        }
//    })
//});








