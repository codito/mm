// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.UI.Commands;

using System.CommandLine;
using Mm.Core;
using Spectre.Console;

public class AssignCommand : Command
{
    public AssignCommand(IEnumerable<ICommandOption> options)
        : base("assign", "Update transactions based on a criteria.")
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

                var commit = false;
                await new AssignWorkflow(host).RunAsync(new AssignWorkflowRequest(config, commit));
            },
            options.Select(o => o as Option).ToArray());
    }
}
