﻿using System.Linq;
using SWLOR.Game.Server.Data.Contracts;
using SWLOR.Game.Server.Data.Entities;
using SWLOR.Game.Server.Enumeration;
using SWLOR.Game.Server.GameObject;
using SWLOR.Game.Server.Item.Contracts;
using NWN;
using SWLOR.Game.Server.Service.Contracts;
using SWLOR.Game.Server.ValueObject;

namespace SWLOR.Game.Server.Item
{
    public class WaterJug: IActionItem
    {
        private readonly IRandomService _random;
        private readonly IDataContext _db;
        private readonly ISkillService _skill;

        public WaterJug(IRandomService random, 
            IDataContext db,
            ISkillService skill)
        {
            _random = random;
            _db = db;
            _skill = skill;
        }

        public CustomData StartUseItem(NWCreature user, NWItem item, NWObject target, Location targetLocation)
        {
            return null;
        }

        public void ApplyEffects(NWCreature user, NWItem item, NWObject target, Location targetLocation, CustomData customData)
        {

            int growingPlantID = target.GetLocalInt("GROWING_PLANT_ID");
            if (growingPlantID <= 0)
            {
                user.SendMessage("Water jugs can only target growing plants.");
                return;
            }
            GrowingPlant growingPlant = _db.GrowingPlants.Single(x => x.GrowingPlantID == growingPlantID);

            if (growingPlant.WaterStatus <= 0)
            {
                user.SendMessage("That plant doesn't need to be watered at this time.");
                return;
            }
            
            if (item.Charges <= 0)
            {
                user.SendMessage("There's no water in that jug!");
                return;
            }
            
            int remainingTicks = growingPlant.RemainingTicks;

            if (growingPlant.WaterStatus > 1)
            {
                remainingTicks = remainingTicks / 2;
            }

            growingPlant.WaterStatus = 0;
            growingPlant.RemainingTicks = remainingTicks;
            _db.SaveChanges();

            user.SendMessage("You water the plant.");
            
            PCSkill pcSkill = _skill.GetPCSkill((NWPlayer)user, SkillType.Farming);
            if (pcSkill == null) return;

            int xp = (int)_skill.CalculateRegisteredSkillLevelAdjustedXP(100, growingPlant.Plant.Level, pcSkill.Rank);
            _skill.GiveSkillXP((NWPlayer)user, SkillType.Farming, xp);
        }

        public float Seconds(NWCreature user, NWItem item, NWObject target, Location targetLocation, CustomData customData)
        {
            return 0;
        }

        public bool FaceTarget()
        {
            return false;
        }

        public int AnimationID()
        {
            return 0;
        }

        public float MaxDistance()
        {
            return 0;
        }

        public bool ReducesItemCharge(NWCreature user, NWItem item, NWObject target, Location targetLocation, CustomData customData)
        {
            return false;
        }

        public string IsValidTarget(NWCreature user, NWItem item, NWObject target, Location targetLocation)
        {
            return null;
        }

        public bool AllowLocationTarget()
        {
            return false;
        }
    }
}
