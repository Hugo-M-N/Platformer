using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip attackSound;
    public AudioClip swishSound;
    public AudioClip musketSound;

    public void playattackSound()
    {
       audioSource.PlayOneShot(attackSound);
    }

    public void playswishSound()
    {
        audioSource.PlayOneShot(swishSound);
    }
    public void playsmusketSound()
    {
        audioSource.PlayOneShot(musketSound);
    }
}
