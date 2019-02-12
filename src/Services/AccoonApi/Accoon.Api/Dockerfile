FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY src/Services/AccoonApi/Accoon.Api/Accoon.Api.csproj src/Services/AccoonApi/Accoon.Api/
RUN dotnet restore src/Services/AccoonApi/Accoon.Api/Accoon.Api.csproj
COPY . .
WORKDIR /src/src/Services/AccoonApi/Accoon.Api
RUN dotnet build Accoon.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Accoon.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Accoon.Api.dll"]
