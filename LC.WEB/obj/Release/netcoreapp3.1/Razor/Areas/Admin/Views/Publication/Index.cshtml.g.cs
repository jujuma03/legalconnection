#pragma checksum "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Admin\Views\Publication\Index.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "131ef3f73c008b4b891c5cf652eee40d98e2eff67a0b9b9615c8eb3792e7e190"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCoreGeneratedDocument.Areas_Admin_Views_Publication_Index), @"mvc.1.0.view", @"/Areas/Admin/Views/Publication/Index.cshtml")]
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
#line 1 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Admin\Views\_ViewImports.cshtml"
using LC.WEB.Extensions

#nullable disable
    ;
#nullable restore
#line 2 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Admin\Views\_ViewImports.cshtml"
using LC.WEB.Areas.Lawyer.Models

#nullable disable
    ;
#nullable restore
#line 3 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Admin\Views\_ViewImports.cshtml"
using LC.CORE.Helpers

#nullable disable
    ;
#nullable restore
#line 4 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Admin\Views\_ViewImports.cshtml"
using LC.CORE.Extensions

#nullable disable
    ;
#nullable restore
#line 5 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Admin\Views\_ViewImports.cshtml"
using LC.WEB.Models

#nullable disable
    ;
    #line default
    #line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"131ef3f73c008b4b891c5cf652eee40d98e2eff67a0b9b9615c8eb3792e7e190", @"/Areas/Admin/Views/Publication/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"237e834be40e29df1d3f1322efecb79483dd118eb57987337eddedfeed64f0e0", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    #nullable restore
    internal sealed class Areas_Admin_Views_Publication_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control m-input m-input--pill m-input--solid"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("placeholder", new global::Microsoft.AspNetCore.Html.HtmlString("Buscar..."), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("search"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/js/areas/admin/publication/index.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::LC.WEB.Helpers.InputRemoveIdTagHelper __LC_WEB_Helpers_InputRemoveIdTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Admin\Views\Publication\Index.cshtml"
  
    ViewData["Title"] = "Listado de Publicaciones";
    ViewData["Breadcrumbs"] = new[] {
    new BreadcrumbViewModel { Name = ViewData["Title"].ToString() }
    };

#line default
#line hidden
#nullable disable

            WriteLiteral(@"
<div class=""m-portlet"">
    <div class=""m-portlet__body"">
        <div class=""m-form--label-align-right m--margin-top-20 m--margin-bottom-30"">
            <div class=""row align-items-center"">
                <div class=""col-xl-8 order-2 order-xl-1"">
                    <div class=""row"">
                        <div class=""form-group m-form__group col-xl-4"">
                            <label>&emsp;</label>
                            <div class=""m-input m-input-icon m-input-icon--left"">
                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "131ef3f73c008b4b891c5cf652eee40d98e2eff67a0b9b9615c8eb3792e7e1907061", async() => {
            }
            );
            __LC_WEB_Helpers_InputRemoveIdTagHelper = CreateTagHelper<global::LC.WEB.Helpers.InputRemoveIdTagHelper>();
            __tagHelperExecutionContext.Add(__LC_WEB_Helpers_InputRemoveIdTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                                <span class=""m-input-icon__icon m-input-icon__icon--left"">
                                    <span><i class=""la la-search""></i></span>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <table class=""table table-hover table-bordered m-table m-table--head-bg-primary table-responsive-lg"" id=""ajax_data""> </table>
    </div>
</div>

");
            Write(
#nullable restore
#line 31 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Admin\Views\Publication\Index.cshtml"
 await Html.PartialAsync("Partials/Modals/PublicationDetailPartialmodal")

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "131ef3f73c008b4b891c5cf652eee40d98e2eff67a0b9b9615c8eb3792e7e1909273", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_4.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion = 
#nullable restore
#line 34 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Admin\Views\Publication\Index.cshtml"
                                                                            true

#line default
#line hidden
#nullable disable
                ;
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
            }
            );
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
