using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.Core {
    public class GameManager : MonoBehaviour {
        private static bool Busy;
        private static GameManager Instance;
        
        public static void LoadScene(string gameScene) {
            if (Busy) return;

            int buildIndex = gameScene switch {
                "Main Menu" => 1,
                "Gameplay" => 2,
                "Exit Game" => 3,
                _ => 0
            };

            Instance.StartCoroutine(Instance.LoadSceneAsync(buildIndex));
        }
        
        public static void LoadScene(GameScene gameScene) {
            if (Busy) return;
            
            Instance.StartCoroutine(Instance.LoadSceneAsync((int)gameScene));
        }

        public static void ExitGame() {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else {
                Destroy(gameObject);
            }
        }

        private void OnDestroy() {
            if (Instance == this) {
                Instance = null;
                Busy = false;
            }
        }
        
        private IEnumerator LoadSceneAsync(int gameScene) {
            Busy = true;
            BlackScreen blackScreen = GetComponentInChildren<BlackScreen>();
            
            yield return blackScreen.FadeOut(0.2f, 0.1f);

            yield return SceneManager.LoadSceneAsync(gameScene);
            
            yield return blackScreen.FadeIn(0.2f);
            Busy = false;
        }
    }
}