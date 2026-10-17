# Preserved .NET 9 migration

These files are exact copies of the added or modified files in commit
`d13ee08c3da37b8c13537f2a45200cac808d0d2b` (`upgrade-to-NET9-1`), compared with
`978ca4a5bb5618988afe13ed68c7ff5c324ed036` (`website`). `manifest.json` records
the source revisions, changed paths, and original Git blob hashes. Deleted paths
are recorded in the manifest. Renames are represented as a deletion and addition.

This is a source snapshot, not a standalone build tree. Unchanged files and
dependencies remain available in the original Git commit. To work on the complete
migration independently, create a branch or worktree from `d13ee08`.

The migration was incomplete: the older `BMF.Web.Admin` project mixed ASP.NET MVC
and ASP.NET Core code, its database context contained an extra closing brace, and
the shared SDK project combined explicit source includes with default includes.
It is excluded from the active solutions so those changes cannot affect admin
or mobile builds. Its source is preserved without attempting a framework port as
part of branch integration.
