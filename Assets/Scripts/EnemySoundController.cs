using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip attackSound;
    public AudioClip swishSound;
    public AudioClip swordSound;
    public AudioClip musketSound;
    public AudioClip hitSound;
    public AudioClip ouchSound;
    public AudioClip bonkSound;

    public void playattackSound()
    {
       audioSource.PlayOneShot(attackSound);
    }

    public void playswishSound()
    {
        audioSource.PlayOneShot(swishSound);
    }
    public void playswordSound()
    {
        audioSource.PlayOneShot(swordSound);
    }
    public void playsmusketSound()
    {
        audioSource.PlayOneShot(musketSound);
    }
    public void playhitSound()
    {
        audioSource.PlayOneShot(hitSound);
    }
    public void playouchSound()
    {
        audioSource.PlayOneShot(ouchSound);
    }
    public void playbonkSound()
    {
        audioSource.PlayOneShot(bonkSound);
    }
}
