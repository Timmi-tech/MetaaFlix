FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5136

ENV ASPNETCORE_URLS=http://+:5136

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["CloudClip.API/CloudClip.API.csproj", "CloudClip.API/"]
RUN dotnet restore "CloudClip.API/CloudClip.API.csproj"
COPY . .
WORKDIR "/src/CloudClip.API"
RUN dotnet build "CloudClip.API.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "CloudClip.API.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CloudClip.API.dll"]
