Entity Project Cheat Sheet
==============

Create a new MVC project using mvc or dojo
```
dotnet new mvc --no-https -o ProjectName
```

Import SQL package
```
dotnet add package MySql.Data -v 8.0.11
```

Run a restore
```
dotnet restore
```

Import Entity package
```
dotnet add package Pomelo.EntityFrameworkCore.MySql -v 2.1.2
```

Run a restore
```
dotnet restore
```

Secure your connection string in appsettings.json by putting a comma after the last item before the ending curly brace and adding the DBInfo key:value
```C#
"DBInfo":
{
    "Name": "MySQLconnect",
    "ConnectionString": "server=localhost;userid=root;password=root;port=3306;database=mydb;SslMode=None"
}
```

Create a context class for your table
```C#
using Microsoft.EntityFrameworkCore;

namespace YourProject.Models
{
    public class MyContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public MyContext(DbContextOptions options) : base(options) { }
    }
}
```

Add your models and entity to Startup.cs using statements
```C#
using YourProject.Models;
using Microsoft.EntityFrameworkCore;
```

Then add this to your Startup.cs ConfigureServices method
```C#
services.AddDbContext<MyContext>(options => options.UseMySql(Configuration["DBInfo:ConnectionString"]));
```

Create a class for your database item and all its columns (don't forget to set default values for things like created_at)
```C#
using System.ComponentModel.DataAnnotations;
using System;
namespace YourProject.Models
{
    public class MyModel
    {
        [Key]
        public int Id { get; set; }
        public string Data { get; set; }
        public DateTime CreatedAt {get;set;} = DateTime.Now;
    }
}
```

Add your model to your MyContext.cs
```C#
public DbSet<MyModel> MyModels {get;set;}
```

Make your first migration
```
dotnet ef migrations add YourMigrationName
```

Update your database
```
dotnet ef database update
```

If you are worried, go to MySql Workbench and confirm your schema and table and columns all exist

Add the entity using statement to your 
HomeController.cs
```C#
using Microsoft.EntityFrameworkCore;
```

Inject your context into your HomeController, at the top before any routes
```C#
private MyContext dbContext;

// here we can "inject" our context service into the constructor
public HomeController(MyContext context)
{
    dbContext = context;
}
```

In your index method, use the power of Entity to save a new item to your database and print it out (ONLY DO THIS ONCE AS A TEST)
```C#
MyModel new_model_item = new MyModel();

new_model_item.Id = 1;
new_model_item.Data = "Example data.";

dbContext.Add(new_model_item);
dbContext.SaveChanges();

MyModel saved_item = dbContext.MyModels.FirstOrDefault();
    
Console.WriteLine($"New item Id: {saved_item.Id}");
Console.WriteLine($"New item Data: {saved_item.Data}");
Console.WriteLine($"New item CreatedAt: {saved_item.CreatedAt}");
```

If you see the values printed out, you’re done!


