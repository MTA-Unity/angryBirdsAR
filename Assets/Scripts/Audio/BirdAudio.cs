using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAudio : MonoBehaviour
{

    public AudioClip birdFly, birdDead;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Fly() {
        audioSource.clip = birdFly;
        audioSource.Play();
    }

    public void Dead() {
        audioSource.clip = birdDead;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
