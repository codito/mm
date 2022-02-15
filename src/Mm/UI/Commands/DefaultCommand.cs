// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.UI.Commands;

using System.CommandLine;

public class DefaultCommand : RootCommand
{
    public DefaultCommand(IEnumerable<ICommandOption> options, IEnumerable<Command> commands)
    {
        this.Description = "Companion to Money Manager Ex.";
        this.Name = "mm";

        foreach (var opt in options)
        {
            this.AddGlobalOption(opt as Option);
        }

        foreach (var command in commands)
        {
            this.AddCommand(command);
        }
    }
}