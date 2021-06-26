using System.Collections;
using UnityEngine;

namespace Core.JRPG.Combat.Turns.Handlers
{
    /// <summary>
    /// AITurnHandler performs a single action for the given combatant.
    /// </summary>
    public class AITurnHandler : TurnHandler
    {
        public override IEnumerator TakeTurn(Combatant combatant)
        {
            Debug.Log("Taking enemy turn!");
            yield return new WaitForSeconds(1);
        }
    }
}