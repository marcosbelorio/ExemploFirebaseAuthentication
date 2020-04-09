# ExemploFirebaseAuthentication

## Cenários contemplados
* Cadastro de novo usuário via Email/Senha
* Login via Email/Senha

## Cenários para serem implementados
* Login com Google / Facebook / Twitter OAuth tokens
* Integrar diferentes métodos de login em um único login
* Trocar senha
* Excluir usuário
* Renovar o Token
* Interações com envio de email

## Como Implementar
### Adicionar os pacotes no projeto
```csharp
dotnet add package FirebaseAuthentication.net
dotnet add package Microsoft.AspNetCore.Authentication
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
```

### Configurar o arquivo Startup
```csharp
services
.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.Authority = "https://securetoken.google.com/<ProjetoID>";
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = "https://securetoken.google.com/<ProjetoID>",
        ValidateAudience = true,
        ValidAudience = "<ProjetoID>",
        ValidateLifetime = true
    };
});

...

app.UseAuthentication();
app.UseAuthorization();
```

### Substituir as strings
```csharp
<ProjetoID> = Nome único do projeto no Firebase (exemplo: https://ProjetoID.firebaseio.com/)
<ApiKey-Firebase> = Apikey para acesso ao projeto no Firebase
```
