// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.UI.Commands;

using System.CommandLine;
using Mm.Core;
using Spectre.Console;

public class ConfigCommand : Command
{
    public ConfigCommand(IEnumerable<ICommandOption> options)
        : base("config", "Validate and create missing configurations.")
    {
        this.SetHandler(
            async (IHost host, FileInfo database, Configuration config) =>
            {
                config ??= new Configuration();
                config.DatabasePath ??= database?.FullName;

                var textUI = host.GetService<IAnsiConsole>();
                if (string.IsNullOrEmpty(config.DatabasePassword) && Path.GetExtension(config.DatabasePath).Equals(".emb", StringComparison.InvariantCultureIgnoreCase))
                {
                    config.DatabasePassword = textUI.Prompt(new TextPrompt<string>("Enter database password: ").Secret());
                }

                var store = host.GetService<IDataStore>();
                store.Configure(config.DatabasePath, config.DatabasePassword);
                if (!await store.ValidateConnection())
                {
                    textUI.MarkupLine($"Invalid database configuration. Path = '{config.DatabasePath}'. Exiting.");
                    return;
                }

                if (config is null)
                {
                    textUI.MarkupLine("[red]Config command requires a config file. Please use --config option.[/]");
                    return;
                }

                await new ConfigWorkflow(host).RunAsync(new DefaultWorkflowRequest(config));
            },
            options.Select(o => o as Option).ToArray());
    }
}
