﻿using System;
using System.Linq;
using SWLOR.Game.Server.Data.Contracts;
using SWLOR.Game.Server.Data.Entities;
using SWLOR.Game.Server.GameObject;

using NWN;
using SWLOR.Game.Server.Service.Contracts;

namespace SWLOR.Game.Server.Service
{
    public class HelmetToggleService: IHelmetToggleService
    {
        private readonly IDataContext _db;
        private readonly INWScript _;

        public HelmetToggleService(IDataContext db, INWScript script)
        {
            _db = db;
            _ = script;
        }

        public void OnModuleItemEquipped()
        {
            NWPlayer player = NWPlayer.Wrap(_.GetPCItemLastEquippedBy());
            if (!player.IsPlayer || !player.IsInitializedAsPlayer) return;

            NWItem item = NWItem.Wrap(_.GetPCItemLastEquipped());
            if (item.BaseItemType != NWScript.BASE_ITEM_HELMET) return;

            PlayerCharacter pc = _db.PlayerCharacters.Single(x => x.PlayerID == player.GlobalID);
            _.SetHiddenWhenEquipped(item.Object, !pc.DisplayHelmet == false ? 0 : 1);
        }

        public void OnModuleItemUnequipped()
        {
            NWPlayer player = NWPlayer.Wrap(_.GetPCItemLastUnequippedBy());
            if (!player.IsPlayer) return;

            NWItem item = NWItem.Wrap(_.GetPCItemLastUnequipped());
            if (item.BaseItemType != NWScript.BASE_ITEM_HELMET) return;

            PlayerCharacter pc = _db.PlayerCharacters.Single(x => x.PlayerID == player.GlobalID);
            _.SetHiddenWhenEquipped(item.Object, !pc.DisplayHelmet == false ? 0 : 1);
        }

        public void ToggleHelmetDisplay(NWPlayer player)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));

            if (!player.IsPlayer) return;

            PlayerCharacter pc = _db.PlayerCharacters.Single(x => x.PlayerID == player.GlobalID);
            pc.DisplayHelmet = !pc.DisplayHelmet;
            _db.SaveChanges();

            _.FloatingTextStringOnCreature(
                pc.DisplayHelmet ? "Now showing equipped helmet." : "Now hiding equipped helmet.", 
                player.Object,
                NWScript.FALSE);

            NWItem helmet = NWItem.Wrap(_.GetItemInSlot(NWScript.INVENTORY_SLOT_HEAD, player.Object));
            if (helmet.IsValid)
            {
                _.SetHiddenWhenEquipped(helmet.Object, !pc.DisplayHelmet == false ? 0 : 1);
            }

        }
    }
}
