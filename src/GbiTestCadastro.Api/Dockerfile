#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG PAT
WORKDIR /src
RUN echo "deb http://deb.debian.org/debian bullseye main contrib" >> /etc/apt/sources.list \
    && apt-get update \
    && apt-get install -y --allow-unauthenticated \
        libc6

RUN wget -qO- https://raw.githubusercontent.com/Microsoft/artifacts-credprovider/master/helpers/installcredprovider.sh | bash
ENV NUGET_CREDENTIALPROVIDER_SESSIONTOKENCACHE_ENABLED true
ENV VSS_NUGET_EXTERNAL_FEED_ENDPOINTS "{\"endpointCredentials\": [{\"endpoint\":\"https://pkgs.dev.azure.com/grupoltm/ab756afb-73b8-4a78-b655-0cc258be6bf3/_packaging/Vertem.Core/nuget/v3/index.json\", \"password\":\"${PAT}\"}]}"

COPY ["Nuget.config", "."]
COPY ["src/GbiTestCadastro.Api/GbiTestCadastro.Api.csproj", "GbiTestCadastro.Api/"]
COPY ["src/GbiTestCadastro.Application/GbiTestCadastro.Application.csproj", "GbiTestCadastro.Application/"]
COPY ["src/GbiTestCadastro.CrossCutting/GbiTestCadastro.CrossCutting.csproj", "GbiTestCadastro.CrossCutting/"]
COPY ["src/GbiTestCadastro.Domain/GbiTestCadastro.Domain.csproj", "GbiTestCadastro.Domain/"]
COPY ["src/GbiTestCadastro.Dto/GbiTestCadastro.Dto.csproj", "GbiTestCadastro.Dto/"]
COPY ["src/GbiTestCadastro.Infra/GbiTestCadastro.Infra.csproj", "GbiTestCadastro.Infra/"]

COPY . .
RUN dotnet restore "src/GbiTestCadastro.Api/GbiTestCadastro.Api.csproj" --configfile Nuget.config

FROM build AS publish
RUN dotnet publish "src/GbiTestCadastro.Api/GbiTestCadastro.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final

RUN echo "deb http://deb.debian.org/debian bullseye main contrib" >> /etc/apt/sources.list \
    && apt-get update \
    && apt-get install -y --allow-unauthenticated \
        libc6

RUN apt-get install -y --allow-unauthenticated curl

# Download and install the Tracer
RUN mkdir -p /opt/datadog \
    && mkdir -p /var/log/datadog \
    && TRACER_VERSION=$(curl -s https://api.github.com/repos/DataDog/dd-trace-dotnet/releases/latest | grep tag_name | cut -d '"' -f 4 | cut -c2-) \
    && curl -LO https://github.com/DataDog/dd-trace-dotnet/releases/download/v${TRACER_VERSION}/datadog-dotnet-apm_${TRACER_VERSION}_amd64.deb \
    && dpkg -i ./datadog-dotnet-apm_${TRACER_VERSION}_amd64.deb \
    && rm ./datadog-dotnet-apm_${TRACER_VERSION}_amd64.deb
# Enable the tracer
ENV CORECLR_ENABLE_PROFILING=1
ENV CORECLR_PROFILER={846F5F1C-F9AE-4B07-969E-05C26BC060D8}
ENV CORECLR_PROFILER_PATH=/opt/datadog/Datadog.Trace.ClrProfiler.Native.so
ENV DD_DOTNET_TRACER_HOME=/opt/datadog
ENV DD_INTEGRATIONS=/opt/datadog/integrations.json
ENV DD_LOGS_INJECTION=true

WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://*:80
ENTRYPOINT ["dotnet", "GbiTestCadastro.Api.dll"]
