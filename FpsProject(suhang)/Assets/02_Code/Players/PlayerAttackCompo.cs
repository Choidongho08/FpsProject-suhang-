using _02_Code.Combat;
using _02_Code.Entities;
using UnityEngine;

namespace _02_Code.Players
{
    public class PlayerAttackCompo : MonoBehaviour, IEntityComponent
    {
        [Header("attack datas"), SerializeField] private AttackDataSO[] attackDataList;
        
        [SerializeField] private float comboWindow;
        private Entity _entity;
        private EntityAnimator _entityAnimator;
        private EntityVFX _vfxCompo;
        private EntityAnimatorTrigger _animationTrigger;

        private readonly int _attackSpeedHash = Animator.StringToHash("ATTACK_SPEED");
        private readonly int _comboCounterHash = Animator.StringToHash("COMBO_COUNTER");

        private float _attackSpeed = 1f;
        private float _lastAttackTime;

        public bool useMouseDirection = false;

        public int ComboCounter { get; set; } = 0;

        public float AttackSpeed
        {
            get => _attackSpeed;
            set
            {
                _attackSpeed = value;
                _entityAnimator.SetParam(_attackSpeedHash, _attackSpeed);
            }
        }
        public void Initialize(Entity entity)
        {
            _entity = entity;
            _entityAnimator = entity.GetCompo<EntityAnimator>();
            _vfxCompo = entity.GetCompo<EntityVFX>();
            AttackSpeed = 1f;
            _animationTrigger = entity.GetCompo<EntityAnimatorTrigger>();
            _animationTrigger.OnAttackVFXTrigger += HandleAttackVFXTrigger;
        }

       

        private void OnDestroy()
        {
            _animationTrigger.OnAttackVFXTrigger -= HandleAttackVFXTrigger;
        }

        private void HandleAttackVFXTrigger()
        {
            _vfxCompo.PlayVfx($"Blade{ComboCounter}", Vector3.zero, Quaternion.identity);
        }
        
        public void Attack()
        {
            bool comboCounterOver = ComboCounter > 2;
            bool comboWindowExhaust = Time.time >= _lastAttackTime + comboWindow;
            if (comboCounterOver || comboWindowExhaust)
                ComboCounter = 0;
            _entityAnimator.SetParam(_comboCounterHash, ComboCounter);
            
            
        }
        
        public AttackDataSO GetCurrentAttackData()
        {
            Debug.Assert(attackDataList.Length > ComboCounter, "Combo counter is out of range");
            return attackDataList[ComboCounter];
        }
        
        public void EndAttack()
        {
            ComboCounter++;
            _lastAttackTime = Time.time;
        }
    }
}