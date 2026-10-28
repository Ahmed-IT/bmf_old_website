# Local and deployment configuration

Checked-in configuration templates contain empty connection-string values. Copy [appsettings.example.json](../BMF.WebCore.Admin/appsettings.example.json) to `BMF.WebCore.Admin/appsettings.json` for local setup; that generated file is ignored. Supply credentials through environment variables or the hosting platform's secret settings, and do not commit them.

## ASP.NET Core admin

`Host.CreateDefaultBuilder` loads environment variables over `appsettings.json`. Configure the values needed for the active environment:

- `ConnectionStrings__DefaultDbContextLocal`
- `ConnectionStrings__DefaultDbContextStaging`
- `ConnectionStrings__DefaultDbContextProd`
- `AzureStorageConfig__ConnectionString`

For local development, use an ignored `appsettings.Development.json` or a local secret store.

For the `bmftest` App Service, set `ASPNETCORE_ENVIRONMENT=Production` and provide `ConnectionStrings__DefaultDbContextProd` and `AzureStorageConfig__ConnectionString` through App Service settings or Key Vault references. The admin portal fails fast when the required database connection string is missing. Keep the values in Azure; do not put them in the workflow or repository.

## ASP.NET Framework applications

Configure these named connection strings in the hosting platform:

- `BMFEntities` and `BMFEntities1` for `BMF_WebSite`
- `BMFDbContext` for `BMF.Web.Admin`

For local development, `BMFEntities1` and `BMFDbContext` can also be supplied as `ConnectionStrings__BMFEntities1` and `ConnectionStrings__BMFDbContext` environment variables. Configure Blob Storage using the `AzStorageAccount` app setting or environment variable.

Local environment-specific settings and secret files are excluded by `.gitignore`.

## GitHub Actions deployment

`.github/workflows/deploy-admin-portal.yml` deploys `BMF.WebCore.Admin` to the `bmftest` App Service on every push, or on manual dispatch. It uses GitHub OIDC; the attached publish-settings file is not used and must not be committed.

Before the first run:

1. Configure an Entra application/service principal with a federated credential:
   - Issuer: `https://token.actions.githubusercontent.com`
   - Audience: `api://AzureADTokenExchange`
   - Subject: `repo:Ahmed-IT/bmf_old_website:environment:bmftest`
2. Grant that identity permission to deploy to the `bmftest` web app.
3. Add these non-secret repository or `bmftest` environment variables in GitHub Actions:
   - `AZURE_CLIENT_ID`
   - `AZURE_TENANT_ID`
   - `AZURE_SUBSCRIPTION_ID`

The admin project targets `netcoreapp3.1`, which is out of support. The App Service must have a compatible runtime, or the project should be upgraded before relying on production deployments.
