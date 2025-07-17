var index = function () {

	var lawyerId = $("#LawyerId").val();
	var legalcaseId = $("#LegalCaseId").val();

	var profile = {
		load: function () {
			mApp.block("#container_lawyer_profile", {
				message: "Cargando perfil..."
			});

			$.ajax({
				url: `/lc/abogado/perfil/get?lawyerId=${lawyerId}&legalcaseId=${legalcaseId}`,
				dataType: "html",
				type: "get"
			})
				.done(function (e) {
					$("#container_lawyer_profile").html(e);
					mApp.unblock("#container_lawyer_profile");
					events.expandable();
					events.onChangeNav();
					commentaries.init();
				});
		},
		init: function () {
			this.load();
		}
	};

	var events = {
		onValidateLawyer: function () {
			$("#validate_lawyer").on("click", function () {
				swal({
					type: "warning",
					title: "Validará el perfil.",
					text: "El usuario podrá tener acceso a toda la funcionalidad como abogado. ¿Esta seguro?",
					confirmButtonText: "Aceptar",
					showCancelButton: true,
					showLoaderOnConfirm: true,
					allowOutsideClick: () => !swal.isLoading(),
					preConfirm: () => {
						return new Promise(() => {
							$.ajax({
								type: "POST",
								url: `/lc/abogado/perfil/aceptar-abogado?lawyerId=${lawyerId}`
							})
								.done(function () {
									window.location.reload();
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
		acceptLawyerPostulation: function () {
			$("#accept_lawyer_postulacion").on("click", function () {
				var $btn = $(this);
				var lawyerId = $(this).data("id");
				var legalcaseId = $(this).data("legalcaseid");
				var formData = new FormData();
				formData.append("LawyerId", lawyerId);
				formData.append("LegalCaseId", legalcaseId);
				$btn.addLoader();
				$.ajax({
					url: `/mis-casos/seleccionar-abogado`,
					data: formData,
					type: "POST",
					contentType: false,
					processData: false
				})
					.done(function () {
						window.location.href = `/mis-casos/${legalcaseId}/detalle`;
					})
					.fail(function (e) {
						$btn.removeLoader();
						swal({
							type: "error",
							title: "Error al guardar los datos.",
							text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
							confirmButtonText: "Aceptar"
						});
					})
			});
		},
		onViewAllQualifactions: function () {
			$("body").on("click", ".on_portlet_qualification", function () {
				$("#portlet_qualifications").removeClass("d-none");
				$("#portlet_main").addClass("d-none");
			});
		},
		onViewMainPortlet: function () {
			$("body").on("click", ".view_main_portlet", function () {
				$("#portlet_qualifications").addClass("d-none");
				$("#portlet_main").removeClass("d-none");
			});
		},
		onViewAllPublications: function () {
			$("body").on("click", ".on_portlet_publication", function () {
				$("#portlet_publications").removeClass("d-none");
				$("#portlet_main").addClass("d-none");
			});
		},
		init: function () {
			this.onValidateLawyer();
			this.acceptLawyerPostulation();
			this.onViewAllQualifactions();
			this.onViewMainPortlet();
			this.onViewAllPublications();
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
	var modal = {
		reject: {
			object: $("#reject_modal"),
			form: {
				object: $("#reject_form").validate({
					submitHandler: function (formElement, e) {
						e.preventDefault();
						var $btn = $(formElement).find("button[type='submit']");
						$btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
						var formData = new FormData(formElement);
						modal.reject.object.find(":input").attr("disabled", true);
						$.ajax({
							url: "/lc/abogado/perfil/rechazar-abogado",
							type: "POST",
							data: formData,
							contentType: false,
							processData: false
						})
							.done(function (e) {
								modal.reject.object.modal("hide");
								window.location.reload();
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
								modal.reject.object.find(":input").attr("disabled", false);
							});
					}
				})
			},
			events: {
				onHidden: function () {
					modal.reject.object.on("hidden.bs.modal", function (e) {
						modal.reject.form.object.resetForm();
					});
				},
				onShow: function () {
					$("#reject_lawyer").click(function () {
						modal.reject.object.find("[name='LawyerId']").val($("#LawyerId").val());
						modal.reject.object.modal("show");
					});
				},
				init: function () {
					this.onHidden();
					this.onShow();
				}
			},
			init: function () {
				this.events.init();
			}
		},
		observation: {
			object: $("#observation_modal"),
			events: {
				onShow: function () {
					$("#view_observations").click(function () {
						modal.observation.object.modal("show");
						modal.observation.object.find(":input").attr("disabled", true);
						$.ajax({
							url: `/lc/abogado/perfil/get-observacion?lawyerId=${lawyerId}`,
							type: "GET"
						})
							.done(function (e) {
								modal.observation.object.find("[name='Observation']").val(e);
								modal.observation.object.find(":input").attr("disabled", false);
							});
					});
				},
				init: function () {
					this.onShow();
				}
			},
			init: function () {
				this.events.init();
			}
		},
		init: function () {
			this.reject.init();
			this.observation.init();
		}
	};

	return {
		init: function () {
			profile.init();
			events.init();
			modal.init();
		}
	};
}();

$(() => {
	index.init();
});