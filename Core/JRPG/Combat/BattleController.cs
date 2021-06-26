using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Core.JRPG.Combat
{
    public class BattleController : MonoBehaviour
    {
        [SerializeField] [Description("List of locations that the player units will spawn.")]
        private List<Transform> playerTeamLocations;

        [SerializeField] [Description("List of locations that the enemy units will spawn.")]
        private List<Transform> enemyLocations;

        [SerializeField] private Party playerParty;
        [SerializeField] private Party enemyParty;

        // [SerializeField] private List<Combatant> _combatants;


        private Dictionary<Team, List<Combatant>> _combatantDict;

        private void Awake()
        {
            _combatantDict = new Dictionary<Team, List<Combatant>>();
        }


        private IEnumerator Start()
        {
            playerParty.Create(playerTeamLocations);
            enemyParty.Create(enemyLocations);

            var player = _combatantDict[Team.Player][0];
            var enemy = _combatantDict[Team.Enemy][0];

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