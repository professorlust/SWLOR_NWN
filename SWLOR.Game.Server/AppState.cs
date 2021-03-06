﻿using System;
using System.Collections.Generic;
using SWLOR.Game.Server.GameObject;
using SWLOR.Game.Server.ValueObject;
using SWLOR.Game.Server.ValueObject.Dialog;
using SWLOR.Game.Server.ValueObject.Skill;

namespace SWLOR.Game.Server
{
    public class AppState
    {
        public Dictionary<string, PlayerDialog> PlayerDialogs { get; }
        public Dictionary<int, bool> DialogFilesInUse { get; }
        public Dictionary<string, int> EffectTicks { get; }
        public Dictionary<string, CreatureSkillRegistration> CreatureSkillRegistrations;
        public Dictionary<CasterSpellVO, int> NPCEffects { get; }
        public Dictionary<string, Action> ProcessingEvents { get; set; }
        public Queue<string> UnregisterProcessingEvents { get; set; }
        public Dictionary<string, EnmityTable> NPCEnmityTables { get; set; }
        public Dictionary<string, CustomData> CustomObjectData { get; set; } 
        public Dictionary<string, NWCreature> NPCBehaviours { get; set; }
        public Dictionary<string, AreaSpawn> AreaSpawns { get; set; }

        public AppState()
        {
            PlayerDialogs = new Dictionary<string, PlayerDialog>();
            DialogFilesInUse = new Dictionary<int, bool>();
            EffectTicks = new Dictionary<string, int>();
            CreatureSkillRegistrations = new Dictionary<string, CreatureSkillRegistration>();
            NPCEffects = new Dictionary<CasterSpellVO, int>();
            ProcessingEvents = new Dictionary<string, Action>();
            UnregisterProcessingEvents = new Queue<string>();
            NPCEnmityTables = new Dictionary<string, EnmityTable>();
            CustomObjectData = new Dictionary<string, CustomData>();
            NPCBehaviours = new Dictionary<string, NWCreature>();
            AreaSpawns = new Dictionary<string, AreaSpawn>();

            for (int x = 1; x <= Constants.NumberOfDialogs; x++)
            {
                DialogFilesInUse.Add(x, false);
            }
        }

    }
}
