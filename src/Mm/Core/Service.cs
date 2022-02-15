// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mm.Core;

using Spectre.Console;

public record Service(
    ILogger Logger,
    IDataStore DataStore,
    IFileSystem FileSystem,
    IAnsiConsole TextUI);

public class ServiceProvider
{
    private Func<ILogger> createLogger;
    private Func<string, string, IDataStore> createDataStore;
    private Func<IFileSystem> createFileSystem;
    private Func<IAnsiConsole> createTextUI;

    private (string DatabasePath, string Password) storeConfig;

    public Service Build()
    {
        if (this.createLogger == null)
        {
            throw new ArgumentNullException(nameof(this.createLogger));
        }

        if (this.createDataStore == null)
        {
            throw new ArgumentNullException(nameof(this.createDataStore));
        }

        if (this.createTextUI == null)
        {
            throw new ArgumentNullException(nameof(this.createTextUI));
        }

        if (string.IsNullOrEmpty(this.storeConfig.DatabasePath))
        {
            throw new ArgumentException(nameof(this.storeConfig.DatabasePath));
        }

        return new Service(
            this.createLogger(),
            this.createDataStore(this.storeConfig.DatabasePath, this.storeConfig.Password),
            this.createFileSystem(),
            this.createTextUI());
    }

    public ServiceProvider WithDataStore(Func<string, string, IDataStore> dataStore)
    {
        this.createDataStore = dataStore;
        return this;
    }

    public ServiceProvider WithDataStoreConfig((string DatabasePath, string Password) storeConfig)
    {
        this.storeConfig = storeConfig;
        return this;
    }

    public ServiceProvider WithFileSystem(Func<IFileSystem> fileSystem)
    {
        this.createFileSystem = fileSystem;
        return this;
    }

    public ServiceProvider WithLogger(Func<ILogger> logger)
    {
        this.createLogger = logger;
        return this;
    }

    public ServiceProvider WithTextUI(Func<IAnsiConsole> console)
    {
        this.createTextUI = console;
        return this;
    }
}
