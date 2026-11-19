using Nuke.Azure.KeyVault;
using Nuke.Common;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.GitVersion;
using Nuke.DocFX;
using Nuke.WebDocu;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.DocFX.DocFXTasks;
using static Nuke.WebDocu.WebDocuTasks;

class Build : NukeBuild
{
    // Console application entry point. Also defines the default target.
    public static int Main() => Execute<Build>(x => x.BuildDocumentation);

    [KeyVaultSettings(
        BaseUrlParameterName = nameof(KeyVaultBaseUrl),
        ClientIdParameterName = nameof(KeyVaultClientId),
        ClientSecretParameterName = nameof(KeyVaultClientSecret))]
    readonly KeyVaultSettings KeyVaultSettings;

    [Solution("WebGAEB.Docs.sln")] readonly Solution Solution;
    AbsolutePath SolutionDirectory => Solution.Directory;
    AbsolutePath OutputDirectory => SolutionDirectory / "output";

    string DocFxFile => SolutionDirectory / "docs" / "docfx.json";

    [Parameter] readonly string KeyVaultBaseUrl;
    [Parameter] readonly string KeyVaultClientId;
    [Parameter] readonly string KeyVaultClientSecret;
    [GitVersion] readonly GitVersion GitVersion;

    [KeyVaultSecret] readonly string DocuBaseUrl;
    [KeyVaultSecret("DanglWebGaebDocs-DocuApiKey")] readonly string DocuApiKey;

    Target Clean => _ => _
            .Executes(() =>
            {
                EnsureCleanDirectory(OutputDirectory);
            });

    Target TestMarkdownDocumentation => _ => _
        .Executes(() => MarkdownDocumentationTests.Run(OutputDirectory / "markdown-tests"));

    Target BuildDocumentation => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DocFXBuild(x => x.SetConfigFile(DocFxFile));

            MarkdownDocumentation.Build(SolutionDirectory, OutputDirectory,
                SolutionDirectory / "output-markdown" / "docs-markdown", requireApi: false);
        });

    Target UploadDocumentation => _ => _
        .DependsOn(BuildDocumentation)
        .Requires(() => DocuApiKey)
        .Requires(() => DocuBaseUrl)
        .Executes(() =>
        {
            Logger.Info("Docs version: " + GitVersion.NuGetVersion);
            MarkdownDocumentation.Publish(DocuBaseUrl, DocuApiKey, GitVersion.NuGetVersion,
                SolutionDirectory / "output-markdown" / "docs-markdown.zip",
                () => WebDocu(s => s
                    .SetDocuBaseUrl(DocuBaseUrl)
                    .SetDocuApiKey(DocuApiKey)
                    .SetSourceDirectory(OutputDirectory)
                    .SetVersion(GitVersion.NuGetVersion)
                ));
        });
}
