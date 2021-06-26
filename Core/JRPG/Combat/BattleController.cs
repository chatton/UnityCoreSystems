using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Core.JRPG.Combat.Turns;
using Core.JRPG.Combat.Turns.Handlers;
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

        private EnemyTurnHandler _enemyTurnHandler;
        private PlayerTurnHandler _playerTurnHandler;

        private INextCombatantProvider _nextCombatantProvider;

        // private Dictionary<Team, List<Combatant>> _combatantDict;
        private Dictionary<Team, TurnHandler> _turnHandlersDict;


        private void Awake()
        {
            _turnHandlersDict = new Dictionary<Team, TurnHandler>();
            // _combatantDict = new Dictionary<Team, List<Combatant>>();
        }


        private IEnumerator Start()
        {
            _turnHandlersDict[Team.Player] = FindObjectOfType<PlayerTurnHandler>();
            _turnHandlersDict[Team.Enemy] = FindObjectOfType<EnemyTurnHandler>();

            // _enemyTurnHandler = FindObjectOfType<EnemyTurnHandler>();
            // _playerTurnHandler = FindObjectOfType<PlayerTurnHandler>();

            var players = playerParty.Create(playerTeamLocations);
            var enemies = enemyParty.Create(enemyLocations);

            _nextCombatantProvider = new InitiativeBasedNextCombatantProvider(players.Concat(enemies).ToList());

            while (true)
            {
                Combatant c = _nextCombatantProvider.NextCombatant();
                Debug.Log($"turn for {c.name}");
                yield return _turnHandlersDict[c.Team].TakeTurn();
                // Combatant player = players[Random.Range(0, 3)];
                // Combatant enemy = enemies[Random.Range(0, 3)];
                // yield return player.UseAbilityOn(enemy);

                // Combatant player2 = players[Random.Range(0, 3)];
                // Combatant enemy2 = enemies[Random.Range(0, 3)];
                // yield return enemy2.UseAbilityOn(player2);

                yield return null;
            }
        }
    }
}