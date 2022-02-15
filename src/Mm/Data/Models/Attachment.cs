// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.Data.Models;

using SQLite;

[Table("ATTACHMENT_V1")]
public class Attachment
{
    [PrimaryKey]
    public int AttachmentId { get; set; }

    // Transaction, Stock, Asset, BankAccount, RepeatingTransaction, Payee
    [NotNull]
    public string RefType { get; set; }

    [NotNull]
    public int RefId { get; set; }

    [Collation("NOCASE")]
    public string Description { get; set; }

    [NotNull]
    [Collation("NOCASE")]
    public string FileName { get; set; }

    // CREATE INDEX IDX_ATTACHMENT_REF ON ATTACHMENT_V1(REFTYPE, REFID);
}
