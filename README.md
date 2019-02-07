# TeamServices
🛠 Программный инструментарий для команды разработки.

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

Установите название вашей команды:
>**\TeamServices\dotnet\WebApplication\Views\Home\Index.cshtml**
```sh
  <p class="w3-large w3-center">
      <<TeamName>>
  </p>
```
>**\TeamServices\dotnet\WebApplication\Views\Team\Index.cshtml**
```sh
  <p class="lead">
      <<TeamName>>
  </p>
```

### Инструкция по откату

Выполните скрипт отката источников.
>**~\TeamServices\mssql\Scripts\rollback.sql**

### Что дальше?

  - Создание сервиса новостей команды.

License
----

MIT
