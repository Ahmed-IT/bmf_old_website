# Local and deployment configuration

Checked-in configuration templates contain empty connection-string values. Copy [appsettings.example.json](../BMF.WebCore.Admin/appsettings.example.json) to `BMF.WebCore.Admin/appsettings.json` for local setup; that generated file is ignored. Supply credentials through environment variables or the hosting platform's secret settings, and do not commit them.

## ASP.NET Core admin

`Host.CreateDefaultBuilder` loads environment variables over `appsettings.json`. Configure the values needed for the active environment:

- `ConnectionStrings__DefaultDbContextLocal`
- `ConnectionStrings__DefaultDbContextStaging`
- `ConnectionStrings__DefaultDbContextProd`
- `AzureStorageConfig__ConnectionString`

For local development, use an ignored `appsettings.Development.json` or a local secret store.

The admin portal runs only on `bmfadmin`. The `bmftest` App Service serves the public `bmf.com.bh` website and must never receive the admin package. Keep the existing credential-based runtime configuration; no managed-identity migration is required.

The `bmfadmin` App Service keeps its existing credential-based configuration in its deployed `appsettings.json`. A configured Blob Storage connection string takes precedence over managed identity. The workflow excludes `appsettings*.json` from the `bmfadmin` package and explicitly disables deployment cleanup, preserving those server-side files. Do not copy their values into GitHub or local committed files. Managed identity is not enabled for `bmfadmin`.

The User Licenses table displays and exports Created Date and Last Updated as `dd-MMM-yyyy hh:mm tt` (for example, `28-Oct-2026 03:00 PM`), with English month names and AM/PM. It preserves the stored UTC time; sorting and date filters still use the underlying timestamps.

## ASP.NET Framework applications

Configure these named connection strings in the hosting platform:

- `BMFEntities` and `BMFEntities1` for `BMF_WebSite`
- `BMFDbContext` for `BMF.Web.Admin`

For local development, `BMFEntities1` and `BMFDbContext` can also be supplied as `ConnectionStrings__BMFEntities1` and `ConnectionStrings__BMFDbContext` environment variables. Configure Blob Storage using the `AzStorageAccount` app setting or environment variable.

Local environment-specific settings and secret files are excluded by `.gitignore`.

The public `BMF_WebSite` application targets .NET Framework 4.7.2 and runs on `bmftest`, which owns the `bmf.com.bh` custom-domain binding. Its existing server-side `Web.config` supplies the Entity Framework connection strings and `AzStorageAccount` setting. The public workflow excludes the root `Web.config` and disables deployment cleanup to retain that configuration and existing uploads. It still deploys `Views/Web.config`, which contains Razor configuration rather than credentials. Do not copy production credentials into tracked files or workflow logs.

## GitHub Actions deployment

Two workflows run on every push or manual dispatch, with separate deployment concurrency groups:

- [deploy-admin-portal.yml](../.github/workflows/deploy-admin-portal.yml) builds `BMF.WebCore.Admin` and deploys **only to `bmfadmin`**.
- [deploy-public-website.yml](../.github/workflows/deploy-public-website.yml) restores NuGet packages and publishes `BMF_WebSite` using Visual Studio MSBuild on Windows, deploying **only to `bmftest`**.

Both use the existing GitHub `bmftest` environment for OIDC deployment authentication; the environment name does not make `bmftest` an admin application or change runtime authentication. The attached publish-settings file is not used and must not be committed.

The public workflow verifies the domain binding and existing ASP.NET Framework configuration before deployment. Both workflows run [Test-PublicWebsite.ps1](../.github/scripts/Test-PublicWebsite.ps1), checking public pages and static assets without following redirects. The admin workflow separately checks the `bmfadmin` login form. A successful admin login page on the public domain is a failure, not a deployment health check.

Workflow smoke tests allow up to eight attempts, logging each failure and waiting 15 seconds between attempts, to tolerate application startup or the other workflow's brief public maintenance window. Exhausted retries fail the workflow.

Each workflow temporarily creates `app_offline.htm` on its own target to release loaded DLLs before incremental deployment, then removes it even when deployment fails. Visitors see a brief maintenance message during this step. An existing maintenance file is not overwritten.

Before the first run:

1. Configure an Entra application/service principal with a federated credential:
   - Issuer: `https://token.actions.githubusercontent.com`
   - Audience: `api://AzureADTokenExchange`
   - Subject: `repo:Ahmed-IT@6608949/bmf_old_website@1436834269:environment:bmftest`
2. Grant that identity permission to deploy to each of the `bmftest` and `bmfadmin` web apps.
3. Add these non-secret repository or `bmftest` environment variables in GitHub Actions:
   - `AZURE_CLIENT_ID`
   - `AZURE_TENANT_ID`
   - `AZURE_SUBSCRIPTION_ID`

The deployment identity must be registered in the subscription's tenant, not another tenant available to your local Azure account. For `bmftest`, the verified settings are:

- Client ID: `8b77415f-4617-40db-a559-62618949af5e`
- Tenant ID: `661bf2ff-6490-4571-90ac-4b4fed7ca17c`
- Subscription ID: `e17eaf83-548d-464f-a1e5-b1528a940a3d`

Its service principal has separate `Website Contributor` assignments scoped to the `bmftest` and `bmfadmin` web apps, not the resource group or subscription. If Azure login fails with `AADSTS700016`, verify that the client ID exists in the configured tenant before changing the workflow or granting additional permissions.

This repository's OIDC token includes the owner and repository IDs in its subject. The federated credential must match the subject emitted by `azure/login` exactly. A name-only subject causes `AADSTS700213`; keep the issuer, audience, and environment scope unchanged when correcting it.

The admin project targets `netcoreapp3.1`, which is out of support. The App Service must have a compatible runtime, or the project should be upgraded before relying on production deployments.
