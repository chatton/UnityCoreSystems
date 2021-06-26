using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.JRPG.Combat
{
    public class Party : MonoBehaviour
    {
        public Team team;
        public List<Combatant> combatantPrefabs;

        public List<Combatant> Create(List<Transform> positions)
        {
            Assert.IsTrue(combatantPrefabs.Count <= positions.Count);
            List<Combatant> combatants = new List<Combatant>();
            for (int i = 0; i < combatantPrefabs.Count; i++)
            {
                Combatant c = Instantiate(combatantPrefabs[i]);
                c.transform.position = positions[i].position;
                combatants.Add(c);
            }

            return combatants;
        }
    }
}