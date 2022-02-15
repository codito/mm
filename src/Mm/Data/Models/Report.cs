// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.Data.Models;

using SQLite;

[Table("REPORT_V1")]
public class Report
{
    [PrimaryKey]
    public int ReportId { get; set; }

    [NotNull]
    [Unique]
    [Collation("NOCASE")]
    public string ReportName { get; set; }

    [Collation("NOCASE")]
    public string GroupName { get; set; }

    public string SqlContent { get; set; }

    public string LuaContent { get; set; }

    public string TemplateContent { get; set; }

    public string Description { get; set; }

    // CREATE INDEX INDEX_REPORT_NAME ON REPORT_V1(REPORTNAME);
}
