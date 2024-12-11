using System;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Gameplay {
    public class PauseMenu : MonoBehaviour {
        public Button firstSelectedButton;
        
        public void ToggleActive() {
            gameObject.SetActive(!gameObject.activeSelf);
        }
        
        private void OnEnable() {
            Time.timeScale = 0;
            firstSelectedButton.Select();
        }

        private void OnDisable() {
            Time.timeScale = 1;
        }
    }
}
