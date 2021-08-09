#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Aluraflix.API/Aluraflix.API.csproj", "Aluraflix.API/"]
RUN dotnet restore "Aluraflix.API/Aluraflix.API.csproj"
COPY . .
WORKDIR "/src/Aluraflix.API"
RUN dotnet build "Aluraflix.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Aluraflix.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Aluraflix.API.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Aluraflix.API.dll
