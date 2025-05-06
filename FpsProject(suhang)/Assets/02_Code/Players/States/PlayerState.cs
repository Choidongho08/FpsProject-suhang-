using _02_Code.Entities;
using _02_Code.FSM;
using UnityEngine;

namespace _02_Code.Players.States
{
     public abstract class PlayerState : EntityState
    {
        protected Player _player;
        protected readonly float _inputThreshold = 0.1f;
        
        public PlayerState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _player = entity as Player;
            Debug.Assert(_player != null, $"Player state using only in player ");
        }
    }
}