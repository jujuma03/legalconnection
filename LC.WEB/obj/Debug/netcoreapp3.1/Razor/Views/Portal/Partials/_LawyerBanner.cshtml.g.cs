#pragma checksum "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_LawyerBanner.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "be6469bf15c4145b6ccb041fb07d780ffe75530d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Portal_Partials__LawyerBanner), @"mvc.1.0.view", @"/Views/Portal/Partials/_LawyerBanner.cshtml")]
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
#line 1 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\_ViewImports.cshtml"
using LC.WEB;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\_ViewImports.cshtml"
using LC.WEB.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\_ViewImports.cshtml"
using LC.WEB.Helpers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\_ViewImports.cshtml"
using LC.WEB.Extensions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\_ViewImports.cshtml"
using LC.CORE.Helpers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\_ViewImports.cshtml"
using LC.CORE.Extensions;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"be6469bf15c4145b6ccb041fb07d780ffe75530d", @"/Views/Portal/Partials/_LawyerBanner.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3e3bc8c63c472908ba78dfd1714377a27367b247", @"/Views/_ViewImports.cshtml")]
    public class Views_Portal_Partials__LawyerBanner : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<LC.WEB.Models.Home.SectionItemViewModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n");
#nullable restore
#line 4 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_LawyerBanner.cshtml"
 if (Model != null && Model.Count() > 0)
{
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_LawyerBanner.cshtml"
     foreach (var item in Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"lc-background-gradient m--margin-top-100\">\r\n            <div class=\"container\">\r\n                <div class=\"row\">\r\n                    <div class=\"col-lg-3 col-md-5 col-sm-6\">\r\n                        <img class=\"m--img-rounded\"");
            BeginWriteAttribute("src", " src=\"", 393, "\"", 425, 2);
            WriteAttributeValue("", 399, "/documentos/", 399, 12, true);
#nullable restore
#line 12 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_LawyerBanner.cshtml"
WriteAttributeValue("", 411, item.UrlImage, 411, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" style=\"height:250px; width:250px;margin-top:-70px;margin-bottom:40px\" />\r\n                    </div>\r\n                    <div class=\"col-lg-9 col-md-7 col-sm-6\">\r\n                        <h1 class=\"toast-title portal-title text-white\">");
#nullable restore
#line 15 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_LawyerBanner.cshtml"
                                                                   Write(item.Headline);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n                        <h6 class=\"text-white text-center m--padding-bottom-20\">\r\n                            ");
#nullable restore
#line 17 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_LawyerBanner.cshtml"
                       Write(item.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </h6>\r\n");
#nullable restore
#line 19 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_LawyerBanner.cshtml"
                         if (!User.Identity.IsAuthenticated)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <div");
            BeginWriteAttribute("class", " class=\"", 965, "\"", 973, 0);
            EndWriteAttribute();
            WriteLiteral(@" style=""height: 70px;text-align:center"">
                                <a class=""btn btn-lg btn-lc-transparent-white m-btn--pill"" href=""/registrar-abogado"" target=""_blank"">
                                    REGISTRARME COMO ABOGADO
                                </a>
                            </div>
");
#nullable restore
#line 26 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_LawyerBanner.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </div>\r\n                </div>\r\n            </div>\r\n            <div class=\"clear\"></div>\r\n        </div>\r\n");
#nullable restore
#line 32 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_LawyerBanner.cshtml"
    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 32 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_LawyerBanner.cshtml"
     
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<LC.WEB.Models.Home.SectionItemViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
