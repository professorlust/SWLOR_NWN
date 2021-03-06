﻿using System;
using SWLOR.Game.Server.GameObject.Contracts;

using NWN;
using static NWN.NWScript;
using Object = NWN.Object;

namespace SWLOR.Game.Server.GameObject
{
    public class NWPlaceable: NWObject, INWPlaceable
    {
        public NWPlaceable(INWScript script,
            AppState state) 
            : base(script, state)
        {
        }
        
        public new static NWPlaceable Wrap(Object @object)
        {
            var obj = (NWPlaceable)App.Resolve<INWPlaceable>();
            obj.Object = @object;

            return obj;
        }

        public virtual bool IsUseable
        {
            get => _.GetUseableFlag(Object) == 1;
            set => _.SetUseableFlag(Object, value ? 1 : 0);
        }

        public virtual bool IsLocked
        {
            get => _.GetLocked(Object) == 1;
            set => _.SetLocked(Object, value ? 1 : 0);
        }

        public static implicit operator Object(NWPlaceable o)
        {
            return o.Object;
        }
        public static implicit operator NWPlaceable(Object o)
        {
            return Wrap(o);
        }
    }
}
