#pragma checksum "D:\ING-SW\Legal Connection\Proyecto\LC.WEB\Views\Shared\Partials\_ModalPreview.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1cd7b6e6b5a7dd835884aad8a43096479b8915c5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Partials__ModalPreview), @"mvc.1.0.view", @"/Views/Shared/Partials/_ModalPreview.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1cd7b6e6b5a7dd835884aad8a43096479b8915c5", @"/Views/Shared/Partials/_ModalPreview.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3e3bc8c63c472908ba78dfd1714377a27367b247", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Partials__ModalPreview : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<div class=""modal fade"" id=""preview-modal"" tabindex=""-1"" role=""dialog"" aria-hidden=""true"">
    <div class=""modal-dialog modal-md"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title text-primary font-weight-bold"">
                    Previo de Imagen
                </h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>
            <div class=""modal-body"">

                <div class=""form-group m-form__group row"">
                    <div class=""m-form__group-sub col-md-12 col-lg-12 col-xs-12"">
                        <img id=""upload-preview"" style=""width: 100%;""");
            BeginWriteAttribute("src", " src=\"", 813, "\"", 819, 0);
            EndWriteAttribute();
            WriteLiteral(@" />
                    </div>
                </div>

                <div class=""m--margin-top-20""></div>

                <div class=""form-group m-form__group row"">
                    <div class=""m-form__group-sub col-md-12 col-lg-12 col-xs-12 text-center"">
                        <button id=""btnCropSave"" class=""btn btn-primary text-center"" type=""submit"">Recortar y Guardar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
