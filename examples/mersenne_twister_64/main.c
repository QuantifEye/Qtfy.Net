// <copyright file="main.c" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

#include <inttypes.h>
#include <stdint.h>
#include <stdio.h>
#include <stdlib.h>

#if defined(__clang__) || defined(__GNUC__)
#pragma GCC diagnostic push
#pragma GCC diagnostic ignored "-Wconversion"
#pragma GCC diagnostic ignored "-Wsign-conversion"
#pragma GCC diagnostic ignored "-Wsign-compare"
#pragma GCC diagnostic ignored "-Wshadow"
#endif

#include "mt19937-64.h"

#if defined(__clang__) || defined(__GNUC__)
#pragma GCC diagnostic pop
#endif

static void PrintUsage(const char* exe)
{
    printf("Usage: %s [seed] [count]\n", exe);
    printf("  seed   64-bit unsigned seed value (default: 5489)\n");
    printf("  count  number of values to print (default: 10)\n");
}

static int TryParseUint64(const char* text, uint64_t* value)
{
    char* end = NULL;
    unsigned long long parsed = strtoull(text, &end, 10);
    if (end == text || *end != '\0' || parsed > UINT64_MAX)
    {
        return 0;
    }

    *value = (uint64_t)parsed;
    return 1;
}

static int TryParseSize(const char* text, size_t* value)
{
    char* end = NULL;
    unsigned long long parsed = strtoull(text, &end, 10);
    if (end == text || *end != '\0' || parsed == 0ULL)
    {
        return 0;
    }

    *value = (size_t)parsed;
    return 1;
}

// This console app prints a deterministic sequence of MT19937-64 outputs.
// It uses the reference mt19937-64 implementation for test data and comparisons.
int main(int argc, char** argv)
{
    uint64_t seed = 5489ULL;
    size_t count = 10;

    if (argc > 1)
    {
        if (!TryParseUint64(argv[1], &seed))
        {
            PrintUsage(argv[0]);
            return 1;
        }
    }

    if (argc > 2)
    {
        if (!TryParseSize(argv[2], &count))
        {
            PrintUsage(argv[0]);
            return 1;
        }
    }

    if (argc > 3)
    {
        PrintUsage(argv[0]);
        return 1;
    }

    init_genrand64((unsigned long long)seed);
    for (size_t i = 0; i < count; ++i)
    {
        printf("%" PRIu64 "\n", (uint64_t)genrand64_int64());
    }

    return 0;
}
