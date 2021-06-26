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
            var players = playerParty.Create(playerTeamLocations);
            var enemies = enemyParty.Create(enemyLocations);


            while (true)
            {
                Combatant player = players[Random.Range(0, 3)];
                Combatant enemy = enemies[Random.Range(0, 3)];
                yield return player.UseAbilityOn(enemy);
                
                Combatant player2 = players[Random.Range(0, 3)];
                Combatant enemy2 = enemies[Random.Range(0, 3)];
                yield return enemy2.UseAbilityOn(player2);
                
                yield return null;
            }
        }
    }
}