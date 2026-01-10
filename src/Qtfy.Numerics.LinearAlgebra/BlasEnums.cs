// <copyright file="BlasEnums.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.LinearAlgebra;

public enum Transpose
{
    NoTrans,

    Trans,

    ConjTrans,
}

public enum Uplo
{
    Upper,

    Lower,
}

public enum Diag
{
    NonUnit,

    Unit,
}

public enum Side
{
    Left,

    Right,
}
