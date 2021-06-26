using System.Collections;
using UnityEngine;

namespace Core.JRPG.Combat.Turns.Handlers
{
    public class PlayerTurnHandler : TurnHandler
    {
        public override IEnumerator TakeTurn()
        {
            Debug.Log("Taking player turn!");
            yield return new WaitForSeconds(1);
        }
    }
}