// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.Core;

using System;

public interface IHost : IDisposable
{
    T GetService<T>();
}
