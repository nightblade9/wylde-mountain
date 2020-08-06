# instructions from: https://docs.sonarqube.org/latest/analysis/scan/sonarscanner-for-msbuild/
# as of writing, install the dotnet-sonarscanner tool first:
# dotnet tool install --global dotnet-sonarscanner --version 4.8.0
# coverlet: dotnet tool install --global coverlet.console

dotnet sonarscanner begin /key:"HydraPeak" /d:sonar.cs.opencover.reportsPaths=HydraPeak.Web.Tests/coverage.opencover.xml /d:sonar.coverage.exclusions="**Test*.cs"
dotnet build #test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
dotnet sonarscanner end