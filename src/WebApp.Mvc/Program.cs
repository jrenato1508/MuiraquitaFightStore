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


//Terminamos a implementação do aritigo. Agora precisamos fazer algumas melhorias.

// Link: https://codewithmukesh.com/blog/user-management-in-aspnet-core-mvc/#what-well-learn
// https://codewithmukesh.com/blog/permission-based-authorization-in-aspnet-core/#whats-role-based-authorization 


// Enie e-mail com Asp.Net: https://codewithmukesh.com/blog/send-emails-with-aspnet-core/#create-a-new-aspnet-core-project
// Globalizando a App: https://codewithmukesh.com/blog/globalization-and-localization-in-aspnet-core/#what-well-learn

/*
                    ## MELHORIAS ##
   
   - Adicionar o CREATE E DELETE de ROLES, por hora só é possível adicionar Roles e listar - OK
   - Validar se não existe nenhuma role igual antes de adicionar - OK
   - Validar se não existe nenhuma role em uso antes de excluir - OK
   - Adicionar a Exlusão de usuários - OK
   - Exibir informações do usuário que será excluido - OK
   - Adicionar a Alteração de Senha - OK.
   - Não listar no troca senha usuário que está logado. - OK
   - Inserir DataAnnotation na view de alteração de senha para validar se senha é alfanumerica e etc... - OK
   - Adicionar as Rotas as controllers - OK
   - Adicionar RegularExpresion para exigir no campo nova senha caractere não alfanumerico -OK
   - Adicionar RegularExpresion para exigir no campo nova senha um caractere Maisculo -OK
   - Adicionar Notificação na view de sucesso, erro, Warning.. com ToastNotification - OK
   - Adicionamos o Supress para esconder,desabilitar botões, links e etc... a dependendo do valor da claim - OK
       - OBS: Falta implementar, apenas fiz a exebição, usei como exemplo a view de Privacy para demostra algumas coisas

   - Adicionar Validações para serem exibidas nas view - Implementado mas não usado.

   - Fizemos uma pequena implementação de exibição das Claims pelo artigo do macorati mas não vamos mais usar 

   - Adicionar CRUD de criação de usuário e habilita-lo para usuários que tem a permisão de Admin. - OK

   - Concentrar alguns recursos em uma pagina onde só o ADM poderá ter acesso. - OK

   - Bloquear algumas paginas para usuários que não estejam autenticados e que não tenham autorização para acessa-la - OK
   - Traduzir a Aplicação - OK 

   - Separar as Controller de autenticação do restante - OK
   - Personalizar Erros de autenticação - OK

   - Implementar a paginação na pagina de visualização de usuário.
   - ...
  
 
 */