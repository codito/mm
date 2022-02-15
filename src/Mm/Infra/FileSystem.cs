// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.Infra;

using Mm.Core;

public class FileSystem : IFileSystem
{
    public void Copy(string source, string destination) => File.Copy(source, destination);

    public bool Exists(string path) => File.Exists(path);
}
