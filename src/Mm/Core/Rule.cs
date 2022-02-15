// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.Core;

public class Rule
{
    public string Name => string.Join(',', this.Condition);

    public Dictionary<string, string> Condition { get; set; }

    public Dictionary<string, string> Update { get; set; }

    public List<string> Tests { get; set; }
}
