using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApp.Mvc.Extension.CustomAuthorization
{
        
    [HtmlTargetElement("a", Attributes = "disable-by-claim-value")]
    public class DesabilitaLinkByClaimTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public DesabilitaLinkByClaimTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HtmlAttributeName("disable-by-claim-value")]
        public string IdentityClaimValue { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            var temAcesso = CustomAuthorization.ValidarClaimsUsuario(_contextAccessor.HttpContext, IdentityClaimValue);

            if (temAcesso) return;

            // Vai remover todos os href(link) da nossa view
            output.Attributes.RemoveAll("href");

            // Vai adicionar o atributo dentro desse link cursor: not-allowed que é o cursor que diz que não temos permisão de acesso
            output.Attributes.Add(new TagHelperAttribute("style", "cursor: not-allowed"));

            //Vai adicionar o atributto tittle dizendo que não temos permisão
            output.Attributes.Add(new TagHelperAttribute("title", "Você não tem permissão"));
        }
    }
    
}
