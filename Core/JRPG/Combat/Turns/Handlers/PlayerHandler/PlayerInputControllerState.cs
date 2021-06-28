using System.Collections;
using Core.State;
using UnityEngine;

namespace Core.JRPG.Combat.Turns.Handlers.PlayerHandler
{
    public abstract class PlayerInputControllerState : IState
    {
        private readonly Camera _camera;
        protected readonly PlayerTurnHandler PlayerController;
        protected bool _onHoverRaycasts;

        public PlayerInputControllerState(PlayerTurnHandler playerTurnHandler)
        {
            _camera = Camera.main;
            PlayerController = playerTurnHandler;
        }

        private void HandleUserInput()
        {
            if (PlayerController.busy)
            {
                return;
            }

            if (Input.GetMouseButtonDown(1))
            {
                HandleRightMouseButtonDown();
                return;
            }

            if (_onHoverRaycasts && Physics.Raycast(GetMouseRay(), out RaycastHit hit))
            {
                OnHoverRaycast(hit);
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(GetMouseRay(), out RaycastHit hit2))
                {
                    PlayerController.StartCoroutine(HandleLeftMouseButtonDown(hit2));
                }
            }
        }

        protected virtual void OnHoverRaycast(RaycastHit hit)
        {
        }

        protected abstract void HandleRightMouseButtonDown();
        protected abstract IEnumerator DoHandleLeftMouseButtonDown(RaycastHit hit);


        private IEnumerator HandleLeftMouseButtonDown(RaycastHit hit)
        {
            PlayerController.busy = true;
            yield return DoHandleLeftMouseButtonDown(hit);
            PlayerController.busy = false;
        }

        public virtual void OnEnter()
        {
        }

        public virtual void OnExit()
        {
        }

        public void Tick(float deltaTime) => HandleUserInput();
        private Ray GetMouseRay() => _camera.ScreenPointToRay(Input.mousePosition);
    }
}