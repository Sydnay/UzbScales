#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
RUN apt-get update && apt-get install -y xauth x11-xserver-utils
RUN apt-get update && apt-get install -y libfontconfig1
RUN apt-get update && apt-get install -y sqlite3
RUN export DISPLAY="\$(ip route show default | sed -n 's/.*via \([^ ]\+\).*$/\1/p'):0" >> ~/.bashrc 

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UzbScales/UzbScales.csproj", "UzbScales/"]
COPY ["BL/BL.csproj", "BL/"]
RUN dotnet restore "UzbScales/UzbScales.csproj"
COPY . .
WORKDIR "/src/UzbScales"
RUN dotnet build "UzbScales.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UzbScales.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .


#ENV $DISPLAY="\$(ip route show default | sed -n 's/.*via \([^ ]\+\).*$/\1/p'):0" 

RUN xhost +local:root


ENTRYPOINT ["dotnet", "UzbScales.dll"]
