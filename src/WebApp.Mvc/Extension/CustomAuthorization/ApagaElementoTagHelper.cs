using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApp.Mvc.Extension.CustomAuthorization
{
    
    [HtmlTargetElement("*", Attributes = "supress-by-claim-value")]
    public class ApagaElementoTagHelper : TagHelper
    {
        
        private readonly IHttpContextAccessor _contextAccessor;

        public ApagaElementoTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }


        [HtmlAttributeName("supress-by-claim-value")]
        public string IdentityClaimValue { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // O Context é aquilo que estamos recebendo na TagHelper
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            //output é a saída, é o que essa tagHelper vai produzir de conteudo
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            // Consultamos se o usuár
            var temAcesso = CustomAuthorization.ValidarClaimsUsuario(_contextAccessor.HttpContext, IdentityClaimValue);

            //Caso tenha acesso não acontecerá nada
            if (temAcesso) return;

            // Caso não tenha acesso as tags não será geradas
            output.SuppressOutput();
        }
    }

}
