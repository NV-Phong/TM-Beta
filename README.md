![Screenshot](ScreenShots/TaskManager.png)

![Screenshot](ScreenShots/ScreenShots.png)
## Step 1: Rebuild The Project
CTRL + B
## Step 2: Rename Server
### • appsetting.json
```json

"ConnectionStrings":
{
    "TaskManagerConnection":
    "Server=MAR03\\SQLEXPRESS;Database=TaskManager;Trusted_Connection=True;TrustServerCertificate=True"
},
  
```

### • TaskManagerContext.cs
```CSharp

protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
=> optionsBuilder.UseSqlServer
("Server=MAR03\\SQLEXPRESS;Database=TaskManager;Trusted_Connection=True;TrustServerCertificate=true;");

```

## Step 3: Update Database

```
Update-Database

```
