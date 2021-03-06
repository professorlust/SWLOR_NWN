﻿using NWN;
using SWLOR.Game.Server.ChatCommand.Contracts;
using SWLOR.Game.Server.Enumeration;
using SWLOR.Game.Server.GameObject;

namespace SWLOR.Game.Server.ChatCommand
{
    [CommandDetails("Displays your current coordinates in the area.", CommandPermissionType.Player | CommandPermissionType.DM)]
    public class Coord : IChatCommand
    {
        public void DoAction(NWPlayer user, params string[] args)
        {
            Vector position = user.Position;
            int cellX = (int)(position.m_X / 10);
            int cellY = (int)(position.m_Y / 10);

            user.SendMessage($"Current Area Coordinates: ({cellX}, {cellY})");
        }
    }
}
