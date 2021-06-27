using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Core.JRPG.Combat.Turns.Handlers
{
    /// <summary>
    /// AITurnHandler performs a single action for the given combatant.
    /// </summary>
    public class AITurnHandler : TurnHandler
    {
        public override IEnumerator TakeTurn(Combatant combatant)
        {
            // choose a random player and just attack them.
            List<Combatant> players =
                FindObjectsOfType<Combatant>().Where(c => c != null && c.Team == Team.Player).ToList();
            Combatant target = players[Random.Range(0, players.Count)];

            yield return combatant.UseAbilityOn(target);
        }
    }
}