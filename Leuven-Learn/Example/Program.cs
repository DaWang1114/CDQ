using ConsoleJson.Example;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;


using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, configuration) =>
    {
        #region Json
        //configuration.Sources.Clear();


        //IHostEnvironment env = hostingContext.HostingEnvironment;

        //configuration
        //    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        //    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);


        //IConfigurationRoot configurationRoot = configuration.Build();

        //TransientFaultHandlingOptions options = new();
        //configurationRoot.GetSection(nameof(TransientFaultHandlingOptions))
        //    .Bind(options);

        //Console.WriteLine($"TransientFaultHandlingOptions.Enabled={options.Enabled}");
        //Console.WriteLine($"TransientFaultHandlingOptions.AutoRetryDelay={options.AutoRetryDelay}");


        #endregion

        #region XML
        configuration.Sources.Clear();

        configuration
            .AddXmlFile("appsettings.xml", optional: true, reloadOnChange: true)
            .AddXmlFile("repeating-example.xml", optional: true, reloadOnChange: true);

        configuration.AddEnvironmentVariables();

        if (args is { Length: > 0 })
        {
            configuration.AddCommandLine(args);
        }

        #endregion
    })
    .Build();

//Application code should start here.

await host.RunAsync();
