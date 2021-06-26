using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.JRPG.Combat
{
    public class BattleArena
    {
        private readonly List<Transform> playerTeamLocations;
        private readonly List<Transform> enemyLocations;

        public BattleArena(List<Transform> playerTeamLocations, List<Transform> enemyLocations)
        {
            Assert.IsNotNull(playerTeamLocations);
            Assert.IsNotNull(enemyLocations);
            this.playerTeamLocations = new List<Transform>(playerTeamLocations);
            this.enemyLocations = new List<Transform>(enemyLocations);
        }

        /// <summary>
        /// Assigns every combatant to the correct position in the grid based on their team. 
        /// </summary>
        /// <param name="combatants"></param>
        public void FillPositions(IEnumerable<Combatant> combatants)
        {
            var playerIndex = 0;
            var enemyIndex = 0;
            foreach (var c in combatants)
            {
                if (c.Team == Team.Player)
                {
                    c.transform.position = playerTeamLocations[playerIndex].transform.position;
                    playerIndex++;
                    continue;
                }

                if (c.Team == Team.Enemy)
                {
                    c.transform.position = enemyLocations[enemyIndex].transform.position;
                    enemyIndex++;
                }
            }
        }
    }
}