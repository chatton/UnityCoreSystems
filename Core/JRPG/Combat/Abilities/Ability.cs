using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Core.JRPG.Combat.Abilities
{
    public abstract class Ability : ScriptableObject
    {
        [SerializeField] [Description("Whether or not the ability can be used on the caster themselves.")]
        public bool canSelfCast;

        public AbilityType type = AbilityType.SingleTarget;

        public abstract IEnumerator Use(Combatant user, List<Combatant> targets);
    }
}