#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NoteQuest.CLI/NoteQuest.CLI.csproj", "NoteQuest.CLI/"]
COPY ["NoteQuest.Infrastructure/NoteQuest.Infrastructure.csproj", "NoteQuest.Infrastructure/"]
COPY ["NoteQuest.Domain/NoteQuest.Domain.csproj", "NoteQuest.Domain/"]
COPY ["NoteQuest.Application/NoteQuest.Application.csproj", "NoteQuest.Application/"]
RUN dotnet restore "NoteQuest.CLI/NoteQuest.CLI.csproj"
COPY . .
WORKDIR "/src/NoteQuest.CLI"
RUN dotnet build "NoteQuest.CLI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NoteQuest.CLI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NoteQuest.CLI.dll"]