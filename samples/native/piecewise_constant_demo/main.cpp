// <copyright file="main.cpp" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

// This app demonstrates std::piecewise_constant_distribution<double> with explicit
// boundaries and interval weights, prints a small sample run, and then summarizes
// the empirical interval proportions to show how the weights map to probabilities.
#include <array>
#include <iomanip>
#include <iostream>
#include <random>

int main()
{
    std::array<double, 4> boundaries{0.0, 1.0, 2.0, 4.0};
    std::array<double, 3> weights{1.0, 2.0, 1.0};
    std::piecewise_constant_distribution<double> dist(
        boundaries.begin(),
        boundaries.end(),
        weights.begin());

    std::mt19937 rng(12345);

    std::cout << "Piecewise-constant demo\n";
    std::cout << "This uses weights as interval probabilities (not densities).\n\n";

    std::cout << std::fixed << std::setprecision(1);
    std::cout << "Boundaries: ";
    for (std::size_t i = 0; i < boundaries.size(); ++i)
    {
        std::cout << boundaries[i] << (i + 1 == boundaries.size() ? "\n" : ", ");
    }

    std::cout << "Weights: ";
    for (std::size_t i = 0; i < weights.size(); ++i)
    {
        std::cout << weights[i] << (i + 1 == weights.size() ? "\n" : ", ");
    }

    const double weight_sum = weights[0] + weights[1] + weights[2];
    std::cout << std::setprecision(4);
    std::cout << "Expected interval probabilities:\n";
    std::cout << "[0, 1): " << weights[0] / weight_sum << "\n";
    std::cout << "[1, 2): " << weights[1] / weight_sum << "\n";
    std::cout << "[2, 4): " << weights[2] / weight_sum << "\n";

    std::cout << "\nSampled values (seed = 12345):\n";
    std::cout << std::setprecision(6);
    for (int i = 0; i < 20; ++i)
    {
        double value = dist(rng);
        std::cout << value;
        std::cout << ((i + 1) % 5 == 0 ? "\n" : " ");
    }

    std::array<std::size_t, 3> counts{};
    for (int i = 0; i < 10000; ++i)
    {
        double value = dist(rng);
        if (value < boundaries[1])
        {
            ++counts[0];
        }
        else if (value < boundaries[2])
        {
            ++counts[1];
        }
        else
        {
            ++counts[2];
        }
    }

    const int total = 10000;
    std::cout << "\nBin counts over " << total << " samples:\n";
    std::cout << std::setprecision(4);
    std::cout << "[0, 1): " << counts[0] << " (" << counts[0] / static_cast<double>(total) << ")\n";
    std::cout << "[1, 2): " << counts[1] << " (" << counts[1] / static_cast<double>(total) << ")\n";
    std::cout << "[2, 4): " << counts[2] << " (" << counts[2] / static_cast<double>(total) << ")\n";

    return 0;
}
