using System.Collections;
using UnityEngine;

namespace Core.JRPG.Combat.Turns.Handlers
{
    public abstract class TurnHandler : MonoBehaviour
    {
        public abstract IEnumerator TakeTurn();
    }
}