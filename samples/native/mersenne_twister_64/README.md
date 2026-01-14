# Mersenne Twister 64-bit example

This console app uses the original MT19937-64 C reference implementation by
Makoto Matsumoto and Takuji Nishimura. The reference files are stored in
`reference_sources/` and are kept unchanged.

Original sources:
- Reference tarball: https://www.math.sci.hiroshima-u.ac.jp/m-mat/MT/mt19937-64.tgz
- Project page: https://www.math.sci.hiroshima-u.ac.jp/m-mat/MT/emt64.html
- Reference C implementation: `reference_sources/mt19937-64.c`
- Reference notes and sample output: `reference_sources/mt19937-64test.c`, `reference_sources/mt19937-64.out.txt`, `reference_sources/mt64.h`

For convenience, the reference implementation is copied into `mt19937-64.h`
so the console app can include it directly.

Licensing information is included in the reference files themselves.
