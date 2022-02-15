// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.Data.Models;

using SQLite;

[Table("CUSTOMFIELD_V1")]
public class CustomField
{
    [PrimaryKey]
    [NotNull]
    public int FieldId { get; set; }

    // Transaction, Stock, Asset, BankAccount, RepeatingTransaction, Payee
    [NotNull]
    public string RefId { get; set; }

    [Collation("NOCASE")]
    public string Description { get; set; }

    // String, Integer, Decimal, Boolean, Date, Time, SingleChoice, MultiChoice
    [NotNull]
    public string Type { get; set; }

    [NotNull]
    public string Properties { get; set; }

    // CREATE INDEX IDX_CUSTOMFIELD_REF ON CUSTOMFIELD_V1(REFTYPE);
}
