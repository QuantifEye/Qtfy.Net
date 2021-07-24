#! bin/bash

# for packing the package locally, packaging for release requires ContinuousIntegrationBuild=true

set -e

declare -r ROOT=$(realpath $(dirname $0)/..)
declare -r PACKAGEDIR=$ROOT/packages

rm -rf $PACKAGEDIR
mkdir $PACKAGEDIR

dotnet pack -warnaserror -c Release -o $PACKAGEDIR
