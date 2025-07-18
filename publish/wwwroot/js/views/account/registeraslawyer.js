var InitApp = function () {

    var wizard = {
        object: null,
        form: {
            object: $("#m_form").validate({
                ignore: ":hidden",
                rules: {
                    NameOrOffice: {
                        alpha: true
                    },
                    Surnames: {
                        alpha: true
                    },
                    Dni: {
                        digits: true,
                        exactlength: 8
                    },
                    PhoneNumber: {
                        digits: true,
                        exactlength: 9
                    },
                    ConfirmPassword: {
                        equalTo: '[name="Password"]'
                    },
                    Email: {
                        email: true
                    }
                },
                invalidHandler: function (event, validator) {
                    mUtil.scrollTop();
                    var currentStep = wizard.object.getStep();
                    var message = "";
                    if (currentStep === 2) {
                        message = "Es necesario seleccionar por lo menos una especialidad.";
                    }

                    if (currentStep === 3) {
                        message = "Es necesario seleccionar por lo menos un tema.";
                    }

                    if (currentStep === 3 || currentStep === 2) {
                        swal({
                            "title": "Observaciones",
                            "text": message,
                            "type": "info"
                        });
                    }
                },
                submitHandler: function (formElement) {
                    var $btn = $("#m_form").find('[data-wizard-action="submit"]');
                    $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                    var formData = new FormData(formElement);
                    $.ajax({
                        url: "/registrar-abogado",
                        type: "POST",
                        data: formData,
                        contentType: false,
                        processData: false
                    })
                        .done(function (e) {
                            window.location.href = "/abogado/registro-exitoso";

                            //swal({
                            //    type: "success",
                            //    allowOutsideClick: false,
                            //    title: "Registro Completado",
                            //    text: "Su registro ha sido satisfactorio. Se ha enviado un correo electrónico para validar su cuenta.",
                            //    confirmButtonText: "Aceptar"
                            //}).then((result) => {
                            //    if (result.value) {
                            //        window.location.href = "/login";
                            //    }
                            //});
                        })
                        .fail(function (e) {
                            swal({
                                type: "error",
                                title: "Error al completar el registro.",
                                text: e.responseText,
                                confirmButtonText: "Aceptar"
                            });
                        })
                        .always(function () {
                            $btn.removeClass("m-loader m-loader--right m-loader--light").attr("disabled", false);
                        });
                }
            }),
            events: {
                onSubmit: function () {
                    $("#m_form").find('[data-wizard-action="submit"]').on('click', function (e) {
                        e.preventDefault();
                        $("#m_form").submit();
                    });
                },
                init: function () {
                    this.onSubmit();
                }
            },
            init: function () {
                this.events.init();
            }
        },
        init: function () {
            this.object = new mWizard('m_wizard', {
                startStep: 1
            });

            //== Validation before going to next page
            this.object.on('beforeNext', function (wizardObj) {

                if (wizard.object.getStep() === 2) {
                    var specialities = $("#m_form").find("[name='Specialities']:checked");
                    if (configuration.maxSpecialities > 0) {
                        if (specialities.length > configuration.maxSpecialities) {
                            wizardObj.stop();
                            swal({
                                "title": "Observaciones",
                                "text": `Se puede seleccionar como máximo ${configuration.maxSpecialities} especialidades`,
                                "type": "info"
                            });
                        }
                    }
                }

                if (wizard.object.getStep() === 3) {
                    var themes = $("#m_form").find("[name='SpecialityThemes']:checked");
                    if (themes.length > configuration.maxThemes) {
                        wizardObj.stop();
                        swal({
                            "title": "Observaciones",
                            "text": `Se puede seleccionar como máximo ${configuration.maxThemes} temas`,
                            "type": "info"
                        });
                    }
                }

                if (wizard.form.object.form() !== true) {
                    wizardObj.stop();
                }
            });


            //== Change event
            this.object.on('change', function (wizard) {
                mUtil.scrollTop();
                if (wizard.getStep() === 3) {
                    events.themes.load();
                }
            });

            this.form.init();
        }
    };

    var configuration = {
        maxSpecialities: parseInt($("#MaxSpecialities").val()),
        maxThemesBySpeciality: parseInt($("#MaxThemesBySpeciality").val()),
        maxThemes: 0
    };

    var selects = {
        department: {
            init: function () {
                selects.department.load();
                selects.department.events();
            },
            load: function () {
                $.ajax({
                    url: `/departamentos/get`
                }).done(function (result) {
                    $("#DepartmentId").select2({
                        data: result.items
                    }).trigger("change");
                });
            },
            events: function () {
                $("#DepartmentId").on("change", function () {
                    selects.province.load($(this).val());
                });
            }
        },
        province: {
            init: function () {
                $("#ProvinceId").select2({
                    disabled: true,
                    placeholder: "Seleccionar provincia"
                });
                selects.province.events();
            },
            load: function (did) {
                $("#ProvinceId").empty();
                $.ajax({
                    url: `/provincias/get/${did}`
                }).done(function (result) {
                    $("#ProvinceId").select2({
                        data: result.items,
                        disabled: false
                    }).trigger("change");
                });
            },
            events: function () {
                $("#ProvinceId").on("change", function () {
                    selects.district.load($(this).val());
                });
            }
        },
        district: {
            init: function () {
                $("#DistrictId").select2({
                    disabled: true,
                    placeholder: "Seleccionar distrito"
                });
            },
            load: function (pid) {
                $("#DistrictId").empty();
                $.ajax({
                    url: `/distritos/get/${pid}`
                }).done(function (result) {
                    $("#DistrictId").select2({
                        data: result.items,
                        disabled: false
                    }).trigger("change");
                });
            },
        },
        init: function () {
            this.department.init();
            this.province.init();
            this.district.init();
        }
    };

    var events = {
        themes: {
            load: function () {
                mApp.block("themes-container", {
                    message: "Cargando temas de especialidad"
                });

                var valuesToPost = [];
                $.each($('input[name="Specialities"]:checked'), function (index, value) {
                    valuesToPost.push(value.value);
                });
                var formData = new FormData();
                formData.append("values", valuesToPost);
                $.ajax({
                    url: `/especialidades-temas/get/v3`,
                    type: "POST",
                    async: false,
                    processData: false,
                    contentType: false,
                    data: formData,
                }).done(function (result) {
                    $("#themes-container").html(result);
                    $("#m_form").find('[data-wizard-action="next"]').removeLoader();
                });
            }
        },
        onSelectSpecialities: {
            quantity: 0,
            init: function () {
                $("[name='Specialities']").on("change", function () {
                    if ($(this).is(":checked")) {
                        events.onSelectSpecialities.quantity++;
                    }
                    else {
                        events.onSelectSpecialities.quantity--;
                    }
                    if (configuration.maxSpecialities > 0) {
                        if (events.onSelectSpecialities.quantity == configuration.maxSpecialities) {
                            $("[name='Specialities']:not(:checked)").attr("disabled", true);
                        } else {
                            $("[name='Specialities']").attr("disabled", false);
                        }
                    }

                    configuration.maxThemes = configuration.maxThemesBySpeciality * events.onSelectSpecialities.quantity;
                    $("#max_themes_span").text(configuration.maxThemes);
                });
            }
        },
        onSelectSpecialityThemes: {
            init: function () {
                $("#themes-container").on("change", "[name='SpecialityThemes']", function () {
                    var container = $(this).parent().parent().parent().parent();
                    var inputsSelected = container.find("[name='SpecialityThemes']:checked");

                    if (inputsSelected.length >= configuration.maxThemesBySpeciality) {
                        container.find("[name='SpecialityThemes']:not(:checked)").attr("disabled", true);
                    } else {
                        container.find("[name='SpecialityThemes']").attr("disabled", false);
                    }
                });
            }
        },
        init: function () {
            this.onSelectSpecialities.init();
            this.onSelectSpecialityThemes.init();
        }
    };

    return {
        init: function () {
            selects.init();
            wizard.init();
            events.init();
        }
    };
}();

$(function () {
    InitApp.init();
});