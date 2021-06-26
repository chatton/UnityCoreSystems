using System.Collections;
using System.Collections.Generic;
using Core.HealthSystem;
using Core.JRPG.Combat.Abilities;
using UnityEngine;

namespace Core.JRPG.Combat
{
    [RequireComponent(typeof(Health))]
    public class Combatant : MonoBehaviour
    {
        [SerializeField] private JRPGTeam _team;
        [SerializeField] private List<Ability> _abilities;

        private Health _health;

        private void Awake()
        {
            _health = GetComponent<Health>();
            
            // every time this health instance takes damage, we log the current and max hp
            _health.OnDamaged += h => Debug.Log($"{name} took damage! {h.CurrentHp}/{h.MaxHp}");
            
            // when dead, we can just destroy the game object for now.
            _health.OnDeath += h => Destroy(h.gameObject);
        }

        public Ability GetSelectedAbility()
        {
            return _abilities[0];
        }

        public IEnumerator UseAbilityOn(Combatant target)
        {
            yield return UseAbilityOn(new List<Combatant> {target});
        }

        public IEnumerator UseAbilityOn(List<Combatant> targets)
        {
            yield return GetSelectedAbility().Use(this, targets);
        }
    }
}