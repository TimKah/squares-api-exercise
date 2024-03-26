FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Squares.API/Squares.API.csproj", "Squares.API/"]
COPY ["Squares/Squares.csproj", "Squares/"]
COPY ["Squares.Services/Squares.Services.csproj", "Squares.Services/"]
COPY ["Squares.Database/Squares.Database.csproj", "Squares.Database/"]
COPY ["Squares.Services.Abstractions/Squares.Services.Abstractions.csproj", "Squares.Services.Abstractions/"]
COPY ["Squares.Database.Memory/Squares.Database.Memory.csproj", "Squares.Database.Memory/"]
COPY ["Squares.DTO/Squares.DTO.csproj", "Squares.DTO/"]
RUN dotnet restore "Squares.API/Squares.API.csproj"
COPY . .
WORKDIR "/src/Squares.API"
RUN dotnet build "Squares.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Squares.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Squares.API.dll"]
