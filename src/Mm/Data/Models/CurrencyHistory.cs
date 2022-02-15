// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.Data.Models;

using SQLite;

[Table("CURRENCYHISTORY_V1")]
public class CurrencyHistory
{
    [PrimaryKey]
    public int CurrHistId { get; set; }

    [NotNull]
    public int CurrencyId { get; set; }

    [NotNull]
    public string CurrDate { get; set; }

    [NotNull]
    public decimal CurrValue { get; set; }

    public int CurrUpdType { get; set; }

    // Unique(CurrencyId, CurrDate)
    // CREATE INDEX IDX_CURRENCYHISTORY_CURRENCYID_CURRDATE ON CURRENCYHISTORY_V1(CURRENCYID, CURRDATE);
}
