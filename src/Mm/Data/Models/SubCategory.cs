// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.Data.Models;

using SQLite;

[Table("SUBCATEGORY_V1")]
public class SubCategory
{
    [PrimaryKey]
    public int SubCategId { get; set; }

    [NotNull]
    [Collation("NOCASE")]
    public string SubCategName { get; set; }

    [NotNull]
    public int CategId { get; set; }

    // Unique(CategId, SubCategName)
    // CREATE INDEX IDX_SUBCATEGORY_CATEGID ON SUBCATEGORY_V1(CATEGID);
}
