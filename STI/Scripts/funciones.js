$(document).ready(function () {
    $('#habitosNav').show(verifica('Habitos de Estudio', '#habitosNav'));
    $('#autoestimaNav').show(verifica('Autoestima', '#autoestimaNav'));
    $('#comunicacionNav').show(verifica('Oral y Escrito', '#comunicacionNav'));
    $('#mateNav').show(verifica('Matematicas', '#mateNav'));
});
function guardar(id, v) {
    var data = {
        RespuestaID: null,
        preguntaID: id,
        AlumnoID: 0,
        valor: v
    };
    $.ajax({
        type: 'POST',                               // tipo de llamada (POST, GET)
        url: '../Preguntas/guardar',                // el URL del método que vamos a llamar
        data: JSON.stringify(data),           // los parámetros en formato JSON
        contentType: "application/json; charset=utf-8",
        dataType: "json",                           // tipo de datos enviados al servidor
        success: function (data) {                      // función que se va a ejecutar si el pedido resulta exitoso
            console.log(data);
        },
        error: function (req, stat, err) {          // función que se va a ejecutar si el pedido falla
            console.log("error al guardar");
            console.log(id);
        }
    });
};

function login() {
    var data = {
        ClaveID: null,
        login: $("#txtNoCuenta").val(),
        password: $("#txtPass").val(),
    };
    $.ajax({
        type: 'POST',                               // tipo de llamada (POST, GET)
        url: '../home/login',                // el URL del método que vamos a llamar
        data: JSON.stringify(data),           // los parámetros en formato JSON
        contentType: "application/json; charset=utf-8",
        dataType: "json",                           // tipo de datos enviados al servidor
        success: function (data) {                      // función que se va a ejecutar si el pedido resulta exitoso
            if (data == "home")
                location.href = "../home/inicio";
            else if (data == "admin")
                location.href = "../home/inicioAdmin";
            else {
                $("#alert").parent().remove();
                $("#formLogin").prepend("<li><div class='alert alert-danger' role='lert' id='alert'>Usuario y/o contraseña incorrectos</div></li>");
            }
        },
        error: function (req, stat, err) {          // función que se va a ejecutar si el pedido falla                
            console.log("Error en la conexion");
        }
    });
};
function verifica(encuesta, li) {
    var data = {
        EncuestaID: null,
        Descripcion: encuesta
    };
    var ok = $('<span class="glyphicon glyphicon-ok"></span>');    
    var mensaje = $(li+'>a>p');
    $.ajax({
        type: 'POST',                               // tipo de llamada (POST, GET)
        url: '../Encuesta/encuestaTerminada',                // el URL del método que vamos a llamar
        data: JSON.stringify(data),           // los parámetros en formato JSON
        contentType: "application/json; charset=utf-8",
        dataType: "json",                           // tipo de datos enviados al servidor
        success: function (data) {                      // función que se va a ejecutar si el pedido resulta exitoso
            if (data == "true") {
                mensaje.append(ok);
                mensaje.addClass('text-success');
                mensaje.parent().attr("href", "#");
            }
        },
        error: function (req, stat, err) {          // función que se va a ejecutar si el pedido falla           
        }
    });
};




