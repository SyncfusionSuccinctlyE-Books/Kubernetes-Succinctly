FROM microsoft/dotnet:2.1-sdk as build-env
WORKDIR /app

# copy csproj and restore as distinct layers
COPY . ./
RUN dotnet restore

# copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# build runtime image
FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app

COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "remind-me-cron-job.dll"]