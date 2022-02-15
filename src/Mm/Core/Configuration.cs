// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.Core;

public record Configuration
{
    public string DatabasePath { get; set; }

    public string DatabasePassword { get; set; }

    public List<Assignment> Assignments { get; set; }

    public string SourcePath { get; set; }
}

public static class ConfigurationExtensions
{
    public static string BackupDatabase(this Configuration config, IFileSystem fileSystem)
    {
        var backupFileName = $"{DateTime.Now.ToFileTime()}_{Path.GetFileName(config.DatabasePath)}";
        var backupPath = Path.Combine(Path.GetTempPath(), backupFileName);
        fileSystem.Copy(config.DatabasePath, backupPath);
        return backupPath;
    }
}
