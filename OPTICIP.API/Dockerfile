#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.2-aspnetcore-runtime-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY ["OPTICIP.API/OPTICIP.API.csproj", "OPTICIP.API/"]
COPY ["OPTICIP.Entities/OPTICIP.Entities.csproj", "OPTICIP.Entities/"]
COPY ["OPTICIP.SeedWork/OPTICIP.SeedWork.csproj", "OPTICIP.SeedWork/"]
COPY ["OPTICIP.BusinessLogicLayer/OPTICIP.BusinessLogicLayer.csproj", "OPTICIP.BusinessLogicLayer/"]
COPY ["OPTICIP.DataAccessLayer/OPTICIP.DataAccessLayer.csproj", "OPTICIP.DataAccessLayer/"]
COPY ["OPTICIP.IContractLayer/OPTICIP.IContractLayer.csproj", "OPTICIP.IContractLayer/"]
RUN dotnet restore "OPTICIP.API/OPTICIP.API.csproj"
COPY . .
WORKDIR "/src/OPTICIP.API"
RUN dotnet build "OPTICIP.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "OPTICIP.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "OPTICIP.API.dll"]