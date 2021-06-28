using System.Collections;
using UnityEngine;

namespace Core.JRPG.Combat.Turns.Handlers.PlayerHandler
{
    public class UseAbilityPlayerHandlerState : PlayerInputControllerState
    {
        public UseAbilityPlayerHandlerState(PlayerTurnHandler playerTurnHandler) : base(playerTurnHandler)
        {
        }

        protected override void HandleRightMouseButtonDown()
        {
        }

        protected override IEnumerator DoHandleLeftMouseButtonDown(RaycastHit hit)
        {
            Combatant target = hit.collider.GetComponent<Combatant>();

            // object was not a combatant
            if (target == null)
            {
                yield break;
            }

            // trying to cast on self, but cannot!
            if (target == PlayerController.selectedCombatant &&
                !PlayerController.selectedCombatant.GetSelectedAbility().canSelfCast)
            {
                yield break;
            }

            // valid target selected
            yield return PlayerController.selectedCombatant.UseAbilityOn(target);
        }
    }
}