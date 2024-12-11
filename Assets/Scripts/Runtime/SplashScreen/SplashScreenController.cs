using System;
using Runtime.Core;
using UnityEngine;

namespace Runtime.SplashScreen {
    public class SplashScreenController : MonoBehaviour {
        public float waitTime;

        private bool _done;
        private float _timer;

        private void Start() {
            _done = false;
        }

        private void Update() {
            _timer += Time.deltaTime;

            if (_timer > waitTime && !_done) {
                _done = true;
                GameManager.LoadScene(GameScene.MAIN_MENU);
            }
        }
    }
}