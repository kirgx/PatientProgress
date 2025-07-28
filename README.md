# PatientProgress
Приложение для стоматологической клиники: загрузка, хранение и просмотр фотографий пациентов.

## Функциональность
- Добавление новых пациентов
- Загрузка фотографий (Drag & Drop и File Picker)
- Хранение в /images/{patientId}/
- Просмотр загруженных изображений

##  Технологии
- WPF (.NET 8, C# 12)
- Entity Framework Core + SQLite
- ImageSharp
- Serilog
- MVVM

## Запуск
1. Клонируйте репозиторий
2. Выполните миграции:
   ```bash
   dotnet ef database update
   
- [Оценка времени выполнения (timing.md)](./docs/timing.md)
- [Вопросы к PM и TL (questions.md)](./docs/questions.md)
