dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
cd WyldeMountain.Web\ClientApp
npm test -- --coverage
cd ..\..
