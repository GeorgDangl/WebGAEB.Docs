using System;
using System.IO;
using System.Linq;
using Nuke.Common;
using Nuke.Common.Git;
using Nuke.Common.Tools.GitVersion;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Common.Tools.DocFx.DocFxTasks;
using Nuke.Common.Tools.DocFx;
using static Nuke.WebDocu.WebDocuTasks;
using Nuke.WebDocu;

class Build : NukeBuild
{
    // Console application entry point. Also defines the default target.
    public static int Main () => Execute<Build>(x => x.BuildDocumentation);

    string DocFxFile => SolutionDirectory / "docs" / "docfx.json";

    [GitVersion] readonly GitVersion GitVersion;

    [Parameter] readonly string DocuApiKey;
    [Parameter] readonly string DocuApiEndpoint;

    Target Clean => _ => _
            .OnlyWhen(() => false) // Disabled for safety.
            .Executes(() =>
            {
                //DeleteDirectories(GlobDirectories(SourceDirectory, "**/bin", "**/obj"));
                EnsureCleanDirectory(OutputDirectory);
            });

    Target BuildDocumentation => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DocFxBuild(DocFxFile, s => s
                .ClearXRefMaps()
                .SetLogLevel(DocFxLogLevel.Info));
        });

    Target UploadDocumentation => _ => _
        .DependsOn(BuildDocumentation)
        .Requires(() => DocuApiKey)
        .Requires(() => DocuApiEndpoint)
        .Executes(() =>
        {
            Logger.Info("Docs version: " + GitVersion.NuGetVersion);
            WebDocu(s => s
                .SetDocuApiEndpoint(DocuApiEndpoint)
                .SetDocuApiKey(DocuApiKey)
                .SetSourceDirectory(OutputDirectory)
                .SetVersion(GitVersion.NuGetVersion)
            );
        });
}
