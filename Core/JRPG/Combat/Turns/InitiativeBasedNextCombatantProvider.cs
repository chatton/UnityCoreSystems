using System.Collections.Generic;
using System.Linq;

namespace Core.JRPG.Combat.Turns
{
    public class InitiativeBasedNextCombatantProvider : INextCombatantProvider
    {
        private readonly List<Combatant> _combatants;
        private Stack<Combatant> _stack;

        public InitiativeBasedNextCombatantProvider(List<Combatant> combatants)
        {
            _combatants = combatants;
            InitStack();
        }

        private void EnsureStack()
        {
            if (_stack.Count == 0)
            {
                InitStack();
            }
        }

        private void InitStack()
        {
            var orderedCombatants = _combatants.Where(c => c != null).OrderBy(c => c.Initiative);
            _stack = new Stack<Combatant>(orderedCombatants);
        }

        public Combatant NextCombatant()
        {
            EnsureStack();

            Combatant next = _stack.Pop();
            while (next == null)
            {
                next = _stack.Pop();
            }

            return next;
        }
    }
}