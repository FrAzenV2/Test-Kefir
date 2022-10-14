using System;
using UnityEngine;

namespace Source.Scripts.Components.UnityComponents
{
    public class TriggerListener : MonoBehaviour
    {
        public event Action<GameObject> OnTriggerEntered;
        public event Action<GameObject> OnTriggerExited;

        private void OnTriggerEnter(Collider other)
        {
            OnTriggerEntered?.Invoke(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            OnTriggerExited?.Invoke(other.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnTriggerEntered?.Invoke(other.gameObject);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            OnTriggerExited?.Invoke(other.gameObject);
        }
    }
}