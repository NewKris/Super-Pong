using UnityEngine;

namespace Runtime.Core {
    public class SceneController : MonoBehaviour {
        public void LoadScene(string gameScene) {
            GameManager.LoadScene(gameScene);
        }
    }
}