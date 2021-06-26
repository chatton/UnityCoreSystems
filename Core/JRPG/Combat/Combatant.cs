using Core.HealthSystem;
using UnityEngine;

namespace Core.JRPG.Combat
{
    [RequireComponent(typeof(Health))]
    public class Combatant : MonoBehaviour
    {
        [SerializeField] private JRPGTeam _team;

        private Health _health;

        private void Awake()
        {
            _health = GetComponent<Health>();
        }
    }
}