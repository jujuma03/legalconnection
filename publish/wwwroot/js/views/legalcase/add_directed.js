var add = function () {
    var google = {
        attach: function () {
            gapi.load('auth2', function () {
                // Retrieve the singleton for the GoogleAuth library and set up the client.
                auth2 = gapi.auth2.init({
                    client_id: GOOGLE_API_ID,
                    cookiepolicy: 'single_host_origin'
                });

                auth2.attachClickHandler(document.getElementById('customBtn'), {},
                    function (googleUser) {
                        var user = auth2.currentUser.get().getBasicProfile().getName();
                        var id_token = googleUser.getAuthResponse().id_token;

                        $("#client_register_data").addClass("d-none");
                        $("#client_register_data").find(":input").attr("disabled", true);
                        $("#normal_register_div").addClass("d-none");
                        $("#google_user_div").removeClass("d-none");
                        $("#google_user_name").text(user);

                        $("#Client_GoogleTokenId").val(id_token);

                    }, function (error) {
                    });
            });
        },
        logOut: function () {
            $("#logout_google").click(function () {
                var auth2 = gapi.auth2.getAuthInstance();

                auth2.signOut().then(function () {
                    $("#client_register_data").removeClass("d-none");
                    $("#client_register_data").find(":input").attr("disabled", false);
                    $("#normal_register_div").removeClass("d-none");
                    $("#google_user_div").addClass("d-none");
                    $("#google_user_name").text("");

                    $("#Client_GoogleTokenId").val("");
                });
            });
        },
        init: function () {
            this.attach();
            this.logOut();
        }
    };

    var select2 = {
        department: {
            load: function () {
                $("#DepartmentId").select2({
                    ajax: {
                        url: "/departamentos/get/v2",
                        delay: 500,
                        data: function (params) {
                            var query = {
                                page: params.page || 1,
                                term: params.term
                            };
                            return query;
                        }
                    },
                    minimumInputLength: 0,
                    placeholder: 'Seleccione departamento',
                    allowClear: false
                });

                if ($("#DepartmentId_value").val() !== "" && $("#DepartmentId_value").val() !== null) {
                    this.setValue($("#DepartmentId_value").val(), $("#Department_value").val());
                }
            },
            setValue: function (id, text) {
                if ($('#DepartmentId').find("option[value='" + id + "']").length) {
                    $('#DepartmentId').val(id).trigger('change');
                } else {
                    var newOption = new Option(text, id, true, true);
                    $('#DepartmentId').append(newOption).trigger('change');
                }
            },
            events: {
                onChange: function () {
                    $("#DepartmentId").on("change", function () {
                        select2.province.clear();
                    });
                },
                init: function () {
                    this.onChange();
                }
            },
            init: function () {
                this.load();
                this.events.init();
            }
        },
        province: {
            load: function () {
                $("#ProvinceId").select2({
                    ajax: {
                        url: "/provincias/get/v2",
                        delay: 500,
                        data: function (params) {
                            var query = {
                                departmentId: $("#DepartmentId").val(),
                                page: params.page || 1,
                                term: params.term
                            };
                            return query;
                        }
                    },
                    minimumInputLength: 0,
                    placeholder: 'Seleccione provincia',
                    allowClear: true
                });


                if ($("#ProvinceId_value").val() !== "" && $("#ProvinceId_value").val() !== null) {
                    this.setValue($("#ProvinceId_value").val(), $("#Province_value").val());
                }
            },
            setValue: function (id, text) {
                if ($('#ProvinceId').find("option[value='" + id + "']").length) {
                    $('#ProvinceId').val(id).trigger('change');
                } else {
                    var newOption = new Option(text, id, true, true);
                    $('#ProvinceId').append(newOption).trigger('change');
                }
            },
            clear: function () {
                $("#ProvinceId").val(null).trigger("change");
            },
            init: function () {
                this.load();
            }
        },
        specialityTheme: {
            load: function () {
                $("#SpecialityThemeId").select2({
                    placeholder: 'Seleccione tema de especialidad',
                });

                $("#SpecialityThemeId").val(null).trigger("change");
            },
            init: function () {
                this.load();
            }
        },
        init: function () {
            this.department.init();
            this.province.init();
            this.specialityTheme.init();
        }
    };

    var form = {
        object: $("#form_add_legal_case").validate({
            rules: {
                Description: {
                    maxlength: 240
                }
            },
            submitHandler: function (formElement, e) {
                e.preventDefault();
                var $btn = $(formElement).find("button[type='submit']");
                var formData = new FormData(formElement);
                $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                $.ajax({
                    url: "/casos-legales/agregar-digirido",
                    type: "POST",
                    data: formData,
                    contentType: false,
                    processData: false
                })
                    .done(function (e) {
                        swal({
                            type: "success",
                            allowOutsideClick: false,
                            title: "Tu caso ha sido registado.",
                            text: e,
                            confirmButtonText: "Entendido"
                        }).then((result) => {
                            if (result.value) {
                                window.location.href = "/mis-casos";
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
                        $btn.removeClass("m-loader m-loader--right m-loader--light").attr("disabled", false);
                    });
            }
        })
    };


    var description = {
        init: function () {
            $('#Description').maxlength({
                alwaysShow: true,
                threshold: 5,
                warningClass: "m-badge m-badge--primary m-badge--rounded m-badge--wide",
                limitReachedClass: "m-badge m-badge--brand m-badge--rounded m-badge--wide",
            });
        }
    };

    var events = {
        onChangeView: function () {
            $("#next_view_register").on("click", function () {
                var specialityThemes = $("#SpecialityThemeId").val();
                var description = $("#Description").val();
                var department = $("#DepartmentId").val();
                var province = $("#ProvinceId").val();
                var phoneNumber = $("#PhoneNumber").val();

                if (specialityThemes.length === 0) {
                    toastr.info("Es necesario seleccionar por lo menos un asunto.", "Información");
                    return;
                }

                if (description === null || description === "") {
                    toastr.info("Es necesario ingresar una breve descripción del caso.", "Información");
                    return;
                }

                if (department === null || department === "undefined" || province === null || province === "undefined") {
                    toastr.info("Es necesario ingresar la ubicación.", "Información");
                    return;
                }

                if (phoneNumber === null || phoneNumber === "") {
                    toastr.info("Es necesario ingresar el celular de contacto.", "Información");
                    return;
                }

                $("#second_part").removeClass("d-none");
                $("#first_part").addClass("d-none");

            });

            $("#previous_view").on("click", function () {
                $("#first_part").removeClass("d-none");
                $("#second_part").addClass("d-none");
            });
        },
        getCurrentPosition: function () {
            if ("geolocation" in navigator) {
                navigator.geolocation.getCurrentPosition(function (position) {
                    console.log(position);
                });
            }
        },
        init: function () {
            this.getCurrentPosition();
            this.onChangeView();
        }
    };

    return {
        init: function () {
            select2.init();
            description.init();
            google.init();
            events.init();
        }
    }
}();

$(() => {
    add.init();
})