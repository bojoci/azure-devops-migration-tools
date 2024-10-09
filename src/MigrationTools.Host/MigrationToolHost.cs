﻿using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MigrationTools.Host.Services;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using Spectre.Console.Cli;
using Serilog.Filters;
using MigrationTools.Host.Commands;
using MigrationTools.Services;
using Spectre.Console.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using static MigrationTools.ConfigurationExtensions;
using MigrationTools.Options;
using MigrationTools.Endpoints;
using MigrationTools.Endpoints.Infrastructure;
using Microsoft.Extensions.Options;
using System.Configuration;
using Spectre.Console;
using OpenTelemetry.Resources;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using Azure.Monitor.OpenTelemetry.Exporter;
using System.Diagnostics;
using OpenTelemetry.Logs;
using System.Net.NetworkInformation;

namespace MigrationTools.Host
{

    

    public static class MigrationToolHost
    {
        static int logs = 1;
        private static bool LoggerHasBeenBuilt = false;

        public static IEnumerable<T> GetAll<T>(this IServiceProvider provider)
        {
            var site = typeof(ServiceProvider).GetProperty("CallSiteFactory", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(provider);
            var desc = site.GetType().GetField("_descriptors", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(site) as ServiceDescriptor[];
            return desc.Select(s => provider.GetRequiredService(s.ServiceType)).OfType<T>();
        }

        public static IHostBuilder CreateDefaultBuilder(string[] args, Action<IConfigurator> extraCommands = null)
        {
            var configFile = CommandSettingsBase.ForceGetConfigFile(args);
            var mtv = new MigrationToolVersion();

            string logsPath = LogLocationService.GetLogPath();

            var hostBuilder = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args);

            var outputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] [{versionString}] {Message:lj} {NewLine}{Exception}"; // 

            hostBuilder.UseSerilog((hostingContext, services, loggerConfiguration) =>
            {
                loggerConfiguration
                    .ReadFrom.Configuration(hostingContext.Configuration)
                    .Enrich.WithProperty("versionString", mtv.GetRunningVersion().versionString)
                    .Enrich.FromLogContext()
                    .Enrich.WithProcessId()
                    .WriteTo.File(Path.Combine(logsPath, $"migration.log"), LogEventLevel.Verbose, shared: true,outputTemplate: outputTemplate, rollOnFileSizeLimit: true, rollingInterval: RollingInterval.Hour)
                    .WriteTo.File(new Serilog.Formatting.Json.JsonFormatter(), Path.Combine(logsPath, $"migration-errors.log"), LogEventLevel.Error, shared: true, rollOnFileSizeLimit: true, rollingInterval: RollingInterval.Day)
                    .WriteTo.Logger(lc => lc
                            .Filter.ByExcluding(Matching.FromSource("Microsoft.Hosting.Lifetime"))
                            .Filter.ByExcluding(Matching.FromSource("Microsoft.Extensions.Hosting.Internal.Host"))
                            .WriteTo.Console(theme: AnsiConsoleTheme.Code, outputTemplate: outputTemplate))
                    ;
                    
                LoggerHasBeenBuilt = true;
            });

            hostBuilder.ConfigureLogging((context, logBuilder) =>
             {
             })
            .ConfigureAppConfiguration(builder =>
            {
                builder.SetBasePath(AppContext.BaseDirectory);
                //builder.AddJsonFile("appsettings.json", optional: false);
                if (!string.IsNullOrEmpty(configFile) && File.Exists(configFile))
                {
                    builder.AddJsonFile(configFile);
                }
                builder.AddEnvironmentVariables();
                builder.AddCommandLine(args);
            });

            hostBuilder.UseOpenTelemeter(mtv.GetRunningVersion().versionString);

            hostBuilder.ConfigureServices((context, services) =>
            {

               services.AddOptions();

                //// Services
                services.AddTransient<IDetectOnlineService, DetectOnlineService>();
                 //services.AddTransient<IDetectVersionService, DetectVersionService>();
                 services.AddTransient<IDetectVersionService2, DetectVersionService2>();

                 services.AddSingleton<IMigrationToolVersionInfo, MigrationToolVersionInfo>();
                 services.AddSingleton<IMigrationToolVersion, MigrationToolVersion>();

                 //// Add Old v1Bits
                 services.AddMigrationToolServicesLegacy();
                 //// New v2Bits
                 services.AddMigrationToolServices(context.Configuration, configFile);
             });

            hostBuilder.UseSpectreConsole(config =>
            {
                config.AddCommand<Commands.ExecuteMigrationCommand>("execute")
                            .WithDescription("Executes the enables processors specified in the configuration file.")
                            .WithExample("execute -config \"configuration.json\"")
                            .WithExample("execute -config \"configuration.json\" --skipVersionCheck ");
                config.AddCommand<Commands.InitMigrationCommand>("init")
                            .WithDescription($"Creates an default configuration file or applies templates! Available templates are `{string.Join("`, `", Enum.GetNames(typeof(OptionsConfigurationTemplate)))}`. With `Reference` loading all options with their defaults.")
                            .WithExample("init -template Basic");
                config.AddCommand<Commands.UpgradeConfigCommand>("upgrade")
                           .WithDescription("Attempts to upgrade your config from the old version to the new one. For each option we will load the defaults, then apply your config. This will only bring across valid settings. This is 'best effort' and you will need to check all the values as we have changed a lot! Also note that it will automatically load any defaults in Environment variables which may require you to remove secrets from the output file.")
                           .WithExample("upgrade -config \"configuration.json\"");

                config.AddCommand<Commands.ConfigurationBuilderCommand>("builder")
                           .WithDescription("Creates or edits a configuration file")
                          .WithExample("builder -config \"configuration.json\"").IsHidden();

                extraCommands?.Invoke(config);
                config.PropagateExceptions();
            });
            hostBuilder.UseConsoleLifetime(configureOptions =>
            {
                configureOptions.SuppressStatusMessages = true;
            } );

            return hostBuilder;
        }

    }
}