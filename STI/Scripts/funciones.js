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
                location.href = "../adminHome/index";
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
                dir = "../Reportes/"+mensaje.parent().data('link');
                mensaje.parent().attr("href", dir);
            }
        },
        error: function (req, stat, err) {          // función que se va a ejecutar si el pedido falla           
        }
    });
};

//graficas

function drawChartAutoestima() {

    var data = google.visualization.arrayToDataTable([
      ['Resultados Autoestima', 'Maximo','Obtenido' ],
      ['Calificacion', 80, parseInt($('#txtGeneral').data('cal'))],
      ['General', 28, parseInt($('#txtCatGeneral').data('cal'))],
      ['Familia', 24, parseInt($('#txtCatFamilia').data('cal'))],
      ['Escolar', 28, parseInt($('#txtCatEscolar').data('cal'))]
    ]);

    var options = {
        title: 'Resultados',
        vAxis: { gridlines: { count: 7 } },
        hAxis:{ viewWindow : {min: 0, max: 80}, ticks:[10,20,30,40,50,60,70,80] }
    };

    var chart = new google.charts.Bar(document.getElementById('chartAutoestima'));

    chart.draw(data, options);
}
function drawChartHabitosEstudio() {

    var data = google.visualization.arrayToDataTable([
      ['Resultado habitos de estudio','Maximo','Obtenido'],    
      ['Calificacion', 120,parseInt($('#txtGeneral').data('cal'))],
      ['Concentracion',20, parseInt($('#catConcentracion').data('cal'))],
      ['Relaciones interpersonales',20, parseInt($('#catRelInter').data('cal'))],
      ['Memoria',20, parseInt($('#catMemoria').data('cal'))],
      ['Motivacion para estudiar',20, parseInt($('#motEstudio').data('cal'))],
      ['Administracion del tiempo',20, parseInt($('#catAdmin').data('cal'))],
      ['Presentacion de evaluaciones',20, parseInt($('#catEva').data('cal'))]
    ]);

    var options = {
        title: 'Resultados'
    };

    var chart = new google.charts.Bar(document.getElementById('chartHabitos'));

    chart.draw(data, options);
}

//archivo
function getUrlArchivo() {
    var file = document.getElementById("archivo");
    console.log(file[0].url);
}