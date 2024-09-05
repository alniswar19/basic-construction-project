using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

// var cache = builder.AddRedis("cache");

var sqlPassowrd = builder.AddParameter("dbServer-password", secret: true);

var sqlShell = "./sql-server";
var sqlScript = "../BCIDB/build";
var sqlServer = builder.AddSqlServer("dbServer", sqlPassowrd, port: 2433);
   //  .WithDataVolume("BciDataVolume");

sqlServer
    // Mount the init scripts directory into the container.
    .WithBindMount(sqlShell, target: "/usr/config")
    // Mount the SQL scripts directory into the container so that the init scripts run.
    .WithBindMount(sqlScript, target: "/docker-entrypoint-initdb.d")
    // Run the custom entrypoint script on startup.
    .WithArgs("/usr/config/entrypoint.sh")
    // Add the database to the application model so that it can be referenced by other resources.
    .AddDatabase("BCIDB");

builder.AddProject<Projects.BCI_ApiService>("apiservice")
    .WithReference(sqlServer);
    //.WithReference(cache);

builder.Build().Run();