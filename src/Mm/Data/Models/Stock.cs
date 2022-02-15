// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.Data.Models;

using SQLite;

[Table("STOCK_V1")]
public class Stock
{
    [PrimaryKey]
    public int StockId { get; set; }

    // Index
    public int HeldAt { get; set; }

    [NotNull]
    public string PurchaseDate { get; set; }

    [Collation("NOCASE")]
    [NotNull]
    public string StockName { get; set; }

    public string Symbol { get; set; }

    public decimal NumShares { get; set; }

    [NotNull]
    public decimal PurchasePrice { get; set; }

    public string Notes { get; set; }

    [NotNull]
    public decimal CurrentPrice { get; set; }

    public decimal Value { get; set; }

    public decimal Commision { get; set; }
}
