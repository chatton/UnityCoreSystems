using System;
using System.Collections;
using UnityEngine;

namespace Core.JRPG.Combat.Turns.Handlers
{
    public class HumanTurnHandler : TurnHandler
    {
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public override IEnumerator TakeTurn(Combatant combatant)
        {
            Debug.Log("Player phase!");
            while (true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit hit;
                    if (Physics.Raycast(GetMouseRay(), out hit))
                    {
                        Combatant target = hit.collider.GetComponent<Combatant>();
                        if (target != null)
                        {
                            yield return combatant.UseAbilityOn(target);
                            yield break;
                        }
                    }
                }

                yield return null;
            }
        }

        private Ray GetMouseRay()
        {
            return _camera.ScreenPointToRay(Input.mousePosition);
        }
    }
}