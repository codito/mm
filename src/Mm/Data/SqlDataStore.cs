// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.Data;

using System.Threading.Tasks;
using Mm.Core;
using Mm.Data.Models;
using SQLite;

public class SqlDataStore : IDataStore
{
    private readonly ILogger logger;
    private string databasePath;
    private string password;

    public SqlDataStore(ILogger logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #region Configuration

    public void Configure(string databasePath, string password)
    {
        this.databasePath = databasePath;
        this.password = password;
    }

    public async Task<bool> ValidateConnection()
    {
        try
        {
            var db = await this.CreateConnection();
            var info = await db.Table<InfoTable>().ToListAsync();
            return info != null && info.Count > 1;
        }
        catch (SQLiteException ex)
        {
            this.logger.Error($"Invalid sql connection. Exception: {ex}");
        }

        return false;
    }

    #endregion

    #region Accounts and transactions

    public async Task<IEnumerable<AccountList>> GetAccounts(IEnumerable<string> accountNames)
    {
        var db = await this.CreateConnection();

        // TODO: a scalar query may be more performant.
        var names = new HashSet<string>(accountNames.Select(a => a.ToLower()));
        var accounts = await db.Table<AccountList>().ToListAsync();
        return accounts
            .Where(a => names.Contains(a.AccountName.ToLower()))
            .ToList();
    }

    public async Task<List<CheckingAccount>> GetTransactions(int accountId, string status)
    {
        var db = await this.CreateConnection();
        return await db.QueryAsync<CheckingAccount>(
            @"SELECT *
FROM CHECKINGACCOUNT_V1 AS t
WHERE t.ACCOUNTID == ? AND t.STATUS == ?",
            accountId,
            status);
    }

    public async Task<bool> UpdateTransactions(List<CheckingAccount> transactions)
    {
        var db = await this.CreateConnection();
        var updatedRowsCount = await db.UpdateAllAsync(transactions, runInTransaction: true);
        return updatedRowsCount == transactions.Count;
    }

    #endregion

    #region Metadata: Payee, Categories

    public async Task<Payee> AddPayee(string payeeName, int? categId = null, int? subCategId = null)
    {
        var db = await this.CreateConnection();
        var payee = new Payee { PayeeName = payeeName, CategId = categId, SubCategId = subCategId };
        if (await db.InsertAsync(payee) == 1)
        {
            return await db.Table<Payee>().Where(p => p.PayeeName == payeeName).FirstAsync();
        }

        throw new Exception($"Unable to insert payee with name: {payeeName}.");
    }

    public async Task<List<Payee>> GetPayees()
    {
        var db = await this.CreateConnection();
        return await db.Table<Payee>().ToListAsync();
    }

    public async Task<Dictionary<Category, List<SubCategory>>> GetCategories()
    {
        var db = await this.CreateConnection();
        var result = new Dictionary<Category, List<SubCategory>>();
        var categories = await db.Table<Category>().ToListAsync();

        foreach (var category in categories)
        {
            result[category] = await db.Table<SubCategory>().Where(c => c.CategId == category.CategId).ToListAsync();
        }

        return result;
    }

    #endregion

    private async Task<SQLiteAsyncConnection> CreateConnection()
    {
        var db = new SQLiteAsyncConnection(this.databasePath);

        if (!string.IsNullOrEmpty(this.password))
        {
            await db.QueryAsync<int>("PRAGMA cipher='aes128cbc'");
            await db.QueryAsync<int>($"PRAGMA key='{this.password}'");
        }

        return db;
    }
}
