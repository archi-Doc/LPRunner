// Copyright (c) All contributors. All rights reserved. Licensed under the MIT license.

using Netsphere;

namespace LP.NetServices;

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
