// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.Data.Models;

using SQLite;

[Table("BUDGETYEAR_V1")]
public class BudgetYear
{
    [PrimaryKey]
    public int BudgetYearId { get; set; }

    [NotNull]
    [Unique]
    public string BudgetYearName { get; set; }

    // CREATE INDEX IDX_BUDGETYEAR_BUDGETYEARNAME ON BUDGETYEAR_V1(BUDGETYEARNAME);
}
