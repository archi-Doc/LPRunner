// Copyright (c) All contributors. All rights reserved. Licensed under the MIT license.

using Arc.Unit;
using BigMachines;
using LP;
using SimpleCommandLine;

namespace LPRunner;

[SimpleCommand("run", Default = true)]
public class ConsoleCommand : ISimpleCommandAsync
{
    public ConsoleCommand(ILogger<ConsoleCommand> logger, Runner runner, BigMachine<Identifier> bigMachine)
    {
        this.logger = logger;
        this.runner = runner;
        this.bigMachine = bigMachine;
    }

    public async Task RunAsync(string[] args)
    {
        this.bigMachine.Start();

        var runner = this.bigMachine.CreateOrGet<RunnerMachine.Interface>(Identifier.Zero);

        await this.bigMachine.Core.WaitForTerminationAsync(-1);
        // await this.runner.Run();
    }

    private ILogger<ConsoleCommand> logger;
    private Runner runner;
    private BigMachine<Identifier> bigMachine;
}
