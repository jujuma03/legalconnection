#pragma checksum "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_HowItWorksLawyerSteps.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e5addb30ecb6420c4ff0414eb37966340f244169"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Portal_Partials__HowItWorksLawyerSteps), @"mvc.1.0.view", @"/Views/Portal/Partials/_HowItWorksLawyerSteps.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e5addb30ecb6420c4ff0414eb37966340f244169", @"/Views/Portal/Partials/_HowItWorksLawyerSteps.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3e3bc8c63c472908ba78dfd1714377a27367b247", @"/Views/_ViewImports.cshtml")]
    public class Views_Portal_Partials__HowItWorksLawyerSteps : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<LC.ENTITIES.Models.HowItWorksStep>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_HowItWorksLawyerSteps.cshtml"
  
    var count = 0;

#line default
#line hidden
#nullable disable
            WriteLiteral("<h2 class=\"toast-title portal-title\">Ahora podr&aacute;s conseguir clientes mucho m&aacute;s f&aacute;cil</h2>\r\n\r\n");
#nullable restore
#line 7 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_HowItWorksLawyerSteps.cshtml"
 if (Model!=null && Model.Count()>0)
{
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_HowItWorksLawyerSteps.cshtml"
     foreach (var item in Model)
    {
        var isLast = count==(Model.Count()-1);

        

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_HowItWorksLawyerSteps.cshtml"
         if (count%2 == 0)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"row align-items-center justify-content-end how-it-works d-flex\">\r\n                <div class=\"col-lg-6 col-md-6\">\r\n                    <h5 class=\"m--padding-left-10 m--padding-right-10 lc-step-number\">\r\n                        #");
#nullable restore
#line 18 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_HowItWorksLawyerSteps.cshtml"
                     Write(count+1);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </h5>\r\n                    <h2 class=\"m--padding-10 lc-step-title\">");
#nullable restore
#line 20 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_HowItWorksLawyerSteps.cshtml"
                                                       Write(item.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n                    <div class=\"m--padding-left-10 m--padding-right-10\">\r\n                        ");
#nullable restore
#line 22 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_HowItWorksLawyerSteps.cshtml"
                   Write(Html.Raw(item.Content));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </div>\r\n");
#nullable restore
#line 24 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_HowItWorksLawyerSteps.cshtml"
                     if (count==0)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <div class=\"text-center\">\r\n                            <a class=\"btn btn-lc-green m-btn--pill\" target=\"_blank\" href=\"/registrar-abogado\"> REGISTRATE </a>\r\n                        </div>\r\n");
#nullable restore
#line 29 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_HowItWorksLawyerSteps.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </div>\r\n                <div class=\"col-lg-6 col-md-6\">\r\n                    <img");
            BeginWriteAttribute("src", " src=\"", 1270, "\"", 1302, 2);
            WriteAttributeValue("", 1276, "/documentos/", 1276, 12, true);
#nullable restore
#line 32 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_HowItWorksLawyerSteps.cshtml"
WriteAttributeValue("", 1288, item.UrlImage, 1288, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"m-link-hover__img img-fluid\"");
            BeginWriteAttribute("alt", " alt=\"", 1339, "\"", 1356, 1);
#nullable restore
#line 32 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_HowItWorksLawyerSteps.cshtml"
WriteAttributeValue("", 1345, item.Title, 1345, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                </div>\r\n            </div>\r\n");
#nullable restore
#line 35 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_HowItWorksLawyerSteps.cshtml"
             if (!isLast)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <!--path between 2-3-->
                <div class=""row timeline"">
                    <div class=""col-7"" style=""margin:auto;display:flex"">
                        <div class=""col-2"">
                            <div class=""corner right-bottom""></div>
                        </div>
                        <div class=""col-8"">
                            <hr />
                        </div>
                        <div class=""col-2"">
                            <div class=""corner top-left""></div>
                        </div>
                    </div>
                </div>
");
#nullable restore
#line 51 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_HowItWorksLawyerSteps.cshtml"
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 51 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_HowItWorksLawyerSteps.cshtml"
             
        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"row align-items-center how-it-works d-flex\">\r\n                <div class=\"col-lg-6 col-md-6\">\r\n                    <img");
            BeginWriteAttribute("src", " src=\"", 2252, "\"", 2284, 2);
            WriteAttributeValue("", 2258, "/documentos/", 2258, 12, true);
#nullable restore
#line 57 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_HowItWorksLawyerSteps.cshtml"
WriteAttributeValue("", 2270, item.UrlImage, 2270, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"m-link-hover__img img-fluid\"");
            BeginWriteAttribute("alt", " alt=\"", 2321, "\"", 2338, 1);
#nullable restore
#line 57 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_HowItWorksLawyerSteps.cshtml"
WriteAttributeValue("", 2327, item.Title, 2327, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                </div>\r\n                <div class=\"col-lg-6 col-md-6\">\r\n                    <h5 class=\"m--padding-left-10 m--padding-right-10 lc-step-number\">\r\n                        #");
#nullable restore
#line 61 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_HowItWorksLawyerSteps.cshtml"
                     Write(count+1);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </h5>\r\n                    <h2 class=\"m--padding-10 lc-step-title\">");
#nullable restore
#line 63 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_HowItWorksLawyerSteps.cshtml"
                                                       Write(item.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n\r\n                    <div class=\"m--padding-left-10 m--padding-right-10\">\r\n                        ");
#nullable restore
#line 66 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_HowItWorksLawyerSteps.cshtml"
                   Write(Html.Raw(item.Content));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </div>\r\n                </div>\r\n            </div>\r\n");
#nullable restore
#line 70 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_HowItWorksLawyerSteps.cshtml"
             if (!isLast)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <!--path between 1-2-->
                <div class=""row timeline"">
                    <div class=""col-7"" style=""margin:auto;display:flex"">
                        <div class=""col-2"">
                            <div class=""corner top-right""></div>
                        </div>
                        <div class=""col-8"">
                            <hr />
                        </div>
                        <div class=""col-2"">
                            <div class=""corner left-bottom""></div>
                        </div>
                    </div>
                </div>
");
#nullable restore
#line 86 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_HowItWorksLawyerSteps.cshtml"
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 86 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_HowItWorksLawyerSteps.cshtml"
             
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 87 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_HowItWorksLawyerSteps.cshtml"
         
        count++;
    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 89 "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Portal\Partials\_HowItWorksLawyerSteps.cshtml"
     
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<LC.ENTITIES.Models.HowItWorksStep>> Html { get; private set; }
    }
}
#pragma warning restore 1591
