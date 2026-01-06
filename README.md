# Qtfy.Net

We are currently working toward version 1.0.0. of the library

<!-- 
## Build
<table>
	<tr>
		 <th colspan="2">Build Status.</th>
 	</tr>
 	<tr>
  		<td>master</td>
      <td><img src="https://travis-ci.com/QuantifEye/Qtfy.Net.Numerics.svg?token=4GppM9ERgowDjXBKpuH5&branch=master" alt=""/></td>
 	</tr>
	<tr>
  		<td>dev</td>
      <td><img src="https://travis-ci.com/QuantifEye/Qtfy.Net.Numerics.svg?token=4GppM9ERgowDjXBKpuH5&branch=dev" alt=""/></td>
 	</tr>
</table> -->

## Development
### Prerequisites
- .NET SDK 10.x (see `./global.json` for the required SDK version)
- dotnet-script (C# script runner):
  - First time: `dotnet tool install -g dotnet-script`
  - Updates: `dotnet tool update -g dotnet-script`
  - If `dotnet script` is not recognized, add the global tools path to `PATH`:
    - macOS/Linux: `$HOME/.dotnet/tools`
    - Windows: `%USERPROFILE%\.dotnet\tools`

### Build and Test (all platforms)
Run these commands from the repository root.
1. Optional: install or update coverage tools to the latest stable versions:
   - `dotnet script build/install_global_tools.csx`
2. Build and test:
   - `dotnet build`
   - `dotnet test`
   - `dotnet test test/Qtfy.Numerics.Tests.BigRationals/Qtfy.Numerics.Tests.BigRationals.csproj`

### Coverage
This requires three global tools `coverlet.console`, `dotnet-reportgenerator-globaltool`, and `dotnet-script`. The install script updates the coverage tools; `dotnet-script` is updated separately.

To run tests and generate coverage report, run `dotnet script build/coverage.csx`. This will delete the contents of `./coverage`, recreate `./coverage` and populate it with the coverage output.
Coverage runs for `Qtfy.Numerics.Tests` and `Qtfy.Numerics.Tests.BigRationals`.

To view a human readable coverage report, open `./coverage/coverage.cobertura.xml.site.site/index.html` with a internet browser.
