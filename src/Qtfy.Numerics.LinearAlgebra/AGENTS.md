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

