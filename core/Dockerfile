FROM microsoft/dotnet:2-sdk

RUN apt-get update \
    && apt-get install -y --no-install-recommends git zip mc

# set up network
ENV ASPNETCORE_URLS http://+:5000

# app installation
WORKDIR /var/www/core
COPY . .
RUN dotnet restore

# RUN rm -rf /var/www/core/bin /var/www/core/obj
# RUN dotnet publish -c Release
# ENTRYPOINT ["dotnet", "watch", "/var/www/core/bin/Release/netcoreapp2.0/Tochka.dll"]
