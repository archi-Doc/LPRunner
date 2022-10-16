// Copyright (c) All contributors. All rights reserved. Licensed under the MIT license.

using System;
using Arc.Unit;
using Netsphere;

namespace LPRunner;

public class Runner
{
    public Runner(ILogger<Runner> logger, NetControl netControl)
    {
        this.logger = logger;
        this.netControl = netControl;
    }

    public async Task Run()
    {
        this.logger.TryGet()?.Log("Runner start");

        NodeAddress.TryParse("127.0.0.1:49152", out var nodeAddress);
        using (var terminal = this.netControl.Terminal.Create(nodeAddress))
        {
            var p = new PacketPing("test56789012345678901234567890123456789");
            var result = await terminal.SendPacketAndReceiveAsync<PacketPing, PacketPingResponse>(p);
            if (result.Value != null)
            {
                this.logger.TryGet()?.Log($"Received: {result.ToString()}");
            }
            else
            {
                this.logger.TryGet(LogLevel.Error)?.Log($"{result}");
            }
        }

        this.logger.TryGet()?.Log("Runner end");
    }

    private ILogger<Runner> logger;
    private NetControl netControl;
}
