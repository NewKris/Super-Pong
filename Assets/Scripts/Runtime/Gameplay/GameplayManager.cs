using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;

namespace Runtime.Gameplay {
    public class GameplayManager : MonoBehaviour {
        [Header("UI")]
        public TextMeshProUGUI player1ScoreDisplay;
        public TextMeshProUGUI player2ScoreDisplay;
        public TextMeshProUGUI roundCountDown;

        [Header("Game Objects")] 
        public Ball ball;
        public Vector2 puttStrength;
        
        [Header("VFX")]
        public Transform player1ConfettiPosition;
        public Transform player2ConfettiPosition;
        public VfxController vfxController;

        private int _player1Score;
        private int _player2Score;
        private PlayerSide _previousWinner;
        
        private void Start() {
            Goal.OnGoalScored += EndRound;
            StartRound();
        }

        private void OnDestroy() {
            Goal.OnGoalScored -= EndRound;
        }

        private void EndRound(PlayerSide winner) {
            _previousWinner = winner;
            AddScore(winner);

            Vector3 confettiPosition = winner == PlayerSide.PLAYER_1 
                    ? player1ConfettiPosition.position 
                    : player2ConfettiPosition.position;
            
            vfxController.PlayGoalVfx(confettiPosition);
            
            roundCountDown.text = "SCORE!";
            Invoke(nameof(StartRound), 3);
        }

        private void AddScore(PlayerSide forPlayer) {
            if (forPlayer == PlayerSide.PLAYER_1) {
                _player1Score++;
            }
            else {
                _player2Score++;
            }
            
            UpdateScoreDisplays();
        }

        private void UpdateScoreDisplays() {
            player1ScoreDisplay.text = $"Score: {_player1Score}";
            player2ScoreDisplay.text = $"Score: {_player2Score}";
        }

        private void StartRound() {
            StartCoroutine(CountDownRound(3));
        }

        private IEnumerator CountDownRound(int countDown) {
            roundCountDown.text = "";
            ball.SetPosition(Vector2.zero);
            
            yield return new WaitForSeconds(1);

            int timer = countDown;
            while (timer > 0) {
                roundCountDown.text = timer.ToString();
                timer--;
                yield return new WaitForSeconds(1);
            }
            
            roundCountDown.text = "GO!";
            yield return new WaitForSeconds(1);
            
            roundCountDown.text = "";

            Vector2 force = puttStrength;
            force.x *= _previousWinner == PlayerSide.PLAYER_1 ? 1 : -1;
            ball.Putt(force);
        }
        
    }
}
