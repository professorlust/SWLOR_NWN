﻿using FluentBehaviourTree;
using SWLOR.Game.Server.GameObject;
using SWLOR.Game.Server.Service.Contracts;

namespace SWLOR.Game.Server.Service
{
    public class BehaviourService : IBehaviourService
    {
        private readonly IObjectProcessingService _objProc;
        private readonly AppState _state;

        public BehaviourService(IObjectProcessingService objProc,
            AppState state)
        {
            _objProc = objProc;
            _state = state;
        }
        
        public void RegisterBehaviour(IBehaviourTreeNode node, NWCreature creature)
        {
            TimeData time = new TimeData(_objProc.ProcessingTickInterval);
            string behaviourID = _objProc.RegisterProcessingEvent(() =>
            {
                if (creature.IsValid)
                {
                    node.Tick(time);
                }
            });
            _state.NPCBehaviours.Add(behaviourID, creature);
        }
        
    }
}
