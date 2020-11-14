# ExploreAspNetCore
Un projet pour explorer les api asp.net core, la documentation swagger, docker, kubernetes, redis, et les github actions.

Table des matières
=================

<!--ts-->
   * [Table of contents](#table-of-contents)
   * [Exemple de code c#](#exemple)
      * [Configuration nécessaire pour que les enums soit vue comme des string](#enumString)
      * [Configuration utilisation de Fluent.Validation](#fluent-validation)
      * [Valider les paramètres de route](#route-parameter-validation)
      * [Retourner des codes 422 lors d'échec de validation des paramètres](#422)
      * [Utiliser un enveloppe de base avec des paramètres géré par un ActionFilter](#action-filter)
   * [Kubernetes](#kubernetes)
      * [Creer des credentials pour déployer dans kubernetes](#credentials)
      * [Exposer un services kubernetes](#expose)
<!--te-->

# Exemple de code c#

## Configuration nécessaire pour que les enums soit vue comme des string

``` xml
<PackageReference Include="Swashbuckle.AspNetCore.Newtonsoft" Version="5.5.1" />
```

``` c#
services.AddControllers()
        .AddNewtonsoftJson(o =>
        {
            o.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            o.SerializerSettings.Converters.Add(new StringEnumConverter());
        });
        
services.AddSwaggerGen();        
        
services.AddSwaggerGenNewtonsoftSupport();
```

## Configuration utilisation de Fluent.Validation

``` xml
<PackageReference Include="FluentValidation" Version="9.0.1" />
<PackageReference Include="FluentValidation.AspNetCore" Version="9.0.1" />
```

``` c#
services.AddControllers()
        .AddFluentValidation(r => r.RegisterValidatorsFromAssemblyContaining<DateArgsValidation>())
```

## Valider les paramètres de route

FluentValidation est fait pour valider des objet complexe. Pour valider les paramètres de route. Un ActionFilterAttribute est utilisé et placer à l'endroit approprié dans le pipeline mvc.

``` c#
public class TypeNameValidatorAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var name = context.RouteData.Values["name"] as string;

        if (name.Any(c => char.IsWhiteSpace(c)))
        {
            context.ModelState.AddModelError("name", "Le nom de peut pas contenir d'esapce");
        }

        if (name.All(c => char.IsDigit(c)))
        {
            context.ModelState.AddModelError("name", "Le nom ne peut pas être seulement des nombres");
        }
    }
}

[HttpGet("{name}")]
[TypeNameValidator(Order = int.MinValue + 100)]
public IActionResult Get(string name)
```

## Retourner des codes 422 lors d'échec de validation des paramètres

```c#
services.AddControllers()
        .ConfigureApiBehaviorOptions(o =>
        {
            o.InvalidModelStateResponseFactory = ApiEnveloppeFactory.InvalidModelStateEnveloppeFactory;
        });
        
public static IActionResult InvalidModelStateEnveloppeFactory(ActionContext actionContext)
{
    var result = new UnprocessableEntityObjectResult(new ApiEnveloppe422
    {
        Messages = new List<Message>
        {
            new Message
            {
                Id = "API_001",
                Code = "422",
                Severity = "1",
                Text = "Une ou plusieurs erreurs de validiton sont survenues"
            }
        },
        Result = new ValidationProblemDetails(actionContext.ModelState).Errors
    });

    return result;
}
```

## Utiliser un enveloppe de base avec des paramètres géré par un ActionFilter

Voir le fichier ApiContextActionFilter.cs et le fichier Startup.cs

# Kubernetes

## Creer des credentials pour déployer dans kubernetes

Dans une session Azure Cloud Shell
```
az ad sp create-for-rbac --name "myApp" --role contributor --scopes /subscriptions/<SUBSCRIPTION_ID>/resourceGroups/<RESOURCE_GROUP> --sdk-auth
```
Source : https://docs.microsoft.com/en-us/azure/aks/kubernetes-action

## Exposer un services kubernetes

```
kubectl expose deployment swaggerdoc-deployment --type=LoadBalancer --name=swaggerdoc-service
```
source : https://kubernetes.io/docs/tutorials/stateless-application/expose-external-ip-address/

# Authentification Https

Créer un certificat auto signé :

```
Windows PowerShell
Copyright (C) Microsoft Corporation. Tous droits réservés.

PS C:\WINDOWS\system32> New-SelfSignedCertificate -DnsName "root_ca_swaggerdoc.com", "root_ca_dev_swaggerdoc.com" -CertStoreLocation "cert:\LocalMachine\My" -NotAfter (Get-Date).AddYears(20) -FriendlyName "root_ca_swaggerdoc.com" -KeyUsageProperty All -KeyUsage CertSign, CRLSign, DigitalSignature


   PSParentPath : Microsoft.PowerShell.Security\Certificate::LocalMachine\My

Thumbprint                                Subject
----------                                -------
***                                       CN=root_ca_swaggerdoc.com


PS C:\WINDOWS\system32> $mypwd = ConvertTo-SecureString -String "***" -Force -AsPlainText
PS C:\WINDOWS\system32> Get-ChildItem -Path cert:\localMachine\my\"***" | Export-PfxCertificate -FilePath C:\root_ca_swaggerdoc.pfx -Password $mypwd


    Répertoire : C:\


Mode                LastWriteTime         Length Name
----                -------------         ------ ----
-a----       2020-10-27     20:21           2749 root_ca_swaggerdoc.pfx


PS C:\WINDOWS\system32> Export-Certificate -Cert cert:\localMachine\my\"***" -FilePath root_ca_swaggerdoc.crt


    Répertoire : C:\WINDOWS\system32


Mode                LastWriteTime         Length Name
----                -------------         ------ ----
-a----       2020-10-27     20:21            865 root_ca_swaggerdoc.crt


PS C:\WINDOWS\system32>
```

# Setup https with nginx

https://www.nginx.com/blog/using-free-ssltls-certificates-from-lets-encrypt-with-nginx/