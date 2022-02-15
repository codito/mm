// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.Data.Models;

using SQLite;

[Table("CURRENCYFORMATS_V1")]
public class CurrencyFormats
{
    [PrimaryKey]
    public int CurrencyId { get; set; }

    [NotNull]
    [Collation("NOCASE")]
    [Unique]
    public string CurrencyName { get; set; }

    [Column("PFX_Symbol")]
    public string PfxSymbol { get; set; }

    [Column("SFX_Symbol")]
    public string SfxSymbol { get; set; }

    [Column("DECIMAL_POINT")]
    public string DecimalPoint { get; set; }

    [Column("GROUP_SEPARATOR")]
    public string GroupSeparator { get; set; }

    [Column("UNIT_NAME")]
    [Collation("NOCASE")]
    public string UnitName { get; set; }

    [Column("CENT_NAME")]
    [Collation("NOCASE")]
    public string CentName { get; set; }

    public int Scale { get; set; }

    public decimal BaseConvRate { get; set; }

    // Index
    [Column("CURRENCY_SYMBOL")]
    [NotNull]
    [Unique]
    [Collation("NOCASE")]
    public string CurrencySymbol { get; set; }
}
