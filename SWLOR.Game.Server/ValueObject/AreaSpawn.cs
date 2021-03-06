﻿using System.Collections.Generic;
using SWLOR.Game.Server.GameObject;

namespace SWLOR.Game.Server.ValueObject
{
    public class AreaSpawn
    {
        public List<ObjectSpawn> Placeables { get; set; }
        public List<ObjectSpawn> Creatures { get; set; }

        public AreaSpawn()
        {
            Placeables = new List<ObjectSpawn>();
            Creatures = new List<ObjectSpawn>();
        }
    }
}
