using _02_Code.Combat;
using _02_Code.Entities;
using UnityEngine;

namespace _02_Code.Players.States
{
    public class PlayerAttackState : PlayerState
    {
        private PlayerAttackCompo _attackcompo;
        private CharacterMovement _movement;
        
        public PlayerAttackState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _attackcompo = _entity.GetCompo<PlayerAttackCompo>();
            _movement = _entity.GetCompo<CharacterMovement>();
        }

        public override void Enter()
        {
            base.Enter();
            _attackcompo.Attack();
            
            _movement.CanManualMovement = false;
            ApplyAttackData();
        }

        private void ApplyAttackData()
        {
            AttackDataSO currentAtkData = _attackcompo.GetCurrentAttackData();
            Vector3 playerDirection = GetPlayerDirection();
            _player.transform.rotation = Quaternion.LookRotation(playerDirection);

            Vector3 movement = playerDirection * currentAtkData.movementPower;
            _movement.SetAutoMovement(movement);
        }

        private Vector3 GetPlayerDirection()
        {
            if(_attackcompo.useMouseDirection == false)
                return _player.transform.forward;
            
            //Vector3 targetPos = _player.PlayerInput.GetWorldPosition();
            //Vector3 direction = targetPos - _player.transform.position;
            //direction.y = 0;
            //return direction.normalized;
            return default(Vector3);
        }

        public override void Exit()
        {
            _attackcompo.EndAttack();
            _movement.CanManualMovement = true;
            _movement.StopImmediately();
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            if(_isTriggerCall)
                _player.ChangeState("IDLE");
        }
    }
}