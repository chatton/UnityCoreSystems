using System.Collections;
using System.Collections.Generic;
using Core.HealthSystem;
using Core.JRPG.Combat.Abilities;
using DG.Tweening;
using UnityEngine;

namespace Core.JRPG.Combat
{
    [RequireComponent(typeof(Health))]
    public class Combatant : MonoBehaviour
    {
        [SerializeField] private Team _team;
        public Team Team => _team;

        [SerializeField] private List<Ability> _abilities;

        private Health _health;

        private void Awake()
        {
            _health = GetComponent<Health>();

            // every time this health instance takes damage, we log the current and max hp
            _health.OnDamaged += h => Debug.Log($"{name} took damage! {h.CurrentHp}/{h.MaxHp}");
            _health.OnDamaged += GetKnockedBack;
            // when dead, we can just destroy the game object for now.
            _health.OnDeath += h => Destroy(h.gameObject);
        }

        private void GetKnockedBack(Health health)
        {
            StartCoroutine(KnockBackRoutine(health));
        }

        private IEnumerator KnockBackRoutine(Health health)
        {
            Vector3 currPos = health.transform.position;
            Transform t = health.transform;

            t.DOJump(currPos - transform.forward, 1, 0, 0.15f);
            yield return new WaitForSeconds(0.15f);
            t.DOMove(currPos, 0.1f);
            yield return null;
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