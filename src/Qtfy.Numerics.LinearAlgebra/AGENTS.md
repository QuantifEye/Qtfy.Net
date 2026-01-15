# AGENTS

Local instructions for `Qtfy.Numerics.LinearAlgebra`. This project is in a scaffolding phase, so keep everything minimal and placeholder-friendly.

## Scope and intent
- Focus on types and signatures; avoid full implementations until the design is settled.
- Assume dense, contiguous storage (no padding, no alignment rules yet).

## Breaking changes are fine.
- The library is in a early stage of development.
- Breaking changes are fine.

## Documentation
- Remove existing XML docs.
- Do not add XML docs beyond a short one-line summary, and avoid `<param>`, `<returns>`, or `<remarks>`.
- Avoid inline comments unless the code would be ambiguous without them.

## Tests
- Do not add or update unit tests in this phase.
- Remove existing unit tests for this project.

## Implementations
- Unless asked to do not implement any unnessesary functions
- Do not handle any errors.

## Function calling
- When calling BLAS or RawBlas function. Always use all named arguemnts and place each arguments on new line

## Immutability.
- for now elements in a matrix/vector does not have to be ReadOnly. That will detract from the current goal.

## Style.
- System.* usings should be in global using.
- Usings should appear inside namespace
