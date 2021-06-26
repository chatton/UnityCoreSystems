using System.Collections;
using UnityEngine;

namespace Core.JRPG.Combat
{
    public class BattleController : MonoBehaviour
    {
        [SerializeField] private Combatant player;
        [SerializeField] private Combatant enemy;

        private IEnumerator Start()
        {
            while (player != null && enemy != null)
            {
                if (player != null)
                {
                    yield return player.UseAbilityOn(enemy);
                }

                if (enemy != null)
                {
                    yield return enemy.UseAbilityOn(player);
                }
            }
            Debug.Log("Battle Over!");
        }
    }
}