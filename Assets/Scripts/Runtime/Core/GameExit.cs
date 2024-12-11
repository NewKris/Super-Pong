using System;
using UnityEngine;

namespace Runtime.Core {
    public class GameExit : MonoBehaviour {
        private void Start() {
            GameManager.ExitGame();
        }
    }
}
