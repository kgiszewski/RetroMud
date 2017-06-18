﻿using System;
using System.Collections.Generic;
using System.Linq;
using RetroMud.Core.Context;
using RetroMud.Core.Players;

namespace RetroMud.Core.Status
{
    public class StatusMessageManager : IStatusMessageManager
    {
        public IEnumerable<IStatusMessage> GetMessages(int count)
        {
            return ClientContext.Instance.StatusMessages.OrderBy(x => x.CreatedOn).Select(x => x);
        }

        public void AddStatusMessage(IPlayer player, string message)
        {
            ClientContext.Instance.StatusMessages.Add(new StatusMessage
            {
                CreatedOn = DateTime.Now,
                Message = message,
                PlayerId = player.Id
            });
        }
    }
}

