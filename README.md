# WebGaeb.Docs

This is the documentation for [WebGAEB](https://www.web-gaeb.de/), it is available at [Dangl**Docu**](https://docs.dangl-it.com/Projects/WebGAEB).

## Build Docs

Simply run the following command in the project root:

    powershell ./build.ps1

## Markdown documentation publishing

The documentation builder uses Fallout.Common 10.5.0 and requires .NET SDK 10.0.100
or later. Product target frameworks are unchanged. Run `build.sh` or `build.ps1`.
`BuildDocumentation` uses DocFX 2.78.5 and packages the configured Markdown,
TOC and resource mappings without modifying source files. API documentation,
where configured, also uses native DocFX Markdown metadata with the same scope.

The Markdown directory is `output-markdown/docs-markdown`; its sibling `.zip` has a root
`index.md` and includes mapped documentation assets. `UploadDocumentation` uses
the official Dangl.Fallout.WebDocu 8.0.1 package with `SetMarkdownPackagePath`,
the existing explicit version, credentials, downloadable assets and conflict policy.
No custom HTTP uploader is used. Deployment targets require real credentials;
use `--help` or `--plan` for safe build-graph inspection.

`TestMarkdownDocumentation` checks packaging, navigation and asset preservation locally.

Azure Key Vault authentication also requires `KeyVaultTenantId` (Jenkins credential
`AzureKeyVaultTenantId`), alongside the existing vault URL, client ID and client secret.
