#pragma checksum "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\EconomicManagement\Partials\Modals\WithdrawalRequestDetailPartialModal.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "b0af27b7e75d3d4471c63a1d9d7d9bbf6fcf4162a894bbf7aa58fb6ad2c29908"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCoreGeneratedDocument.Areas_Lawyer_Views_EconomicManagement_Partials_Modals_WithdrawalRequestDetailPartialModal), @"mvc.1.0.view", @"/Areas/Lawyer/Views/EconomicManagement/Partials/Modals/WithdrawalRequestDetailPartialModal.cshtml")]
namespace AspNetCoreGeneratedDocument
{
    #line default
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Mvc;
    using global::Microsoft.AspNetCore.Mvc.Rendering;
    using global::Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\_ViewImports.cshtml"
using LC.WEB.Extensions

#nullable disable
    ;
#nullable restore
#line 2 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\_ViewImports.cshtml"
using LC.WEB.Areas.Lawyer.Models

#nullable disable
    ;
#nullable restore
#line 3 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\_ViewImports.cshtml"
using LC.CORE.Helpers

#nullable disable
    ;
#nullable restore
#line 4 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\_ViewImports.cshtml"
using LC.CORE.Extensions

#nullable disable
    ;
    #line default
    #line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"b0af27b7e75d3d4471c63a1d9d7d9bbf6fcf4162a894bbf7aa58fb6ad2c29908", @"/Areas/Lawyer/Views/EconomicManagement/Partials/Modals/WithdrawalRequestDetailPartialModal.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"fbe9f53555a70c23360e9008a8b6fceff255765bc8836e7eeb549b951c1cee36", @"/Areas/Lawyer/Views/_ViewImports.cshtml")]
    #nullable restore
    internal sealed class Areas_Lawyer_Views_EconomicManagement_Partials_Modals_WithdrawalRequestDetailPartialModal : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<div class=""modal fade bd-example-modal-lg"" tabindex=""-1"" role=""dialog"" aria-labelledby=""myLargeModalLabel"" aria-hidden=""true"" id=""withdrawal_request_detail_modal"">
    <div class=""modal-dialog modal-lg"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title"">Editar Caso</h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>
            <div class=""modal-body"">
                <div class=""row"">
                    <div class=""form-group m-form__group col-lg-12"">
                        <label>Banco</label>
                        <input class=""form-control m-input"" name=""FinancialEntity"" />
                    </div>
                </div>
                <div class=""row"">
                    <div class=""form-group m-form__group col-lg-12"">
                        <label>Cuenta de Banco<");
            WriteLiteral(@"/label>
                        <input class=""form-control m-input"" name=""BankAccount"" />
                    </div>
                </div>
                <div class=""row"">
                    <div class=""form-group m-form__group col-lg-12"">
                        <label>Cuenta Interbancaria</label>
                        <input class=""form-control m-input"" name=""InterbankAccount"" />
                    </div>
                </div>
                <div class=""row"">
                    <div class=""form-group m-form__group col-lg-6"">
                        <label>Nombre Completo</label>
                        <input class=""form-control m-input"" name=""FullName"" />
                    </div>
                    <div class=""form-group m-form__group col-lg-6"">
                        <label>Dni</label>
                        <input class=""form-control m-input"" name=""Dni"" />
                    </div>
                </div>
                <div class=""row"">
                    <div class=");
            WriteLiteral(@"""form-group m-form__group col-lg-12"">
                        <label>Monto Solicitado</label>
                        <input class=""form-control m-input"" name=""Amount"" />
                    </div>
                </div>
                <div class=""row"">
                    <div class=""form-group m-form__group col-lg-12"">
                        <label>Observaciones</label>
                        <textarea rows=""4"" name=""Observation"" class=""form-control m-input""></textarea>
                    </div>
                </div>
            </div>
            <div class=""modal-footer"">
                <button type=""button"" class=""btn btn-secondary"" data-dismiss=""modal"">Cerrar</button>
            </div>
        </div>
    </div>
</div>");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
