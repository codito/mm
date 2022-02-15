// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.Core;

using Mm.Data.Models;

public interface IDataStore
{
    Task<Payee> AddPayee(string payeeName, int? categId = null, int? subCategId = null);

    void Configure(string databasePath, string databasePassword);

    Task<IEnumerable<AccountList>> GetAccounts(IEnumerable<string> accountName);

    Task<Dictionary<Category, List<SubCategory>>> GetCategories();

    Task<List<Payee>> GetPayees();

    Task<List<CheckingAccount>> GetTransactions(int accountId, string status);

    Task<bool> UpdateTransactions(List<CheckingAccount> transactions);

    Task<bool> ValidateConnection();
}
