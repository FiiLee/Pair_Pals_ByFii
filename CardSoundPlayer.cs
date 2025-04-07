using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSoundPlayer : MonoBehaviour
{
    public static CardSoundPlayer Instance;

    [SerializeField] private AudioClip matchSound;
    [SerializeField] private AudioClip mismatchSound;

    private AudioSource audioSource;

    private void Awake()
    {
        // Singleton pattern so you can access from anywhere
        if (Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMatchSound()
    {
        if (matchSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(matchSound);
        }
    }

    public void PlayMismatchSound()
    {
        if (mismatchSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(mismatchSound);
        }
    }
}

