# mm

A tiny command line companion to [Money Manager Ex][mmex]. It serves one purpose
for now - automate assignment of non-reconciled transactions given a set of
rules.

[mmex]: https://github.com/moneymanagerex/moneymanagerex

## Usage

At this time only running from source code is available since I'm the only user.
Please create an issue if you need binaries.

```sh
> cd e:\tmp\mm
> dotnet build
> cd src\mm
> dotnet run -- --help
Description:
  Companion to Money Manager Ex.

Usage:
  mm [command] [options]

Options:
  -d, --database <database>  Path to MMEX db.
  -c, --config <config>      Path to a configuration file with assignment rules. [default: Configuration { DatabasePath = , DatabasePassword = , Assignments = , SourcePath =  }]
  --verbose                  Enable verbose logging.
  --version                  Show version information
  -?, -h, --help             Show help and usage information

Commands:
  assign  Update transactions based on a criteria.
  config  Validate and create missing configurations.
```

**Please note that this program will modify your mmex database. Kindly take
a backup before running `assign` command.**

## Configuration

Please see [docs/sample.yml](./docs/sample.yml) for a sample annotated
configuration file.

## License

MIT

## Backlog

Bunch of these are not done primarily because I will likely be the only user of
this tool. I will be glad to pick these up or accept any contribution if anyone
needs these. Open to feedback/suggestions, please create an issue!

- Add a CI and some unit tests
- Do binary releases
- More functionality around stats on the console, or import qif files directly etc.
