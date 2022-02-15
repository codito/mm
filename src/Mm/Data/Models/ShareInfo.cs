// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.Data.Models;

using SQLite;

[Table("SHAREINFO_V1")]
public class ShareInfo
{
    [PrimaryKey]
    [NotNull]
    public int ShareInfoId { get; set; }

    [NotNull]
    public int CheckingAccountId { get; set; }

    public decimal ShareNumber { get; set; }

    public decimal SharePrice { get; set; }

    public decimal ShareCommission { get; set; }

    public string ShareLot { get; set; }

    // CREATE INDEX IDX_SHAREINFO ON SHAREINFO_V1(CHECKINGACCOUNTID);
}
