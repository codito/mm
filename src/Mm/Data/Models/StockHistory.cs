// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.Data.Models;

using SQLite;

[Table("STOCKHISTORY_V1")]
public class StockHistory
{
    [PrimaryKey]
    public int HistId { get; set; }

    // Unique(Symbol, Date)
    // Index
    [NotNull]
    public string Symbol { get; set; }

    [NotNull]
    public string Date { get; set; }

    [NotNull]
    public decimal Value { get; set; }

    public int UpdType { get; set; }
}
