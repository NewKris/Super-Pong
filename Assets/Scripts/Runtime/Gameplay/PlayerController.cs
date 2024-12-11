using System;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Gameplay {
    public class PlayerController : MonoBehaviour {
        public PlayerSide playerSide;

        public UnityEvent onPause;
        
        [Header("Moving")] 
        public float maxMoveSpeed;
        public float maxAcceleration;
        public float dashStrength;

        private PlayerControls _controls;
        private Rigidbody2D _rigidBody;

        private void Awake() {
            _controls = new PlayerControls();
            _rigidBody = GetComponent<Rigidbody2D>();

            _controls.Player1Controls.Pause.performed += _ => onPause.Invoke();
            
            if (playerSide == PlayerSide.PLAYER_1) {
                _controls.Player1Controls.Enable();
            }
            else {
                _controls.Player2Controls.Enable();
            }
        }

        private void OnDestroy() {
            _controls.Dispose();
        }

        private void Update() {
            float move = ReadMovement();
            float deltaVelocity = (move * maxMoveSpeed) - _rigidBody.velocity.y;
            deltaVelocity = Mathf.Clamp(deltaVelocity, -maxAcceleration, maxAcceleration);
            
            _rigidBody.AddForce(Vector2.up * deltaVelocity, ForceMode2D.Impulse);
        }

        private float ReadMovement() {
            return playerSide == PlayerSide.PLAYER_1 
                ? _controls.Player1Controls.Move.ReadValue<float>() 
                : _controls.Player2Controls.Move.ReadValue<float>();
        }
    }
}
