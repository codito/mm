// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.Data.Models;

using SQLite;

[Table("CHECKINGACCOUNT_V1")]
public class CheckingAccount
{
    [PrimaryKey]
    public int TransId { get; set; }

    [NotNull]
    public int AccountId { get; set; }

    public int ToAccountId { get; set; }

    [NotNull]
    public int PayeeId { get; set; }

    // Withdrawal, Deposit, Transfer
    [NotNull]
    public string TransCode { get; set; }

    [NotNull]
    public decimal TransAmount { get; set; }

    // None, Reconciled, Void, Follow up, Duplicate
    public string Status { get; set; }

    public string TransactionNumber { get; set; }

    public string Notes { get; set; }

    public int CategId { get; set; }

    public int SubCategId { get; set; }

    public string TransDate { get; set; }

    public int FollowupId { get; set; }

    public decimal ToTransAmount { get; set; }

    // CREATE INDEX IDX_CHECKINGACCOUNT_ACCOUNT ON CHECKINGACCOUNT_V1(ACCOUNTID, TOACCOUNTID);
    // CREATE INDEX IDX_CHECKINGACCOUNT_TRANSDATE ON CHECKINGACCOUNT_V1(TRANSDATE);
}
