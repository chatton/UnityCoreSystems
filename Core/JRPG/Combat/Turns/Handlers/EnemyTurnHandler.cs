using System.Collections;
using UnityEngine;

namespace Core.JRPG.Combat.Turns.Handlers
{
    public class EnemyTurnHandler : TurnHandler
    {
        public override IEnumerator TakeTurn()
        {
            Debug.Log("Taking enemy turn!");
            yield return new WaitForSeconds(1);
        }
    }
}