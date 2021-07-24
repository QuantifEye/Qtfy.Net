// <copyright file="StandardNormalSampler.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Random.Samplers
{
    using System;
    using static System.Math;

    /// <summary>
    /// The simple form box muller transform.
    /// </summary>
    public sealed class StandardNormalSampler : ISampler<double>
    {
        private readonly IRandomNumberEngine engine;

        private double spare;

        private bool hasSpare;

        /// <summary>
        /// Initializes a new instance of the <see cref="StandardNormalSampler"/> class.
        /// </summary>
        /// <param name="engine">
        /// The pseudo random engine used to generate random numbers.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="engine"/> is null.
        /// </exception>
        public StandardNormalSampler(IRandomNumberEngine engine)
        {
            this.engine = engine ?? throw new ArgumentNullException(nameof(engine));
        }

        /// <inheritdoc />
        public double GetNext()
        {
            if (this.hasSpare)
            {
                this.hasSpare = false;
                return this.spare;
            }
            else
            {
                var engine = this.engine;
                double s, u, v, logS;
                do
                {
                    u = FusedMultiplyAdd(engine.NextStandardUniform(), 2d, -1d);
                    v = FusedMultiplyAdd(engine.NextStandardUniform(), 2d, -1d);
                    s = u * u + v * v;
                }
                while (s >= 1d || u == 0d || v == 0d);

                if (s > 1e-4)
                {
                    logS = Log(s);
                }
                else
                {
                    var exp = -ILogB(Max(Abs(u), Abs(v)));
                    u = ScaleB(u, exp);
                    v = ScaleB(v, exp);
                    s = u * u + v * v;
                    logS = FusedMultiplyAdd(exp, -Constants.TwoLnTwo, Log(s));
                }

                var f = Sqrt(-2d * logS / s);
                this.spare = f * v;
                this.hasSpare = true;
                return f * u;
            }
        }

        /// <summary>
        /// Fills the provided array with independent standard normal values.
        /// </summary>
        /// <param name="buffer">
        /// Fills the buffer.
        /// </param>
        public void Fill(Span<double> buffer)
        {
            for (var i = 0; i < buffer.Length; ++i)
            {
                buffer[i] = this.GetNext();
            }
        }
    }
}
