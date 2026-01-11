# AGENTS

Local instructions for `Qtfy.Numerics.LinearAlgebra`. This project is in a scaffolding phase, so keep everything minimal and placeholder-friendly.

## Scope and intent
- Focus on types and signatures; avoid full implementations until the design is settled.
- Assume dense, contiguous storage (no padding, no alignment rules yet).

## Documentation
- Remove existing XML docs.
- Do not add XML docs beyond a short one-line summary, and avoid `<param>`, `<returns>`, or `<remarks>`.
- Avoid inline comments unless the code would be ambiguous without them.

## Tests
- Do not add or update unit tests in this phase.
- Remove existing unit tests for this project.

## Implementations
- For linear algebra math routines (BLAS, vector/matrix ops), keep method bodies as `throw new NotImplementedException();`.
- No argument validation or exception handling, other than the `NotImplementedException` placeholders.

## BLAS surface area
- Keep BLAS-related APIs limited to: `Dot`, `AddScaled`, `MatrixVectorMultiply`, `MatrixMultiply`.
- Apply this limit consistently across `GenericBlas`, `IGenericBlas`, `GenericRawBlas`, `IGenericRawBlas`, `GenericParallelRawBlas`, `IUnsafeRawBlas`, and `UnsafeRawBlas`.
- Only add additional BLAS routines if explicitly requested.

## Function calling
- When calling BLAS or RawBlas function. Always use all named arguemnts and place each arguments on new line

## Immutability.
- for now elements in a matrix/vector does not have to be ReadOnly. That will detract from the current goal.

## Style.
- System.* usings should be in global using.
- Usings should appear inside namespace
