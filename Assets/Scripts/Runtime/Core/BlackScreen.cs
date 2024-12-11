using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Core {
    [RequireComponent(typeof(Image))]
    public class BlackScreen : MonoBehaviour {
        public IEnumerator FadeIn(float fadeTime, float padding = 0) {
            yield return new WaitForSecondsRealtime(padding);
            yield return Fade(Color.black, Color.clear, fadeTime);
        }
        
        public IEnumerator FadeOut(float fadeTime, float padding = 0) {
            yield return Fade(Color.clear, Color.black, fadeTime);
            yield return new WaitForSecondsRealtime(padding);
        }

        private IEnumerator Fade(Color from, Color to, float fadeTime) {
            Image image = GetComponent<Image>();
            float t = 0;

            while (t < fadeTime) {
                image.color = Color.Lerp(from, to, t / fadeTime);
                
                Debug.Log(t);
                
                t += Time.unscaledDeltaTime;
                yield return null;
            }

            image.color = to;
        }
    }
}
