# TeamServices
🛠 Программный инструментарий для Scrum-команды.

### Что нового?

  - Добавлена страница с подробной историей отчётов.
  
### Инструкция по установке

Выполните скрипт наката источников на свой MS SQL Server, предварительно указав необходимую БД.

>**~\TeamServices\mssql\Scripts\deploy.sql**

```sh
USE <<SERVERNAME>>
GO
```

Необходимо изменить строку подключения в *~\TeamServices\dotnet\WebApplication\appsettings.json*

```sh
"DefaultConnection": "Data Source=<<SQL Server>>; Initial Catalog=<<Data Base>>; Integrated Security=False; User ID=<<UserName>>;Password=<<Password>>;"
```

### Инструкция по откату

Выполните скрипт отката источников.
>**~\TeamServices\mssql\Scripts\rollback.sql**

###Что дальше?

  - Создание сервиса новостей команды.

License
----

MIT
