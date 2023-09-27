using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigAudio : MonoBehaviour
{

    public AudioClip pigSnore, pigDead, pigHert;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Snore() {
        audioSource.clip = pigSnore; 
        audioSource.Play();
    }

    public void Dead() {
        audioSource.clip = pigDead; 
        audioSource.Play();
    }

    public void Hert() {
        audioSource.clip = pigHert; 
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
