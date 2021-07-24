#! bin/bash

set -e

export ContinuousIntegrationBuild=true

dotnet test -warnaserror -c Release
