using System;
using UnityEngine;

namespace Runtime.Gameplay {
    public class Goal : MonoBehaviour {
        public static event Action<PlayerSide> OnGoalScored; 
        
        public PlayerSide scoreForPlayer;

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Ball")) {
                OnGoalScored?.Invoke(scoreForPlayer);
            }
        }
    }
}