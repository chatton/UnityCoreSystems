using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Core.JRPG.Combat.Turns;
using Core.JRPG.Combat.Turns.Handlers;
using Core.JRPG.Combat.Turns.Handlers.PlayerHandler;
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

        // _aiTurnHandler will handle all decisions made by the AI for which actions should be used by the enemy.
        private AITurnHandler _aiTurnHandler;

        // _humanTurnHandler will wait for user inputs to make decisions.
        private PlayerTurnHandler _playerTurnHandler;

        // _nextCombatantProvider returns the next Combatant who should act.
        private INextCombatantProvider _nextCombatantProvider;

        private Dictionary<Team, TurnHandler> _turnHandlersDict;


        private void Awake()
        {
            _turnHandlersDict = new Dictionary<Team, TurnHandler>();
        }

        private IEnumerator Start()
        {
            _turnHandlersDict[Team.Player] = FindObjectOfType<PlayerTurnHandler>();
            _turnHandlersDict[Team.Enemy] = FindObjectOfType<AITurnHandler>();


            IEnumerable<Combatant> players = playerParty.Create(playerTeamLocations);
            IEnumerable<Combatant> enemies = enemyParty.Create(enemyLocations);

            _nextCombatantProvider = new InitiativeBasedNextCombatantProvider(players.Concat(enemies).ToList());

            while (true)
            {
                Combatant c = _nextCombatantProvider.NextCombatant();
                Debug.Log($"turn for {c.name}");
                yield return _turnHandlersDict[c.Team].TakeTurn(c);
            }
        }
    }
}