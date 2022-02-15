// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.Data.Models;

using SQLite;

[Table("ACCOUNTLIST_V1")]
public class AccountList
{
    [PrimaryKey]
    public int AccountId { get; set; }

    [NotNull]
    [Unique]
    [Collation("NOCASE")]
    public string AccountName { get; set; }

    // Index
    // Values: Checking, Term, Investment, Credit Card
    [NotNull]
    public string AccountType { get; set; }

    public string AccountNum { get; set; }

    // Open, Closed
    [NotNull]
    public string Status { get; set; }

    public string Notes { get; set; }

    public string HeldAt { get; set; }

    public string Website { get; set; }

    public string ContactInfo { get; set; }

    public string AccessInfo { get; set; }

    public decimal InitialBal { get; set; }

    [NotNull]
    public string FavoriteAcct { get; set; }

    [NotNull]
    public int CurrencyId { get; set; }

    public int StatementLocked { get; set; }

    public string StatementDate { get; set; }

    public decimal MinimumBalance { get; set; }

    public decimal CreditLimit { get; set; }

    public decimal InterestRate { get; set; }

    public string PaymentDueDate { get; set; }

    public decimal MinimumPayment { get; set; }

    // CREATE INDEX IDX_ACCOUNTLIST_ACCOUNTTYPE ON ACCOUNTLIST_V1(ACCOUNTTYPE);
}
