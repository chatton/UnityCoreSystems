using System.Collections;
using UnityEngine;

namespace Core.JRPG.Combat.Turns.Handlers
{
    public class HumanTurnHandler : TurnHandler
    {
        public override IEnumerator TakeTurn(Combatant combatant)
        {
            Debug.Log("Taking player turn!");
            yield return new WaitForSeconds(1);
        }
    }
}