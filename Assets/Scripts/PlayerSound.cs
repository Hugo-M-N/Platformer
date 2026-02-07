using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip attackSound;
    public AudioClip swishSound;
    public AudioClip stepSound;

    public void playattackSound()
    {
        audioSource.PlayOneShot(attackSound);
    }

    public void playswishSound()
    {
        audioSource.PlayOneShot(swishSound);
    }
    public void playsstepsound()
    {
        audioSource.PlayOneShot(stepSound);
    }
}
