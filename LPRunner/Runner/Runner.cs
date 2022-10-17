// Copyright (c) All contributors. All rights reserved. Licensed under the MIT license.

using System;
using System.Diagnostics;
using Arc.Threading;
using Arc.Unit;
using LP;
using LP.NetServices;
using Microsoft.Extensions.DependencyInjection;
using Netsphere;

namespace LPRunner;

public class Runner
{
    public Runner(ILogger<Runner> logger, LPBase lPBase, NetControl netControl, IServiceProvider sp)
    {
        this.logger = logger;
        this.lpBase = lPBase;
        this.netControl = netControl;
        var a = sp.GetRequiredService<IRemoteControlService>();
    }

    public async Task Run()
    {
        this.logger.TryGet()?.Log($"Runner start");
        this.logger.TryGet()?.Log($"Root directory: {this.lpBase.RootDirectory}");
        this.logger.TryGet()?.Log("Press Ctrl+C to exit.");

        /*while (!ThreadCore.Root.IsTerminated)
        {
            ThreadCore.Root.Sleep(1000);
        }*/

        NodeAddress.TryParse("127.0.0.1:49152", out var nodeAddress);
        using (var terminal = this.netControl.Terminal.Create(nodeAddress))
        {
            var remoteControl = terminal.GetService<IRemoteControlService>();
            var netTask = remoteControl.Restart();
            var response = await netTask.ResponseAsync;
            Console.WriteLine(response.Result);

            // Console.WriteLine(terminal);
            /*var p = new PacketPing("test56789012345678901234567890123456789");
            var result = await terminal.SendPacketAndReceiveAsync<PacketPing, PacketPingResponse>(p);
            if (result.Value != null)
            {
                this.logger.TryGet()?.Log($"Received: {result.ToString()}");
            }
            else
            {
                this.logger.TryGet(LogLevel.Error)?.Log($"{result}");
            }*/
        }

        /*var process = new Process();
        process.StartInfo.FileName = "/bin/bash";
        process.StartInfo.ArgumentList = "ls";*/

        try
        {
            /*var process = Process.Start("/bin/bash", "ls");
            process.WaitForExit();*/

            var startInfo = new ProcessStartInfo
            {
                FileName = @"/bin/bash",
                Arguments = "-c \"echo hello\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
            };

            using (var process = new Process { StartInfo = startInfo })
            {
                process.Start();
                string result = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                Console.WriteLine(result);
            }
        }
        catch
        {
            this.logger.TryGet()?.Log("ex");
        }

        this.logger.TryGet()?.Log("Runner end");
    }

    private ILogger<Runner> logger;
    private LPBase lpBase;
    private NetControl netControl;
}
