// <copyright file="SpecialFunctions.Erf.cs" company="QuantifEye">
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
        /// Help function for approximating the error function on a certain interval, see Erf function below.
        /// </summary>
        /// <param name="z">
        /// The point at which to evaluate the help function.
        /// </param>
        /// <returns>
        /// A double value used in the Erf function.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static double ErfImplA(double z)
        {
            const double n0 = 0.0033791670955125737;
            const double n1 = -0.0007369565304816795;
            const double n2 = -0.3747323373929196;
            const double n3 = 0.08174424487335873;
            const double n4 = -0.04210893199365486;
            const double n5 = 0.007016570951209575;
            const double n6 = -0.004950912559824351;
            const double n7 = 0.0008716465990379225;

            const double d0 = 1.0;
            const double d1 = -0.21808821808792464;
            const double d2 = 0.4125429727254421;
            const double d3 = -0.08418911478731067;
            const double d4 = 0.06553388564002416;
            const double d5 = -0.012001960445494177;
            const double d6 = 0.00408165558926174;
            const double d7 = -0.0006159007215577697;

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
        /// Help function for approximating the error function on a certain interval, see Erf function below.
        /// </summary>
        /// <param name="z">
        /// The point at which to evaluate the help function.
        /// </param>
        /// <returns>
        /// A double value used in the Erf function.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static double ErfImplB(double z)
        {
            const double n0 = -0.03617903907182625;
            const double n1 = 0.2922518834448827;
            const double n2 = 0.2814470417976045;
            const double n3 = 0.12561020886276694;
            const double n4 = 0.027413502826893053;
            const double n5 = 0.0025083967216806575;

            const double d0 = 1.0;
            const double d1 = 1.8545005897903486;
            const double d2 = 1.4357580303783142;
            const double d3 = 0.5828276587530365;
            const double d4 = 0.12481047693294975;
            const double d5 = 0.011372417654635328;

            var n =
                FusedMultiplyAdd(
                    FusedMultiplyAdd(
                        FusedMultiplyAdd(
                            FusedMultiplyAdd(
                                FusedMultiplyAdd(n5,
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
                                FusedMultiplyAdd(d5,
                                    z, d4),
                                z, d3),
                            z, d2),
                        z, d1),
                    z, d0);
            return n / d;
        }

        /// <summary>
        /// Help function for approximating the error function on a certain interval, see Erf function below.
        /// </summary>
        /// <param name="z">
        /// The point at which to evaluate the help function.
        /// </param>
        /// <returns>
        /// A double value used in the Erf function.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static double ErfImplC(double z)
        {
            const double n0 = -0.03978768926111369;
            const double n1 = 0.1531652124678783;
            const double n2 = 0.19126029560093624;
            const double n3 = 0.10276327061989304;
            const double n4 = 0.029637090615738836;
            const double n5 = 0.004609348678027549;
            const double n6 = 0.0003076078203486802;

            const double d0 = 1.0;
            const double d1 = 1.955200729876277;
            const double d2 = 1.6476231719938486;
            const double d3 = 0.7682386070221262;
            const double d4 = 0.20979318593650978;
            const double d5 = 0.031956931689991336;
            const double d6 = 0.0021336316089578537;

            var n =
                FusedMultiplyAdd(
                    FusedMultiplyAdd(
                        FusedMultiplyAdd(
                            FusedMultiplyAdd(
                                FusedMultiplyAdd(
                                    FusedMultiplyAdd(n6,
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
        /// Help function for approximating the error function on a certain interval, see Erf function below.
        /// </summary>
        /// <param name="z">
        /// The point at which to evaluate the help function.
        /// </param>
        /// <returns>
        /// A double value used in the Erf function.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static double ErfImplD(double z)
        {
            const double n0 = -0.030083856055794972;
            const double n1 = 0.05385788298444545;
            const double n2 = 0.07262115416519142;
            const double n3 = 0.036762846988804936;
            const double n4 = 0.009646290155725275;
            const double n5 = 0.0013345348007529107;
            const double n6 = 7.780875997825043E-05;

            const double d0 = 1.0;
            const double d1 = 1.7596709814716753;
            const double d2 = 1.3288357143796112;
            const double d3 = 0.5525285965087576;
            const double d4 = 0.13379305694133287;
            const double d5 = 0.017950964517628076;
            const double d6 = 0.0010471244001993736;
            const double d7 = -1.0664038182035734E-08;

            var n =
                FusedMultiplyAdd(
                    FusedMultiplyAdd(
                        FusedMultiplyAdd(
                            FusedMultiplyAdd(
                                FusedMultiplyAdd(
                                    FusedMultiplyAdd(n6,
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
        /// Help function for approximating the error function on a certain interval, see Erf function below.
        /// </summary>
        /// <param name="z">
        /// The point at which to evaluate the help function.
        /// </param>
        /// <returns>
        /// A double value used in the Erf function.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static double ErfImplE(double z)
        {
            const double n0 = -0.011790757013722784;
            const double n1 = 0.01426213209053881;
            const double n2 = 0.020223443590296084;
            const double n3 = 0.009306682999904321;
            const double n4 = 0.00213357802422066;
            const double n5 = 0.00025022987386460105;
            const double n6 = 1.2053491221958819E-05;

            const double d0 = 1.0;
            const double d1 = 1.5037622520362048;
            const double d2 = 0.9653977862044629;
            const double d3 = 0.3392652304767967;
            const double d4 = 0.06897406495415698;
            const double d5 = 0.007710602624917683;
            const double d6 = 0.0003714211015310693;

            var n =
                FusedMultiplyAdd(
                    FusedMultiplyAdd(
                        FusedMultiplyAdd(
                            FusedMultiplyAdd(
                                FusedMultiplyAdd(
                                    FusedMultiplyAdd(n6,
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
        /// Help function for approximating the error function on a certain interval, see Erf function below.
        /// </summary>
        /// <param name="z">
        /// The point at which to evaluate the help function.
        /// </param>
        /// <returns>
        /// A double value used in the Erf function.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static double ErfImplF(double z)
        {
            const double n0 = -0.005469547955387293;
            const double n1 = 0.004041902787317071;
            const double n2 = 0.005496336955316117;
            const double n3 = 0.002126164726039454;
            const double n4 = 0.0003949840144950839;
            const double n5 = 3.655654770644424E-05;
            const double n6 = 1.3548589710993232E-06;

            const double d0 = 1.0;
            const double d1 = 1.2101969777363077;
            const double d2 = 0.6209146682211439;
            const double d3 = 0.17303843066114277;
            const double d4 = 0.027655081377343203;
            const double d5 = 0.0024062597442430973;
            const double d6 = 8.918118172513365E-05;
            const double d7 = -4.655288362833827E-12;

            var n =
                FusedMultiplyAdd(
                    FusedMultiplyAdd(
                        FusedMultiplyAdd(
                            FusedMultiplyAdd(
                                FusedMultiplyAdd(
                                    FusedMultiplyAdd(n6,
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
        /// Help function for approximating the error function on a certain interval, see Erf function below.
        /// </summary>
        /// <param name="z">
        /// The point at which to evaluate the help function.
        /// </param>
        /// <returns>
        /// A double value used in the Erf function.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static double ErfImplG(double z)
        {
            const double n0 = -0.0027072253590577837;
            const double n1 = 0.00131875634250294;
            const double n2 = 0.0011992593326100233;
            const double n3 = 0.00027849619811344664;
            const double n4 = 2.6782298821833186E-05;
            const double n5 = 9.230436723150282E-07;

            const double d0 = 1.0;
            const double d1 = 0.8146328085431416;
            const double d2 = 0.26890166585629954;
            const double d3 = 0.044987721610304114;
            const double d4 = 0.0038175966332024847;
            const double d5 = 0.00013157189788859692;
            const double d6 = 4.048153596757641E-12;

            var n =
                FusedMultiplyAdd(
                    FusedMultiplyAdd(
                        FusedMultiplyAdd(
                            FusedMultiplyAdd(
                                FusedMultiplyAdd(n5,
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
        /// Help function for approximating the error function on a certain interval, see Erf function below.
        /// </summary>
        /// <param name="z">
        /// The point at which to evaluate the help function.
        /// </param>
        /// <returns>
        /// A double value used in the Erf function.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static double ErfImplH(double z)
        {
            const double n0 = -0.001099467206917422;
            const double n1 = 0.00040642544275042267;
            const double n2 = 0.0002744994894169007;
            const double n3 = 4.652937706466594E-05;
            const double n4 = 3.2095542539576746E-06;
            const double n5 = 7.782860181450209E-08;

            const double d0 = 1.0;
            const double d1 = 0.5881737106118461;
            const double d2 = 0.13936333128940975;
            const double d3 = 0.016632934041708368;
            const double d4 = 0.0010002392131023491;
            const double d5 = 2.4254837521587224E-05;

            var n =
                FusedMultiplyAdd(
                    FusedMultiplyAdd(
                        FusedMultiplyAdd(
                            FusedMultiplyAdd(
                                FusedMultiplyAdd(n5,
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
                                FusedMultiplyAdd(d5,
                                    z, d4),
                                z, d3),
                            z, d2),
                        z, d1),
                    z, d0);
            return n / d;
        }

        /// <summary>
        /// Help function for approximating the error function on a certain interval, see Erf function below.
        /// </summary>
        /// <param name="z">
        /// The point at which to evaluate the help function.
        /// </param>
        /// <returns>
        /// A double value used in the Erf function.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static double ErfImplI(double z)
        {
            const double n0 = -0.0005690799360109496;
            const double n1 = 0.00016949854037376225;
            const double n2 = 5.184723545811009E-05;
            const double n3 = 3.8281931223192885E-06;
            const double n4 = 8.249899312818944E-08;

            const double d0 = 1.0;
            const double d1 = 0.33963725005113937;
            const double d2 = 0.04347264787031066;
            const double d3 = 0.002485493352246371;
            const double d4 = 5.356333053371529E-05;
            const double d5 = -1.1749094440545958E-13;

            var n =
                FusedMultiplyAdd(
                    FusedMultiplyAdd(
                        FusedMultiplyAdd(
                            FusedMultiplyAdd(n4,
                                z, n3),
                            z, n2),
                        z, n1),
                    z, n0);
            var d =
                FusedMultiplyAdd(
                    FusedMultiplyAdd(
                        FusedMultiplyAdd(
                            FusedMultiplyAdd(
                                FusedMultiplyAdd(d5,
                                    z, d4),
                                z, d3),
                            z, d2),
                        z, d1),
                    z, d0);
            return n / d;
        }

        /// <summary>
        /// Help function for approximating the error function on a certain interval, see Erf function below.
        /// </summary>
        /// <param name="z">
        /// The point at which to evaluate the help function.
        /// </param>
        /// <returns>
        /// A double value used in the Erf function.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static double ErfImplJ(double z)
        {
            const double n0 = -0.00024131359948399134;
            const double n1 = 5.742249752025015E-05;
            const double n2 = 1.1599896292738377E-05;
            const double n3 = 5.817621344025938E-07;
            const double n4 = 8.539715550856736E-09;

            const double d0 = 1.0;
            const double d1 = 0.23304413829968784;
            const double d2 = 0.02041869405464403;
            const double d3 = 0.0007971856475643983;
            const double d4 = 1.1701928167017232E-05;

            var n =
                FusedMultiplyAdd(
                    FusedMultiplyAdd(
                        FusedMultiplyAdd(
                            FusedMultiplyAdd(n4,
                                z, n3),
                            z, n2),
                        z, n1),
                    z, n0);
            var d =
                FusedMultiplyAdd(
                    FusedMultiplyAdd(
                        FusedMultiplyAdd(
                            FusedMultiplyAdd(d4,
                                z, d3),
                            z, d2),
                        z, d1),
                    z, d0);
            return n / d;
        }

        /// <summary>
        /// Help function for approximating the error function on a certain interval, see Erf function below.
        /// </summary>
        /// <param name="z">
        /// The point at which to evaluate the help function.
        /// </param>
        /// <returns>
        /// A double value used in the Erf function.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static double ErfImplK(double z)
        {
            const double n0 = -0.00014667469927776036;
            const double n1 = 1.6266655211228053E-05;
            const double n2 = 2.6911624850916523E-06;
            const double n3 = 9.79584479468092E-08;
            const double n4 = 1.0199464762572346E-09;

            const double d0 = 1.0;
            const double d1 = 0.16590781294484722;
            const double d2 = 0.010336171619150588;
            const double d3 = 0.0002865930263738684;
            const double d4 = 2.9840157084090034E-06;

            var n =
                FusedMultiplyAdd(
                    FusedMultiplyAdd(
                        FusedMultiplyAdd(
                            FusedMultiplyAdd(n4,
                                z, n3),
                            z, n2),
                        z, n1),
                    z, n0);
            var d =
                FusedMultiplyAdd(
                    FusedMultiplyAdd(
                        FusedMultiplyAdd(
                            FusedMultiplyAdd(d4,
                                z, d3),
                            z, d2),
                        z, d1),
                    z, d0);
            return n / d;
        }

        /// <summary>
        /// Help function for approximating the error function on a certain interval, see Erf function below.
        /// </summary>
        /// <param name="z">
        /// The point at which to evaluate the help function.
        /// </param>
        /// <returns>
        /// A double value used in the Erf function.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static double ErfImplL(double z)
        {
            const double n0 = -5.839057976297718E-05;
            const double n1 = 4.125103251054962E-06;
            const double n2 = 4.3179092242025094E-07;
            const double n3 = 9.933651555900132E-09;
            const double n4 = 6.534805100201047E-11;

            const double d0 = 1.0;
            const double d1 = 0.10507708607203992;
            const double d2 = 0.004142784286754756;
            const double d3 = 7.263387546445238E-05;
            const double d4 = 4.778184710473988E-07;

            var n =
                FusedMultiplyAdd(
                    FusedMultiplyAdd(
                        FusedMultiplyAdd(
                            FusedMultiplyAdd(n4,
                                z, n3),
                            z, n2),
                        z, n1),
                    z, n0);
            var d =
                FusedMultiplyAdd(
                    FusedMultiplyAdd(
                        FusedMultiplyAdd(
                            FusedMultiplyAdd(d4,
                                z, d3),
                            z, d2),
                        z, d1),
                    z, d0);
            return n / d;
        }

        /// <summary>
        /// Help function for approximating the error function on a certain interval, see Erf function below.
        /// </summary>
        /// <param name="z">
        /// The point at which to evaluate the help function.
        /// </param>
        /// <returns>
        /// A double value used in the Erf function.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static double ErfImplM(double z)
        {
            const double n0 = -1.9645779760922958E-05;
            const double n1 = 1.572438876668007E-06;
            const double n2 = 5.439025111927009E-08;
            const double n3 = 3.174724923691177E-10;

            const double d0 = 1.0;
            const double d1 = 0.05280398924095763;
            const double d2 = 0.0009268760691517533;
            const double d3 = 5.410117232266303E-06;
            const double d4 = 5.350938458036424E-16;

            var n =
                FusedMultiplyAdd(
                    FusedMultiplyAdd(
                        FusedMultiplyAdd(n3,
                            z, n2),
                        z, n1),
                    z, n0);
            var d =
                FusedMultiplyAdd(
                    FusedMultiplyAdd(
                        FusedMultiplyAdd(
                            FusedMultiplyAdd(d4,
                                z, d3),
                            z, d2),
                        z, d1),
                    z, d0);
            return n / d;
        }

        /// <summary>
        /// Help function for approximating the error function on a certain interval, see Erf function below.
        /// </summary>
        /// <param name="z">
        /// The point at which to evaluate the help function.
        /// </param>
        /// <returns>
        /// A double value used in the Erf function.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static double ErfImplN(double z)
        {
            const double n0 = -7.892247039787227E-06;
            const double n1 = 6.22088451660987E-07;
            const double n2 = 1.457284456768824E-08;
            const double n3 = 6.037155055427153E-11;

            const double d0 = 1.0;
            const double d1 = 0.03753288463562937;
            const double d2 = 0.0004679195359746253;
            const double d3 = 1.9384703927584565E-06;

            var n =
                FusedMultiplyAdd(
                    FusedMultiplyAdd(
                        FusedMultiplyAdd(n3,
                            z, n2),
                        z, n1),
                    z, n0);
            var d =
                FusedMultiplyAdd(
                    FusedMultiplyAdd(
                        FusedMultiplyAdd(d3,
                            z, d2),
                        z, d1),
                    z, d0);
            return n / d;
        }

        /// <summary>
        /// Calculates the error function (also called the Gauss error function).
        /// <see href="https://en.wikipedia.org/wiki/Error_function"/>.
        /// </summary>
        /// <param name="x">
        /// The point at which to evaluate the error function.
        /// </param>
        /// <returns>
        /// A double in the range [-1, 1].
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static double Erf(double x)
        {
            if (x < 0.0)
            {
                return -Erf(-x);
            }
            else if (x < 0.5)
            {
                return x >= 1E-10
                    ? x * 1.125 + x * ErfImplA(x)
                    : x * 1.125 + x * 0.0033791670955125737;
            }
            else if (x < 110.0)
            {
                var r = Exp(-x * x) / x;
                if (x < 0.75)
                {
                    return 1.0 - (r * ErfImplB(x - 0.50) + r * 0.3440242111682892);
                }
                else if (x < 1.25)
                {
                    return 1.0 - (r * ErfImplC(x - 0.75) + r * 0.4199909269809723);
                }
                else if (x < 2.25)
                {
                    return 1.0 - (r * ErfImplD(x - 1.25) + r * 0.48986250162124634);
                }
                else if (x < 3.50)
                {
                    return 1.0 - (r * ErfImplE(x - 2.25) + r * 0.5317370891571045);
                }
                else if (x < 5.25)
                {
                    return 1.0 - (r * ErfImplF(x - 3.50) + r * 0.5489973425865173);
                }
                else if (x < 8.00)
                {
                    return 1.0 - (r * ErfImplG(x - 5.25) + r * 0.5571740865707397);
                }
                else if (x < 11.5)
                {
                    return 1.0 - (r * ErfImplH(x - 8.00) + r * 0.5609807968139648);
                }
                else if (x < 17.0)
                {
                    return 1.0 - (r * ErfImplI(x - 11.5) + r * 0.5626493692398071);
                }
                else if (x < 24.0)
                {
                    return 1.0 - (r * ErfImplJ(x - 17.0) + r * 0.5634598135948181);
                }
                else if (x < 38.0)
                {
                    return 1.0 - (r * ErfImplK(x - 24.0) + r * 0.5638477802276611);
                }
                else if (x < 60.0)
                {
                    return 1.0 - (r * ErfImplL(x - 38.0) + r * 0.5640528202056885);
                }
                else if (x < 85.0)
                {
                    return 1.0 - (r * ErfImplM(x - 60.0) + r * 0.5641309022903442);
                }
                else
                {
                    return 1.0 - (r * ErfImplN(x - 85.0) + r * 0.5641584396362305);
                }
            }
            else
            {
                return double.IsNaN(x) ? double.NaN : 1.0;
            }
        }
    }
}
