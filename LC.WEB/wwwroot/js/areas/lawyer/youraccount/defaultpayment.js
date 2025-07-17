var CurrentExecuteFormData = null;

function culqi() {
    if (Culqi.token) {
        var token = Culqi.token.id;
        CurrentExecuteFormData.append("Token", token);
        paymentmethods.submitNewCard();
    } else {
        swal({
            type: "error",
            allowOutsideClick: false,
            title: "Error",
            text: Culqi.error.mensaje,
            confirmButtonText: "Aceptar"
        }).then((result) => {
            window.location.reload();
        });
    }

}
