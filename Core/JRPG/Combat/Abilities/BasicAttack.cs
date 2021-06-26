using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Core.HealthSystem;
using DG.Tweening;
using UnityEngine;

namespace Core.JRPG.Combat.Abilities
{
    [CreateAssetMenu(menuName = "Core/JRPG/Abilities/Basic Attack")]
    public class BasicAttack : Ability
    {
        [SerializeField] [Description("The base amount of damage the attack will deal")]
        protected int baseDamage = 10;

        public override IEnumerator Use(Combatant user, List<Combatant> targets)
        {
            Vector3 startPos = user.transform.position;
            yield return MoveToTarget(user, targets[0]);

            foreach (Combatant target in targets)
            {
                yield return AttackTarget(user, target);
            }

            yield return JumpBackToStartingPosition(user, startPos);
        }


        private IEnumerator MoveToTarget(Combatant user, Combatant target)
        {
            // run up to the target.
            var transform = target.transform;
            user.transform.DOMove(transform.position + transform.forward * 1.5f, 1f);
            yield return new WaitForSeconds(1);
        }

        private IEnumerator AttackTarget(Combatant user, Combatant target)
        {
            int damage = baseDamage + Random.Range(0, baseDamage / 2);

            // begin attack animation
            user.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.1f);
            yield return new WaitForSeconds(0.1f);

            // do the actual damage
            target.GetComponent<Health>().Damage(damage);

            // finish attack animation
            user.transform.DOScale(new Vector3(1, 1, 1), 0.1f);
            yield return new WaitForSeconds(0.1f);
        }

        private IEnumerator JumpBackToStartingPosition(Combatant user, Vector3 startPos)
        {
            // jump back to the original position
            user.transform.DOJump(startPos, 1f, 1, 0.5f);
            yield return new WaitForSeconds(0.5f);
        }
    }
}