#! bin/bash

set -e

export ContinuousIntegrationBuild=true

declare -r APIKEY=$1
declare -r SOURCES=$2

declare -r ROOT=$(realpath $(dirname $0)/..)
declare -r PACKAGEDIR=$ROOT/package

rm -rf $PACKAGEDIR
mkdir $PACKAGEDIR

dotnet pack --no-build -c Release -o $PACKAGEDIR
dotnet nuget push $PACKAGEDIR/*.nupkg -k $APIKEY -s $SOURCES
