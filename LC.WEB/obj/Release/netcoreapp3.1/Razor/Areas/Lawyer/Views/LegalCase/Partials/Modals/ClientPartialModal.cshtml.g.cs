#pragma checksum "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\LegalCase\Partials\Modals\ClientPartialModal.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "638374531f69af590b173e48146ada1d7dd35222967bb4d7e537da277c623f84"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCoreGeneratedDocument.Areas_Lawyer_Views_LegalCase_Partials_Modals_ClientPartialModal), @"mvc.1.0.view", @"/Areas/Lawyer/Views/LegalCase/Partials/Modals/ClientPartialModal.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"638374531f69af590b173e48146ada1d7dd35222967bb4d7e537da277c623f84", @"/Areas/Lawyer/Views/LegalCase/Partials/Modals/ClientPartialModal.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"fbe9f53555a70c23360e9008a8b6fceff255765bc8836e7eeb549b951c1cee36", @"/Areas/Lawyer/Views/_ViewImports.cshtml")]
    #nullable restore
    internal sealed class Areas_Lawyer_Views_LegalCase_Partials_Modals_ClientPartialModal : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<
#nullable restore
#line 1 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\LegalCase\Partials\Modals\ClientPartialModal.cshtml"
       LC.ENTITIES.Custom.General.ClientCustomModel

#line default
#line hidden
#nullable disable
    >
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/general/profile.jpg"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("width", new global::Microsoft.AspNetCore.Html.HtmlString("80"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("border-radius:50%;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div class=""modal fade bd-example-modal-lg"" tabindex=""-1"" role=""dialog"" aria-labelledby=""myLargeModalLabel"" aria-hidden=""true"" id=""client_modal"">
    <div class=""modal-dialog modal-dialog-centered"">
        <div class=""modal-content"">
            <div class=""modal-body"">
                <div class=""row text-center"">
                    <h5 class=""col-12"">Ponte en contacto con tu cliente</h5>
                    <div class=""col-12 row"">
                        <div class=""col-12"">
");
#nullable restore
#line 11 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\LegalCase\Partials\Modals\ClientPartialModal.cshtml"
                             if (string.IsNullOrEmpty(Model.PhotoUrl))
                            {

#line default
#line hidden
#nullable disable

            WriteLiteral("                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "638374531f69af590b173e48146ada1d7dd35222967bb4d7e537da277c623f846359", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 14 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\LegalCase\Partials\Modals\ClientPartialModal.cshtml"
                            }
                            else
                            {

#line default
#line hidden
#nullable disable

            WriteLiteral("                                <img");
            BeginWriteAttribute("src", " src=\"", 897, "\"", 930, 2);
            WriteAttributeValue("", 903, "/documentos/", 903, 12, true);
            WriteAttributeValue("", 915, 
#nullable restore
#line 17 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\LegalCase\Partials\Modals\ClientPartialModal.cshtml"
                                                       Model.PhotoUrl

#line default
#line hidden
#nullable disable
            , 915, 15, false);
            EndWriteAttribute();
            WriteLiteral(" width=\"80\" style=\"border-radius:50%;\" />\r\n");
#nullable restore
#line 18 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\LegalCase\Partials\Modals\ClientPartialModal.cshtml"
                            }

#line default
#line hidden
#nullable disable

            WriteLiteral("                        </div>\r\n                        <div class=\"col-12 mt-3\">\r\n                            <h5 class=\"m--font-boldest\">");
            Write(
#nullable restore
#line 21 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\LegalCase\Partials\Modals\ClientPartialModal.cshtml"
                                                         Model.FullName

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("</h5>\r\n                            <span class=\"m--font-bold\">");
            Write(
#nullable restore
#line 22 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\LegalCase\Partials\Modals\ClientPartialModal.cshtml"
                                                        Model.Email

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("</span><br />\r\n                            <span class=\"m--font-bold\">");
            Write(
#nullable restore
#line 23 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\LegalCase\Partials\Modals\ClientPartialModal.cshtml"
                                                        Model.Department

#line default
#line hidden
#nullable disable
            );
            WriteLiteral(" - ");
            Write(
#nullable restore
#line 23 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\LegalCase\Partials\Modals\ClientPartialModal.cshtml"
                                                                            Model.Province

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("</span>\r\n                        </div>\r\n                    </div>\r\n                    \r\n                    <h5 class=\"col-12 mt-3\">Cont&aacute;ctalo al : <span class=\"m--font-boldest2\">");
            Write(
#nullable restore
#line 27 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\LegalCase\Partials\Modals\ClientPartialModal.cshtml"
                                                                                                   Model.PhoneNumber

#line default
#line hidden
#nullable disable
            );
            WriteLiteral(@"</span></h5>
                    <div class=""col-12 mt-4"">
                        <button data-dismiss=""modal"" type=""button"" class=""btn btn-primary m-btn m-btn--custom"">Entendido</button>
                    </div>
                </div>
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<LC.ENTITIES.Custom.General.ClientCustomModel> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
