// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.UI.Commands;

using System.CommandLine;

public class DatabaseOption : Option<FileInfo>, ICommandOption
{
    public DatabaseOption()
        : base(new[] { "-d", "--database" }, "Path to MMEX db.")
    {
        this.ExistingOnly();
    }

    public bool Global => true;
}
