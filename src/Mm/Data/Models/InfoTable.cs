// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.Data.Models;

using SQLite;

[Table("INFOTABLE_V1")]
public class InfoTable
{
    public int InfoId { get; set; }

    // Index
    public string InfoName { get; set; }

    public string InfoValue { get; set; }
}
