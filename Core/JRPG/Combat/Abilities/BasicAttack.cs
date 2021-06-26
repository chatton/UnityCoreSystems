using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Core.HealthSystem;
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
            // 1. move towards target
            
            // 2. play attack animation
            
            // 3. damage all the targets
            
            foreach (Combatant target in targets)
            {
                target.GetComponent<Health>().Damage(baseDamage);
            }

            // 4. move away from target
            
            yield return null;
        }
    }
}