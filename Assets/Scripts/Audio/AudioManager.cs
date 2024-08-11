using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class AudioManager : MonoBehaviour
    {
        public AudioClip lightsOffClip; // Assign your AudioClip in the Inspector

        private AudioSource audioSource;

        void Awake()
        {
            // Make this object persistent across scenes
            DontDestroyOnLoad(gameObject);
            // Get the AudioSource component attached to the AudioManager
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayLightsOff()
        {
            // Play the assigned AudioClip globally
            audioSource.PlayOneShot(lightsOffClip);
        }
    }
}