using Serilog;
using Fallout.Common.IO;
using Fallout.Common.Tools.AzureKeyVault;
using Fallout.Common;
using Fallout.Common.ProjectModel;
using Fallout.Common.Tools.GitVersion;
using Fallout.Common.Tools.DocFX;
using Fallout.WebDocu;
using static Fallout.Common.IO.PathConstruction;
using static Fallout.Common.Tools.DocFX.DocFXTasks;
using static Fallout.WebDocu.WebDocuTasks;

partial class Build : FalloutBuild
{
    // Console application entry point. Also defines the default target.
    Target TestMarkdownDocumentation => _ => _
        .Executes(() => MarkdownDocumentationTests.Run(OutputDirectory / "markdown-tests"));

    public static int Main() => Execute<Build>(x => x.BuildDocumentation);

    [AzureKeyVaultConfiguration(
        BaseUrlParameterName = nameof(KeyVaultBaseUrl),
        ClientIdParameterName = nameof(KeyVaultClientId),
        ClientSecretParameterName = nameof(KeyVaultClientSecret),
    TenantIdParameterName = nameof(KeyVaultTenantId))]
    readonly AzureKeyVaultConfiguration AzureKeyVaultConfiguration;

    [Solution("WebGaeb.Docs.sln")] readonly Solution Solution;
    AbsolutePath SolutionDirectory => Solution.Directory;
    AbsolutePath OutputDirectory => SolutionDirectory / "output";

    string DocFxFile => SolutionDirectory / "docs" / "docfx.json";

    [Parameter] readonly string KeyVaultBaseUrl;
    [Parameter] readonly string KeyVaultClientId;
    [Parameter] readonly string KeyVaultClientSecret;
    [Parameter] readonly string KeyVaultTenantId;
    [GitVersion(Framework = "netcoreapp3.1")] readonly GitVersion GitVersion;

    [AzureKeyVaultSecret] readonly string DocuBaseUrl;
    [AzureKeyVaultSecret("DanglWebGaebDocs-DocuApiKey")] readonly string DocuApiKey;

    Target Clean => _ => _
            .Executes(() =>
            {
                OutputDirectory.CreateOrCleanDirectory();
                (SolutionDirectory / "output-markdown").CreateOrCleanDirectory();
            });

    Target BuildDocumentation => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            RunDocFx("build", DocFxFile);

            MarkdownDocumentation.Create(DocFxFile, SolutionDirectory / "output-markdown" / "docs-markdown");
        });

    Target UploadDocumentation => _ => _
        .DependsOn(BuildDocumentation)
        .Requires(() => DocuApiKey)
        .Requires(() => DocuBaseUrl)
        .Executes(() =>
        {
            Log.Information("Docs version: " + GitVersion.NuGetVersion);
            WebDocu(s => s
                    .SetDocuBaseUrl(DocuBaseUrl)
                    .SetDocuApiKey(DocuApiKey)
                    .SetSourceDirectory(OutputDirectory)
                    .SetMarkdownPackagePath(SolutionDirectory / "output-markdown" / "docs-markdown.zip")
                    .SetVersion(GitVersion.NuGetVersion)
                    .SetSkipForVersionConflicts(false));
        });
}
