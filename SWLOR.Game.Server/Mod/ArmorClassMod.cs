﻿using System;
using System.Linq;
using SWLOR.Game.Server.GameObject;
using SWLOR.Game.Server.Mod.Contracts;
using SWLOR.Game.Server.Service.Contracts;

namespace SWLOR.Game.Server.Mod
{
    public class ArmorClassMod: IMod
    {
        private readonly IItemService _item;
        
        public ArmorClassMod(IItemService item)
        {
            _item = item;
        }

        public string CanApply(NWPlayer player, NWItem target, params string[] args)
        {
            if (target.CustomAC >= 50)
                return "You cannot improve that item's AC any further.";

            if (!_item.ArmorBaseItemTypes.Contains(target.BaseItemType))
                return "This mod can only be applied to armors.";

            return null;
        }

        public void Apply(NWPlayer player, NWItem target, params string[] args)
        {
            int amount = Convert.ToInt32(args[0]);
            target.CustomAC += amount;
        }

        public string Description(NWPlayer player, NWItem target, params string[] args)
        {
            int amount = Convert.ToInt32(args[0]);
            return "AC +" + amount;
        }
    }
}
