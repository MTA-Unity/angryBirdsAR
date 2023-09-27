using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAudio : MonoBehaviour
{
    public AudioClip rolling, damange;
    private AudioSource audioSource;
    private bool isPlaying = false;
    private float duration = 0.5f;
    private float timer = 0.0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isPlaying)
        {
            timer += Time.deltaTime;

            if (timer >= duration)
            {
                // Stop playing the sound effect after one second
                audioSource.Stop();
                isPlaying = false;
            }
        }
    }

    public void Roll() {
        audioSource.clip = rolling;
        if (!isPlaying)
            {
                // Play the sound effect
                audioSource.Play();
                isPlaying = true;
                timer = 0.0f;
            }
    }

    public void Damage() {
        audioSource.clip = damange;
        audioSource.Play();
    }
}
