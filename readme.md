###### install hangfire
dotnet add package Hangfire
dotnet add package Hangfire.Core
dotnet add package Hangfire.AspNetCore
dotnet add package Hangfire.SqlServer


#### add ef core
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools



<!-- dotnet add package Hangfire.Storage.SQLite -->
##### Get CRON JOB HERE
https://freeformatter.com/
https://freeformatter.com/cron-expression-generator-quartz.html



##### Add Hangfire to Program.cs
```csharp
using FireApp.Data;
using FireApp.Services;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserService, UserService>();

//  builder.Services.AddHangfire(configuration => configuration
//             .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
//             .UseSimpleAssemblyNameTypeSerializer()
//             .UseRecommendedSerializerSettings()
//             .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
//             {
//                 CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
//                 SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
//                 QueuePollInterval = TimeSpan.Zero,
//                 UseRecommendedIsolationLevel = true,
//                 DisableGlobalLocks = true
//             }));

// Add the processing server as IHostedService
 builder.Services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));

// Enable the Hangfire Server
builder.Services.AddHangfireServer();
        
builder.Services.AddTransient<IServiceManagement, ServiceManagement>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard();
// app.UseHangfireServer();
app.MapHangfireDashboard();

RecurringJob.AddOrUpdate<IServiceManagement>(x => x.SyncData(), "0 * * ? * *"); // every minute

app.Run();
```


#### go to hangfire dashboard
http://localhost:5287/hangfire


##### Secure hangfire dashboard
- dotnet add package Hangfire.Dashboard.Basic.Authentication
