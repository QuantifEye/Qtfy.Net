# Mersenne Twister 32-bit example

This console app uses the original MT19937 C reference implementation by
Makoto Matsumoto and Takuji Nishimura. The reference files are stored in
`reference_sources/` and are kept unchanged.

Original sources:
- Reference tarball: https://www.math.sci.hiroshima-u.ac.jp/m-mat/MT/MT2002/CODES/mt19937ar.tgz
- Project page ("Mersenne Twister with improved initialization"): https://www.math.sci.hiroshima-u.ac.jp/m-mat/MT/MT2002/emt19937ar.html
- Reference C implementation: https://www.math.sci.hiroshima-u.ac.jp/m-mat/MT/MT2002/CODES/mt19937ar.c
- Reference notes and sample output: `reference_sources/readme-mt.txt`, `reference_sources/mt19937ar.out`

For convenience, the reference implementation is copied into `mt19937ar.h`,
and the `main` function is removed so the console app can provide its own
entry point.

Licensing information is included in the reference files themselves.
