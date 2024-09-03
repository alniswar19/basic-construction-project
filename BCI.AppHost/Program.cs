using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var sql = builder.AddSqlServer("dbServer").AddDatabase("bciDb");

builder.AddProject<Projects.BCI_ApiService>("apiservice")
    .WithReference(sql)
    .WithReference(cache);

builder.Build().Run();
