#!/bin/bash

set -e

declare -r DOTNETVERSION=net5.0
declare -r CONFIGURATION=Debug
declare -r PROJECTNAME=Qtfy.Net.Numerics.Tests

declare -r ROOT=$(realpath $(dirname $0)/..)
declare -r COVERAGEFOLDER=$ROOT/coverage
declare -r PROJECTFOLDER=$ROOT/test/$PROJECTNAME
declare -r TESTCSPROJ=$PROJECTFOLDER/$PROJECTNAME.csproj
declare -r TESTDLL=$PROJECTFOLDER/bin/$CONFIGURATION/$DOTNETVERSION/$BINFOLDER/$PROJECTNAME.dll
declare -r COVERAGEFILE=$COVERAGEFOLDER/coverage.cobertura.xml
declare -r COVERAGESITE=${COVERAGEFILE}.site

rm -rf $COVERAGEFOLDER
mkdir $COVERAGEFOLDER

# clean so that all warnings appear
dotnet clean

dotnet test -c $CONFIGURATION
coverlet $TESTDLL --target "dotnet" --targetargs "test $TESTCSPROJ --no-build" --output $COVERAGEFILE  --format cobertura
reportgenerator "-reports:${COVERAGEFILE}" "-targetdir:${COVERAGESITE}.site" -reporttypes:html
