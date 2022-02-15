// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.Data.Models;

using SQLite;

[Table("TRANSLINK_V1")]
public class TransLink
{
    [PrimaryKey]
    [NotNull]
    public int TransLinkId { get; set; }

    [NotNull]
    public int CheckingAccountId { get; set; }

    // Asset, Stock
    [NotNull]
    public string LinkType { get; set; }

    [NotNull]
    public int LinkRecordId { get; set; }

    // CREATE INDEX IDX_CHECKINGACCOUNT ON TRANSLINK_V1(CHECKINGACCOUNTID);
    // CREATE INDEX IDX_LINKRECORD ON TRANSLINK_V1(LINKTYPE, LINKRECORDID);
}
