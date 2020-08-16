push-location ../../

dotnet tool restore

dotnet paket install

dotnet paket restore

dotnet build src/gaspra.slackapi.sln

pop-location

dotnet build gaspra.slackapi.example.sln