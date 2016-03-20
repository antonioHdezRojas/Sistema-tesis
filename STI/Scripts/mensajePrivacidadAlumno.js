function mensaje(id) {
    var aviso;
    if (confirm("La información que se genera a partir de tus respuestas es completamente confidencial y será usada únicamente con fines académicos.") == true) {
        aviso = true;
    } else {
        aviso = false;
    }
    enviar(aviso);  
    location.reload(true);
};
function enviar(a) {
    var actiondata = "{ 'a':'" + a + "'}";
    $.ajax({
        type: 'POST',                               // tipo de llamada (POST, GET)
        url: '../home/avisoPrivacidad',                // el URL del método que vamos a llamar
        data: actiondata,           // los parámetros en formato JSON
        contentType: "application/json; charset=utf-8",
        dataType: "json",                           // tipo de datos enviados al servidor
        success: function (data) {                      // función que se va a ejecutar si el pedido resulta exitoso
            cosole.log(data);
        },
        error: function (req, stat, err) {          // función que se va a ejecutar si el pedido falla                
            console.log("Error en la conexion");
        }
    });
};
