// <copyright file="ULongEngineTester.cs" company="QuantifEye">
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

    public class ULongEngineTester : EngineTester<MersenneTwister64Bit19937>
    {
        protected override MersenneTwister64Bit19937 GetEngine()
        {
            var ss = new SeedSequence(1, 2, 3);
            return new MersenneTwister64Bit19937(ss);
        }

        [Test]
        public void TestULongMax()
        {
            var expected = new[]
            {
                1831209241179374162UL,
                4398843623863442686UL,
                2280222209083243558UL,
                4510746540251130221UL,
                3107701279045384467UL,
                244096890834070194UL,
                1086545272889272370UL,
                1274390812019108257UL,
                12568859400304541251UL,
                18435551483339772824UL,
                6344645167688650627UL,
                6447600125556800630UL,
                6360183476137325872UL,
                5572990799971676320UL,
                6881517021134196898UL,
                13582756119889365828UL,
                5093349270420745936UL,
                9496198057143275374UL,
                184899198103798245UL,
                13139041164398136512UL,
                6152799897302857159UL,
                6928933013888610031UL,
                15099895210921580593UL,
                8510258085793372635UL,
                11389751187763450802UL,
            };

            this.Compare(expected, mt => mt.NextULong());
            this.Compare(expected, mt => mt.NextULong(ulong.MaxValue));
        }

        [Test]
        public void TestULongZero()
        {
            var expected = Enumerable.Repeat(0UL, 100).ToArray();
            this.TestULong(expected, 0);
        }

        [Test]
        public void TestULongOne()
        {
            var expected = new[]
            {
                0UL,
                0UL,
                0UL,
                0UL,
                0UL,
                0UL,
                0UL,
                0UL,
                1UL,
                1UL,
                0UL,
                0UL,
                0UL,
                0UL,
                0UL,
                1UL,
                0UL,
                1UL,
                0UL,
                1UL,
                0UL,
                0UL,
                1UL,
                0UL,
                1UL,
            };

            this.TestULong(expected, 1);
        }

        [Test]
        public void TestULongLarge()
        {
            var expected = new[]
            {
                915604620589687081UL,
                2199421811931721343UL,
                1140111104541621779UL,
                2255373270125565110UL,
                1553850639522692233UL,
                122048445417035097UL,
                543272636444636185UL,
                637195406009554128UL,
                6284429700152270625UL,
                9217775741669886412UL,
                3172322583844325313UL,
                3223800062778400315UL,
                3180091738068662936UL,
                2786495399985838160UL,
                3440758510567098449UL,
                6791378059944682914UL,
                2546674635210372968UL,
                4748099028571637687UL,
                92449599051899122UL,
                6569520582199068256UL,
                3076399948651428579UL,
                3464466506944305015UL,
                7549947605460790296UL,
                4255129042896686317UL,
                5694875593881725401UL,
            };

            this.TestULong(expected, ulong.MaxValue / 2U - 7U);
        }

        [Test]
        public void TestULongVeryLarge()
        {
            var expected = new[]
            {
                1831209241179374162UL,
                4398843623863442686UL,
                2280222209083243558UL,
                4510746540251130221UL,
                3107701279045384467UL,
                244096890834070194UL,
                1086545272889272370UL,
                1274390812019108257UL,
                12568859400304541251UL,
                18435551483339772824UL,
                6344645167688650627UL,
                6447600125556800630UL,
                6360183476137325872UL,
                5572990799971676320UL,
                6881517021134196898UL,
                13582756119889365828UL,
                5093349270420745936UL,
                9496198057143275374UL,
                184899198103798245UL,
                13139041164398136512UL,
                6152799897302857159UL,
                6928933013888610031UL,
                15099895210921580593UL,
                8510258085793372635UL,
                11389751187763450802UL,
            };

            this.TestULong(expected, ulong.MaxValue - 7U);
        }

        [Test]
        public void TestUIntMax()
        {
            var expected = new[]
            {
                426361626U,
                1024185592U,
                530905604U,
                1050240020U,
                723568089U,
                56833236U,
                252981035U,
                296717233U,
                2926415624U,
                4292361318U,
                1477227818U,
                1501198887U,
                1480845612U,
                1297563035U,
                1602228037U,
                3162481850U,
                1185887789U,
                2211005906U,
                43050199U,
                3059171412U,
                1432560360U,
                1613267933U,
                3515718321U,
                1981448868U,
                2651883100U,
            };

            this.Compare(expected, mt => mt.NextUInt());
            this.TestUInt(expected, uint.MaxValue);
        }

        [Test]
        public void TestUIntLarge()
        {
            var expected = new[]
            {
                213180812U,
                512092794U,
                265452801U,
                525120008U,
                361784043U,
                28416618U,
                126490517U,
                148358616U,
                1463207807U,
                2146180651U,
                738613906U,
                750599441U,
                740422803U,
                648781515U,
                801114016U,
                1581240919U,
                592943892U,
                1105502949U,
                21525099U,
                1529585700U,
                716280177U,
                806633964U,
                1757859154U,
                990724430U,
                1325941545U,
            };

            this.TestUInt(expected, uint.MaxValue / 2U - 7U);
        }

        [Test]
        public void TestUIntVeryLarge()
        {
            var expected = new[]
            {
                426361625U,
                1024185590U,
                530905603U,
                1050240018U,
                723568087U,
                56833236U,
                252981034U,
                296717232U,
                2926415619U,
                4292361310U,
                1477227815U,
                1501198884U,
                1480845610U,
                1297563032U,
                1602228034U,
                3162481844U,
                1185887787U,
                2211005902U,
                43050199U,
                3059171406U,
                1432560358U,
                1613267930U,
                3515718315U,
                1981448864U,
                2651883095U,
            };

            this.TestUInt(expected, uint.MaxValue - 7U);
        }
    }
}
