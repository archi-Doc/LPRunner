// Copyright (c) All contributors. All rights reserved. Licensed under the MIT license.

using Arc.Unit;
using SimpleCommandLine;

namespace LPRunner;

[SimpleCommand("run", Default = true)]
public class ConsoleCommand : ISimpleCommandAsync
{
    public ConsoleCommand(ILogger<ConsoleCommand> logger, Runner runner)
    {
        this.logger = logger;
        this.runner = runner;
    }

    public async Task RunAsync(string[] args)
    {
        await this.runner.Run();
    }

    private ILogger<ConsoleCommand> logger;
    private Runner runner;
}
