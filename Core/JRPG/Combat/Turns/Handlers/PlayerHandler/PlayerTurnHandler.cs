using System;
using System.Collections;
using Core.State;
using UnityEngine;

namespace Core.JRPG.Combat.Turns.Handlers.PlayerHandler
{
    public class PlayerTurnHandler : TurnHandler
    {
        private Camera _camera;
        // private PlayerInputController _inputController;

        internal Combatant selectedCombatant;
        internal bool actionTaken;
        internal bool actionFinished;
        internal bool busy;
        private StateMachine _stateMachine;

        private void Awake()
        {
            _camera = Camera.main;
            _stateMachine = BuildStateMachine();
        }

        internal void BlockingCoroutine(Func<IEnumerator> enumerator)
        {
            if (busy)
            {
                return;
            }

            busy = true;
            StartCoroutine(enumerator.Invoke());
            busy = false;
        }

        public override IEnumerator TakeTurn(Combatant combatant)
        {
            selectedCombatant = combatant;
            while (true)
            {
                _stateMachine.Tick(Time.deltaTime);
                yield return null;
            }
        }

        private StateMachine BuildStateMachine()
        {
            UseAbilityPlayerHandlerState useAbilityPlayerHandlerState = new UseAbilityPlayerHandlerState(this);
            StateMachine sm = new StateMachine();
            sm.SetState(useAbilityPlayerHandlerState);
            return sm;
        }
    }
}