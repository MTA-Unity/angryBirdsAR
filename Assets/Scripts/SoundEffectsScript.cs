using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsScript : MonoBehaviour
{
    public AudioSource src;
    public AudioClip slinshotStrech, launch, military,theme,win,lose;

    public void SlinshotStrech()
    {
        src.clip = slinshotStrech;
        src.Play();
    }
    public void Shoot()
    {
        src.clip = launch;
        src.Play();
    }

    public void  Military()
    {
        src.clip = military;
        src.Play();
    }

    public void Win() {
        src.clip = win;
        src.Play();
    }

    public void Lose() {
        src.clip = lose;
        src.Play();
    }

    public void Theme() {
        src.clip = theme;
        src.Play();
    }
}
