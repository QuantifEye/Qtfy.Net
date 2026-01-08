# BigRational

`BigRational` is an arbitrary-precision rational number type built on `BigInteger`. It aims to feel
like a BCL number: predictable value semantics, rich operator support, consistent exceptions,
and full participation in generic math (`INumber`/`ISignedNumber`).

## Design goals
- Canonical value semantics: equal values compare equal regardless of construction.
- BCL-style behavior: explicit vs implicit conversions, consistent exceptions, and familiar APIs.
- Generic math compatibility: works with `INumber` and friends for algorithm reuse.
- Practical precision: rational arithmetic without floating-point rounding artifacts.

## Representation and normalization
- Stored as `numerator` and `denominator` (`BigInteger`).
- Always normalized to lowest terms with a positive denominator.
- A default-initialized `BigRational` is treated as `0/1` via the `Denominator` property.
- No NaN/Infinity values are represented; all instances are finite.

## Construction and constants
- Constructors accept a numerator or a numerator/denominator pair.
- A zero denominator throws `DivideByZeroException`.
- Common constants: `Zero`, `One`, `NegativeOne`, and `MinusOne` (alias).

## Operators and comparisons
- Full arithmetic: `+`, `-`, `*`, `/`, `%` across `BigRational` and integral types.
- Equality and ordering operators across `BigRational`, `BigInteger`, and signed/unsigned integers.
- `CompareTo(double/float/Half)` uses total ordering semantics similar to `double.CompareTo`:
  - NaN is treated as less than any `BigRational` (result is `1` when comparing to NaN).
  - `+Infinity` is greater, `-Infinity` is less.
  - `BigRational` itself never represents NaN/Infinity.

## Conversions
- Implicit conversions from integral types, `BigInteger`, `decimal`, `float`, `double`, and `Half`.
  - Non-finite floating-point inputs throw `ArgumentException`.
- Explicit conversions to floating-point types use the `double` conversion path.
- Explicit conversions to integral types truncate toward zero and clamp to the destination range.
- Generic math helpers: `CreateChecked`, `CreateSaturating`, `CreateTruncating`.

## Rounding and numeric helpers
- `Ceiling`, `Floor`, `Truncate`, and `Round` (with `MidpointRoundingMode` and tick size).
- Predicates and helpers from `INumber`/`INumberBase` (for example `IsEvenInteger`, `IsPow2`).
- `Reciprocal`, `Pow` (integer exponent), and series-expansion `Exp` and `Log`.

## Parsing and formatting
- Parses integers or `numerator/denominator` forms, with culture-aware overloads.
- UTF-8 parsing paths decode to string then reuse the same parser.
- Formatting always returns numerator/denominator form for determinism.

## Exceptions and diagnostics
- `DivideByZeroException` for zero denominators and zero-to-negative powers.
- `ArgumentOutOfRangeException` for invalid term counts and bounds.
- `ArgumentException` for invalid parse or conversion inputs.
- `OverflowException` for out-of-range conversions.

## Why this fits the BCL
- Clear, predictable value semantics and canonicalization.
- Broad operator support consistent with built-in numeric types.
- Full generic math integration with consistent conversion behavior.
- Self-contained, immutable, and thread-safe by design.
