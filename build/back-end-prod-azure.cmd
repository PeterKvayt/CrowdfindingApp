pushd ..

dotnet publish --configuration Release --output ./deploy/prod/azure/back-end

pause