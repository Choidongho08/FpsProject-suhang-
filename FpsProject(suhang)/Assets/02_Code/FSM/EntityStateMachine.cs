using System;
using System.Collections.Generic;
using _02_Code.Entities;
using UnityEngine;

namespace _02_Code.FSM
{
    public class EntityStateMachine
    {
        public EntityState CurrentState { get; set; }
        private Dictionary<string, EntityState> _states;

        public EntityStateMachine(Entity entity, StateDataSO[] stateList)
        {
            _states = new Dictionary<string, EntityState>();
            foreach (StateDataSO stateData in stateList)
            {
                Type type = Type.GetType(stateData.className);
                Debug.Assert(type != null, $"Finding type is null : {stateData.className}");
                EntityState entityState = Activator.CreateInstance(type, entity, stateData.animationHash) as EntityState;
                
                _states.Add(stateData.stateName, entityState);
            }
        }

        public void ChangeState(string newStateName, bool forced = false)
        {
            EntityState newState = _states.GetValueOrDefault(newStateName);
            Debug.Assert(newState != null, $"State is null : {newStateName}");

            if (forced == false && CurrentState == newState)
                return;
            
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }

        public void UpdateStateMachine()
        {
            CurrentState?.Update();
        }
    }
}