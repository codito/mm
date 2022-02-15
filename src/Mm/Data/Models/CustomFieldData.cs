// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.Data.Models;

using SQLite;

[Table("CUSTOMFIELDDATA_V1")]
public class CustomFieldData
{
    [PrimaryKey]
    [NotNull]
    public int FieldDataId { get; set; }

    [NotNull]
    public int FieldId { get; set; }

    [NotNull]
    public int RefId { get; set; }

    public string Content { get; set; }

    // Unique(FieldId, RefId)
    // CREATE INDEX IDX_CUSTOMFIELDDATA_REF ON CUSTOMFIELDDATA_V1(FIELDID, REFID);
}
