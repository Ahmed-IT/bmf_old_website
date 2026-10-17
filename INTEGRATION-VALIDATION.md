# Integration validation — 17 October 2026

## Passed

- All 94 conflicts are resolved, with no unmerged Git index entries.
- All 66 added/modified .NET 9 migration files match their original Git blob
  hashes. The manifest records all 91 added, modified, or deleted paths when
  renames are expanded into separate additions and deletions.
- All 154 compile references in the mobile and admin shared-source lists exist.
- All nine project references across the combined and admin-only solutions exist.
- Mobile, public website, and license sources match `website` (`978ca4a`),
  except for removing three pre-existing conflict-marker lines from the website
  project file while retaining its `_ViewStart.cshtml` content entry.
- The older admin subtree matches `admin-dev` (`5d9b313`) exactly.
- The newer Core admin's only project change selects its separate shared-source
  list. Its application code, views, plugins, and dependency versions are retained.
- Active C# project files parse as XML; the old website conflict markers are fixed.
- All 17 original untracked local files match their pre-integration backups.
- `dotnet restore BMF.Admin.sln --disable-parallel --verbosity minimal` succeeds.
- `dotnet build BMF.Admin.sln --no-restore --verbosity minimal` succeeds with
  **0 errors and 379 warnings** on the installed .NET 10 SDK.
- The final incremental admin build also succeeds with **0 errors**.

## Limits and existing issues

The warnings include unused variables and other compiler diagnostics, the old
ASP.NET Core 3.1 target, and NuGet advisories for Azure.Storage.Blobs 12.8.0
(NU1902) and Newtonsoft.Json 12.0.3 (NU1903). Dependency upgrades were not mixed
into the history consolidation.

The application was not started and no database migration or live-data operation
was performed. ASP.NET Core 3.1 is not installed as a runtime on this machine, so
running the admin requires that runtime or a separately validated framework
upgrade. The whole Xamarin/legacy MVC/Windows Forms solution was not built;
the successful build covers the admin-only solution.

The preserved .NET 9 snapshot is incomplete migration work, excluded from active
solutions. It is retained for reference and future migration development.

Azure DevOps authentication subsequently succeeded using system-browser sign-in.
A fresh fetch completed, and the tips of `origin/App_Dev`, `origin/admin-dev`,
`origin/master`, `origin/upgrade-to-NET9-1`, and `origin/website` were all verified
as ancestors of the integrated `main`. The new `main` branch was pushed normally
to Azure DevOps, and local `main` now tracks `origin/main`. Original remote
branches and the server default-branch setting remain unchanged.
