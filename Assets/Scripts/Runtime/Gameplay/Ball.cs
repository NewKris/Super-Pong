using System;
using UnityEngine;

namespace Runtime.Gameplay {
    public class Ball : MonoBehaviour {
        public LayerMask dustSurfaces;
        public VfxController vfxController;
        public AudioSource hitSound;
        
        public void SetPosition(Vector2 position) {
            transform.position = position;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        public void Putt(Vector2 force) {
            GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (IsValidLayer(other.gameObject.layer)) {
                ContactPoint2D contact = other.GetContact(0);
                float angle = Mathf.Atan2(contact.normal.y, contact.normal.x) * Mathf.Rad2Deg;
                
                vfxController.PlayHitDust(contact.point, angle);
                hitSound.Play();
            }
        }

        private bool IsValidLayer(int layer) {
            return (dustSurfaces & (1 << layer)) != 0;
        }
    }
}