// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.Data.Models;

using SQLite;

[Table("BUDGETTABLE_V1")]
public class BudgetTable
{
    [PrimaryKey]
    public int BudgetEntryId { get; set; }

    public int BudgetYearId { get; set; }

    public int CategId { get; set; }

    public int SubCategId { get; set; }

    // None, Weekly, Bi-Weekly, Monthly, Bi-Monthly, Quarterly, Half-Yearly, Yearly, Daily
    [NotNull]
    public string Period { get; set; }

    [NotNull]
    public decimal Amount { get; set; }

    // CREATE INDEX IDX_BUDGETTABLE_BUDGETYEARID ON BUDGETTABLE_V1(BUDGETYEARID);
}
