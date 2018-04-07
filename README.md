# AspNetCore2

## Core (backend)

### Установка и настройка

1. Выполняем команду `docker-compose up --build -d`
2. Заходим в контейнер `docker exec -ti aspnetcore2_core_1 /bin/bash`
3. Выполняем миграцию `dotnet ef database update` (строка подключения appsettings.json)
4. Выходим из контейнера `exit`

### Учетные записи

* root - Pa$$w0rd
* nobody - Pa$$w0rd
* anonymous - Pa$$w0rd

### Повседневная работа

Для разработки используйте Visual Studio IDE. Чтобы собрать решение нужно пересобрать образы докер.

## Frontend

### Установка и настройка

1. Выполняем команду `docker-compose up --build -d` (если не выполнили раньше)
2. Заходим в контейнер `docker exec -ti aspnetcore2_frontend_1 /bin/bash`
3. Устанавливаем пакеты `npm install`
4. Выходим из контейнера `exit`

### Повседневная работа

Выполняйте `npm run generate` в контейнере фронта чтобы увидеть ваши изменения.
