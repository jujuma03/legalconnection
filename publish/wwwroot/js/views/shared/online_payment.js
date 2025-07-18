var CurrentExecuteUrl = null;
var CurrentExecuteFormData = null;
var CurrentPaymentAmount = null;

function ProcessOnlinePayment(amount, url, formData) {
    Culqi.settings({
        title: 'Legal Connection',
        currency: 'PEN',
        description: 'legalconnection.com',
        amount: amount
    });
    CurrentPaymentAmount = amount;
    CurrentExecuteUrl = url;
    CurrentExecuteFormData = formData;
    Culqi.open();
}

function culqi() {
    var token = Culqi.token.id;
    var email = Culqi.token.email;
    CurrentExecuteFormData.append("Token", token);
    CurrentExecuteFormData.append("Email", email);
    CurrentExecuteFormData.append("Amount", CurrentPaymentAmount);

    mApp.block("body", {
        message: "Procesando pago. Por favor no cierre la página."
    });

    $(":input").attr("disabled", true);
 
    $.ajax({
        url: CurrentExecuteUrl,
        data: CurrentExecuteFormData,
        type: "POST",
        contentType: false,
        processData: false
    })
        .done(function (e) {
            swal({
                type: "success",
                allowOutsideClick: false,
                title: "Éxito!",
                text: "EL PAGO HA SIDO REALIZADA CON ÉXITO.",
                confirmButtonText: "Entendido"
            }).then((result) => {
                if (result) {
                    window.location.reload();
                }
            });
        })
        .fail(function (e) {
            swal({
                type: "error",
                title: "Error al guardar los datos.",
                text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                confirmButtonText: "Aceptar"
            });
        })
        .always(function () {
            mApp.unblock("body");
            $(":input").attr("disabled", false);
        });
}
