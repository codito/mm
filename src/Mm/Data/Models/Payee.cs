// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.Data.Models;

using SQLite;

[Table("PAYEE_V1")]
public class Payee
{
    [PrimaryKey]
    [AutoIncrement]
    public int PayeeId { get; set; }

    // Index
    [NotNull]
    [Collation("NOCASE")]
    public string PayeeName { get; set; }

    public int? CategId { get; set; }

    public int? SubCategId { get; set; }
}
