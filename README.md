# WebGaeb.Docs

This is the documentation for [WebGAEB](https://www.web-gaeb.de/), it is available at [Dangl**Docu**](https://docs.dangl-it.com/Projects/WebGAEB).

## Build Docs

Simply run the following command in the project root:

    powershell ./build.ps1


## Markdown documentation publishing

`BuildDocumentation` also generates `output-markdown/docs-markdown` and `output-markdown/docs-markdown.zip`.
Start with `index.md` (the source README), or `toc.md` for all articles and API pages.
Changelog/license text and linked documentation assets are included; the ZIP has no
enclosing directory.

The Markdown renderer converts the existing published DocFX article HTML, including
API signatures, code examples and anchors. This keeps the current documentation
toolchain and public project boundaries intact, without requiring newer DocFX or
migrating the legacy NUKE build. It does not modify documentation sources.

`UploadDocumentation` uploads HTML first, then sends the Markdown ZIP to
`/API/MarkdownDocuments/Upload` with the same API key and exact version. Generation
or upload errors fail publishing; existing branch gates and version-conflict policies
are preserved. `TestMarkdownDocumentation` exercises packaging, links, assets,
failure handling and the multipart contract using only a local HTTP server.
