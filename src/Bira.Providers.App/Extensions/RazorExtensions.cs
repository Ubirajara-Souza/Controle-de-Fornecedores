using Microsoft.AspNetCore.Mvc.Razor;

namespace Bira.Providers.App.Extensions
{
    public static class RazorExtensions
    {
        public static string FormatDocument(this RazorPage page, int typePerson, string document)
        {
            return typePerson == 1 ? Convert.ToUInt64(document).ToString(@"000\.000\.000\-00") : Convert.ToUInt64(document).ToString(@"00\.000\.000\/0000\-00");
        }

        public static string MarkOption(this RazorPage page, int typePerson, int value)
        {
            return typePerson == value ? "checked" : "";
        }

        /* public static bool IfClaim(this RazorPage page, string claimName, string claimValue)
         {
             return CustomAuthorization.ValidarClaimsUsuario(page.Context, claimName, claimValue);
         }

         public static string IfClaimShow(this RazorPage page, string claimName, string claimValue)
         {
             return CustomAuthorization.ValidarClaimsUsuario(page.Context, claimName, claimValue) ? "" : "disabled";
         }

         public static IHtmlContent IfClaimShow(this IHtmlContent page, HttpContext context, string claimName, string claimValue)
         {
             return CustomAuthorization.ValidarClaimsUsuario(context, claimName, claimValue) ? page : null;
         }*/
    }
}
