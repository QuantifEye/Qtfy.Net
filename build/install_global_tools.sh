#! bin/bash

# these tools are installed to create locally generated coverage reports
# next step is to automate coverate in ci

dotnet tool install --global --verbosity quiet coverlet.console
dotnet tool install --global --verbosity quiet dotnet-reportgenerator-globaltool
