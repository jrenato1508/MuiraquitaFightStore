using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Mvc.Configurations;
using WebApp.Mvc.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();


builder.Services.AddIdentityConfiguration(builder.Configuration);
builder.Services.AddMvcConfiguration();
builder.Services.ResolveDependencies();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseGlobalizationConfig();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();


//Terminamos a implementa��o do aritigo. Agora precisamos fazer algumas melhorias.

// Link: https://codewithmukesh.com/blog/user-management-in-aspnet-core-mvc/#what-well-learn
// https://codewithmukesh.com/blog/permission-based-authorization-in-aspnet-core/#whats-role-based-authorization 


// Enie e-mail com Asp.Net: https://codewithmukesh.com/blog/send-emails-with-aspnet-core/#create-a-new-aspnet-core-project
// Globalizando a App: https://codewithmukesh.com/blog/globalization-and-localization-in-aspnet-core/#what-well-learn

/*
                    ## MELHORIAS ##
   
   - Adicionar o CREATE E DELETE de ROLES, por hora s� � poss�vel adicionar Roles e listar - OK
   - Validar se n�o existe nenhuma role igual antes de adicionar - OK
   - Validar se n�o existe nenhuma role em uso antes de excluir - OK
   - Adicionar a Exlus�o de usu�rios - OK
   - Exibir informa��es do usu�rio que ser� excluido - OK
   - Adicionar a Altera��o de Senha - OK.
   - N�o listar no troca senha usu�rio que est� logado. - OK
   - Inserir DataAnnotation na view de altera��o de senha para validar se senha � alfanumerica e etc... - OK
   - Adicionar as Rotas as controllers - OK
   - Adicionar RegularExpresion para exigir no campo nova senha caractere n�o alfanumerico -OK
   - Adicionar RegularExpresion para exigir no campo nova senha um caractere Maisculo -OK
   - Adicionar Notifica��o na view de sucesso, erro, Warning.. com ToastNotification - OK
   - Adicionamos o Supress para esconder,desabilitar bot�es, links e etc... a dependendo do valor da claim - OK
       - OBS: Falta implementar, apenas fiz a exebi��o, usei como exemplo a view de Privacy para demostra algumas coisas

   - Adicionar Valida��es para serem exibidas nas view - Implementado mas n�o usado.

   - Fizemos uma pequena implementa��o de exibi��o das Claims pelo artigo do macorati mas n�o vamos mais usar 

   - Adicionar CRUD de cria��o de usu�rio e habilita-lo para usu�rios que tem a permis�o de Admin. - OK

   - Concentrar alguns recursos em uma pagina onde s� o ADM poder� ter acesso. - OK

   - Bloquear algumas paginas para usu�rios que n�o estejam autenticados e que n�o tenham autoriza��o para acessa-la - OK
   - Traduzir a Aplica��o - OK 

   - Separar as Controller de autentica��o do restante - OK
   - Personalizar Erros de autentica��o - OK

   - Implementar a pagina��o na pagina de visualiza��o de usu�rio.
   - ...
  
 
 */