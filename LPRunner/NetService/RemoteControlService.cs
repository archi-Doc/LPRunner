// Copyright (c) All contributors. All rights reserved. Licensed under the MIT license.

using LP;
using Netsphere;
using NetsphereTest;

namespace LPRunner.NetService;

[NetServiceObject]
internal class RemoteControlService : IRemoteControlService
{
    public async NetTask RequestAuthorization(Token token)
    {
    }

    public async NetTask Restart()
    {
    }
}
