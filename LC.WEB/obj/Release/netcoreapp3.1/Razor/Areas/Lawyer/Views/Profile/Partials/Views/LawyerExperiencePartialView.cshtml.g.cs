#pragma checksum "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "178e91ddb4e79fc8a58aa6e50b9a2d0f8ec9286be25cd0f3e4d7510b88cfeece"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCoreGeneratedDocument.Areas_Lawyer_Views_Profile_Partials_Views_LawyerExperiencePartialView), @"mvc.1.0.view", @"/Areas/Lawyer/Views/Profile/Partials/Views/LawyerExperiencePartialView.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"178e91ddb4e79fc8a58aa6e50b9a2d0f8ec9286be25cd0f3e4d7510b88cfeece", @"/Areas/Lawyer/Views/Profile/Partials/Views/LawyerExperiencePartialView.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"fbe9f53555a70c23360e9008a8b6fceff255765bc8836e7eeb549b951c1cee36", @"/Areas/Lawyer/Views/_ViewImports.cshtml")]
    #nullable restore
    internal sealed class Areas_Lawyer_Views_Profile_Partials_Views_LawyerExperiencePartialView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<
#nullable restore
#line 1 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml"
       List<LC.WEB.Areas.Lawyer.Models.Profile.ExperienceViewModel>

#line default
#line hidden
#nullable disable
    >
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/general/company.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("logo_company_experience"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("Logo compañia"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml"
 if (Model.Any())
{
    

#line default
#line hidden
#nullable disable

#nullable restore
#line 5 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml"
     for (int i = 0; i < Model.Count(); i++)
    {

#line default
#line hidden
#nullable disable

            WriteLiteral("        <div class=\"row\">\r\n            <div class=\"col-xl-3 col-lg-3 col-sm-3 mt-2 text-center\">\r\n");
#nullable restore
#line 9 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml"
                 if (string.IsNullOrEmpty(Model[i].PhotoUrl))
                {

#line default
#line hidden
#nullable disable

            WriteLiteral("                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "178e91ddb4e79fc8a58aa6e50b9a2d0f8ec9286be25cd0f3e4d7510b88cfeece6581", async() => {
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
#line 12 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml"

                }
                else
                {

#line default
#line hidden
#nullable disable

            WriteLiteral("                    <img");
            BeginWriteAttribute("src", " src=\"", 528, "\"", 564, 2);
            WriteAttributeValue("", 534, "/documentos/", 534, 12, true);
            WriteAttributeValue("", 546, 
#nullable restore
#line 16 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml"
                                           Model[i].PhotoUrl

#line default
#line hidden
#nullable disable
            , 546, 18, false);
            EndWriteAttribute();
            WriteLiteral(" class=\"logo_company_experience\" alt=\"Logo compañia\" />\r\n");
#nullable restore
#line 17 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml"
                }

#line default
#line hidden
#nullable disable

            WriteLiteral("            </div>\r\n            <div class=\"mt-2 col-xl-9 col-lg-9 col-sm-9\">\r\n                <div class=\"col-12 row pr-0\">\r\n                    <p class=\"col-8 m--font-boldest\">\r\n                        ");
            Write(
#nullable restore
#line 22 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml"
                         Model[i].Company

#line default
#line hidden
#nullable disable
            );
            WriteLiteral(" - ");
            Write(
#nullable restore
#line 22 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml"
                                             Model[i].Position

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n                    </p>\r\n                    <div class=\"col-4 d-flex align-items-center justify-content-end\">\r\n                        <i data-id=\"");
            Write(
#nullable restore
#line 25 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml"
                                     Model[i].Id

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\" class=\"fa fa-edit options_partialview btn-experience-edit\"></i>\r\n                        <i data-id=\"");
            Write(
#nullable restore
#line 26 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml"
                                     Model[i].Id

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\" class=\"fa fa-trash options_partialview btn-experience-delete\"></i>\r\n                    </div>\r\n                </div>\r\n                <p class=\"col-12\">");
            Write(
#nullable restore
#line 29 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml"
                                   Model[i].StartDate

#line default
#line hidden
#nullable disable
            );
            WriteLiteral(" - ");
            Write(
#nullable restore
#line 29 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml"
                                                          string.IsNullOrEmpty(Model[i].EndDate) ? "A la actualidad" : Model[i].EndDate

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("</p>\r\n                <p class=\"col-12 text-justify pr-0 expandable\">\r\n                    ");
            Write(
#nullable restore
#line 31 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml"
                     Model[i].Description

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n                </p>\r\n            </div>\r\n        </div>\r\n");
            WriteLiteral("        <div class=\"m-divider\">\r\n            <span></span>\r\n        </div>\r\n");
#nullable restore
#line 39 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml"
    }

#line default
#line hidden
#nullable disable

#nullable restore
#line 39 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml"
     
}
else
{

#line default
#line hidden
#nullable disable

            WriteLiteral(@"    <div class=""m-demo__preview col-12"">
        <blockquote class=""blockquote"">
            <p class=""mb-0""> Aún no se agregan experiencias laborales</p>
            <footer class=""blockquote-footer""><cite title=""Source Title""> Recuerda que para recibir m&aacute;s ofertas de casos necesitas tener tu perfil completo y asi podr&aacute;s tener m&aacute;s opciones de ser seleccionado.</cite></footer>
        </blockquote>
    </div>
");
#nullable restore
#line 49 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml"
}

#line default
#line hidden
#nullable disable

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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<LC.WEB.Areas.Lawyer.Models.Profile.ExperienceViewModel>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
