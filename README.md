# Localisation-Csharp

Для установки проекта необходимо выполнить команды:

```bash
$ git clone https://github.com/Vladimir2ht/Localisation-Csharp
$ cd Localisation-Csharp
```

Запуск приложения через Docker (с логами):
```bash
$ docker compose up
```

Запуск приложения через Docker (без логов) с пересборкой образа:
```bash
$ docker-compose up -d --build
```

---

## Используемые порты

- **Frontend (Next.js)**: доступен на [http://localhost:3000](http://localhost:3000)
- **Backend (ASP.NET Core)**: доступен на [http://localhost:2000](http://localhost:2000)
- **PostgreSQL**: база данных доступна на порту `5432`

## Локальный запуск для разработки (без Docker)

1. Запустите сервер базы данных PostgreSQL локально или через контейнер, убедитесь, что параметры подключения соответствуют настройкам в `backend/appsettings.json`.
2. Перейдите в папку `backend` и выполните команду:
   ```bash
   dotnet run
   ```
   Сервер будет доступен на порту 2000.
3. Перейдите в папку `frontend` и выполните команды:
   ```bash
   npm install
   npm run dev
   ```
   Приложение будет доступно на порту 3000.

Все сервисы должны быть запущены одновременно для корректной работы приложения.