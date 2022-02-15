// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.UI;

using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using Jab;
using Mm.Core;
using Mm.Data;
using Mm.Infra;
using Mm.UI.Commands;
using Spectre.Console;

/// <summary>
/// Application host container.
/// </summary>
[ServiceProvider]
[Singleton(typeof(IDataStore), typeof(SqlDataStore))]
[Singleton(typeof(IFileSystem), typeof(FileSystem))]
[Singleton(typeof(ILogger), typeof(Logger))]
[Singleton(typeof(IAnsiConsole), Factory = nameof(GetTextUI))]
[Singleton(typeof(RootCommand), Factory = nameof(GetDefaultCommand))]
[Singleton(typeof(Command), typeof(AssignCommand))]
[Singleton(typeof(Command), typeof(ConfigCommand))]
[Singleton(typeof(ICommandOption), typeof(DatabaseOption))]
[Singleton(typeof(ICommandOption), typeof(ConfigOption))]
[Singleton(typeof(ICommandOption), typeof(VerboseOption))]
public partial class Host : IHost
{
    public static IAnsiConsole GetTextUI() => AnsiConsole.Console;

    public RootCommand GetDefaultCommand() => new DefaultCommand(this.GetService<IEnumerable<ICommandOption>>(), this.GetService<IEnumerable<Command>>());

    public async Task<int> RunAsync(string[] args)
    {
        var parser = new CommandLineBuilder(this.GetService<RootCommand>())
                         .UseDefaults()
                         .AddMiddleware(async (context, next) =>
                         {
                             context.BindingContext.AddService(typeof(IHost), (_) => this);
                             await next(context);
                         })
                         .UseExceptionHandler((e, _) =>
                         {
                             var textUI = this.GetService<IAnsiConsole>();
                             textUI.WriteLine(Environment.NewLine);
                             textUI.WriteException(e);
                         })
                         .Build();
        return await parser.InvokeAsync(args);
    }
}
