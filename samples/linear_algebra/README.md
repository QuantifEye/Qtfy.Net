# Linear algebra CSX examples

This folder is for standalone C# script examples that exercise the linear algebra library.

Suggested layout:
- Keep each example in a single `.csx` file.
- Use a numeric prefix so scripts sort in a deliberate order.
- Optional: add a `lib/` subfolder later if you want shared helpers, but keep examples runnable on their own.

Example filenames:
- `01_basic_matrix.csx`
- `02_row_column_views.csx`
- `03_blas_multiply.csx`

Running scripts:
- Build the library once: `dotnet build src/Qtfy.Numerics.LinearAlgebra/Qtfy.Numerics.LinearAlgebra.csproj`
- Run a script: `dotnet script samples/linear_algebra/01_basic_matrix.csx`

Tip: scripts can reference the built DLL via `#r "src/Qtfy.Numerics.LinearAlgebra/bin/Debug/net10.0/Qtfy.Numerics.LinearAlgebra.dll"`.
