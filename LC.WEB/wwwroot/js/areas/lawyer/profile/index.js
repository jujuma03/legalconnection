var profile = function () {

    var configuration = {
        maxThemes: $("#MaxThemes").val(),
        maxSpeciality: $("#MaxSpeciality").val()
    };

    var summernote = {
        defaultOptions: {
            lang: "es-ES",
            airMode: false,
            height: 250,
            toolbar: [
                ['style', ['style']],
                ['font', ['bold', 'italic', 'underline', 'strikethrough', 'superscript', 'subscript', 'clear']],
                ['fontname', ['fontname']],
                ['fontsize', ['fontsize']],
                ['color', ['color']],
                ['para', ['ol', 'ul', 'paragraph', 'height']],
                ['table', ['table']],
                ['insert', ['link', 'video', 'hr']],
                ['view', ['undo', 'redo', 'fullscreen', 'codeview']]
            ]
        },
    };

    var cards = {
        lawyerInformation: {
            object: $("#portlet_lawyer_information"),
            update: function (formElement) {
                var name = $(formElement).find("[name='PersonalInformation.Name']").val();
                var surnames = $(formElement).find("[name='PersonalInformation.Surnames']").val();
                var department = $(formElement).find("[name='PersonalInformation.DepartmentId'] option:selected").text();
                var province = $(formElement).find("[name='PersonalInformation.ProvinceId'] option:selected").text();

                cards.lawyerInformation.object.find(".bi_cal").text($(formElement).find("[name='BasicInformation.CAL']").val());
                cards.lawyerInformation.object.find(".pi_fullname").text(`${name} ${surnames}`);
                cards.lawyerInformation.object.find(".pi_ubigeo").text(`${department} - ${province}`);
            },
            modal: {
                object: $("#lawyer_information_modal"),
                form: {
                    object: $("#lawyer_information_form").validate({
                        submitHandler: function (formElement, e) {
                            e.preventDefault();

                            if ($("#upload-demo-1").is(":visible")) {
                                swal({
                                    type: "info",
                                    title: "Recorta la imágen.",
                                    text: "Para registrar una imagen debe Adjuntar -> click en Previsualizar -> Recortar y Guardar",
                                    confirmButtonText: "Aceptar"
                                });
                                return;
                            }

                            var $btn = $(formElement).find("button[type='submit']");
                            $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                            var formData = new FormData(formElement);
                            cards.lawyerInformation.modal.object.find(":input").attr("disabled", true);
                            $.ajax({
                                url: "/abogado/perfil/actualizar-informacion",
                                type: "POST",
                                data: formData,
                                contentType: false,
                                processData: false
                            })
                                .done(function (e) {
                                    if (e !== null)
                                        $("#lawyer_img_profile").attr("src", "/documentos/"+e);

                                    cards.lawyerInformation.update(formElement);
                                    cards.lawyerInformation.modal.object.modal("hide");
                                    swal({
                                        type: "success",
                                        allowOutsideClick: false,
                                        title: "Éxito",
                                        text: "Los datos actualizados serán validados por nuestros asesores. Una vez confirmados se verán reflejados en su perfil.",
                                        confirmButtonText: "Aceptar"
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
                                    cards.lawyerInformation.modal.object.find(":input").attr("disabled", false);
                                });
                        }
                    })
                },
                load: function () {
                    var $page = cards.lawyerInformation.modal.object;
                    $page.find(":input").attr("disabled", true);
                    $.ajax({
                        url: "/abogado/perfil/get-informacion",
                        type: "GET"
                    })
                        .done(function (e) {
                            cards.lawyerInformation.modal.object.find("[name='PersonalInformation.Name']").val(e.personalInformation.name);
                            cards.lawyerInformation.modal.object.find("[name='PersonalInformation.Surnames']").val(e.personalInformation.surnames);
                            select2.department.setValue(e.personalInformation.departmentId, e.personalInformation.department);
                            select2.province.setValue(e.personalInformation.provinceId, e.personalInformation.province);
                            select2.district.setValue(e.personalInformation.districtId, e.personalInformation.district);

                            cards.lawyerInformation.modal.object.find("[name='PersonalInformation.Photo']").val(null);
                            cards.lawyerInformation.modal.object.find(".custom-file-label").text("Seleccione un archivo");

                            $page.find(":input").attr("disabled", false);
                        });
                },
                onShow: function () {
                    $("#edit_information").click(function () {
                        cards.lawyerInformation.modal.load();
                        cards.lawyerInformation.modal.object.modal("show");
                    });
                },
                onHide: function () {
                    $("#lawyer_information_modal").on('hidden.bs.modal', function () {
                        $("#upload-demo-1").hide();
                        $("#upload-offi-1").hide();
                        $("#upload-result-1").prop("disabled",true);
                    });
                },
                init: function () {
                    this.onShow();
                    this.onHide();
                }
            },
            init: function () {
                this.modal.init();
            }
        },
        personalInformation: {
            update: function (formElement) {
                cards.lawyerInformation.object.find(".pi_birthdate").text($(formElement).find("[name='PersonalInformation.BirthDate']").val());
                cards.lawyerInformation.object.find(".pi_dni").text($(formElement).find("[name='PersonalInformation.DNI']").val());
                cards.lawyerInformation.object.find(".pi_houseNumber").text($(formElement).find("[name='PersonalInformation.HouseNumber']").val());
                cards.lawyerInformation.object.find(".pi_phoneNumber").text($(formElement).find("[name='PersonalInformation.PhoneNumber']").val());
                cards.lawyerInformation.object.find(".pi_sex").text($(formElement).find("[name='PersonalInformation.Sex'] option:selected").text());
            },
            modal: {
                object: $("#personal_information_modal"),
                form: {
                    object: $("#personal_information_form").validate({
                        rules: {
                            "PersonalInformation.HouseNumber": {
                                digits: true,
                                exactlength: 7
                            },
                            "PersonalInformation.PhoneNumber": {
                                digits: true,
                                exactlength: 9
                            },
                            "PersonalInformation.DNI": {
                                digits: true,
                                exactlength: 8
                            },
                        },
                        submitHandler: function (formElement, e) {
                            e.preventDefault();
                            var $btn = $(formElement).find("button[type='submit']");
                            $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                            var formData = new FormData(formElement);
                            cards.personalInformation.modal.object.find(":input").attr("disabled", true);
                            $.ajax({
                                url: "/abogado/perfil/actualizar-informacion-personal",
                                type: "POST",
                                data: formData,
                                contentType: false,
                                processData: false
                            })
                                .done(function (e) {
                                    cards.personalInformation.update(formElement);
                                    cards.personalInformation.modal.object.modal("hide");
                                    swal({
                                        type: "success",
                                        allowOutsideClick: false,
                                        title: "Éxito",
                                        text: "Los datos actualizados serán validados por nuestros asesores. Una vez confirmados se verán reflejados en su perfil.",
                                        confirmButtonText: "Aceptar"
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
                                    cards.personalInformation.modal.object.find(":input").attr("disabled", false);
                                });
                        }
                    })
                },
                load: function () {
                    var $page = cards.personalInformation.modal.object;
                    $page.find(":input").attr("disabled", true);
                    $.ajax({
                        url: "/abogado/perfil/get-informacion-personal",
                        type: "GET"
                    })
                        .done(function (e) {
                            cards.personalInformation.modal.object.find("[name='PersonalInformation.HouseNumber']").val(e.personalInformation.houseNumber);
                            cards.personalInformation.modal.object.find("[name='PersonalInformation.PhoneNumber']").val(e.personalInformation.phoneNumber);
                            cards.personalInformation.modal.object.find("[name='PersonalInformation.DNI']").val(e.personalInformation.dni);
                            cards.personalInformation.modal.object.find("[name='PersonalInformation.BirthDate']").val(e.personalInformation.birthDate);
                            cards.personalInformation.modal.object.find("[name='PersonalInformation.Sex']").val(e.personalInformation.sex).trigger("change");
                            $page.find(":input").attr("disabled", false);
                        });
                },
                onShow: function () {
                    $("#personal_information").click(function () {
                        cards.personalInformation.modal.load();
                        cards.personalInformation.modal.object.modal("show");
                    });
                },
                init: function () {
                    this.onShow();
                }
            },
            init: function () {
                this.modal.init();
            }
        },
        speciality: {
            update: function (formElement) {
                var arrSpecialitiesText = "";
                var arrSpecialitiesThemeText = "";


                $("#speciality_select").find("option:selected").each(function () {
                    var $this = $(this);
                    if ($this.length) {
                        var selText = $this.text();
                        arrSpecialitiesText += `<span class="m-badge m-badge--metal m-badge--wide">${selText}</span>`;
                    }
                });

                $("#SpecialityThemesId").find("option:selected").each(function () {
                    var $this = $(this);
                    if ($this.length) {
                        var selText = $this.text();
                        arrSpecialitiesThemeText += `<span class="m-badge m-badge--primary m-badge--wide">${selText}</span>`;
                    }
                });

                $(".bi_specialities").html(arrSpecialitiesText);
                $(".bi_themes").html(arrSpecialitiesThemeText);
            },
            modal: {
                object: $("#speciality_modal"),
                form: {
                    object: $("#speciality_form").validate({
                        submitHandler: function (formElement, e) {
                            e.preventDefault();
                            var $btn = $(formElement).find("button[type='submit']");
                            $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                            var formData = new FormData(formElement);
                            cards.speciality.modal.object.find(":input").attr("disabled", true);
                            $.ajax({
                                url: "/abogado/perfil/actualizar-especialidades",
                                type: "POST",
                                data: formData,
                                contentType: false,
                                processData: false
                            })
                                .done(function (e) {
                                    cards.speciality.update(formElement);
                                    cards.speciality.modal.object.modal("hide");
                                    swal({
                                        type: "success",
                                        allowOutsideClick: false,
                                        title: "Éxito",
                                        text: "Especialidades actualizados satisfactoriamente.",
                                        confirmButtonText: "Aceptar"
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
                                    cards.speciality.modal.object.find(":input").attr("disabled", false);
                                });
                        }
                    })
                },
                load: function () {
                    var $page = cards.speciality.modal.object;
                    $page.find(":input").attr("disabled", true);
                    $.ajax({
                        url: "/abogado/perfil/get-especialidades",
                        type: "GET"
                    })
                        .done(function (e) {
                            select2.speciality.setValues(e.specialities);
                            select2.specialityTheme.setValues(e.specialityThemes);
                            $page.find(":input").attr("disabled", false);
                        });
                },
                onShow: function () {
                    $("#edit_specialities").click(function () {
                        cards.speciality.modal.load();
                        cards.speciality.modal.object.modal("show");
                    });
                },
                init: function () {
                    this.onShow();
                }
            },
            init: function () {
                this.modal.init();
            }
        },
        fee: {
            update: function (formElement) {
                cards.lawyerInformation.object.find(".bi_fee").text($(formElement).find("[name='Fee']").val());

                if ($(formElement).find("[name='FreeFirst']").val() === "true") {
                    cards.lawyerInformation.object.find(".bi_freeFirst").text("Primera consulta sin costo ");
                } else {
                    cards.lawyerInformation.object.find(".bi_freeFirst").text("");
                }
            },
            modal: {
                object: $("#fee_modal"),
                form: {
                    object: $("#fee_form").validate({
                        submitHandler: function (formElement, e) {
                            e.preventDefault();
                            var $btn = $(formElement).find("button[type='submit']");
                            $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                            var formData = new FormData(formElement);
                            cards.fee.modal.object.find(":input").attr("disabled", true);
                            $.ajax({
                                url: "/abogado/perfil/actualizar-valor-consulta",
                                type: "POST",
                                data: formData,
                                contentType: false,
                                processData: false
                            })
                                .done(function (e) {
                                    cards.fee.update(formElement);
                                    cards.fee.modal.object.modal("hide");
                                    swal({
                                        type: "success",
                                        allowOutsideClick: false,
                                        title: "Éxito",
                                        text: "Los datos actualizados serán validados por nuestros asesores. Una vez confirmados se verán reflejados en su perfil.",
                                        confirmButtonText: "Aceptar"
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
                                    cards.fee.modal.object.find(":input").attr("disabled", false);
                                });
                        }
                    })
                },
                load: function () {
                    var $page = cards.fee.modal.object;
                    $page.find(":input").attr("disabled", true);
                    $.ajax({
                        url: "/abogado/perfil/get-valor-consulta",
                        type: "GET"
                    })
                        .done(function (e) {
                            $page.find(":input").attr("disabled", false);
                            cards.fee.modal.object.find("[name='Fee']").val(e.fee);
                            cards.fee.modal.object.find("[name='FreeFirst']").val(`${e.freeFirst}`).trigger("change");
                        });
                },
                onShow: function () {
                    $("#edit_fee").click(function () {
                        cards.fee.modal.load();
                        cards.fee.modal.object.modal("show");
                    });
                },
                init: function () {
                    this.onShow();
                }
            },
            init: function () {
                this.modal.init();
            }
        },
        aboutMe: {
            object: $("#container_about_me_text"),
            update: function (formElement) {
                $("#empty_aboutme").empty();
                cards.aboutMe.object.text($(formElement).find("[name='AboutMe']").val());
                events.expandable();
            },
            modal: {
                object: $("#about_me_modal"),
                form: {
                    object: $("#about_me_form").validate({
                        submitHandler: function (formElement, e) {
                            e.preventDefault();
                            var $btn = $(formElement).find("button[type='submit']");
                            $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                            var formData = new FormData(formElement);
                            cards.aboutMe.modal.object.find(":input").attr("disabled", true);
                            $.ajax({
                                url: "/abogado/perfil/actualizar-sobremi",
                                type: "POST",
                                data: formData,
                                contentType: false,
                                processData: false
                            })
                                .done(function (e) {
                                    cards.aboutMe.update(formElement);
                                    cards.aboutMe.modal.object.modal("hide");
                                    swal({
                                        type: "success",
                                        allowOutsideClick: false,
                                        title: "Éxito",
                                        text: "Los datos actualizados serán validados por nuestros asesores. Una vez confirmados se verán reflejados en su perfil.",
                                        confirmButtonText: "Aceptar"
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
                                    cards.aboutMe.modal.object.find(":input").attr("disabled", false);
                                });
                        }
                    })
                },
                load: function () {
                    var $page = cards.aboutMe.modal.object;
                    $page.find(":input").attr("disabled", true);
                    $.ajax({
                        url: "/abogado/perfil/get-sobremi",
                        type: "GET"
                    })
                        .done(function (e) {
                            cards.aboutMe.modal.object.find("[name='AboutMe']").val(e);
                            $page.find(":input").attr("disabled", false);
                        });
                },
                onShow: function () {
                    cards.aboutMe.modal.object.on('show.bs.modal', function (e) {
                        cards.aboutMe.modal.load();
                    });
                },
                init: function () {
                    this.onShow();
                }
            },
            init: function () {
                this.modal.init();
            }
        },
        experience: {
            object: $("#container_experience"),
            update: function () {
                mApp.block(cards.experience.object, {
                    message: "Cargando experiencia laboral..."
                });

                $.ajax({
                    url: "/abogado/perfil/get-experiencias",
                    type: "GET",
                    dataType: "html"
                })
                    .done(function (e) {
                        $("#lawyer_experience_div").html(e);
                        events.expandable();
                    })
                    .always(function () {
                        mApp.unblock(cards.experience.object);
                    });
            },
            modal: {
                object: $("#experiencie_modal"),
                form: {
                    object: $("#experience_form").validate({
                        submitHandler: function (formElement, e) {
                            e.preventDefault();
                            var $btn = $(formElement).find("button[type='submit']");
                            $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                            var formData = new FormData(formElement);
                            cards.experience.modal.object.find(":input").attr("disabled", true);
                            $.ajax({
                                url: $(formElement).attr("action"),
                                type: "POST",
                                data: formData,
                                contentType: false,
                                processData: false
                            })
                                .done(function (e) {
                                    events.getTotalExperience();
                                    cards.experience.update();
                                    cards.experience.modal.object.modal("hide");
                                    swal({
                                        type: "success",
                                        allowOutsideClick: false,
                                        title: "Éxito",
                                        text: "Los datos actualizados serán validados por nuestros asesores. Una vez confirmados se verán reflejados en su perfil.",
                                        confirmButtonText: "Aceptar"
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
                                    cards.experience.modal.object.find(":input").attr("disabled", false);
                                });
                        }
                    })
                },
                events: {
                    showAdd: function () {
                        cards.experience.object.on("click", ".btn-experience-add", function () {
                            var url = "/abogado/perfil/agregar-experiencia";
                            $("#experience_form").attr("action", url);
                            cards.experience.modal.object.modal("show");
                        });
                    },
                    showEdit: function () {
                        cards.experience.object.on("click", ".btn-experience-edit", function () {
                            cards.experience.modal.object.find(":input").attr("disabled", true);
                            var url = "/abogado/perfil/actualizar-experiencia";
                            $("#experience_form").attr("action", url);
                            cards.experience.modal.object.modal("show");
                            var id = $(this).data("id");
                            $.ajax({
                                url: `/abogado/perfil/get-experiencia?experienceId=${id}`,
                                type: "GET"
                            })
                                .done(function (e) {
                                    cards.experience.modal.object.find("[name='Company']").val(e.company);
                                    cards.experience.modal.object.find("[name='Description']").val(e.description);
                                    cards.experience.modal.object.find("[name='Id']").val(e.id);
                                    cards.experience.modal.object.find("[name='Position']").val(e.position);
                                    cards.experience.modal.object.find("[name='StartDate']").val(e.startDate);
                                    cards.experience.modal.object.find("[name='WorkArea']").val(e.workArea);
                                    cards.experience.modal.object.find("[name='EndDate']").val(e.endDate);
                                    cards.experience.modal.object.find(":input").attr("disabled", false);

                                    if (e.endDate === null || e.endDate === "") {
                                        $("#present_experience").prop("checked", true).trigger("change");
                                    }
                                });
                        });
                    },
                    onHidden: function () {
                        cards.experience.modal.object.on('hidden.bs.modal', function (e) {
                            cards.experience.modal.form.object.resetForm();
                            cards.experience.modal.object.find("[name='Photo']").val(null);
                            cards.experience.modal.object.find(".custom-file-label").text("Seleccione un archivo");
                            $("#present_experience").prop("checked", false).trigger("change");
                        });
                    },
                    onPresent: function () {
                        $("#present_experience").on("change", function () {
                            if ($(this).is(":checked")) {
                                cards.experience.modal.object.find("[name='EndDate']").attr("disabled", true);
                                cards.experience.modal.object.find("[name='EndDate']").val(null);
                            } else {
                                cards.experience.modal.object.find("[name='EndDate']").attr("disabled", false);
                            }
                        });
                    },
                    init: function () {
                        this.showAdd();
                        this.showEdit();
                        this.onHidden();
                        this.onPresent();
                    }
                },
                init: function () {
                    this.events.init();
                }
            },
            events: {
                onDelete: function () {
                    cards.experience.object.on("click", ".btn-experience-delete", function () {
                        var id = $(this).data("id");
                        swal({
                            type: "warning",
                            title: "Eliminará la experiencia seleccionada.",
                            text: "¿Seguro que desea eliminarlo?.",
                            confirmButtonText: "Aceptar",
                            showCancelButton: true,
                            showLoaderOnConfirm: true,
                            allowOutsideClick: () => !swal.isLoading(),
                            preConfirm: () => {
                                return new Promise(() => {
                                    $.ajax({
                                        type: "POST",
                                        url: `/abogado/perfil/eliminar-experiencia?experienceId=${id}`
                                    })
                                        .done(function () {
                                            cards.experience.update();
                                            swal({
                                                type: "success",
                                                title: "Completado",
                                                text: "Los datos actualizados serán validados por nuestros asesores. Una vez confirmados se verán reflejados en su perfil.",
                                                confirmButtonText: "Aceptar"
                                            });
                                        })
                                        .fail(function (e) {
                                            swal({
                                                type: "error",
                                                title: "Error",
                                                confirmButtonClass: "btn btn-danger m-btn m-btn--custom",
                                                confirmButtonText: "Aceptar",
                                                text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText
                                            });
                                        });
                                });
                            }
                        });

                    });
                },
                init: function () {
                    this.onDelete();
                }
            },
            init: function () {
                this.update();
                this.modal.init();
                this.events.init();
            }
        },
        study: {
            object: $("#container_study"),
            update: function () {
                mApp.block(cards.study.object, {
                    message: "Cargando formación académica..."
                });

                $.ajax({
                    url: "/abogado/perfil/get-estudios",
                    type: "GET",
                    dataType: "html"
                })
                    .done(function (e) {
                        $("#lawyer_study_div").html(e);
                        events.expandable();
                    })
                    .always(function () {
                        mApp.unblock(cards.study.object);
                    });
            },
            modal: {
                object: $("#study_modal"),
                form: {
                    object: $("#study_form").validate({
                        submitHandler: function (formElement, e) {
                            e.preventDefault();
                            var $btn = $(formElement).find("button[type='submit']");
                            $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                            var formData = new FormData(formElement);
                            cards.experience.modal.object.find(":input").attr("disabled", true);
                            $.ajax({
                                url: $(formElement).attr("action"),
                                type: "POST",
                                data: formData,
                                contentType: false,
                                processData: false
                            })
                                .done(function (e) {
                                    cards.study.update();
                                    cards.study.modal.object.modal("hide");
                                    swal({
                                        type: "success",
                                        allowOutsideClick: false,
                                        title: "Éxito",
                                        text: "Los datos actualizados serán validados por nuestros asesores. Una vez confirmados se verán reflejados en su perfil.",
                                        confirmButtonText: "Aceptar"
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
                                    cards.study.modal.object.find(":input").attr("disabled", false);
                                });
                        }
                    })
                },
                events: {
                    showAdd: function () {
                        cards.study.object.on("click", ".btn-study-add", function () {
                            var url = "/abogado/perfil/agregar-estudio";
                            $("#study_form").attr("action", url);
                            cards.study.modal.object.modal("show");
                        });
                    },
                    showEdit: function () {
                        cards.study.object.on("click", ".btn-study-edit", function () {
                            cards.study.modal.object.find(":input").attr("disabled", true);
                            var url = "/abogado/perfil/actualizar-estudio";
                            $("#study_form").attr("action", url);
                            cards.study.modal.object.modal("show");
                            var id = $(this).data("id");
                            $.ajax({
                                url: `/abogado/perfil/get-estudio?studyId=${id}`,
                                type: "GET"
                            })
                                .done(function (e) {
                                    cards.study.modal.object.find("[name='Description']").val(e.description);
                                    cards.study.modal.object.find("[name='EndDate']").val(e.endDate);
                                    cards.study.modal.object.find("[name='Grade']").val(e.grade).trigger("change");
                                    cards.study.modal.object.find("[name='Id']").val(e.id);
                                    cards.study.modal.object.find("[name='StartDate']").val(e.startDate);
                                    cards.study.modal.object.find("[name='Ubication']").val(e.ubication);
                                    cards.study.modal.object.find("[name='Mention']").val(e.mention);

                                    cards.study.modal.object.find(":input").attr("disabled", false);

                                    if (e.endDate === null || e.endDate === "") {
                                        $("#present_study").prop("checked", true).trigger("change");
                                    }
                                });
                        });
                    },
                    onPresent: function () {
                        $("#present_study").on("change", function () {
                            if ($(this).is(":checked")) {
                                cards.study.modal.object.find("[name='EndDate']").attr("disabled", true);
                                cards.study.modal.object.find("[name='EndDate']").val(null);
                            } else {
                                cards.study.modal.object.find("[name='EndDate']").attr("disabled", false);
                            }
                        });
                    },
                    onHidden: function () {
                        cards.study.modal.object.on('hidden.bs.modal', function (e) {
                            cards.study.modal.form.object.resetForm();
                            select2.grade.clear();
                            $("#present_study").prop("checked", false).trigger("change");
                        });
                    },
                    init: function () {
                        this.showAdd();
                        this.showEdit();
                        this.onHidden();
                        this.onPresent();
                    }
                },
                init: function () {
                    this.events.init();
                }
            },
            events: {
                onDelete: function () {
                    cards.study.object.on("click", ".btn-study-delete", function () {
                        var id = $(this).data("id");
                        swal({
                            type: "warning",
                            title: "Eliminará la formación académica seleccionada.",
                            text: "¿Seguro que desea eliminarlo?.",
                            confirmButtonText: "Aceptar",
                            showCancelButton: true,
                            showLoaderOnConfirm: true,
                            allowOutsideClick: () => !swal.isLoading(),
                            preConfirm: () => {
                                return new Promise(() => {
                                    $.ajax({
                                        type: "POST",
                                        url: `/abogado/perfil/eliminar-estudio?studyId=${id}`
                                    })
                                        .done(function () {
                                            cards.study.update();
                                            swal({
                                                type: "success",
                                                title: "Completado",
                                                text: "Los datos actualizados serán validados por nuestros asesores. Una vez confirmados se verán reflejados en su perfil.",
                                                confirmButtonText: "Aceptar"
                                            });
                                        })
                                        .fail(function (e) {
                                            swal({
                                                type: "error",
                                                title: "Error",
                                                confirmButtonClass: "btn btn-danger m-btn m-btn--custom",
                                                confirmButtonText: "Aceptar",
                                                text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText
                                            });
                                        });
                                });
                            }
                        });

                    });
                },
                init: function () {
                    this.onDelete();
                }
            },
            init: function () {
                this.update();
                this.modal.init();
                this.events.init();
            }
        },
        language: {
            object: $("#container_language"),
            update: function () {
                mApp.block(cards.language.object, {
                    message: "Cargando idiomas..."
                });

                $.ajax({
                    url: "/abogado/perfil/get-idiomas",
                    type: "GET",
                    dataType: "html"
                })
                    .done(function (e) {
                        $("#lawyer_language_div").html(e);
                    })
                    .always(function () {
                        mApp.unblock(cards.language.object);
                    });
            },
            modal: {
                object: $("#language_modal"),
                form: {
                    object: $("#language_form").validate({
                        submitHandler: function (formElement, e) {
                            e.preventDefault();
                            var $btn = $(formElement).find("button[type='submit']");
                            $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                            var formData = new FormData(formElement);
                            cards.language.modal.object.find(":input").attr("disabled", true);
                            $.ajax({
                                url: "/abogado/perfil/agregar-idioma",
                                type: "POST",
                                data: formData,
                                contentType: false,
                                processData: false
                            })
                                .done(function (e) {
                                    cards.language.update();
                                    cards.language.modal.object.modal("hide");
                                    swal({
                                        type: "success",
                                        title: "Completado",
                                        text: "Los datos actualizados serán validados por nuestros asesores. Una vez confirmados se verán reflejados en su perfil.",
                                        confirmButtonText: "Aceptar"
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
                                    cards.language.modal.object.find(":input").attr("disabled", false);
                                });
                        }
                    })
                },
                events: {
                    onHidden: function () {
                        cards.language.modal.object.on('hidden.bs.modal', function (e) {
                            cards.language.modal.form.object.resetForm();
                            select2.language.clear();
                            select2.languageLevel.clear();
                        });
                    },
                    init: function () {
                        this.onHidden();
                    }
                },
                init: function () {
                    this.events.init();
                }
            },
            events: {
                onDelete: function () {
                    cards.language.object.on("click", ".btn-language-delete", function () {
                        var id = $(this).data("id");
                        swal({
                            type: "warning",
                            title: "Eliminará el idioma seleccionado.",
                            text: "¿Seguro que desea eliminarlo?.",
                            confirmButtonText: "Aceptar",
                            showCancelButton: true,
                            showLoaderOnConfirm: true,
                            allowOutsideClick: () => !swal.isLoading(),
                            preConfirm: () => {
                                return new Promise(() => {
                                    $.ajax({
                                        type: "POST",
                                        url: `/abogado/perfil/eliminar-idioma?languageId=${id}`
                                    })
                                        .done(function () {
                                            cards.language.update();
                                            swal({
                                                type: "success",
                                                title: "Completado",
                                                text: "Los datos actualizados serán validados por nuestros asesores. Una vez confirmados se verán reflejados en su perfil.",
                                                confirmButtonText: "Aceptar"
                                            });
                                        })
                                        .fail(function (e) {
                                            swal({
                                                type: "error",
                                                title: "Error",
                                                confirmButtonClass: "btn btn-danger m-btn m-btn--custom",
                                                confirmButtonText: "Aceptar",
                                                text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText
                                            });
                                        });
                                });
                            }
                        });

                    });
                },
                init: function () {
                    this.onDelete();
                }
            },
            init: function () {
                this.modal.init();
                this.update();
                this.events.init();
            }
        },
        publication: {
            object: $("#container_publication"),
            object2: $("#lawyer_publication_v2_div"),
            summenote: {
                description: {
                    object: null,
                    getValue: function () {
                        return cards.publication.summenote.description.object.summernote('code');
                    },
                    clear: function () {
                        cards.publication.summenote.description.object.summernote("code", "");
                    },
                    init: function () {
                        cards.publication.summenote.description.object = $("#Publication_Description").summernote(summernote.defaultOptions);
                    }
                },
                init: function () {
                    this.description.init();
                }
            },
            update: function () {
                mApp.block(cards.publication.object, {
                    message: "Cargando publicaciones..."
                });

                $.ajax({
                    url: "/abogado/perfil/get-publicaciones",
                    type: "GET",
                    dataType: "html"
                })
                    .done(function (e) {
                        $("#lawyer_publication_div").html(e);
                        events.expandable();
                    })
                    .always(function () {
                        mApp.unblock(cards.publication.object);
                    });
            },
            modal: {
                object: $("#portlet_add_publications"),
                form: {
                    object: $("#publication_form").validate({
                        submitHandler: function (formElement, e) {
                            e.preventDefault();
                            var $btn = $(formElement).find("button[type='submit']");
                            $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                            var formData = new FormData(formElement);
                            //formData.append("Description", cards.publication.summenote.description.getValue());
                            cards.publication.modal.object.find(":input").attr("disabled", true);
                            $.ajax({
                                url: "/abogado/perfil/agregar-publicacion",
                                type: "POST",
                                data: formData,
                                contentType: false,
                                processData: false
                            })
                                .done(function (e) {
                                    $(".btn-cancel-publication").click();
                                    //cards.publication.modal.object.modal("hide");
                                    swal({
                                        type: "success",
                                        allowOutsideClick: false,
                                        title: "Solicitud Enviada con Éxito",
                                        text: "Tu publicación se encuentra en revisión. Una vez sea aceptada aparecerá en el BLOG de Legal Connection.",
                                        confirmButtonText: "Aceptar"
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
                                    cards.publication.modal.object.find(":input").attr("disabled", false);
                                });
                        }
                    })
                },
                //events: {
                //    onHidden: function () {
                //        cards.publication.modal.object.on('hidden.bs.modal', function (e) {
                //            cards.publication.modal.form.object.resetForm();
                //            cards.publication.modal.object.find("[name='Photo']").val(null);
                //            cards.publication.modal.object.find(".custom-file-label").text("Seleccione un archivo");
                //            cards.publication.summenote.description.clear();
                //        });
                //    },
                //    init: function () {
                //        this.onHidden();
                //    }
                //},
                //init: function () {
                //    this.events.init();
                //}
            },
            events: {
                onDelete: function () {
                    cards.publication.object.on("click", ".btn-publication-delete", function () {
                        var id = $(this).data("id");
                        cards.publication.events.confirmationMessage(id);
                    });
                    cards.publication.object2.on("click", ".btn-publication-delete", function () {
                        var id = $(this).data("id");
                        cards.publication.events.confirmationMessage(id);
                    });
                },
                confirmationMessage: function (id) {
                    swal({
                        type: "warning",
                        title: "Eliminará la publicación seleccionada.",
                        text: "¿Seguro que desea eliminarlo?.",
                        confirmButtonText: "Aceptar",
                        showCancelButton: true,
                        showLoaderOnConfirm: true,
                        allowOutsideClick: () => !swal.isLoading(),
                        preConfirm: () => {
                            return new Promise(() => {
                                $.ajax({
                                    type: "POST",
                                    url: `/abogado/perfil/eliminar-publicacion?publicationId=${id}`
                                })
                                    .done(function () {
                                        cards.publication.update();
                                        swal({
                                            type: "success",
                                            title: "Completado",
                                            text: "Registro eliminado satisfactoriamente.",
                                            confirmButtonText: "Aceptar"
                                        });
                                    })
                                    .fail(function (e) {
                                        swal({
                                            type: "error",
                                            title: "Error",
                                            confirmButtonClass: "btn btn-danger m-btn m-btn--custom",
                                            confirmButtonText: "Aceptar",
                                            text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText
                                        });
                                    });
                            });
                        }
                    });
                },
                init: function () {
                    this.onDelete();
                }
            },
            init: function () {
                //this.modal.init();
                this.update();
                this.events.init();
                this.summenote.init();
            }
        },
        qualification: {
            object: $("#container_qualification"),
            update: function () {
                mApp.block(cards.qualification.object, {
                    message: "Cargando califaciones..."
                });

                $.ajax({
                    url: "/abogado/perfil/get-califaciones",
                    type: "GET",
                    dataType: "html"
                })
                    .done(function (e) {
                        $("#lawyer_qualification_div").html(e);
                        events.expandable();
                    })
                    .always(function () {
                        mApp.unblock(cards.qualification.object);
                    });
            },
            init: function () {
                this.update();
            }
        },
        qualification_v2: {
            activePage: 1,
            recordsPerDraw: 10,
            object: $("#container_qualification_v2"),
            update: function () {
                mApp.block(cards.qualification_v2.object, {
                    message: "Cargando califaciones..."
                });
                $.ajax({
                    url: "/abogado/perfil/get-all-califaciones",
                    data: {
                        page: cards.qualification_v2.activePage,
                        rpdraw: cards.qualification_v2.recordsPerDraw
                    },
                    type: "GET",
                    dataType: "html"
                })
                    .done(function (e) {
                        $("#lawyer_qualification_v2_div").html(e);
                        events.expandable();
                    })
                    .always(function () {
                        mApp.unblock(cards.qualification_v2.object);
                    });
            },
            events: {
                onChangePage: function () {
                    cards.qualification_v2.object.on("click", ".previous-item", function () {
                        cards.qualification_v2.activePage--;
                        cards.qualification_v2.update();
                    });

                    cards.qualification_v2.object.on("click", ".next-item", function () {
                        cards.qualification_v2.activePage++;
                        cards.qualification_v2.update();
                    });
                },
                init: function () {
                    this.onChangePage();
                }
            },
            init: function () {
                this.update();
                this.events.init();
            }
        },
        publication_v2: {
            activePage: 1,
            recordsPerDraw: 10,

            object: $("#container_publication_v2"),
            update: function () {
                mApp.block(cards.publication_v2.object, {
                    message: "Cargando publicaciones..."
                });
                $.ajax({
                    url: "/abogado/perfil/get-all-publicaiones",
                    data: {
                        page: cards.publication_v2.activePage,
                        rpdraw: cards.publication_v2.recordsPerDraw
                    },
                    type: "GET",
                    dataType: "html"
                })
                    .done(function (e) {
                        $("#lawyer_publication_v2_div").html(e);
                        events.expandable();
                    })
                    .always(function () {
                        mApp.unblock(cards.publication_v2.object);
                    });
            },
            events: {
                onChangePage: function () {
                    cards.publication_v2.object.on("click", ".previous-item", function () {
                        cards.publication_v2.activePage--;
                        cards.publication_v2.update();
                    });

                    cards.qualification_v2.object.on("click", ".next-item", function () {
                        cards.publication_v2.activePage++;
                        cards.publication_v2.update();
                    });
                },
                init: function () {
                    this.onChangePage();
                }
            },
            init: function () {
                this.update();
                this.events.init();
            }
        },
        init: function () {
            this.lawyerInformation.init();
            this.fee.init();
            this.speciality.init();
            this.aboutMe.init();
            this.experience.init();
            this.study.init();
            this.publication.init();
            this.publication_v2.init();
            this.language.init();
            this.personalInformation.init();
            this.qualification.init();
            this.qualification_v2.init();
        }
    };

    var select2 = {
        sex: {
            init: function () {
                $("#PersonalInformation_Sex").select2({
                    dropdownParent: cards.personalInformation.modal.object,
                    minimumResultsForSearch: -1
                });
            }
        },
        languageLevel: {
            clear: function () {
                $("#Level").val(1).trigger("change");
            },
            init: function () {
                $("#Level").select2({
                    dropdownParent: cards.language.modal.object,
                    minimumResultsForSearch: -1
                });
            }
        },
        language: {
            load: function () {
                $("#LanguageId").select2({
                    ajax: {
                        url: "/idiomas/get/v2",
                        delay: 500,
                        data: function (params) {
                            var query = {
                                page: params.page || 1,
                                term: params.term
                            };
                            return query;
                        }
                    },
                    dropdownParent: cards.language.modal.object,
                    minimumInputLength: 0,
                    placeholder: 'Seleccione idioma',
                    allowClear: false
                });
            },
            clear: function () {
                $("#LanguageId").val(null).trigger("change");
            },
            init: function () {
                this.load();
            }
        },
        freeFirst: {
            init: function () {
                $("#FreeFirst").select2({
                    dropdownParent: cards.fee.modal.object,
                    minimumResultsForSearch: -1
                });
            }
        },
        grade: {
            clear: function () {
                $("#Grade").val(null).trigger("change");
            },
            init: function () {
                $("#Grade").select2({
                    dropdownParent: cards.study.modal.object,
                    minimumResultsForSearch: -1,
                    placeholder: "Seleccionar grado académico"
                });
            }
        },
        department: {
            load: function () {
                $("#PersonalInformation_DepartmentId").select2({
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
                    dropdownParent: cards.lawyerInformation.modal.object,
                    minimumInputLength: 0,
                    placeholder: 'Seleccione departamento',
                    allowClear: false
                });
            },
            setValue: function (value, text) {
                if ($('#PersonalInformation_DepartmentId').find("option[value='" + value + "']").length) {
                    $('#PersonalInformation_DepartmentId').val(value).trigger('change');
                } else {
                    var newOption = new Option(text, value, true, true);
                    $('#PersonalInformation_DepartmentId').append(newOption).trigger('change');
                }
            },
            events: {
                onChange: function () {
                    $("#PersonalInformation_DepartmentId").on("change", function () {
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
                $("#PersonalInformation_ProvinceId").select2({
                    ajax: {
                        url: "/provincias/get/v2",
                        delay: 500,
                        data: function (params) {
                            var query = {
                                departmentId: $("#PersonalInformation_DepartmentId").val(),
                                page: params.page || 1,
                                term: params.term
                            };
                            return query;
                        }
                    },
                    dropdownParent: cards.lawyerInformation.modal.object,
                    minimumInputLength: 0,
                    placeholder: 'Seleccione provincia',
                    allowClear: true
                });
            },
            setValue: function (value, text) {
                if ($('#PersonalInformation_ProvinceId').find("option[value='" + value + "']").length) {
                    $('#PersonalInformation_ProvinceId').val(value).trigger('change');
                } else {
                    var newOption = new Option(text, value, true, true);
                    $('#PersonalInformation_ProvinceId').append(newOption).trigger('change');
                }
            },
            clear: function () {
                $("#PersonalInformation_ProvinceId").val(null).trigger("change");
            },
            events: {
                onChange: function () {
                    $("#PersonalInformation_ProvinceId").on("change", function () {
                        select2.district.clear();
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
        district: {
            load: function () {
                $("#PersonalInformation_DistrictId").select2({
                    ajax: {
                        url: "/distritos/get/v2",
                        delay: 500,
                        data: function (params) {
                            var query = {
                                provinceId: $("#PersonalInformation_ProvinceId").val(),
                                page: params.page || 1,
                                term: params.term
                            };
                            return query;
                        }
                    },
                    dropdownParent: cards.lawyerInformation.modal.object,
                    minimumInputLength: 0,
                    placeholder: 'Seleccione distrito',
                    allowClear: true
                });
            },
            setValue: function (value, text) {
                if ($('#PersonalInformation_DistrictId').find("option[value='" + value + "']").length) {
                    $('#PersonalInformation_DistrictId').val(value).trigger('change');
                } else {
                    var newOption = new Option(text, value, true, true);
                    $('#PersonalInformation_DistrictId').append(newOption).trigger('change');
                }
            },
            clear: function () {
                $("#PersonalInformation_DistrictId").val(null).trigger("change");
            },
            init: function () {
                this.load();
            }
        },
        speciality: {
            load: function () {
                $("#speciality_select").select2({
                    ajax: {
                        url: "/especialidades/get/v2",
                        delay: 500,
                        data: function (params) {
                            var query = {
                                page: params.page || 1,
                                term: params.term
                            };
                            return query;
                        }
                    },
                    maximumSelectionLength: configuration.maxSpeciality,
                    language: {
                        maximumSelected: function (e) {
                            return `Solo puedes seleccionar ${e.maximum} especialidaes`;
                        }
                    },
                    dropdownParent: cards.speciality.modal.object,
                    minimumInputLength: 0,
                    placeholder: 'Seleccione especialidad',
                    allowClear: true
                });
            },
            setValues: function (object) {
                var values = [];
                $.each(object, function (i, v) {
                    if (!$('#speciality_select').find("option[value='" + v.id + "']").length) {
                        var newOption = new Option(v.text, v.id, true, true);
                        $('#speciality_select').append(newOption);
                    }
                    values.push(v.id);
                });
                $('#speciality_select').val(values).trigger("change");
            },
            clear: function () {
                $("#speciality_select").val(null).trigger("change");
            },
            events: {
                onChange: function () {
                    $("#speciality_select").on("change", function () {
                        select2.specialityTheme.clear();
                    })
                },
                init: function () {
                    this.onChange();
                }
            },
            init: function () {
                this.events.init();
                this.load();
            }
        },
        specialityTheme: {
            load: function () {
                $("#SpecialityThemesId").select2({
                    ajax: {
                        url: "/especialidades-temas/get/v2",
                        delay: 500,
                        data: function (params) {
                            var query = {
                                page: params.page || 1,
                                term: params.term,
                                specialitiesId: JSON.stringify($("#speciality_select").val()),
                                toProfile: true
                            };

                            return query;
                        }
                    },
                    maximumSelectionLength: configuration.maxThemes,
                    language: {
                        maximumSelected: function (e) {
                            return `Solo puedes seleccionar ${e.maximum} temas`;
                        }
                    },
                    dropdownParent: cards.speciality.modal.object,
                    minimumInputLength: 0,
                    placeholder: 'Seleccione tema de especialidad',
                    allowClear: true
                });
            },
            setValues: function (object) {
                var values = [];
                $.each(object, function (i, v) {
                    if (!$('#SpecialityThemesId').find("option[value='" + v.id + "']").length) {
                        var newOption = new Option(v.text, v.id, true, true);
                        $('#SpecialityThemesId').append(newOption);
                    }
                    values.push(v.id);
                });
                $('#SpecialityThemesId').val(values).trigger("change");
            },
            clear: function () {
                $("#SpecialityThemesId").val(null).trigger("change");
            },
            init: function () {
                this.load();
            }
        },
        init: function () {
            select2.sex.init();
            select2.department.init();
            select2.province.init();
            select2.district.init();
            select2.freeFirst.init();
            select2.languageLevel.init();
            select2.language.init();
            select2.speciality.init();
            select2.specialityTheme.init();
            select2.grade.init();
        }
    };

    var datepicker = {
        init: function () {
            $(".datepicker_input").datepicker({
                endDate: '+0d',
                clearBtn: false,
                dateFormat: "dd/mm/yyyy",
                orientation: "bottom"
            }).one("show", function () {
                $(this).val("01/01/1990").datepicker("update").trigger("change");
            })
            $(".datepicker_input_2010").datepicker({
                endDate: '+0d',
                clearBtn: false,
                dateFormat: "dd/mm/yyyy",
                orientation: "bottom"
            }).one("show", function () {
                $(this).val("01/01/2010").datepicker("update").trigger("change");
            })
        }
    };

    var events = {
        getTotalExperience: function () {
            $.ajax({
                url: "/abogado/perfil/get-total-experiencia",
                type: "GET"
            })
                .done(function (e) {
                    $("#total_experience_label").text(e);
                });
        },
        expandable: function () {
            $('.expandable').expander({
                slicePoint: 200,
                expandText: 'VER MÁS',
                userCollapseText: '...OCULTAR'
            });
        },
        onChangeNav: function () {

            $("#about_me_link").click(function () {
                $('html, body').animate({
                    scrollTop: $("#container_about_me").offset().top
                }, 1000);
            });

            $("#labor_experience_link").click(function () {
                $('html, body').animate({
                    scrollTop: $("#container_experience").offset().top
                }, 1000);
            });

            $("#studies_link").click(function () {
                $('html, body').animate({
                    scrollTop: $("#container_study").offset().top
                }, 1000);
            });

            $("#languages_link").click(function () {
                $('html, body').animate({
                    scrollTop: $("#container_language").offset().top
                }, 1000);
            });

            $("#publications_link").click(function () {
                $('html, body').animate({
                    scrollTop: $("#container_publication").offset().top
                }, 1000);
            });

        },
        requestReviewObservation: function () {
            $("#request_review_observations").click(function () {
                var $btn = $(this);

                swal({
                    title: "¿Está seguro?",
                    text: "Se enviará una solicitud para revisar las observaciones enviadas..",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonText: "Sí, enviar",
                    cancelButtonText: "Cancelar",
                    showLoaderOnConfirm: true,
                    preConfirm: () => {
                        return new Promise((resolve) => {
                            $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                            $.ajax({
                                url: "/abogado/perfil/solicitar-revision-observaciones",
                                type: "POST"
                            })
                                .done(function () {
                                    swal({
                                        type: "success",
                                        allowOutsideClick: false,
                                        title: "Éxito",
                                        text: "Solicitud de revisión enviada exitosamente.",
                                        confirmButtonText: "Aceptar"
                                    }).then((result) => {
                                        window.location.reload();
                                    });
                                })
                                .fail(function (e) {
                                    swal({
                                        type: "error",
                                        title: "Error al validar el perfil.",
                                        text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                                        confirmButtonText: "Aceptar"
                                    });
                                })
                                .always(function () {
                                    $btn.removeClass("m-loader m-loader--right m-loader--light").attr("disabled", false);
                                });
                        });
                    },
                    allowOutsideClick: () => !swal.isLoading()
                });
            });
        },
        onViewAllQualifactions: function () {
            $("body").on("click", ".on_portlet_qualification", function () {
                $("#portlet_qualifications").removeClass("d-none");
                $("#portlet_main").addClass("d-none");
            });
        },
        onViewAllPublications: function () {
            $("body").on("click", ".on_portlet_publication", function () {
                $("#portlet_publications").removeClass("d-none");
                $("#portlet_main").addClass("d-none");
            });
        },
        onViewMainPortlet: function () {
            $("body").on("click", ".view_main_portlet", function () {
                $("#portlet_qualifications").addClass("d-none");
                $("#portlet_publications").addClass("d-none");
                $("#portlet_main").removeClass("d-none");
            });
        },
        onViewAddPublicationPortlet: function () {
            $("body").on("click", ".view_add_publication_portlet", function () {
                $("#portlet_publications").addClass("d-none");
                $("#portlet_add_publications").removeClass("d-none");
            });
        },
        onCancelAddPublication: function () {
            $("body").on("click", ".btn-cancel-publication", function () {
                cards.publication.modal.form.object.resetForm();
                cards.publication.modal.object.find("[name='Photo']").val(null);
                cards.publication.modal.object.find(".custom-file-label").text("Seleccione un archivo");
                cards.publication.summenote.description.clear();
                $("#portlet_publications").removeClass("d-none");
                $("#portlet_add_publications").addClass("d-none");
                $('.upload-result-0').prop("disabled", true);
                $('#upload-demo-0, #upload-offi-0').hide();
                $(".cr-slider-wrap").hide();
            });
        },
        onRequestValidatedProfile: function () {
            $("#request_validated_profile").on("click", function () {
                swal({
                    title: "¿Está seguro?",
                    text: "Se generará una solicitud para la revisión de tu perfil.",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonText: "Sí, enviar",
                    cancelButtonText: "Cancelar",
                    showLoaderOnConfirm: true,
                    preConfirm: () => {
                        return new Promise((resolve) => {
                            var $btn = $(this);
                            $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                            $.ajax({
                                url: "/abogado/perfil/solicitar-primera-revision",
                                type: "POST"
                            })
                                .done(function () {
                                    swal({
                                        type: "success",
                                        allowOutsideClick: false,
                                        title: "¡Gracias por tu registro!",
                                        text: "Nos comunicaremos a la brevedad para poder continuar con el proceso.",
                                        confirmButtonText: "Aceptar"
                                    }).then((result) => {
                                        window.location.href = "/abogado/perfil";
                                    });
                                })
                                .fail(function (e) {
                                    swal({
                                        type: "error",
                                        title: "Error al enviar la solicitud.",
                                        text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                                        confirmButtonText: "Aceptar"
                                    });
                                })
                                .always(function () {
                                    $btn.removeClass("m-loader m-loader--right m-loader--light").attr("disabled", false);
                                });
                        });
                    },
                    allowOutsideClick: () => !swal.isLoading()
                });
            });
        },
        onSelectInterview: function () {
            $("#btn_select_interview").on("click", function () {
                var lawyerInterviewId = $("input[name='lawyer_interview_selected']:checked").val();

                if (lawyerInterviewId === undefined || lawyerInterviewId === null) {
                    toastr.info("Es necesario seleccionar una opción", "Información");
                    return;
                }

                swal({
                    title: "¿Está seguro?",
                    text: "Seleccionará el horario y se agendará una entrevista.",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonText: "Sí",
                    cancelButtonText: "Cancelar",
                    showLoaderOnConfirm: true,
                    preConfirm: () => {
                        return new Promise((resolve) => {
                            var $btn = $(this);
                            $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                            $.ajax({
                                url: `/abogado/perfil/seleccionar-entrevista?lawyerInterviewId=${lawyerInterviewId}`,
                                type: "POST"
                            })
                                .done(function () {
                                    swal({
                                        type: "success",
                                        allowOutsideClick: false,
                                        title: "¡Gracias!",
                                        text: "Nos comunicaremos a la brevedad para poder continuar con el proceso.",
                                        confirmButtonText: "Aceptar"
                                    }).then((result) => {
                                        window.location.reload();
                                    });
                                })
                                .fail(function (e) {
                                    swal({
                                        type: "error",
                                        title: "Error al enviar la solicitud.",
                                        text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                                        confirmButtonText: "Aceptar"
                                    });
                                })
                                .always(function () {
                                    $btn.removeClass("m-loader m-loader--right m-loader--light").attr("disabled", false);
                                });
                        });
                    },
                    allowOutsideClick: () => !swal.isLoading()
                });
            });
        },
        init: function () {
            this.getTotalExperience();
            this.expandable();
            this.requestReviewObservation();
            this.onViewAllQualifactions();
            this.onViewAllPublications();
            this.onChangeNav();
            this.onViewMainPortlet();
            this.onViewAddPublicationPortlet();
            this.onCancelAddPublication();
            this.onRequestValidatedProfile();
            this.onSelectInterview();
        }
    };

    var imageaction = {
        indexUpload: -1,
        uploadCropBlogImage: null,
        uploadCropLawyerPhoto: null,
        init: function () {
            $('#upload-demo-0').hide();
            $('#upload-demo-1').hide();
            imageaction.uploadCropBlogImage = $('#upload-demo-0').croppie({
                enableExif: true,
                viewport: {
                    width: 525,
                    height: 305,
                    type: 'square'
                },
                boundary: {
                    width: 530,
                    height: 320
                }
            });
            imageaction.uploadCropLawyerPhoto = $('#upload-demo-1').croppie({
                enableExif: true,
                viewport: {
                    width: 200,
                    height: 200,
                    type: 'circle'
                },
                boundary: {
                    width: 205,
                    height: 205
                }
            });
            $('#Image-0').on('change', function () {
                $("#upload-offi-0").hide();
                $('#upload-demo-0').show();
                var reader = new FileReader();
                reader.onload = function (event) {
                    imageaction.uploadCropBlogImage.croppie('bind', {
                        url: event.target.result
                    }).then(function () {
                        $('.upload-result-0').prop("disabled", false);
                        $(".cr-slider-wrap").show();
                    });
                }
                reader.readAsDataURL(this.files[0]);
                $(this).parent().find(".custom-file-label").text("Archivo seleccionado");
            });
            $('#Image-1').on('change', function () {
                $("#upload-offi-1").hide();
                $('#upload-demo-1').show();
                var reader = new FileReader();
                reader.onload = function (event) {
                    imageaction.uploadCropLawyerPhoto.croppie('bind', {
                        url: event.target.result
                    }).then(function () {
                        $('.upload-result-1').prop("disabled", false);
                        $(".cr-slider-wrap").show();
                    });
                }
                reader.readAsDataURL(this.files[0]);
                $(this).parent().find(".custom-file-label").text("Archivo seleccionado");
            });
            $('.upload-result-0').click(function (event) {
                imageaction.indexUpload = 0
                imageaction.uploadCropBlogImage.croppie('result', {
                    type: 'canvas',
                    size: 'viewport'
                }).then(function (response) {
                    $("#upload-preview").attr("src", response);
                    $("#preview-modal").modal("show");
                });
            });
            $('.upload-result-1').click(function (event) {
                imageaction.indexUpload = 1
                imageaction.uploadCropLawyerPhoto.croppie('result', {
                    type: 'canvas',
                    size: 'viewport'
                }).then(function (response) {
                    $("#upload-preview").attr("src", response);
                    $("#preview-modal").modal("show");
                });
            });
            $('#btnCropSave').click(function (event) {
                if (imageaction.indexUpload == 0) {
                    imageaction.uploadCropBlogImage.croppie('result', {
                        type: 'canvas',
                        size: 'viewport'
                    }).then(function (response) {
                        $("#preview-modal").modal("hide");
                        $("#img-offi-" + imageaction.indexUpload).attr("src", response);
                        $("#upload-demo-" + imageaction.indexUpload).hide();
                        $("#upload-offi-" + imageaction.indexUpload).show();
                        $('.upload-result-' + imageaction.indexUpload).prop("disabled", true);
                        $("#UrlCropImg-" + imageaction.indexUpload).val(response);
                    });
                } else {
                    imageaction.uploadCropLawyerPhoto.croppie('result', {
                        type: 'canvas',
                        size: 'viewport'
                    }).then(function (response) {
                        $("#preview-modal").modal("hide");
                        $("#img-offi-" + imageaction.indexUpload).attr("src", response);
                        $("#upload-demo-" + imageaction.indexUpload).hide();
                        $("#upload-offi-" + imageaction.indexUpload).show();
                        $('.upload-result-' + imageaction.indexUpload).prop("disabled", true);
                        $("#UrlCropImg-" + imageaction.indexUpload).val(response);
                    });
                }
            });
        }
    };

    var boostrapSwitch = {
        onChangeVisibleProfile: function () {
            if ($("#PublicProfile").is(":checked")) {
                $(".bootstrap-switch-label").text("Perfil Privado");
            } else {
                $(".bootstrap-switch-label").text("Perfil Público");
            }

            $("#PublicProfile").on('switchChange.bootstrapSwitch', function () {
                var isPublic = $(this).is(":checked");

                if (isPublic) {
                    $(".bootstrap-switch-label").text("Perfil Privado");
                } else {
                    $(".bootstrap-switch-label").text("Perfil Público");
                }

                $.ajax({
                    type: "POST",
                    url: `/abogado/perfil/actualizar-visibilidad-perfil?publicProfile=${isPublic}`
                })
                    .done(function (e) {
                        toastr.success("Perfil actualizado éxitosamente.", "Hecho!");
                    })
                    .fail(function () {
                        toastr.error("Error al actualizar la visibilidad del perfil.", "Error!");
                    });
            });
        },
        init: function () {
            $('#PublicProfile').bootstrapSwitch();
            this.onChangeVisibleProfile();
        }
    };

    return {
        init: function () {
            select2.init();
            cards.init();
            datepicker.init();
            events.init();
            imageaction.init();
            boostrapSwitch.init();
        }
    };
}();

$(() => {
    profile.init();
});