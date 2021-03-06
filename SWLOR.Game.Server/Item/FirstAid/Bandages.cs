﻿using SWLOR.Game.Server.Data.Entities;
using SWLOR.Game.Server.Enumeration;
using SWLOR.Game.Server.GameObject;
using SWLOR.Game.Server.Item.Contracts;

using NWN;
using SWLOR.Game.Server.Service.Contracts;
using SWLOR.Game.Server.ValueObject;

namespace SWLOR.Game.Server.Item.FirstAid
{
    public class Bandages: IActionItem
    {
        private readonly INWScript _;
        private readonly ISkillService _skill;
        private readonly ICustomEffectService _customEffect;
        private readonly IRandomService _random;
        private readonly IPerkService _perk;

        public Bandages(INWScript script,
            ISkillService skill,
            ICustomEffectService customEffect,
            IRandomService random,
            IPerkService perk)
        {
            _ = script;
            _skill = skill;
            _customEffect = customEffect;
            _random = random;
            _perk = perk;
        }

        public CustomData StartUseItem(NWCreature user, NWItem item, NWObject target, Location targetLocation)
        {
            user.SendMessage("You begin treating " + target.Name + "'s wounds...");
            return null;
        }

        public void ApplyEffects(NWCreature user, NWItem item, NWObject target, Location targetLocation, CustomData customData)
        {
            NWPlayer player = NWPlayer.Wrap(user.Object);

            _customEffect.RemovePCCustomEffect((NWPlayer)target, CustomEffectType.Bleeding);
            _.ApplyEffectToObject(NWScript.DURATION_TYPE_INSTANT, _.EffectHeal(2 + player.EffectiveFirstAidBonus/2), target.Object);
            player.SendMessage("You finish bandaging " + target.Name + "'s wounds.");

            PCSkill skill = _skill.GetPCSkill(player, SkillType.FirstAid);
            int xp = (int)_skill.CalculateRegisteredSkillLevelAdjustedXP(100, item.RecommendedLevel, skill.Rank);
            _skill.GiveSkillXP(player, SkillType.FirstAid, xp);
        }

        public float Seconds(NWCreature user, NWItem item, NWObject target, Location targetLocation, CustomData customData)
        {
            if (_random.Random(100) + 1 <= _perk.GetPCPerkLevel((NWPlayer)user, PerkType.SpeedyMedic) * 10)
            {
                return 0.1f;
            }

            PCSkill skill = _skill.GetPCSkill((NWPlayer)user, SkillType.FirstAid);
            float seconds = 6.0f - (skill.Rank * 0.2f);
            if (seconds < 1.0f) seconds = 1.0f;
            return seconds;
        }

        public bool FaceTarget()
        {
            return true;
        }

        public int AnimationID()
        {
            return NWScript.ANIMATION_LOOPING_GET_MID;
        }

        public float MaxDistance()
        {
            return 3.5f;
        }

        public bool ReducesItemCharge(NWCreature user, NWItem item, NWObject target, Location targetLocation, CustomData customData)
        {
            return true;
        }

        public string IsValidTarget(NWCreature user, NWItem item, NWObject target, Location targetLocation)
        {
            if (_.GetIsPC(target.Object) == NWScript.FALSE || _.GetIsDM(target.Object) == NWScript.TRUE)
            {
                return "Only players may be targeted with this item.";
            }

            if (!_customEffect.DoesPCHaveCustomEffect((NWPlayer)target, CustomEffectType.Bleeding))
            {
                return "Your target is not bleeding.";
            }

            return null;
        }

        public bool AllowLocationTarget()
        {
            return false;
        }
    }
}
