# Project rules for Claude

## What this is

PdfPasswordRemover is a small Windows Forms application that removes the password from a PDF file.
The user picks a PDF, optionally types its user password, and the app writes an unprotected copy to
a second file. The PDF handling itself is not implemented here, it comes from the NuGet package
[iTextSharp](https://www.nuget.org/packages/iTextSharp/). The repository ships a GUI application
with an installer, it is **not** published as a NuGet package: no `GeneratePackageOnBuild`, no push
script for a package.

One solution `src/PdfPasswordRemover.sln` with exactly one project:

- `src/PdfPasswordRemover/PdfPasswordRemover.csproj`, `OutputType` `WinExe`,
  `UseWindowsForms`, the whole application.

Layout inside `src/PdfPasswordRemover`:

- `Program.cs`: the `[STAThread]` entry point, `Application.Run(new Main())`.
- `Main.cs` plus `Main.Designer.cs` and `Main.resx`: the single form. `Main.cs` holds the logic,
  `Main.Designer.cs` is generated designer code, do not hand edit it against the designer. The form
  loads a PDF, and if the password box is empty it brute forces the password, otherwise it removes
  the given password. It also drives the language combo box and the show/hide password button.
- `RemovePassword.cs` plus `IRemovePassword.cs`: `CopyPdf` opens the PDF with iTextSharp and copies
  every page into a new, unprotected document. This is the one place that touches iTextSharp.
- `languages/de-DE.xml` and `languages/en-US.xml`: the translation files consumed by
  `HaemmerElectronics.SeppPenner.Language`. Both are copied next to the executable
  (`CopyToOutputDirectory=Always`).
- `GlobalUsings.cs`: all usings of the project.
- `Icon.ico` and `License.txt`: the application icon and the license shipped next to the executable.

Repository root: `README.md` (the only user documentation), `Changelog.md`, `License.txt` (MIT),
`Screenshot_DE.PNG` and `Screenshot_EN.PNG` (linked from the README), `.editorconfig` lives under
`src`, `.gitattributes` and `.gitignore` at the root. There is no `Updating.md` and no
`HowToUse.md`.

`Setup/` holds the Inno Setup script `PdfPasswordRemover-Setup.iss`, the build helper
`build-setup-files.bat` and the built installer `PdfPasswordRemover-Setup.exe`.

## Build

```powershell
dotnet build src/PdfPasswordRemover.sln
```

There are no unit tests in this repository, so there is nothing to `dotnet test`. A behaviour
change is verified by running the app and by building the installer, see the last two bullets.

- Single target framework `net10.0-windows`, no multi-targeting. The project is `WinExe` with
  `UseWindowsForms`, so it only builds on Windows. `RuntimeIdentifiers` is `win-x64`.
- All build properties live directly in `PdfPasswordRemover.csproj`. There is **no**
  `Directory.Build.props` in this repository.
- `TreatWarningsAsErrors` is enabled, so every warning breaks the build, NuGet warnings (`NU****`)
  from restore included. A clean build reports zero warnings, keep it that way.
- `NU1701`, `NU1703` and `NU1803` are suppressed via `NoWarn` (iTextSharp resolves through the
  .NET Framework compatibility shim, which raises `NU1701`). Fix warnings instead of extending that
  list. `NuGetAudit` and `NuGetAuditMode=all` are on, so a vulnerable transitive package fails the
  build too.
- Versions come from GitVersion.MsBuild out of the git tags, for example `1.0.9-1` for the first
  commit after tag `1.0.8`. Never edit a version property or an assembly version by hand. The form
  title is `Application.ProductName + " " + Application.ProductVersion`, so an untagged build shows a
  prerelease string like `PdfPasswordRemover 1.0.9-1+Branch.master.Sha...` in the window caption.
- Restore needs nuget.org. If a private feed is configured globally on the machine and answers 404
  for public packages, restore fails with `NU1301`. Then build with an explicit source:
  `dotnet build src/PdfPasswordRemover.sln --source https://api.nuget.org/v3/index.json`.
- Running the app: start `PdfPasswordRemover.exe` from the build or publish output. It is a GUI, it
  writes nothing to stdout, judge a run by the window coming up with the loaded languages, not by
  console output.
- Building the installer: `Setup/build-setup-files.bat` publishes self contained to
  `bin/publish` and removes the `*.pdb`, then Inno Setup's `ISCC.exe` compiles
  `Setup/PdfPasswordRemover-Setup.iss` into `Setup/PdfPasswordRemover-Setup.exe`.

## Code conventions

Follow the surrounding code, it is consistent throughout every file:

- File header comment block with `<copyright file="..." company="Hämmer Electronics">` and a
  `<summary>`, then the file-scoped namespace.
- XML doc comments on every type and every member, private members included, no exceptions.
  Implementations of an interface member additionally carry `<inheritdoc cref="..."/>` and
  `<seealso cref="..."/>` pointing at that interface.
- `Nullable`, `ImplicitUsings` and `LangVersion latest` are enabled.
- New `using` directives go into `GlobalUsings.cs`, inside the existing `#pragma warning disable
  IDE0065` block, never at the top of a file. The editorconfig requires usings inside the namespace
  (`csharp_using_directive_placement=inside_namespace:warning`), which global usings cannot satisfy,
  that is what the pragma is for. Do not add other pragmas. The comment text in that block is German
  because Visual Studio generated it, leave it alone.
- Fields, properties, methods and events are always accessed with `this.` qualification
  (`dotnet_style_qualification_for_*` at severity `warning`).
- `src/.editorconfig` also enforces braces everywhere, no multiple blank lines, four spaces, CRLF,
  UTF-8, file scoped namespaces, `System` usings sorted first and `IDE0005` as warning. Analyzer
  warnings are fixed, not silenced.

## Known quirks

Do not silently "clean up" these, they are existing behaviour:

- **iTextSharp is the iText 5 line, do not "upgrade" to iText7.** The package id `iTextSharp`
  (5.5.13.x) is the last iText 5 release. iText 7 is the separate package `itext7` with a completely
  different API. `dotnet list package --outdated` will never suggest the jump because they are
  different ids, and a switch would rewrite `RemovePassword.cs`. Stay on `iTextSharp`.
- **`PdfReader.unethicalreading = true`.** `RemovePassword.CopyPdf` sets this static flag so
  iTextSharp opens a PDF that carries an owner password without the owner password. Removing the
  line makes the app refuse exactly the files it exists to open.
- **The password is encoded as ASCII.** `CopyPdf` does `Encoding.ASCII.GetBytes(userPassword)`, so a
  password with non-ASCII characters is mangled before it reaches iTextSharp. Existing behaviour,
  left as is.
- **Brute force never reports success.** When the password box is empty, `BruteForceUserPassword`
  walks `Bruteforcing` (all strings of length 1 to 16 over a large charset) and calls `CopyPdf` for
  each candidate inside a `try` that swallows every exception. A correct guess simply writes the
  output file, a wrong one throws and is ignored, and the loop keeps going. There is no progress, no
  cancel and no "found it" signal, and for a real password it runs effectively forever. This is the
  original design, do not quietly rewrite it into a real cracker.
- **`GetWord` returns `null` for an unknown key.** `HaemmerElectronics.SeppPenner.Language` has no
  fallback to another language. Every key a call site uses must exist in **both** `de-DE.xml` and
  `en-US.xml`. The error dialog title uses the key `ErrorTitle` (`Fehler` / `Error`), added in
  version 1.0.9.0 together with the message box fix below.
- **The error message box had text and title swapped.** Up to 1.0.8.0 `TryLoadPdfFile` called
  `MessageBox.Show(ex.StackTrace, ex.Message, ...)`, so the stack trace was the message and the
  exception message was the window caption. Since 1.0.9.0 the caption is the localized `ErrorTitle`
  and the body is the message followed by the stack trace. Do not swap it back.
- **The language files are UTF-8 without BOM, tab indented, CRLF.** They are edited by hand and read
  by the language library, keep the tabs and the encoding when you touch them, do not let an editor
  reformat them to spaces or add a BOM.
- **The installer is published self contained.** `build-setup-files.bat` runs
  `dotnet publish -c Release -r win-x64 --self-contained true`. The `-r win-x64` is required, a bare
  `--self-contained` without a runtime identifier fails the publish. The resulting
  `runtimeconfig.json` carries `includedFrameworks`, so the target machine needs no installed .NET
  runtime, at the price of a much larger installer.
- **The `.iss` is UTF-8 with a BOM.** Inno Setup 6 only reads a script as UTF-8 when a BOM is
  present, otherwise it falls back to the system code page. The script contains `Hämmer Electronics`
  and a copyright sign, so the BOM has to stay, do not save it as "UTF-8 without BOM". Keep CRLF.
- **The installer exe is tracked although `.gitignore` excludes `*.exe`.** `Setup/PdfPasswordRemover-Setup.exe`
  is committed with `git add -f`. Every release rebuilds it and commits the new binary.
- **`Icon.ico` and `License.txt` exist twice.** There is a copy under `src/` (used by the `.iss` as
  `SetupIconFile`) and a copy under `src/PdfPasswordRemover/` (the application icon and the license
  shipped next to the executable). Keep both in sync when either changes.
- **Badges without CI in the repository.** `README.md` links an AppVeyor build and a Snyk scan that
  are configured outside of this repository. There is no `.github` folder and no pipeline file here.
- **`src/PdfPasswordRemover.sln.DotSettings`** is tracked and holds nothing but a ReSharper user
  dictionary. Leave it alone.
- **`.gitattributes` sets `* text=auto`**, every rule of the Visual Studio template below it is
  commented out. Any binary file that must not be normalized needs its own explicit rule.

## Releasing

The release order matters, the tag has to sit on the commit that the installer is built from, so
that GitVersion burns the plain release version into the shipped exe instead of a prerelease string.

1. Make the change and add a `Changelog.md` entry at the top in the existing format:
   `* **Version 1.0.9.0 (2026-08-15)** : Short description.`
2. Bump `MyAppVersion` in `Setup/PdfPasswordRemover-Setup.iss` to the same four part version (keep
   the BOM and CRLF).
3. Commit those changes.
4. Tag that commit with the plain three part version number, no `v` prefix (`1.0.9`, `1.0.8`, ...).
   The existing tags are lightweight tags, create new ones the same way.
5. Run `Setup/build-setup-files.bat` while `HEAD` is the tagged commit, then compile the installer
   with `ISCC.exe`. The exe now carries the plain version.
6. `git add -f Setup/PdfPasswordRemover-Setup.exe` and commit it, for example `Updated setup.`.
7. Push the commits and the tag.

The version in the `Changelog.md` has four parts (`1.0.9.0`), the tag has three (`1.0.9`).
GitVersion turns the tag into the assembly version, so an untagged commit produces something like
`1.0.9-1+Branch.master.Sha...`.

## Git

- **Never amend a commit.** No `git commit --amend`, not for a typo in the message, not to add a
  forgotten file, not even when the commit is still local. Write a follow-up commit instead. The
  release versions come from tags on exact commits, an amended commit leaves its tag pointing at a
  commit that no longer exists in the branch.

## Writing style

- Commit messages are written **in English only**: short, precise subject line, explanatory body
  when needed.
- Code comments and comments in project files such as `.csproj` are **always English**, regardless
  of the language used in the conversation.
- **No em dashes or en dashes** (`—`, `–`), neither in prose, commit messages, code comments nor
  documentation. Use a regular hyphen, comma, colon, parentheses or a separate sentence.
- German texts (documentation, chat replies) always use real umlauts and ß, never ASCII
  transliterations such as `ae`, `oe`, `ue` or `ss`. Identifiers, file names and configuration keys
  stay unchanged where umlauts are technically undesirable.
