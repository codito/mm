// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.Infra;

using Mm.Core;
using Spectre.Console;

public class Logger : ILogger
{
    private readonly IAnsiConsole console;
    private LogLevel level;

    public Logger(IAnsiConsole console)
    {
        this.console = console;
    }

    public void Configure(LogLevel level) => this.level = level;

    public void Error(string message)
    {
        if (this.level >= LogLevel.Error)
        {
            this.console.WriteLine($"ERROR: {message}");
        }
    }

    public void Verbose(string message)
    {
        if (this.level >= LogLevel.Verbose)
        {
            this.console.MarkupLine($"[yellow]WARN: {message}[/]");
        }
    }

    public void Warn(string message)
    {
        if (this.level >= LogLevel.Warning)
        {
            this.console.MarkupLine($"[yellow]WARN: {message}[/]");
        }
    }
}
