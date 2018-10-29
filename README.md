Discord message backup tool
===========================

Instructions:
------------
```dotnet restore```

```dotnet ef database update```

```dotnet build```

```dotnet run```

Use a bot account from Discord's developer tools or change TokenType.Bot to TokenType.User in Program.cs.

To get the ID of a channel, right-click and then click on the "Copy ID" menu entry.

Format:
------- 

This tool backs up the conversation to an SQLite3 database. You can then use tools such as https://sqlitebrowser.org/ or the sqlite3 command-line tool to access to the contents.

Libraries used:
--------------

Entity Framework Core with the SQLite3 package and Discord.NET are the libraries used by the tool.
