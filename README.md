# shop

## Установка и настройка

Есть 2 варианта установки:

### 1. Docker

Или режим тестера. Можно посмотреть что все запустилось и работает. Не подходит для разработки.

Вам понадобиться:

* Docker https://www.docker.com/

Step by step:

1. Переходим в папку `cd .../shop` 
2. Выполняем команду `docker-compose up --build -d` 

### 2. Локально

Вам понадобиться:

* .NET Core SDK https://www.microsoft.com/net/download/
* Node.js https://nodejs.org/
* IDE (рекомендую Visual Studio Code)

Step by step:

**ASP.NET Core + Angular 5 = SSR**

1. Убеждаемся в наличии переменной среды `ASPNETCORE_Environment` со значением `Development`
* Windows 10 - SET ASPNETCORE_Environment=Development;
* Linux/macOS - ASPNETCORE_Environment=Development.
2. Переходим в папку `cd .../shop`
3. Собирем решение `dotnet build`
4. Запускаем `dotnet run`

**... или только фронт**

1. Переходим в папку `cd .../shop/ClientApp`
2. Устанавливаем пакеты `npm install`
2. Собираем webpack `npm run build`
3. Запускаем `npm run start`
