#  Оценка времени выполнения

| Задача                                               | План (ч) | Факт (ч) | Комментарий                                                                  |
|------------------------------------------------------|----------|----------|------------------------------------------------------------------------------|
| Структура проекта, настройка DI, базовая архитектура | 1        | 1        | Использована структура MVVM, DI через `ServiceProvider`.                     |
| UI: форма пациентов + добавление нового              | 0.5      | 0.5      | Стандартные компоненты WPF, без сложных валидаций.                           |
| UI: окно с изображениями (drag&drop + кнопка)        | 0.5      | 0.5      | Нужна была ручная привязка drag&drop + команд.                               |
| Хранение изображений по пациенту (обработка/resize)  | 0.5      | 0.5      | ImageSharp + логика resize/safe-save.                                        |
| БД: модель, миграции, EF Core + SQLite               | 1        | 1        | Без сложных связей.                               |
| CI/CD: GitHub Actions (build+test)                   | 0.5      | 0.5      | Базовый .NET workflow + restore и build.                                     |
| Обработка ошибок + user-friendly сообщения           | 0.5      | 0.5      | Через `MessageBox` и `try-catch`, добавлены логи.                            |
| Логирование (Serilog + вывод в файл)                 | 0.5      | 0.5      | Простая интеграция, логируются операции загрузки.                            |
