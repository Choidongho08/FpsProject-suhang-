using _02_Code.Core.DI;
using _02_Code.Entities;
using _02_Code.Players;
using UnityEngine;

namespace _02_Code.Managers
{
    [DefaultExecutionOrder(-1)]
    public class PlayerManager : MonoBehaviour
    {
        [Inject, SerializeField] private Player player;
        [SerializeField] private EntityFinderSO playerFinder;

        private void Awake()
        {
            playerFinder.SetTarget(player);
        }
    }
}