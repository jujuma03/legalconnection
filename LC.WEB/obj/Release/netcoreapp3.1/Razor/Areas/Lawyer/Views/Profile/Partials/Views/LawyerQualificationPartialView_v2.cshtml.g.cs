#pragma checksum "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerQualificationPartialView_v2.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "cb7743d1568b4e658ec0ab399e8b7f2f0f773515f29db930ed21114402e8f7c0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCoreGeneratedDocument.Areas_Lawyer_Views_Profile_Partials_Views_LawyerQualificationPartialView_v2), @"mvc.1.0.view", @"/Areas/Lawyer/Views/Profile/Partials/Views/LawyerQualificationPartialView_v2.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"cb7743d1568b4e658ec0ab399e8b7f2f0f773515f29db930ed21114402e8f7c0", @"/Areas/Lawyer/Views/Profile/Partials/Views/LawyerQualificationPartialView_v2.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"fbe9f53555a70c23360e9008a8b6fceff255765bc8836e7eeb549b951c1cee36", @"/Areas/Lawyer/Views/_ViewImports.cshtml")]
    #nullable restore
    internal sealed class Areas_Lawyer_Views_Profile_Partials_Views_LawyerQualificationPartialView_v2 : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<
#nullable restore
#line 1 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerQualificationPartialView_v2.cshtml"
       LC.CORE.Structs.PaginationStructs.ReturnedData<LC.ENTITIES.Models.LawyerQualification>

#line default
#line hidden
#nullable disable
    >
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/general/profile.jpg"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("logo_company_experience"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("Logo usuario"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 3 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerQualificationPartialView_v2.cshtml"
   
    var result = Model.Data.ToArray();

#line default
#line hidden
#nullable disable

#nullable restore
#line 6 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerQualificationPartialView_v2.cshtml"
 if (Model.Data.Any())
{
    

#line default
#line hidden
#nullable disable

#nullable restore
#line 8 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerQualificationPartialView_v2.cshtml"
     for (int i = 0; i < Model.Data.Count(); i++)
    {

#line default
#line hidden
#nullable disable

            WriteLiteral("        <div class=\"row\">\r\n            <div class=\"col-xl-3 col-lg-3 col-sm-3 mt-2 text-center\">\r\n");
#nullable restore
#line 12 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerQualificationPartialView_v2.cshtml"
                 if (string.IsNullOrEmpty(result[i].Client.User.Picture))
                {

#line default
#line hidden
#nullable disable

            WriteLiteral("                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "cb7743d1568b4e658ec0ab399e8b7f2f0f773515f29db930ed21114402e8f7c06976", async() => {
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
#line 15 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerQualificationPartialView_v2.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable

            WriteLiteral("                    <img");
            BeginWriteAttribute("src", " src=\"", 621, "\"", 669, 2);
            WriteAttributeValue("", 627, "/documentos/", 627, 12, true);
            WriteAttributeValue("", 639, 
#nullable restore
#line 18 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerQualificationPartialView_v2.cshtml"
                                           result[i].Client.User.Picture

#line default
#line hidden
#nullable disable
            , 639, 30, false);
            EndWriteAttribute();
            WriteLiteral(" class=\"logo_company_experience\" alt=\"Logo usuario\" />\r\n");
#nullable restore
#line 19 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerQualificationPartialView_v2.cshtml"
                }

#line default
#line hidden
#nullable disable

            WriteLiteral("            </div>\r\n            <div class=\"mt-2 col-xl-9 col-lg-9 col-sm-9\">\r\n                <div class=\"col-12 row pr-0\">\r\n                    <p class=\"col-8 m--font-boldest\">\r\n                        ");
            Write(
#nullable restore
#line 24 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerQualificationPartialView_v2.cshtml"
                         result[i].Client.User.Name

#line default
#line hidden
#nullable disable
            );
            WriteLiteral(" ");
            Write(
#nullable restore
#line 24 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerQualificationPartialView_v2.cshtml"
                                                     result[i].Client.User.Surnames

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n                    </p>\r\n                    <div class=\"col-4 text-right\">\r\n");
#nullable restore
#line 27 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerQualificationPartialView_v2.cshtml"
                         for (int v = 1; v <= 5; v++)
                        {
                            

#line default
#line hidden
#nullable disable

#nullable restore
#line 29 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerQualificationPartialView_v2.cshtml"
                             if (v <= result[i].Qualification)
                            {

#line default
#line hidden
#nullable disable

            WriteLiteral("                                <i class=\"fa fa-star start-graff\"></i>\r\n");
#nullable restore
#line 32 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerQualificationPartialView_v2.cshtml"
                            }
                            else
                            {

#line default
#line hidden
#nullable disable

            WriteLiteral("                                <i class=\"fa fa-star\"></i>\r\n");
#nullable restore
#line 36 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerQualificationPartialView_v2.cshtml"
                            }

#line default
#line hidden
#nullable disable

#nullable restore
#line 36 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerQualificationPartialView_v2.cshtml"
                             
                        }

#line default
#line hidden
#nullable disable

            WriteLiteral("                    </div>\r\n                </div>\r\n                <p class=\"col-12 text-justify pr-0 expandable\">\r\n                    ");
            Write(
#nullable restore
#line 41 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerQualificationPartialView_v2.cshtml"
                     result[i].Commentary

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n                </p>\r\n            </div>\r\n        </div>\r\n");
#nullable restore
#line 45 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerQualificationPartialView_v2.cshtml"

        if (i < Model.Data.Count() - 1)
        {

#line default
#line hidden
#nullable disable

            WriteLiteral("            <div class=\"m-divider\">\r\n                <span></span>\r\n            </div>\r\n");
#nullable restore
#line 51 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerQualificationPartialView_v2.cshtml"
        }
    }

#line default
#line hidden
#nullable disable

            Write(
#nullable restore
#line 54 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerQualificationPartialView_v2.cshtml"
     await Html.PartialAsync("Partials/_PaginationPartial", Model.PaginationData)

#line default
#line hidden
#nullable disable
            );
#nullable restore
#line 54 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerQualificationPartialView_v2.cshtml"
                                                                                 

}
else
{

#line default
#line hidden
#nullable disable

            WriteLiteral(@"    <div class=""m-demo__preview col-12"">
        <blockquote class=""blockquote"">
            <p class=""mb-0""> Aún no se han registrado comentarios</p>
            <footer class=""blockquote-footer""><cite title=""Source Title""> Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to ma</cite></footer>
        </blockquote>
    </div>
");
#nullable restore
#line 65 "C:\Users\Junior.Delacruz\Documents\Personal\legalconnection\legalconnection\LC.WEB\Areas\Lawyer\Views\Profile\Partials\Views\LawyerQualificationPartialView_v2.cshtml"
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<LC.CORE.Structs.PaginationStructs.ReturnedData<LC.ENTITIES.Models.LawyerQualification>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
