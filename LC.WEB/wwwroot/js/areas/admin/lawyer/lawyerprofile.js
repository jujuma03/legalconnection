var index = function () {

	var lawyerId = $("#LawyerId").val();

	var profile = {
		load: function () {
			mApp.block("#container_lawyer_profile", {
				message: "Cargando perfil..."
			});

			$.ajax({
				url: `/lc/abogado/perfil/get?lawyerId=${lawyerId}`,
				dataType: "html",
				type: "get"
			})
				.done(function (e) {
					$("#container_lawyer_profile").html(e);
					mApp.unblock("#container_lawyer_profile");
					events.expandable();
					events.onChangeNav();
					commentaries.init();
					publications.init();
				});
		},
		init: function () {
			this.load();
		}
	};

	var events = {
		getTotalExperience: function () {
			//$.ajax({
			//	url: `/lc/abogado/perfil/get-total-experiencia/${lawyerId}`,
			//	type: "GET"
			//})
			//	.done(function (e) {
			//		$("#total_experience_label").text(e);
			//	});
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
		onViewAllQualifactions: function () {
			$("body").on("click", ".on_portlet_qualification", function () {
				$("#portlet_qualifications").removeClass("d-none");
				$("#portlet_main").addClass("d-none");
			});
		},
		onViewAllQualifactions: function () {
			$("body").on("click", ".on_portlet_publication", function () {
				$("#portlet_publications").removeClass("d-none");
				$("#portlet_main").addClass("d-none");
			});
		},
		onViewMainPortlet: function () {
			$("body").on("click", ".view_main_portlet", function () {
				$("#portlet_publications").addClass("d-none");
				$("#portlet_qualifications").addClass("d-none");
				$("#portlet_main").removeClass("d-none");
			});
		},
		adminEvents: {
			onValidateProfile: function () {
				$("#validate_profile").on("click", function () {
					swal({
						title: "¿Está seguro?",
						text: "El perfil será validado y continuará con el proceso.",
						type: "warning",
						showCancelButton: true,
						confirmButtonText: "Sí, validar",
						cancelButtonText: "Cancelar",
						showLoaderOnConfirm: true,
						preConfirm: () => {
							return new Promise((resolve) => {
								var $btn = $(this);
								$btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
								$.ajax({
									url: `/admin/abogados/validar-perfil-abogado?lawyerId=${lawyerId}`,
									type: "POST"
								})
									.done(function () {
										swal({
											type: "success",
											allowOutsideClick: false,
											title: "Éxito",
											text: "Perfil validado satisfactoriamente.",
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
			onValidateInterview: function () {
				$("#validate_lawyer").on("click", function () {
					swal({
						title: "¿Está seguro?",
						text: "El abogado será validado y aceptado.",
						type: "warning",
						showCancelButton: true,
						confirmButtonText: "Sí, validar",
						cancelButtonText: "Cancelar",
						showLoaderOnConfirm: true,
						preConfirm: () => {
							return new Promise((resolve) => {
								var $btn = $(this);
								$btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
								$.ajax({
									url: `/admin/abogados/aceptar-abogado?lawyerId=${lawyerId}`,
									type: "POST"
								})
									.done(function () {
										swal({
											type: "success",
											allowOutsideClick: false,
											title: "Éxito",
											text: "Abogado validado satisfactoriamente.",
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
			onRejectLawyer: function () {
				$("#reject_lawyer").on("click", function () {
					swal({
						title: "¿Está seguro?",
						text: "El abogado será rechazado.",
						type: "warning",
						showCancelButton: true,
						confirmButtonText: "Sí, denegar",
						cancelButtonText: "Cancelar",
						showLoaderOnConfirm: true,
						preConfirm: () => {
							return new Promise((resolve) => {
								var $btn = $(this);
								$btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
								$.ajax({
									url: `/admin/abogados/rechazar-abogado?lawyerId=${lawyerId}`,
									type: "POST"
								})
									.done(function () {
										swal({
											type: "success",
											allowOutsideClick: false,
											title: "Éxito",
											text: "Abogado rechazado.",
											confirmButtonText: "Aceptar"
										}).then(() => {
											window.location.href = "/admin/abogados/nuevos";
										});
									})
									.fail(function (e) {
										swal({
											type: "error",
											title: "Error al rechazar al abogado.",
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
			onAcceptProfileWithChanges: function () {
				$("#accept_profile_with_changes").on("click", function () {
					swal({
						title: "¿Está seguro?",
						text: "Los cambios del abogado serán aprobados.",
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
									url: `/admin/abogados/aceptar-cambios-abogado?lawyerId=${lawyerId}`,
									type: "POST"
								})
									.done(function () {
										swal({
											type: "success",
											allowOutsideClick: false,
											title: "Éxito",
											text: "Cambios validados.",
											confirmButtonText: "Aceptar"
										}).then(() => {
											window.location.reload();
										});
									})
									.fail(function (e) {
										swal({
											type: "error",
											title: "Error al aceptar los cambios del abogado.",
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
			onRejectProfileWithChanges: function () {
				$("#reject_profile_with_changes").on("click", function () {
					swal({
						title: "¿Está seguro?",
						text: "Los cambios del abogado serán rechazados.",
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
									url: `/admin/abogados/rechazar-cambios-abogado?lawyerId=${lawyerId}`,
									type: "POST"
								})
									.done(function () {
										swal({
											type: "success",
											allowOutsideClick: false,
											title: "Éxito",
											text: "Cambios rechazados.",
											confirmButtonText: "Aceptar"
										}).then(() => {
											window.location.reload();
										});
									})
									.fail(function (e) {
										swal({
											type: "error",
											title: "Error al denegar los cambios del abogado.",
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
				this.onValidateProfile();
				this.onValidateInterview();
				this.onRejectLawyer();
				this.onAcceptProfileWithChanges();
				this.onRejectProfileWithChanges();
			}
		},
		init: function () {
			this.onViewAllQualifactions();
			this.onViewMainPortlet();
			this.adminEvents.init();
			this.getTotalExperience();
		}
	};

	var modal = {
		lawyerInterview: {
			object: $("#request_interview_modal"),
			form: {
				object: $("#request_interview_form").validate({
					submitHandler: function (formElement, e) {
						e.preventDefault();
						var $btn = $(formElement).find("button[type='submit']");
						$btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
						var formData = new FormData(formElement);
						modal.lawyerInterview.object.find(":input").attr("disabled", true);
						$.ajax({
							url: "/admin/abogados/solicitar-entrevista",
							type: "POST",
							data: formData,
							contentType: false,
							processData: false
						})
							.done(function (e) {
								modal.lawyerInterview.object.modal("hide");
								swal({
									type: "success",
									allowOutsideClick: false,
									title: "Éxito",
									text: "Solicitud enviada con éxito.",
									confirmButtonText: "Aceptar"
								}).then((result) => {
									window.location.reload();
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
								modal.lawyerInterview.object.find(":input").attr("disabled", false);
							});
					}
				})
			}
		},
		lawyerObservation: {
			object: $("#send_observation_modal"),
			form: {
				object: $("#send_observation_form").validate({
					submitHandler: function (formElement, e) {
						e.preventDefault();
						var $btn = $(formElement).find("button[type='submit']");
						$btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
						var formData = new FormData(formElement);
						modal.lawyerObservation.object.find(":input").attr("disabled", true);
						$.ajax({
							url: "/admin/abogados/enviar-observaciones-abogado",
							type: "POST",
							data: formData,
							contentType: false,
							processData: false
						})
							.done(function (e) {
								modal.lawyerObservation.object.modal("hide");
								swal({
									type: "success",
									allowOutsideClick: false,
									title: "Éxito",
									text: "Observaciones enviadas con éxito.",
									confirmButtonText: "Aceptar"
								}).then((result) => {
									window.location.reload();
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
								modal.lawyerObservation.object.find(":input").attr("disabled", false);
							});
					}
				})
			}
		}
	};


	var datepicker = {
		init: function () {
			$(".input-datepicker").datepicker({
				startDate: '+0d',
				clearBtn: false,
				dateFormat: "dd/mm/yyyy",
				orientation: "bottom"
			});
		}
	};

	var timepicker = {
		init: function () {
			$(".input-timepicker").timepicker({
				minuteStep: 5
			});
		}
	};

	var commentaries = {
		activePage: 1,
		recordsPerDraw: 10,
		object: $("#container_qualification_v2"),
		update: function () {
			mApp.block(commentaries.object, {
				message: "Cargando califaciones..."
			});
			$.ajax({
				url: `/lc/abogado/perfil/get-all-califaciones/${lawyerId}`,
				data: {
					page: commentaries.activePage,
					rpdraw: commentaries.recordsPerDraw
				},
				type: "GET",
				dataType: "html"
			})
				.done(function (e) {
					$("#lawyer_qualification_v2_div").html(e);
					events.expandable();
				})
				.always(function () {
					mApp.unblock(commentaries.object);
				});
		},
		events: {
			onChangePage: function () {
				commentaries.object.on("click", ".previous-item", function () {
					commentaries.activePage--;
					commentaries.update();
				});

				commentaries.object.on("click", ".next-item", function () {
					commentaries.activePage++;
					commentaries.update();
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
	};
	var publications = {
		activePage: 1,
		recordsPerDraw: 10,
		object: $("#container_publication_v2"),
		update: function () {
			mApp.block(publications.object, {
				message: "Cargando publicaciones..."
			});
			$.ajax({
				url: `/lc/abogado/perfil/get-all-publicaciones/${lawyerId}`,
				data: {
					page: publications.activePage,
					rpdraw: publications.recordsPerDraw
				},
				type: "GET",
				dataType: "html"
			})
				.done(function (e) {
					$("#lawyer_publication_v2_div").html(e);
					events.expandable();
				})
				.always(function () {
					mApp.unblock(publications.object);
				});
		},
		events: {
			onChangePage: function () {
				publications.object.on("click", ".previous-item", function () {
					publications.activePage--;
					publications.update();
				});

				publications.object.on("click", ".next-item", function () {
					publications.activePage++;
					publications.update();
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
	};
	return {
		init: function () {
			profile.init();
			events.init();
			datepicker.init();
			timepicker.init();
		}
	};
}();

$(() => {
	index.init();
});