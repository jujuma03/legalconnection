#pragma checksum "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ab3f3b1898f013daf527d39e80a6fd415b2a93e0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Lawyer_Views_Profile_Partials_Views_LawyerExperiencePartialView), @"mvc.1.0.view", @"/Areas/Lawyer/Views/Profile/Partials/Views/LawyerExperiencePartialView.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Areas\Lawyer\Views\_ViewImports.cshtml"
using LC.WEB.Extensions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Areas\Lawyer\Views\_ViewImports.cshtml"
using LC.WEB.Areas.Lawyer.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Areas\Lawyer\Views\_ViewImports.cshtml"
using LC.CORE.Helpers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Areas\Lawyer\Views\_ViewImports.cshtml"
using LC.CORE.Extensions;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ab3f3b1898f013daf527d39e80a6fd415b2a93e0", @"/Areas/Lawyer/Views/Profile/Partials/Views/LawyerExperiencePartialView.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8ab7c119e14d866e56ce26cdd2dd49ac4986d7ac", @"/Areas/Lawyer/Views/_ViewImports.cshtml")]
    public class Areas_Lawyer_Views_Profile_Partials_Views_LawyerExperiencePartialView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<LC.WEB.Areas.Lawyer.Models.Profile.ExperienceViewModel>>
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
#line 3 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml"
 if (Model.Any())
{
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml"
     for (int i = 0; i < Model.Count(); i++)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"row\">\r\n            <div class=\"col-xl-3 col-lg-3 col-sm-3 mt-2 text-center\">\r\n");
#nullable restore
#line 9 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml"
                 if (string.IsNullOrEmpty(Model[i].PhotoUrl))
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "ab3f3b1898f013daf527d39e80a6fd415b2a93e05833", async() => {
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
#line 12 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml"

                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <img");
            BeginWriteAttribute("src", " src=\"", 528, "\"", 564, 2);
            WriteAttributeValue("", 534, "/documentos/", 534, 12, true);
#nullable restore
#line 16 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml"
WriteAttributeValue("", 546, Model[i].PhotoUrl, 546, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"logo_company_experience\" alt=\"Logo compañia\" />\r\n");
#nullable restore
#line 17 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n            <div class=\"mt-2 col-xl-9 col-lg-9 col-sm-9\">\r\n                <div class=\"col-12 row pr-0\">\r\n                    <p class=\"col-8 m--font-boldest\">\r\n                        ");
#nullable restore
#line 22 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml"
                   Write(Model[i].Company);

#line default
#line hidden
#nullable disable
            WriteLiteral(" - ");
#nullable restore
#line 22 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml"
                                       Write(Model[i].Position);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </p>\r\n                    <div class=\"col-4 d-flex align-items-center justify-content-end\">\r\n                        <i data-id=\"");
#nullable restore
#line 25 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml"
                               Write(Model[i].Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" class=\"fa fa-edit options_partialview btn-experience-edit\"></i>\r\n                        <i data-id=\"");
#nullable restore
#line 26 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml"
                               Write(Model[i].Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" class=\"fa fa-trash options_partialview btn-experience-delete\"></i>\r\n                    </div>\r\n                </div>\r\n                <p class=\"col-12\">");
#nullable restore
#line 29 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml"
                             Write(Model[i].StartDate);

#line default
#line hidden
#nullable disable
            WriteLiteral(" - ");
#nullable restore
#line 29 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml"
                                                    Write(string.IsNullOrEmpty(Model[i].EndDate) ? "A la actualidad" : Model[i].EndDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                <p class=\"col-12 text-justify pr-0 expandable\">\r\n                    ");
#nullable restore
#line 31 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml"
               Write(Model[i].Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </p>\r\n            </div>\r\n        </div>\r\n");
            WriteLiteral("        <div class=\"m-divider\">\r\n            <span></span>\r\n        </div>\r\n");
#nullable restore
#line 39 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml"
    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 39 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml"
     
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
#line 49 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerExperiencePartialView.cshtml"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<LC.WEB.Areas.Lawyer.Models.Profile.ExperienceViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
