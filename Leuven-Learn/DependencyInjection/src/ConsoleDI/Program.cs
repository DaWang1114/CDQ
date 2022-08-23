using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ConsoleDI.Example;


using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        // 为DI注册服务 services.Add{LIFETIME 生存周期 }<SERVICE 服务> 拓展方法添加 （并可能配置）服务
        // Application code should start here
        services.AddTransient<ITransientOperation, DefaultOperation>()
            .AddScoped<IScopedOperation, DefaultOperation>()
            .AddSingleton<ISingletonOperation, DefaultOperation>()
            .AddTransient<OperationLogger>()) 
    .Build();

ExemplifyScoping(host.Services, "Scope 1");
ExemplifyScoping(host.Services, "Scope 2");

await host.RunAsync();


//举例说明范围界定 方法
static void ExemplifyScoping(IServiceProvider services, string scope)
{
    using IServiceScope serviceScope = services.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;

    OperationLogger logger = provider.GetRequiredService<OperationLogger>();
    logger.LogOperations($"{scope}-Call 1 .GetRequiredService<OperationLogger>()");

    Console.WriteLine("...");

    logger = provider.GetRequiredService<OperationLogger>();
    logger.LogOperations($"{scope}-Call 2 .GetRequiredService<OperationLogger>()");

    Console.WriteLine();
    //Transient 操作总是不同，每次检索服务时，都会创建一个新实例。
    //Scoped 仅随着新范围更改，但在一个范围中是相同的实例。
    //Singleton 操作总是相同，新实例仅被创建一次

}