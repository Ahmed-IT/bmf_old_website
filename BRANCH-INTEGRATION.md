# Consolidated source baseline

The integration joins the histories of `upgrade-to-NET9-1` (`d13ee08`) and
`admin-dev` (`5d9b313`). The former already contains the `website`,
`upgrade-to-NET9`, `App_Dev`, and `master` branch tips recorded locally.
Original branches are retained.

## Admin development

Open `BMF.Admin.sln` and use `BMF.WebCore.Admin` as the startup project. This is
the newer admin application from `admin-dev`, including its authentication,
entities, services, views, and plugins. Its framework and package versions are
preserved; no database migration is run by the branch-integration checks.

The project targets ASP.NET Core 3.1. Running it requires the corresponding
runtime and a separately configured development database. The local machine's
.NET 9/10 SDKs alone do not supply the ASP.NET Core 3.1 runtime. Review local
configuration before starting the application.

## Other applications

`BMF_NewApp.sln` includes projects from both source solutions. Mobile, public
website, and license application code use the latest pre-migration `website`
baseline. Three conflict-marker lines already committed in the website project
file are removed, retaining its `_ViewStart.cshtml` content entry so the project
is valid XML. The older `BMF.Web.Admin` application uses the complete `admin-dev`
version, preserving its legacy MVC configuration and source files.

The two shared-source lists are intentionally separate:

- `BMF.Shared/BMF.Shared.projitems` retains the original mobile/legacy models.
- `BMF.Shared/BMF.Shared.Admin.projitems` retains the newer admin shared entities,
  models, services, mappings, and authentication code.

This prevents the admin's framework dependencies from entering the mobile builds
and preserves the old models that the automatic merge would otherwise delete.

## Conflict resolution

The trial merge had 94 conflicted paths: 83 static assets and 11 project,
database, mapping, startup, and solution files. The older admin subtree is kept
intact from `admin-dev`, including its original asset locations, EF6 mappings,
startup, project, and package configuration. Mobile and license changes from the
incomplete .NET 9 migration are isolated with the rest of that migration.

All added or modified migration files are preserved byte-for-byte in
`migration-snapshots/net9`, with hashes and deleted paths in its manifest. The
complete migration also remains in Git history and on its original branch.

## Validation and publishing

See `INTEGRATION-VALIDATION.md` for the checks and build result. The merge is based
on locally recorded remote branch tips. Azure DevOps authentication must succeed
before the server can be refreshed and the consolidated branch published. No
force push, deletion of original branches, or default-branch change is needed.
