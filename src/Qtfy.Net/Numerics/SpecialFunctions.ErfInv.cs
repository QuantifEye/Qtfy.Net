// <copyright file="SpecialFunctions.ErfInv.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics
{
    using System.Runtime.CompilerServices;
    using static System.Math;

    /// <summary>
    /// A collection of special mathematical functions.
    /// </summary>
    public static partial class SpecialFunctions
    {
        /// <summary>
        /// Help function for approximating the inverse error function on a certain interval, see Erf function below.
        /// </summary>
        /// <param name="z">
        /// The point at which to evaluate the help function.
        /// </param>
        /// <returns>
        /// A double value used in the inverse Erf function.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static double ErfInvImplA(double z)
        {
            const double n0 = -0.0005087819496582806;
            const double n1 = -0.008368748197417368;
            const double n2 = 0.03348066254097446;
            const double n3 = -0.012692614766297404;
            const double n4 = -0.03656379714117627;
            const double n5 = 0.02198786811111689;
            const double n6 = 0.008226878746769157;
            const double n7 = -0.005387729650712429;

            const double d0 = 1.0;
            const double d1 = -0.9700050433032906;
            const double d2 = -1.5657455823417585;
            const double d3 = 1.5622155839842302;
            const double d4 = 0.662328840472003;
            const double d5 = -0.7122890234154284;
            const double d6 = -0.05273963823400997;
            const double d7 = 0.07952836873415717;
            const double d8 = -0.0023339375937419;
            const double d9 = 0.0008862163904564247;

            var n =
                FusedMultiplyAdd(
                    FusedMultiplyAdd(
                        FusedMultiplyAdd(
                            FusedMultiplyAdd(
                                FusedMultiplyAdd(
                                    FusedMultiplyAdd(
                                        FusedMultiplyAdd(n7,
                                            z, n6),
                                        z, n5),
                                    z, n4),
                                z, n3),
                            z, n2),
                        z, n1),
                    z, n0);
            var d =
                FusedMultiplyAdd(
                    FusedMultiplyAdd(
                        FusedMultiplyAdd(
                            FusedMultiplyAdd(
                                FusedMultiplyAdd(
                                    FusedMultiplyAdd(
                                        FusedMultiplyAdd(
                                            FusedMultiplyAdd(
                                                FusedMultiplyAdd(d9,
                                                    z, d8),
                                                z, d7),
                                            z, d6),
                                        z, d5),
                                    z, d4),
                                z, d3),
                            z, d2),
                        z, d1),
                    z, d0);
            return n / d;
        }

        /// <summary>
        /// Help function for approximating the inverse error function on a certain interval, see Erf function below.
        /// </summary>
        /// <param name="z">
        /// The point at which to evaluate the help function.
        /// </param>
        /// <returns>
        /// A double value used in the inverse Erf function.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static double ErfInvImplB(double z)
        {
            const double n0 = -0.20243350835593876;
            const double n1 = 0.10526468069939171;
            const double n2 = 8.3705032834312;
            const double n3 = 17.644729840837403;
            const double n4 = -18.851064805871424;
            const double n5 = -44.6382324441787;
            const double n6 = 17.445385985570866;
            const double n7 = 21.12946554483405;
            const double n8 = -3.6719225470772936;

            const double d0 = 1.0;
            const double d1 = 6.242641248542475;
            const double d2 = 3.971343795334387;
            const double d3 = -28.66081804998;
            const double d4 = -20.14326346804852;
            const double d5 = 48.560921310873994;
            const double d6 = 10.826866735546016;
            const double d7 = -22.643693341313973;
            const double d8 = 1.7211476576120028;

            var n =
                FusedMultiplyAdd(
                    FusedMultiplyAdd(
                        FusedMultiplyAdd(
                            FusedMultiplyAdd(
                                FusedMultiplyAdd(
                                    FusedMultiplyAdd(
                                        FusedMultiplyAdd(
                                            FusedMultiplyAdd(n8,
                                                z, n7),
                                            z, n6),
                                        z, n5),
                                    z, n4),
                                z, n3),
                            z, n2),
                        z, n1),
                    z, n0);
            var d =
                FusedMultiplyAdd(
                    FusedMultiplyAdd(
                        FusedMultiplyAdd(
                            FusedMultiplyAdd(
                                FusedMultiplyAdd(
                                    FusedMultiplyAdd(
                                        FusedMultiplyAdd(
                                            FusedMultiplyAdd(d8,
                                                z, d7),
                                            z, d6),
                                        z, d5),
                                    z, d4),
                                z, d3),
                            z, d2),
                        z, d1),
                    z, d0);
            return n / d;
        }

        /// <summary>
        /// Help function for approximating the inverse error function on a certain interval, see Erf function below.
        /// </summary>
        /// <param name="z">
        /// The point at which to evaluate the help function.
        /// </param>
        /// <returns>
        /// A double value used in the inverse Erf function.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static double ErfInvImplC(double z)
        {
            const double n0 = -0.1311027816799519;
            const double n1 = -0.16379404719331705;
            const double n2 = 0.11703015634199525;
            const double n3 = 0.38707973897260434;
            const double n4 = 0.3377855389120359;
            const double n5 = 0.14286953440815717;
            const double n6 = 0.029015791000532906;
            const double n7 = 0.0021455899538880526;
            const double n8 = -6.794655751811263E-07;
            const double n9 = 2.8522533178221704E-08;
            const double n10 = -6.81149956853777E-10;

            const double d0 = 1.0;
            const double d1 = 3.4662540724256723;
            const double d2 = 5.381683457070069;
            const double d3 = 4.778465929458438;
            const double d4 = 2.5930192162362027;
            const double d5 = 0.848854343457902;
            const double d6 = 0.15226433829533179;
            const double d7 = 0.011059242293464892;

            var n =
                FusedMultiplyAdd(
                    FusedMultiplyAdd(
                        FusedMultiplyAdd(
                            FusedMultiplyAdd(
                                FusedMultiplyAdd(
                                    FusedMultiplyAdd(
                                        FusedMultiplyAdd(
                                            FusedMultiplyAdd(
                                                FusedMultiplyAdd(
                                                    FusedMultiplyAdd(n10,
                                                        z, n9),
                                                    z, n8),
                                                z, n7),
                                            z, n6),
                                        z, n5),
                                    z, n4),
                                z, n3),
                            z, n2),
                        z, n1),
                    z, n0);
            var d =
                FusedMultiplyAdd(
                    FusedMultiplyAdd(
                        FusedMultiplyAdd(
                            FusedMultiplyAdd(
                                FusedMultiplyAdd(
                                    FusedMultiplyAdd(
                                        FusedMultiplyAdd(d7,
                                            z, d6),
                                        z, d5),
                                    z, d4),
                                z, d3),
                            z, d2),
                        z, d1),
                    z, d0);
            return n / d;
        }

        /// <summary>
        /// Help function for approximating the inverse error function on a certain interval, see Erf function below.
        /// </summary>
        /// <param name="z">
        /// The point at which to evaluate the help function.
        /// </param>
        /// <returns>
        /// A double value used in the inverse Erf function.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static double ErfInvImplD(double z)
        {
            const double n0 = -0.0350353787183178;
            const double n1 = -0.0022242652921344794;
            const double n2 = 0.018557330651423107;
            const double n3 = 0.009508047013259196;
            const double n4 = 0.0018712349281955923;
            const double n5 = 0.00015754461742496055;
            const double n6 = 4.60469890584318E-06;
            const double n7 = -2.304047769118826E-10;
            const double n8 = 2.6633922742578204E-12;

            const double d0 = 1.0;
            const double d1 = 1.3653349817554064;
            const double d2 = 0.7620591645536234;
            const double d3 = 0.22009110576413124;
            const double d4 = 0.03415891436709477;
            const double d5 = 0.00263861676657016;
            const double d6 = 7.646752923027944E-05;

            var n =
                FusedMultiplyAdd(
                    FusedMultiplyAdd(
                        FusedMultiplyAdd(
                            FusedMultiplyAdd(
                                FusedMultiplyAdd(
                                    FusedMultiplyAdd(
                                        FusedMultiplyAdd(
                                            FusedMultiplyAdd(n8,
                                                z, n7),
                                            z, n6),
                                        z, n5),
                                    z, n4),
                                z, n3),
                            z, n2),
                        z, n1),
                    z, n0);
            var d =
                FusedMultiplyAdd(
                    FusedMultiplyAdd(
                        FusedMultiplyAdd(
                            FusedMultiplyAdd(
                                FusedMultiplyAdd(
                                    FusedMultiplyAdd(d6,
                                        z, d5),
                                    z, d4),
                                z, d3),
                            z, d2),
                        z, d1),
                    z, d0);
            return n / d;
        }

        /// <summary>
        /// Help function for approximating the inverse error function on a certain interval, see Erf function below.
        /// </summary>
        /// <param name="z">
        /// The point at which to evaluate the help function.
        /// </param>
        /// <returns>
        /// A double value used in the inverse Erf function.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static double ErfInvImplE(double z)
        {
            const double n0 = -0.016743100507663373;
            const double n1 = -0.0011295143874558028;
            const double n2 = 0.001056288621524929;
            const double n3 = 0.00020938631748758808;
            const double n4 = 1.4962478375834237E-05;
            const double n5 = 4.4969678992770644E-07;
            const double n6 = 4.625961635228786E-09;
            const double n7 = -2.811287356288318E-14;
            const double n8 = 9.905570997331033E-17;

            const double d0 = 1.0;
            const double d1 = 0.5914293448864175;
            const double d2 = 0.1381518657490833;
            const double d3 = 0.016074608709367652;
            const double d4 = 0.0009640118070051656;
            const double d5 = 2.7533547476472603E-05;
            const double d6 = 2.82243172016108E-07;

            var n =
                FusedMultiplyAdd(
                    FusedMultiplyAdd(
                        FusedMultiplyAdd(
                            FusedMultiplyAdd(
                                FusedMultiplyAdd(
                                    FusedMultiplyAdd(
                                        FusedMultiplyAdd(
                                            FusedMultiplyAdd(n8,
                                                z, n7),
                                            z, n6),
                                        z, n5),
                                    z, n4),
                                z, n3),
                            z, n2),
                        z, n1),
                    z, n0);
            var d =
                FusedMultiplyAdd(
                    FusedMultiplyAdd(
                        FusedMultiplyAdd(
                            FusedMultiplyAdd(
                                FusedMultiplyAdd(
                                    FusedMultiplyAdd(d6,
                                        z, d5),
                                    z, d4),
                                z, d3),
                            z, d2),
                        z, d1),
                    z, d0);
            return n / d;
        }

        /// <summary>
        /// Help function for approximating the inverse error function on a certain interval, see Erf function below.
        /// </summary>
        /// <param name="z">
        /// The point at which to evaluate the help function.
        /// </param>
        /// <returns>
        /// A double value used in the inverse Erf function.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static double ErfInvImplF(double z)
        {
            const double n0 = -0.002497821279189813;
            const double n1 = -7.79190719229054E-06;
            const double n2 = 2.5472303741302746E-05;
            const double n3 = 1.6239777734251093E-06;
            const double n4 = 3.963410113048012E-08;
            const double n5 = 4.116328311909442E-10;
            const double n6 = 1.4559628671867504E-12;
            const double n7 = -1.1676501239718427E-18;

            const double d0 = 1.0;
            const double d1 = 0.2071231122144225;
            const double d2 = 0.01694108381209759;
            const double d3 = 0.0006905382656226846;
            const double d4 = 1.4500735981823264E-05;
            const double d5 = 1.4443775662814415E-07;
            const double d6 = 5.097612765997785E-10;

            var n =
                FusedMultiplyAdd(
                    FusedMultiplyAdd(
                        FusedMultiplyAdd(
                            FusedMultiplyAdd(
                                FusedMultiplyAdd(
                                    FusedMultiplyAdd(
                                        FusedMultiplyAdd(n7,
                                            z, n6),
                                        z, n5),
                                    z, n4),
                                z, n3),
                            z, n2),
                        z, n1),
                    z, n0);
            var d =
                FusedMultiplyAdd(
                    FusedMultiplyAdd(
                        FusedMultiplyAdd(
                            FusedMultiplyAdd(
                                FusedMultiplyAdd(
                                    FusedMultiplyAdd(d6,
                                        z, d5),
                                    z, d4),
                                z, d3),
                            z, d2),
                        z, d1),
                    z, d0);
            return n / d;
        }

        /// <summary>
        /// Help function for approximating the inverse error function on a certain interval, see Erf function below.
        /// </summary>
        /// <param name="z">
        /// The point at which to evaluate the help function.
        /// </param>
        /// <returns>
        /// A double value used in the inverse Erf function.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static double ErfInvImplG(double z)
        {
            const double n0 = -0.0005390429110190785;
            const double n1 = -2.8398759004727723E-07;
            const double n2 = 8.994651148922914E-07;
            const double n3 = 2.2934585926592085E-08;
            const double n4 = 2.2556144486350015E-10;
            const double n5 = 9.478466275030226E-13;
            const double n6 = 1.3588013010892486E-15;
            const double n7 = -3.4889039339994887E-22;

            const double d0 = 1.0;
            const double d1 = 0.08457462340018994;
            const double d2 = 0.002820929847262647;
            const double d3 = 4.682929219408942E-05;
            const double d4 = 3.999688121938621E-07;
            const double d5 = 1.6180929088790448E-09;
            const double d6 = 2.315586083102596E-12;

            var n =
                FusedMultiplyAdd(
                    FusedMultiplyAdd(
                        FusedMultiplyAdd(
                            FusedMultiplyAdd(
                                FusedMultiplyAdd(
                                    FusedMultiplyAdd(
                                        FusedMultiplyAdd(n7,
                                            z, n6),
                                        z, n5),
                                    z, n4),
                                z, n3),
                            z, n2),
                        z, n1),
                    z, n0);
            var d =
                FusedMultiplyAdd(
                    FusedMultiplyAdd(
                        FusedMultiplyAdd(
                            FusedMultiplyAdd(
                                FusedMultiplyAdd(
                                    FusedMultiplyAdd(d6,
                                        z, d5),
                                    z, d4),
                                z, d3),
                            z, d2),
                        z, d1),
                    z, d0);
            return n / d;
        }

        /// <summary>
        /// Calculates the inverse error of the error function.
        /// <see href="https://en.wikipedia.org/wiki/Error_function#Inverse_functions"/>.
        /// </summary>
        /// <param name="p">
        /// The value at which to evaluate the function.
        /// </param>
        /// <returns>
        /// The value of the inverse error function evaluated at <paramref name="p"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static double ErfInv(double p)
        {
            if (p < 0d)
            {
                return -ErfInv(-p);
            }
            else if (p <= 0.5)
            {
                var v = p * (p + 10.0);
                return v * 0.08913147449493408 + v * ErfInvImplA(p);
            }
            else if (p <= 0.75)
            {
                return Sqrt(-2.0 * Log(1 - p)) / (2.249481201171875 + ErfInvImplB(0.75 - p));
            }
            else if (p < 1.0)
            {
                var v = Sqrt(-Log(1 - p));

                // p = BitDecrement(1d) => v = 6.061089058055252
                // The original implementation was written to also accomodate 80bit floating point values.
                // additional branches are retained in case some systems allow the retention of 80bit temporaries.
                if (v < 3.0)
                {
                    return 0.807220458984375 * v + ErfInvImplC(v - 1.125) * v;
                }
                else if (v < 6.0)
                {
                    return 0.9399557113647461 * v + ErfInvImplD(v - 3.0) * v;
                }
                else if (v < 18.0)
                {
                    return 0.9836282730102539 * v + ErfInvImplE(v - 6.0) * v;
                }
                else if (v < 44.0)
                {
                    return 0.9971456527709961 * v + ErfInvImplF(v - 18.0) * v;
                }
                else
                {
                    return 0.9994134902954102 * v + ErfInvImplG(v - 44.0) * v;
                }
            }
            else
            {
                return p == 1d ? double.PositiveInfinity : double.NaN;
            }
        }
    }
}
