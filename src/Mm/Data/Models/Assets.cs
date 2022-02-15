// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.Data.Models;

using SQLite;

[Table("ASSETS_V1")]
public class Assets
{
    [PrimaryKey]
    public int AssetId { get; set; }

    [NotNull]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public string StartDate { get; set; }

    [NotNull]
    [Collation("NOCASE")]
    public string AssetName { get; set; }

    public decimal Value { get; set; }

    // None, Appreciates, Depreciates
    public string ValueChange { get; set; }

    public string Notes { get; set; }

    public decimal ValueChangeRate { get; set; }

    // Property, Automobile, Household Object, Art, Jewellary, Cash, Other
    // Index
    public string AssetType { get; set; }
}
