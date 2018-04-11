# AspNetCore2

## Установка и настройка

### Docker

Или режим тестера. Можно посмотреть что все запустилось и работает, но не подходит для разработки.

Вам понадобиться:

* Docker https://www.docker.com/

Step by step:

1. Переходим в папку `cd .../AspNetCore2` 
2. Выполняем команду `docker-compose up --build -d` 

### Локально

Вам понадобиться:

* .NET Core SDK https://www.microsoft.com/net/download/
* Node.js https://nodejs.org/

Step by step:

1. Переходим в папку `cd .../AspNetCore2/core` 
2. Устанавливаем зависимости `npm install` и `dotnet restore`
3. Запускаем `dotnet run`

## Повседневная работа

Для разработки рекомендую использовать Visual Studio Code.
