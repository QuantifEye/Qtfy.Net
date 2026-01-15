# ModernBlas

This folder contains a **modern, descriptive façade** over classic BLAS routines.

- **C# API surface:** `ModernBlas.cs`
  - `RealBlas` (real-valued routines)
  - `ComplexBlas` (complex-valued routines)
- **Mapping table:** below (modern names → classic BLAS → C++ `std::linalg`)

> Notes
>
> - In classic BLAS names, the leading letter encodes the scalar type:
>   - **Real:** `S*` (float), `D*` (double)
>   - **Complex:** `C*` (complex<float>), `Z*` (complex<double>)
> - Classic BLAS index-returning routines are often **1-based** (e.g., `IxAMAX`). This API uses **0-based** indices (C# convention).
> - This API assumes **transpose / conjugate / conjugate-transpose** can be expressed via your *view* types (e.g., `MatrixView` wrappers), similar in spirit to C++ `std::linalg::transposed(...)`, `conjugated(...)`, etc.

## Mapping table

| Scalar domain | Modern C# API (this repo) | Classic BLAS routine(s) | C++ `std::linalg` name (C++26) | Notes / overload mapping |
|---|---|---|---|---|
| Real | `RealBlas.Dot(x, y)` | `SDOT`, `DDOT` | `dot` | Dot product |
| Real | `RealBlas.AddScaled(alpha, x, y)` | `SAXPY`, `DAXPY` | *(no single direct equivalent)* | `y := alpha*x + y` |
| Real | `RealBlas.AddScaled(x, y)` | `SAXPY`, `DAXPY` with `alpha = 1` | *(no single direct equivalent)* | Convenience overload: `y := x + y` |
| Real | `RealBlas.Scale(alpha, x)` | `SSCAL`, `DSCAL` | `scale` | In-place scaling of a vector |
| Real | `RealBlas.Copy(x, y)` | `SCOPY`, `DCOPY` | `copy` | `y := x` |
| Real | `RealBlas.Swap(x, y)` | `SSWAP`, `DSWAP` | `swap_elements` | Swaps element-wise |
| Real | `RealBlas.SetupGivensRotation(a, b) -> (c, s, r)` | `SROTG`, `DROTG` | `setup_givens_rotation` | Generates plane rotation parameters |
| Real | `RealBlas.ApplyGivensRotation(x, y, c, s)` | `SROT`, `DROT` | `apply_givens_rotation` | Applies a plane rotation to `(x, y)` in place |
| Real | `RealBlas.Norm2(x)` | `SNRM2`, `DNRM2` | `vector_two_norm` | Euclidean norm |
| Real | `RealBlas.AbsSum(x)` | `SASUM`, `DASUM` | `vector_abs_sum` | Sum of absolute values |
| Real | `RealBlas.IndexAbsMax(x)` | `ISAMAX`, `IDAMAX` | `vector_idx_abs_max` | Returns **0-based** index (BLAS returns 1-based) |
| Real | `RealBlas.MatrixVectorMultiply(alpha, A, x, beta, y)` | `SGEMV`, `DGEMV` | `matrix_vector_product` | `y := alpha*A*x + beta*y` |
| Real | `RealBlas.MatrixVectorMultiply(A, x, y)` | `SGEMV`, `DGEMV` with `alpha = 1, beta = 0` | `matrix_vector_product` | Convenience overload |
| Real | `RealBlas.SymmetricMatrixVectorMultiply(tri, alpha, A, x, beta, y)` | `SSYMV`, `DSYMV` | `symmetric_matrix_vector_product` | `tri` selects upper/lower storage |
| Real | `RealBlas.TriangularMatrixVectorMultiply(tri, diag, A, x, y)` | `STRMV`, `DTRMV` | `triangular_matrix_vector_product` | Classic BLAS overwrites `x`; this API writes to `y` (pass `y == x` for in-place) |
| Real | `RealBlas.TriangularMatrixVectorSolve(tri, diag, A, b, x)` | `STRSV`, `DTRSV` | `triangular_matrix_vector_solve` | Solves triangular system for a single RHS |
| Real | `RealBlas.Rank1Update(alpha, x, y, A)` | `SGER`, `DGER` | `matrix_rank_1_update` | `A := alpha*x*y^T + A` |
| Real | `RealBlas.SymmetricRank1Update(tri, alpha, x, A)` | `SSYR`, `DSYR` | `symmetric_matrix_rank_1_update` | `A := alpha*x*x^T + A` |
| Real | `RealBlas.SymmetricRank2Update(tri, alpha, x, y, A)` | `SSYR2`, `DSYR2` | `symmetric_matrix_rank_2_update` | `A := alpha*x*y^T + alpha*y*x^T + A` |
| Real | `RealBlas.MatrixMultiply(alpha, A, B, beta, C)` | `SGEMM`, `DGEMM` | `matrix_product` | `C := alpha*A*B + beta*C` |
| Real | `RealBlas.MatrixMultiply(A, B, C)` | `SGEMM`, `DGEMM` with `alpha = 1, beta = 0` | `matrix_product` | Convenience overload |
| Real | `RealBlas.SymmetricMatrixMultiply(side, tri, alpha, A, B, beta, C)` | `SSYMM`, `DSYMM` | `symmetric_matrix_product` | `side` selects left/right multiply |
| Real | `RealBlas.TriangularMatrixMultiply(side, tri, diag, alpha, A, B)` | `STRMM`, `DTRMM` | `triangular_matrix_left_product` / `triangular_matrix_right_product` | Overwrites `B` (in-place) |
| Real | `RealBlas.TriangularMatrixSolve(side, tri, diag, alpha, A, B)` | `STRSM`, `DTRSM` | `triangular_matrix_matrix_left_solve` / `triangular_matrix_matrix_right_solve` | Overwrites `B` (in-place) |
| Real | `RealBlas.SymmetricRankKUpdate(tri, alpha, A, beta, C)` | `SSYRK`, `DSYRK` | `symmetric_matrix_rank_k_update` | `C` is symmetric; `tri` selects stored triangle |
| Real | `RealBlas.SymmetricRank2KUpdate(tri, alpha, A, B, beta, C)` | `SSYR2K`, `DSYR2K` | `symmetric_matrix_rank_2k_update` | Symmetric rank-2k update |
| Complex | `ComplexBlas.Dot(x, y)` | `CDOTU`, `ZDOTU` | `dot` | Unconjugated dot product |
| Complex | `ComplexBlas.DotConjugate(x, y)` | `CDOTC`, `ZDOTC` | `dotc` | Conjugates the first vector |
| Complex | `ComplexBlas.AddScaled(alpha, x, y)` | `CAXPY`, `ZAXPY` | *(no single direct equivalent)* | `y := alpha*x + y` |
| Complex | `ComplexBlas.AddScaled(x, y)` | `CAXPY`, `ZAXPY` with `alpha = 1` | *(no single direct equivalent)* | Convenience overload: `y := x + y` |
| Complex | `ComplexBlas.Scale(alpha, x)` | `CSCAL`, `ZSCAL` | `scale` | In-place scaling of a vector |
| Complex | `ComplexBlas.Copy(x, y)` | `CCOPY`, `ZCOPY` | `copy` | `y := x` |
| Complex | `ComplexBlas.Swap(x, y)` | `CSWAP`, `ZSWAP` | `swap_elements` | Swaps element-wise |
| Complex | `ComplexBlas.SetupGivensRotation(a, b) -> (c, s, r)` | `CROTG`, `ZROTG` | `setup_givens_rotation` | Generates plane rotation parameters |
| Complex | `ComplexBlas.ApplyGivensRotation(x, y, c, s)` | `CROT`, `ZROT` | `apply_givens_rotation` | Applies a plane rotation to `(x, y)` in place |
| Complex | `ComplexBlas.Norm2(x)` | `SCNRM2`, `DZNRM2` | `vector_two_norm` | Euclidean norm |
| Complex | `ComplexBlas.AbsSum(x)` | `SCASUM`, `DZASUM` | `vector_abs_sum` | Sum of absolute values |
| Complex | `ComplexBlas.IndexAbsMax(x)` | `ICAMAX`, `IZAMAX` | `vector_idx_abs_max` | Returns **0-based** index (BLAS returns 1-based) |
| Complex | `ComplexBlas.MatrixVectorMultiply(alpha, A, x, beta, y)` | `CGEMV`, `ZGEMV` | `matrix_vector_product` | `y := alpha*A*x + beta*y` |
| Complex | `ComplexBlas.MatrixVectorMultiply(A, x, y)` | `CGEMV`, `ZGEMV` with `alpha = 1, beta = 0` | `matrix_vector_product` | Convenience overload |
| Complex | `ComplexBlas.SymmetricMatrixVectorMultiply(tri, alpha, A, x, beta, y)` | `CSYMV`, `ZSYMV` | `symmetric_matrix_vector_product` | `tri` selects upper/lower storage |
| Complex | `ComplexBlas.HermitianMatrixVectorMultiply(tri, alpha, A, x, beta, y)` | `CHEMV`, `ZHEMV` | `hermitian_matrix_vector_product` | `tri` selects upper/lower storage |
| Complex | `ComplexBlas.TriangularMatrixVectorMultiply(tri, diag, A, x, y)` | `CTRMV`, `ZTRMV` | `triangular_matrix_vector_product` | Classic BLAS overwrites `x`; this API writes to `y` (pass `y == x` for in-place) |
| Complex | `ComplexBlas.TriangularMatrixVectorSolve(tri, diag, A, b, x)` | `CTRSV`, `ZTRSV` | `triangular_matrix_vector_solve` | Solves triangular system for a single RHS |
| Complex | `ComplexBlas.Rank1Update(alpha, x, y, A)` | `CGERU`, `ZGERU` | `matrix_rank_1_update` | `A := alpha*x*y^T + A` (unconjugated) |
| Complex | `ComplexBlas.Rank1UpdateConjugate(alpha, x, y, A)` | `CGERC`, `ZGERC` | `matrix_rank_1_update_c` | `A := alpha*x*y^H + A` |
| Complex | `ComplexBlas.SymmetricRank1Update(tri, alpha, x, A)` | `CSYR`, `ZSYR` | `symmetric_matrix_rank_1_update` | `A := alpha*x*x^T + A` |
| Complex | `ComplexBlas.HermitianRank1Update(tri, alpha, x, A)` | `CHER`, `ZHER` | `hermitian_matrix_rank_1_update` | `A := alpha*x*x^H + A` |
| Complex | `ComplexBlas.SymmetricRank2Update(tri, alpha, x, y, A)` | `CSYR2`, `ZSYR2` | `symmetric_matrix_rank_2_update` | `A := alpha*x*y^T + alpha*y*x^T + A` |
| Complex | `ComplexBlas.HermitianRank2Update(tri, alpha, x, y, A)` | `CHER2`, `ZHER2` | `hermitian_matrix_rank_2_update` | `A := alpha*x*y^H + conj(alpha)*y*x^H + A` |
| Complex | `ComplexBlas.MatrixMultiply(alpha, A, B, beta, C)` | `CGEMM`, `ZGEMM` | `matrix_product` | `C := alpha*A*B + beta*C` |
| Complex | `ComplexBlas.MatrixMultiply(A, B, C)` | `CGEMM`, `ZGEMM` with `alpha = 1, beta = 0` | `matrix_product` | Convenience overload |
| Complex | `ComplexBlas.SymmetricMatrixMultiply(side, tri, alpha, A, B, beta, C)` | `CSYMM`, `ZSYMM` | `symmetric_matrix_product` | `side` selects left/right multiply |
| Complex | `ComplexBlas.HermitianMatrixMultiply(side, tri, alpha, A, B, beta, C)` | `CHEMM`, `ZHEMM` | `hermitian_matrix_product` | `side` selects left/right multiply |
| Complex | `ComplexBlas.TriangularMatrixMultiply(side, tri, diag, alpha, A, B)` | `CTRMM`, `ZTRMM` | `triangular_matrix_left_product` / `triangular_matrix_right_product` | Overwrites `B` (in-place) |
| Complex | `ComplexBlas.TriangularMatrixSolve(side, tri, diag, alpha, A, B)` | `CTRSM`, `ZTRSM` | `triangular_matrix_matrix_left_solve` / `triangular_matrix_matrix_right_solve` | Overwrites `B` (in-place) |
| Complex | `ComplexBlas.SymmetricRankKUpdate(tri, alpha, A, beta, C)` | `CSYRK`, `ZSYRK` | `symmetric_matrix_rank_k_update` | `C` is symmetric; `tri` selects stored triangle |
| Complex | `ComplexBlas.HermitianRankKUpdate(tri, alpha, A, beta, C)` | `CHERK`, `ZHERK` | `hermitian_matrix_rank_k_update` | `C` is Hermitian; `tri` selects stored triangle |
| Complex | `ComplexBlas.SymmetricRank2KUpdate(tri, alpha, A, B, beta, C)` | `CSYR2K`, `ZSYR2K` | `symmetric_matrix_rank_2k_update` | Symmetric rank-2k update |
| Complex | `ComplexBlas.HermitianRank2KUpdate(tri, alpha, A, B, beta, C)` | `CHER2K`, `ZHER2K` | `hermitian_matrix_rank_2k_update` | Hermitian rank-2k update |

## Quick cross-language naming hints

- BLAS: `GEMV` / `GEMM` / `SYR(K)` / `TRSM` etc are terse “operation codes”.
- C++ `std::linalg` (C++26) favors explicit verbs like `matrix_vector_product` and `triangular_matrix_vector_solve`.
- This C# API keeps the same semantics but uses .NET-style, descriptive names:
  - `RealBlas.MatrixVectorMultiply` / `ComplexBlas.MatrixVectorMultiply` instead of `*GEMV`
  - `RealBlas.MatrixMultiply` / `ComplexBlas.MatrixMultiply` instead of `*GEMM`
  - `RealBlas.AddScaled` / `ComplexBlas.AddScaled` instead of `*AXPY`
