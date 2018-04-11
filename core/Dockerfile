FROM registry.tech.bank24.int/artur.sokolov/aspnet-core:2.0.6

WORKDIR /var/www/html
COPY . .

RUN rm -rf /var/www/html/bin /var/www/html/obj
RUN dotnet publish -c Release
ENTRYPOINT ["dotnet", "/var/www/html/bin/Release/netcoreapp2.0/Tochka.dll"]
