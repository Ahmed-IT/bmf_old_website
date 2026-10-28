# Local and deployment configuration

The checked-in configuration files contain empty connection-string values. Supply credentials through environment variables or the hosting platform's secret settings; do not commit them.

## ASP.NET Core admin

`Host.CreateDefaultBuilder` loads environment variables over `appsettings.json`. Configure the values needed for the active environment:

- `ConnectionStrings__DefaultDbContextLocal`
- `ConnectionStrings__DefaultDbContextStaging`
- `ConnectionStrings__DefaultDbContextProd`
- `AzureStorageConfig__ConnectionString`

For local development, use an ignored `appsettings.Development.json` or a local secret store.

## ASP.NET Framework applications

Configure these named connection strings in the hosting platform:

- `BMFEntities` and `BMFEntities1` for `BMF_WebSite`
- `BMFDbContext` for `BMF.Web.Admin`

For local development, `BMFEntities1` and `BMFDbContext` can also be supplied as `ConnectionStrings__BMFEntities1` and `ConnectionStrings__BMFDbContext` environment variables. Configure Blob Storage using the `AzStorageAccount` app setting or environment variable.

Local environment-specific settings and secret files are excluded by `.gitignore`.
