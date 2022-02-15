// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.Core;

using Spectre.Console;

public class ConfigWorkflow : IWorkflow
{
    private readonly IHost host;
    private readonly ILogger logger;

    public ConfigWorkflow(IHost host)
    {
        this.host = host ?? throw new ArgumentNullException(nameof(host));
        this.logger = this.host.GetService<ILogger>();
    }

    public async Task RunAsync(IWorkflowRequest request)
    {
        var config = (request as DefaultWorkflowRequest).Configuration;
        var textUI = this.host.GetService<IAnsiConsole>();
        var store = this.host.GetService<IDataStore>();
        var payees = (await store.GetPayees()).ToDictionary(k => k.PayeeName.ToLower(), v => v);
        var categories = (await store.GetCategories()).SelectMany(c => c.Value.Select(sc => $"{c.Key.CategName.ToLower()}:{sc.SubCategName.ToLower()}")).ToHashSet();

        var unmatchedPayees = new Dictionary<string, Rule>();
        var unmatchedCategories = new Dictionary<string, Rule>();
        foreach (var rule in config.Assignments.SelectMany(a => a.Rules))
        {
            var payee = rule.Update["payee"].ToLower();
            if (!payees.TryGetValue(payee, out var p))
            {
                unmatchedPayees[payee] = rule;
            }

            if (!rule.Update.TryGetValue("category", out var cat) || !rule.Update.TryGetValue("sub_category", out var subcat))
            {
                this.logger.Error($"Invalid category configuration for rule: {rule.Name}.");
                continue;
            }

            var category = $"{cat.ToLower()}:{subcat?.ToLower()}";
            if (!categories.Contains(category))
            {
                unmatchedCategories[category] = rule;
            }
        }

        if (unmatchedPayees.Count > 0)
        {
            textUI.MarkupLine("[blue]## Payees not available[/]");
            var table = new Table();
            table.AddColumns("Payee", "Rule");
            foreach (var kv in unmatchedPayees)
            {
                table.AddRow(kv.Key.EscapeMarkup(), kv.Value.Name.EscapeMarkup());
            }

            textUI.Write(table);
        }

        if (unmatchedCategories.Count > 0)
        {
            textUI.MarkupLine("[blue]## Categories not available[/]");
            var table = new Table();
            table.AddColumns("Category:Subcategory", "Rule");
            foreach (var kv in unmatchedCategories)
            {
                table.AddRow(kv.Key.EscapeMarkup(), kv.Value.Name.EscapeMarkup());
            }

            textUI.Write(table);
        }

        textUI.WriteLine();
        textUI.MarkupLine($"[green]Invalid payees: {unmatchedPayees.Count}, Invalid categories: {unmatchedCategories.Count}[/].");
    }
}
