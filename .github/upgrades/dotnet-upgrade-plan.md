# .NET 9.0 Upgrade Plan

## Execution Steps

Execute steps below sequentially one by one in the order they are listed.

1. Validate that an .NET 9.0 SDK required for this upgrade is installed on the machine and if not, help to get it installed.
2. Ensure that the SDK version specified in global.json files is compatible with the .NET 9.0 upgrade.
3. Upgrade BMF_NewApp\BMF_NewApp\BMF_NewApp\BMF_NewApp.csproj
4. Upgrade BMF_LicenseApp\BMF_LicenseApp.csproj
5. Upgrade BMF.Shared\BMF.Shared.shproj
6. Upgrade BMF.Web.Admin\BMF.Web.Admin.csproj
7. Upgrade BMF_NewApp\BMF_NewApp\BMF_NewApp.iOS\BMF_NewApp.iOS.csproj
8. Upgrade BMF_NewApp\BMF_NewApp\BMF_NewApp.Android\BMF_NewApp.Android.csproj

## Settings

### Excluded projects

| Project name | Description |
|:-------------|:-----------:|

### Aggregate NuGet packages modifications across all projects

NuGet packages used across all selected projects or their dependencies that need version update in projects that reference them. (Security vulnerability related packages excluded per user instruction.)

| Package Name | Current Version | New Version | Description |
|:-------------|:---------------:|:-----------:|:------------|
| Antlr | 3.5.0.2 | 4.6.6 | Recommended for .NET 9.0 |
| EntityFramework | 6.4.4;6.4.4 | 6.5.1 | Recommended for .NET 9.0 / deprecated replacement |
| Microsoft.AppCenter | 4.2.0;4.2.0;4.2.0 | 5.0.7 | Recommended for .NET 9.0 |
| Microsoft.AppCenter.Analytics | 4.2.0;4.2.0;4.2.0 | 5.0.7 | Recommended for .NET 9.0 |
| Microsoft.AppCenter.Crashes | 4.2.0;4.2.0;4.2.0 | 5.0.7 | Recommended for .NET 9.0 |
| Newtonsoft.Json | 13.0.1;13.0.1;13.0.1;13.0.3 | 13.0.4 | Recommended for .NET 9.0 |
| Refractored.MvvmHelpers | 1.6.2;1.6.2;1.6.2 | 1.6.2 | Deprecated (no newer version) |
| System.IO.Packaging | 6.0.0 | 9.0.10 | Recommended for .NET 9.0 (excluding security treatment) |
| System.Runtime.CompilerServices.Unsafe | 4.7.0 | 6.1.2 | Recommended for .NET 9.0 |

### Project upgrade details

#### BMF_NewApp\\BMF_NewApp\\BMF_NewApp\\BMF_NewApp.csproj modifications

Project properties changes:
  - Target framework should be changed (multi-platform Xamarin.Forms migration will move to .NET 9 platform-specific TFMs later) – requires conversion strategy (MAUI or .NET iOS/Android split).

NuGet packages changes:
  - Microsoft.AppCenter should be updated from `4.2.0` to `5.0.7`.
  - Microsoft.AppCenter.Analytics should be updated from `4.2.0` to `5.0.7`.
  - Microsoft.AppCenter.Crashes should be updated from `4.2.0` to `5.0.7`.
  - Newtonsoft.Json should be updated from `13.0.1` to `13.0.4`.
  - Xamarin.Essentials functionality included in framework reference (remove when migrating to .NET 9 target TFMs).
  - Xamarin.Forms functionality included in framework reference (replace with MAUI or .NET SDK UI stack).
  - Refractored.MvvmHelpers deprecated (evaluate replacement or removal).

Other changes:
  - Plan detailed MAUI migration (out of scope for immediate TFM switch until project restructuring).

#### BMF_LicenseApp\\BMF_LicenseApp.csproj modifications

Project properties changes:
  - Convert project file to SDK-style.
  - Target framework should be changed from `.NETFramework,Version=v4.7.1` to `net9.0-windows`.

NuGet packages changes:
  - Newtonsoft.Json should be updated from `13.0.3` to `13.0.4`.
  - System.IO.Packaging should be updated from `6.0.0` to `9.0.10`.
  - System.Runtime.CompilerServices.Unsafe should be updated from `4.7.0` to `6.1.2`.
  - Packages now provided by framework reference should be removed: System.Buffers, System.IO, System.Memory, System.Net.Http, System.Numerics.Vectors, System.Runtime, System.Security.Cryptography.* , System.Text.RegularExpressions.

Other changes:
  - After SDK-style conversion remove redundant binding redirects and cleanup app.config if present.

#### BMF.Shared\\BMF.Shared.shproj modifications

Project properties changes:
  - Target framework should be changed from legacy shared project (v4.5.1 implied) to `net9.0` via conversion to SDK-style class library (create new .csproj referencing shared code files).

Other changes:
  - Introduce new SDK-style `BMF.Shared.csproj` and migrate includes.

#### BMF.Web.Admin\\BMF.Web.Admin.csproj modifications

Project properties changes:
  - Convert project file to SDK-style.
  - Target framework should be changed from `.NETFramework,Version=v4.7.2` to `net9.0`.

NuGet packages changes:
  - Antlr should be updated from `3.5.0.2` to `4.6.6`.
  - EntityFramework should be updated from `6.4.4` to `6.5.1` (deprecated replacement).
  - Newtonsoft.Json should be updated from `12.0.3` to `13.0.4`.
  - Remove incompatible / framework provided packages: Microsoft.AspNet.* (Mvc, Razor, WebApi, WebPages), Microsoft.CodeDom.Providers.DotNetCompilerPlatform, Microsoft.Web.Infrastructure.

Feature upgrades:
  - Replace System.Web.Optimization with explicit script/style tags.
  - Convert RouteCollection registrations to endpoint routing (MapControllerRoute in Program.cs).
  - Convert GlobalFilterCollection to middleware / services configuration.
  - Adjust classic EntityFramework initialization for .NET Core (DbContext registration, connection config).
  - Migrate Global.asax startup logic to Program.cs / minimal hosting model.

Other changes:
  - Review web.config for removal/replacement with appsettings.json.

#### BMF_NewApp\\BMF_NewApp\\BMF_NewApp.iOS\\BMF_NewApp.iOS.csproj modifications

Project properties changes:
  - Convert project file to SDK-style.
  - Target framework should be changed from `Xamarin.iOS,Version=v1.0` to `net9.0-ios`.

NuGet packages changes:
  - Microsoft.AppCenter packages update.
  - Newtonsoft.Json update.
  - Remove Xamarin.Essentials (framework provided).
  - Replace Xamarin.Forms with .NET MAUI (future step – requires project restructuring).
  - Refractored.MvvmHelpers deprecated.

Other changes:
  - Prepare for MAUI migration (Info.plist adjustments, single-project structure potential).

#### BMF_NewApp\\BMF_NewApp\\BMF_NewApp.Android\\BMF_NewApp.Android.csproj modifications

Project properties changes:
  - Convert project file to SDK-style.
  - Target framework should be changed from `MonoAndroid,Version=v13.0` to `net9.0-android`.

NuGet packages changes:
  - Microsoft.AppCenter packages update.
  - Newtonsoft.Json update.
  - Remove Xamarin.Essentials (framework provided).
  - Replace Xamarin.Forms with .NET MAUI (future step – requires project restructuring).
  - Refractored.MvvmHelpers deprecated.

Other changes:
  - Evaluate migration path to MAUI single project (AndroidManifest, resources).
