using System;
using System.Collections;
using UnityEngine;

namespace Core.JRPG.Combat.Turns.Handlers
{
    public class PlayerTurnHandler : TurnHandler
    {
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public override IEnumerator TakeTurn(Combatant combatant)
        {
            while (true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit hit;
                    if (Physics.Raycast(GetMouseRay(), out hit))
                    {
                        Combatant target = hit.collider.GetComponent<Combatant>();
                        
                        // object was not a combatant
                        if (target == null)
                        {
                            continue;
                        }

                        // trying to cast on self, but cannot!
                        if (target == combatant && !combatant.GetSelectedAbility().canSelfCast)
                        {
                            continue;
                        }

                        // valid target selected
                        yield return combatant.UseAbilityOn(target);
                        yield break;
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