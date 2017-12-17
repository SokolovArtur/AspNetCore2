# AspNetCore2

## Установка и настройка

1. Переходим в директорию и выполняем команду `docker-compose up --build -d`
2. Наблюдаем (`docker ps`) что образ core не запустился
3. Восклицаем "почему все так сложно?"

![alt text](https://pbs.twimg.com/media/Bg8l7EdIIAA_a6H.png "ухаха")

1. Устанавливаем [.NET Core SDK](https://www.microsoft.com/net/download) на локальную машину
2. Переходим в директорию core и выполняем:
- Восстановление пакетов: `dotnet restore`
- Сборка решения: `dotnet build`
- Миграции: `dotnet ef database update` (строка подключения в appsettings.Development.json)
- Публикация решения: `dotnet publish -c Release`
3. Возвращаемся в прежнюю директорию и снова запускаем `docker-compose up --build -d`
4. Заходим в контейнер `docker exec -ti aspnetcore2_core_1 /bin/bash`
5. Выполняем миграцию `dotnet ef database update` (теперь боевая БД appsettings.json)
6. Выходим из контейнера `exit`

## Повседневная работа

Вы тщательно протестировали решение на своей локальной машине - а значит, пришло время опубликовать его.

Нужно войти в контейнер `docker exec -ti aspnetcore_core_1 /bin/bash` и опубликовать `dotnet publish -c Release`

Что может быть проще?
