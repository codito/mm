// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.Data.Models;

using SQLite;

[Table("BUDGETSPLITTRANSACTIONS_V1")]
public class BudgetSplitTransactions
{
    [PrimaryKey]
    public int SplitTransId { get; set; }

    [NotNull]
    public int TransId { get; set; }

    public int CategId { get; set; }

    public int SubCategId { get; set; }

    public decimal SplitTransAmount { get; set; }

    // CREATE INDEX IDX_BUDGETSPLITTRANSACTIONS_TRANSID ON BUDGETSPLITTRANSACTIONS_V1(TRANSID);
}
