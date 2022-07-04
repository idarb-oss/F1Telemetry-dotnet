FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Runtime.Console/Runtime.Console.csproj", "Runtime.Console/"]
RUN dotnet restore "src/Runtime.Console/Runtime.Console.csproj"
COPY . .
WORKDIR "/src/Runtime.Console"
RUN dotnet build "Runtime.Console.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Runtime.Console.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Runtime.Console.dll"]
