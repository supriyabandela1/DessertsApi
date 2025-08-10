FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["DessertsApi/DessertsApi.csproj", "DessertsApi/"]
RUN dotnet restore "DessertsApi/DessertsApi.csproj"

# Copy the rest of the code
COPY . .
WORKDIR "/src/DessertsApi"
RUN dotnet build "DessertsApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DessertsApi.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DessertsApi.dll"]
