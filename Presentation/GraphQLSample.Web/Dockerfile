#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Presentation/GraphQLSample.Web/GraphQLSample.Web.csproj", "Presentation/GraphQLSample.Web/"]
COPY ["Core/Application/GraphQLSample.Core.Application.IoC/GraphQLSample.Core.Application.IoC.csproj", "Core/Application/GraphQLSample.Core.Application.IoC/"]
COPY ["Core/Infrastructure/GraphQLSample.Core.Infrastructure.DataLayer/GraphQLSample.Core.Infrastructure.DataLayer.csproj", "Core/Infrastructure/GraphQLSample.Core.Infrastructure.DataLayer/"]
COPY ["Core/Infrastructure/GraphQLSample.Core.Infrastructure.Domain.Config/GraphQLSample.Core.Infrastructure.Domain.Config.csproj", "Core/Infrastructure/GraphQLSample.Core.Infrastructure.Domain.Config/"]
COPY ["Core/Infrastructure/GraphQLSample.Core.Infrastructure.Domain/GraphQLSample.Core.Infrastructure.Domain.csproj", "Core/Infrastructure/GraphQLSample.Core.Infrastructure.Domain/"]
COPY ["Shared/GraphQLSample.Shared.Common/GraphQLSample.Shared.Common.csproj", "Shared/GraphQLSample.Shared.Common/"]
COPY ["Core/Application/GraphQLSample.Core.Infrastructure.Mapper/GraphQLSample.Core.Infrastructure.Mapper.csproj", "Core/Application/GraphQLSample.Core.Infrastructure.Mapper/"]
COPY ["Core/Application/GraphQLSample.Core.Application.Services/GraphQLSample.Core.Application.Services.csproj", "Core/Application/GraphQLSample.Core.Application.Services/"]
RUN dotnet restore "Presentation/GraphQLSample.Web/GraphQLSample.Web.csproj"
COPY . .
WORKDIR "/src/Presentation/GraphQLSample.Web"
RUN dotnet build "GraphQLSample.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GraphQLSample.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GraphQLSample.Web.dll"]