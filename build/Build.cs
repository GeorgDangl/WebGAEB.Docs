using Nuke.Common;
using Nuke.Common.Tools.GitVersion;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.DocFx.DocFxTasks;
using Nuke.Common.Tools.DocFx;
using static Nuke.WebDocu.WebDocuTasks;
using Nuke.WebDocu;
using Nuke.Azure.KeyVault;

[KeyVaultSettings(
    VaultBaseUrlParameterName = nameof(KeyVaultBaseUrl),
    ClientIdParameterName = nameof(KeyVaultClientId),
    ClientSecretParameterName = nameof(KeyVaultClientSecret))]
class Build : NukeBuild
{
    // Console application entry point. Also defines the default target.
    public static int Main () => Execute<Build>(x => x.BuildDocumentation);

    string DocFxFile => SolutionDirectory / "docs" / "docfx.json";

    [Parameter] string KeyVaultBaseUrl;
    [Parameter] string KeyVaultClientId;
    [Parameter] string KeyVaultClientSecret;
    [GitVersion] readonly GitVersion GitVersion;

    [KeyVaultSecret] string DocuApiEndpoint;
    [KeyVaultSecret("DanglWebGaebDocs-DocuApiKey")] string DocuApiKey;

    Target Clean => _ => _
            .Executes(() =>
            {
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
