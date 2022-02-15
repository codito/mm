// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.Data.Models;

using SQLite;

[Table("CATEGORY_V1")]
public class Category
{
    [PrimaryKey]
    public int CategId { get; set; }

    [NotNull]
    [Unique]
    [Collation("NOCASE")]
    public string CategName { get; set; }

    // CREATE INDEX IDX_CATEGORY_CATEGNAME ON CATEGORY_V1(CATEGNAME);
}
