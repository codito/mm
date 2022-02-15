// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.Core;

using System.Text.RegularExpressions;
using Mm.Data.Models;
using Spectre.Console;

public class AssignWorkflow : IWorkflow
{
    private readonly IHost host;

    public AssignWorkflow(IHost host)
    {
        this.host = host;
    }

    public async Task RunAsync(IWorkflowRequest request)
    {
        if (request is not AssignWorkflowRequest)
        {
            throw new ArgumentException("Reuqest", nameof(request));
        }

        var assignRequest = request as AssignWorkflowRequest;
        var config = assignRequest.Configuration;
        var commit = assignRequest.Commit;

        var store = this.host.GetService<IDataStore>();
        var fileSystem = this.host.GetService<IFileSystem>();
        var textUI = this.host.GetService<IAnsiConsole>();

        var providedAccountIds = config.Assignments.ConvertAll(a => a.Account);
        var accounts = await store.GetAccounts(providedAccountIds);
        var accountIds = accounts
            .Zip(providedAccountIds)
            .ToDictionary(az => az.Second, az => az.First.AccountId);
        if (accountIds.Count == 0)
        {
            textUI.MarkupLine($"[red]We didn't find an account matching the provided id. AccountIds = {string.Join(',', providedAccountIds)}.[/]");
            return;
        }

        var updates = new Dictionary<Rule, List<CheckingAccount>>();
        var unmatchedTransactions = new List<CheckingAccount>();
        foreach (var assign in config.Assignments)
        {
            var transactions = await store.GetTransactions(accountIds[assign.Account], status: string.Empty);
            var transactionIds = new HashSet<int>();
            foreach (var rule in assign.Rules)
            {
                var filter = CreatePredicate(rule.Condition);
                var matchingTransactions = transactions
                        .Where(t => !transactionIds.Contains(t.TransId) && filter(t))
                        .Select(t =>
                        {
                            transactionIds.Add(t.TransId);
                            return t;
                        })
                        .ToList();
                if (matchingTransactions.Count > 0)
                {
                    updates.Add(rule, matchingTransactions);
                }
            }

            unmatchedTransactions.AddRange(transactions.Where(t => !transactionIds.Contains(t.TransId)));
        }

        textUI.MarkupLine("[blue]## Matching Transactions[/]");
        var matchCount = 0;
        var table = new Table();
        foreach (var update in updates)
        {
            textUI.MarkupLine($"[bold violet]Rule: {update.Key.Name.EscapeMarkup()}[/]");
            table = new Table();
            table.AddColumns("Transaction Id", "Amount", "Notes");
            foreach (var t in update.Value)
            {
                matchCount++;
                var sign = t.TransCode == "Withdrawal" ? "-" : string.Empty;
                table.AddRow(t.TransId.ToString(), $"{sign}{t.TransAmount}", t.Notes);
            }

            textUI.Write(table);
        }

        textUI.MarkupLine("[blue]## Unmatching Transactions[/]");
        var unmatchCount = unmatchedTransactions.Count;
        table = new Table();
        table.AddColumns("Transaction Id", "Amount", "Notes");
        foreach (var t in unmatchedTransactions)
        {
            var sign = t.TransCode == "Withdrawal" ? "-" : string.Empty;
            table.AddRow(t.TransId.ToString(), $"{sign}{t.TransAmount}", t.Notes);
        }

        textUI.Write(table);
        textUI.WriteLine();
        textUI.MarkupLine($"[green]Total = [b]{matchCount + unmatchCount}[/], Matched = [b]{matchCount}[/], Not matched = [b]{unmatchCount}[/].[/]");

        var backupPath = config.BackupDatabase(fileSystem);
        textUI.MarkupLine($"Backed up database to [green bold]{backupPath}[/]");

        var transactionsToCommit = new List<CheckingAccount>();
        var categories = await store.GetCategories();
        var categoryHash = categories.Keys.ToDictionary(k => k.CategName.ToLower(), k => k);
        var subcategoryHash = categories.SelectMany(kv => kv.Value.Select(v => (Id: $"{kv.Key.CategName.ToLower()}:{v.SubCategName.ToLower()}", SubCategory: v))).ToDictionary(p => p.Id, p => p.SubCategory);
        var payees = (await store.GetPayees()).ToDictionary(p => p.PayeeName.ToLower(), p => p);
        foreach (var update in updates)
        {
            var rule = update.Key;
            var categId = categoryHash[rule.Update["category"].ToLower()].CategId;
            var subCategKey = $"{rule.Update["category"].ToLower()}:{rule.Update["sub_category"].ToLower()}";
            var subCategId = subcategoryHash[subCategKey].SubCategId;

            if (!payees.TryGetValue(rule.Update["payee"].ToLower(), out var payee))
            {
                payee = await store.AddPayee(rule.Update["payee"], categId, subCategId);
            }

            foreach (var transaction in update.Value)
            {
                transaction.PayeeId = payee.PayeeId;
                transaction.CategId = categId;
                transaction.SubCategId = subCategId;

                transactionsToCommit.Add(transaction);
            }
        }

        var rowsUpdated = await store.UpdateTransactions(transactionsToCommit);
        textUI.MarkupLine("Committed transaction updates to database.");
    }

    private static Func<CheckingAccount, bool> CreatePredicate(IDictionary<string, string> filters)
    {
        Regex notesRegex = null;
        return transaction =>
        {
            return filters.All(f =>
            {
                return f.Key.ToLower() switch
                {
                    "notes" => (notesRegex ??= new Regex(f.Value, RegexOptions.IgnoreCase)).IsMatch(transaction.Notes),
                    _ => false
                };
            });
        };
    }
}
