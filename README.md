# AspNetCore2

## Установка и настройка

1. Переходим в директорию правим `volumes` в docker-compose.yml
2. Выполняем команду `docker-compose up --build -d`
3. Заходим в контейнер `docker exec -ti aspnetcore2_core_1 /bin/bash`
4. Выполняем миграцию `dotnet ef database update` (строка подключения appsettings.json)
5. Выходим из контейнера `exit`

### Учетные записи

* root - Pa$$w0rd
* nobody - Pa$$w0rd
* anonymous - Pa$$w0rd

### Повседневная работа

Вы тщательно протестировали решение на своей локальной машине - а значит, пришло время опубликовать его командой `dotnet publish -c Release`.

Что может быть проще?
