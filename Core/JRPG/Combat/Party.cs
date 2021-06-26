using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.JRPG.Combat
{
    public class Party : MonoBehaviour
    {
        public Team team;
        public List<Combatant> combatantPrefabs;
        
        public void Create(List<Transform> positions)
        {
            Assert.IsTrue(combatantPrefabs.Count <= positions.Count);

            for (int i = 0; i < combatantPrefabs.Count; i++)
            {
                Combatant c = Instantiate(combatantPrefabs[i], transform);
                c.transform.position = positions[i].position;
            }
        }
    }
}