using UnityEngine;

namespace _02_Code.FSM
{
    [CreateAssetMenu(fileName = "StatData", menuName = "SO/FSM/StateData", order = 0)]
    public class StateDataSO : ScriptableObject
    {
        public string stateName;
        public string className;
        public string animParamName;
        
        // 이 해시값은 절대로 private하면 안됨 (build했을 때 작동 안함)
        public int animationHash;

        private void OnValidate()
        {
            animationHash = Animator.StringToHash(animParamName);
        }
    }
}