#pragma checksum "G:\EVision Task\ProductCatalog_API_CeanArch\CleanArch.Web\Views\Home\Edit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b6d84420dc717ef3acbbd0f4858a98ffcbf27781"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Edit), @"mvc.1.0.view", @"/Views/Home/Edit.cshtml")]
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
#line 1 "G:\EVision Task\ProductCatalog_API_CeanArch\CleanArch.Web\Views\_ViewImports.cshtml"
using CleanArch.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "G:\EVision Task\ProductCatalog_API_CeanArch\CleanArch.Web\Views\_ViewImports.cshtml"
using CleanArch.Web.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "G:\EVision Task\ProductCatalog_API_CeanArch\CleanArch.Web\Views\_ViewImports.cshtml"
using X.PagedList.Mvc.Core;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "G:\EVision Task\ProductCatalog_API_CeanArch\CleanArch.Web\Views\_ViewImports.cshtml"
using CleanArch.Common.Helper;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b6d84420dc717ef3acbbd0f4858a98ffcbf27781", @"/Views/Home/Edit.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"38e7736c2016dad5f41fcf22137c22cde17adbfc", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Edit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CleanArch.Web.Models.ProductViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "G:\EVision Task\ProductCatalog_API_CeanArch\CleanArch.Web\Views\Home\Edit.cshtml"
  
    ViewData["Title"] = "Edit";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div>\r\n    ");
#nullable restore
#line 8 "G:\EVision Task\ProductCatalog_API_CeanArch\CleanArch.Web\Views\Home\Edit.cshtml"
Write(Html.Partial("_ProductForm", Model));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CleanArch.Web.Models.ProductViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
