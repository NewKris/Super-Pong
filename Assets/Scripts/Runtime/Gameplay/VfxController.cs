using UnityEngine;
using UnityEngine.VFX;

namespace Runtime.Gameplay {
    public class VfxController : MonoBehaviour {
        public VisualEffect goalConfetti;
        public VisualEffect hitDust;

        public void PlayGoalVfx(Vector3 position) {
            goalConfetti.SetVector3("Position", position);
            goalConfetti.Play();
        }

        public void PlayHitDust(Vector3 position, float angle) {
            hitDust.SetVector3("Position", position);
            hitDust.transform.localRotation = Quaternion.Euler(0, 0, angle - 90);
            hitDust.Play();
        }
    }
}