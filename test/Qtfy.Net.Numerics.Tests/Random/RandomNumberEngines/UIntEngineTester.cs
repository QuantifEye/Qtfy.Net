// <copyright file="UIntEngineTester.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests.Random.RandomNumberEngines
{
    using System.Linq;
    using NUnit.Framework;
    using Qtfy.Net.Numerics.Random.RandomNumberEngines;
    using Qtfy.Net.Numerics.Random.SeedSequences;

    public class UIntEngineTester : EngineTester<MersenneTwister32Bit19937>
    {
        protected override MersenneTwister32Bit19937 GetEngine()
        {
            var ss = new SeedSequence(1, 2, 3);
            return new MersenneTwister32Bit19937(ss);
        }

        [Test]
        public void TestULongMax()
        {
            var expected = new[]
            {
                7348181598068725948UL,
                2702344000030125349UL,
                11375148292589950588UL,
                18207285961861342652UL,
                3773165389014961973UL,
                10191580196664936082UL,
                6752009953712574357UL,
                6218369684835154984UL,
                14121094415488446727UL,
                4604238859425825965UL,
                15396486439930459784UL,
                3119433781204442948UL,
                1109824652235264138UL,
                6409718533463238233UL,
                10208140272300551294UL,
                9339690911086437250UL,
                13828573667309943552UL,
                302343616819709665UL,
                787386895844678098UL,
                16176951034750438675UL,
                8793023328021258836UL,
                11734878215962017651UL,
                1890568995285127UL,
                10861772371055007636UL,
                15537961840005424265UL,
            };

            this.Compare(expected, mt => mt.NextULong());
            this.Compare(expected, mt => mt.NextULong(ulong.MaxValue));
        }

        [Test]
        public void TestULongZero()
        {
            this.TestULong(Enumerable.Repeat(0UL, 100).ToArray(), 0);
        }

        [Test]
        public void TestULongOne()
        {
            var expected = new[]
            {
                0UL,
                0UL,
                0UL,
                1UL,
                1UL,
                1UL,
                1UL,
                1UL,
                0UL,
                0UL,
                1UL,
                0UL,
                0UL,
                0UL,
                0UL,
                0UL,
                1UL,
                1UL,
                0UL,
                0UL,
                1UL,
                1UL,
                0UL,
                0UL,
                0UL,
            };

            this.TestULong(expected, 1);
        }

        [Test]
        public void TestULongSmall()
        {
            var expected = new[]
            {
                3UL,
                1UL,
                1UL,
                7UL,
                4UL,
                4UL,
                7UL,
                5UL,
                1UL,
                1UL,
                4UL,
                0UL,
                2UL,
                3UL,
                2UL,
                1UL,
                6UL,
                6UL,
                1UL,
                1UL,
                6UL,
                6UL,
                1UL,
                0UL,
                0UL,
            };

            this.TestULong(expected, 7);
        }

        [Test]
        public void TestULongLarge()
        {
            var expected = new[]
            {
                1049740228898773180UL,
                386049144951942437UL,
                1625021185718163068UL,
                1818957541663373806UL,
                360652254508729393UL,
                104166878489081159UL,
                1265534049612207688UL,
                521461951314277517UL,
                2253767785952152281UL,
                464524923082854103UL,
                2153221767983428735UL,
                298981598675002276UL,
                323789296650937611UL,
                2072315275950856574UL,
                286518357117452134UL,
                939337839372990075UL,
                2041597534399505386UL,
                112483843849492946UL,
                2050426627632338977UL,
                333834715295033744UL,
                585578443887261558UL,
                1043675367534806124UL,
                987045975717703160UL,
                731880887429059322UL,
                173861415430571833UL,
            };

            const ulong value = (1UL << 61) - 7U;
            this.TestULong(expected, value);
        }

        [Test]
        public void TestULongVeryLarge()
        {
            var expected = new[]
            {
                7348181598068725948UL,
                2702344000030125349UL,
                11375148292589950588UL,
                18207285961861342652UL,
                3773165389014961973UL,
                10191580196664936082UL,
                6752009953712574357UL,
                6218369684835154984UL,
                14121094415488446727UL,
                4604238859425825965UL,
                15396486439930459784UL,
                3119433781204442948UL,
                1109824652235264138UL,
                6409718533463238233UL,
                10208140272300551294UL,
                9339690911086437250UL,
                13828573667309943552UL,
                302343616819709665UL,
                787386895844678098UL,
                16176951034750438675UL,
                8793023328021258836UL,
                11734878215962017651UL,
                1890568995285127UL,
                10861772371055007636UL,
                15537961840005424265UL,
            };
            this.TestULong(expected, ulong.MaxValue - 7U);
        }

        [Test]
        public void TestUIntMax()
        {
            var expected = new[]
            {
                1710881851U,
                703781052U,
                629188492U,
                3870567717U,
                2648483098U,
                2671187580U,
                4239214109U,
                2964563388U,
                878508526U,
                587796277U,
                2372912177U,
                169772690U,
                1572074823U,
                2062585749U,
                1447827016U,
                849886248U,
                3287823501U,
                3673223431U,
                1072007897U,
                757089453U,
                3584773847U,
                3509352072U,
                726299775U,
                487284548U,
                258401188U,
            };

            this.Compare(expected, mt => mt.NextUInt());
            this.TestUInt(expected, uint.MaxValue);
        }

        [Test]
        public void TestUIntZero()
        {
            var expected = new[]
            {
                0U,
                0U,
                0U,
            };

            this.TestUInt(expected, 0);
        }

        [Test]
        public void TestUIntOne()
        {
            var expected = new[]
            {
                0U,
                0U,
                0U,
                1U,
                1U,
                1U,
                1U,
                1U,
                0U,
                0U,
                1U,
                0U,
                0U,
                0U,
                0U,
                0U,
                1U,
                1U,
                0U,
                0U,
                1U,
                1U,
                0U,
                0U,
                0U,
            };

            this.TestUInt(expected, 1);
        }

        [Test]
        public void TestUIntLarge()
        {
            var expected = new[]
            {
                1710881851U,
                703781052U,
                629188492U,
                3870567717U,
                2648483098U,
                2671187580U,
                4239214109U,
                2964563388U,
                878508526U,
                587796277U,
                2372912177U,
                169772690U,
                1572074823U,
                2062585749U,
                1447827016U,
                849886248U,
                3287823501U,
                3673223431U,
                1072007897U,
                757089453U,
                3584773847U,
                3509352072U,
                726299775U,
                487284548U,
                258401188U,
            };

            this.TestUInt(expected, uint.MaxValue - 2U);
        }

        [Test]
        public void TestUIntSmall()
        {
            var expected = new[]
            {
                3U,
                1U,
                1U,
                7U,
                4U,
                4U,
                7U,
                5U,
                1U,
                1U,
                4U,
                0U,
                2U,
                3U,
                2U,
                1U,
                6U,
                6U,
                1U,
                1U,
                6U,
                6U,
                1U,
                0U,
                0U,
            };

            this.TestUInt(expected, 7);
        }
    }
}
