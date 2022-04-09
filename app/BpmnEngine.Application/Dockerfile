#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["app/BpmnEngine.Application/BpmnEngine.Application.csproj", "app/BpmnEngine.Application/"]
RUN dotnet restore "app/BpmnEngine.Application/BpmnEngine.Application.csproj"
COPY . .
WORKDIR "/src/app/BpmnEngine.Application"
RUN dotnet build "BpmnEngine.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BpmnEngine.Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BpmnEngine.Application.dll"]