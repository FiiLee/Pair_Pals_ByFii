using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class SoundToggleButton : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;

    [Header("Button UI")]
    public UnityEngine.UI.Image toggleButtonImage;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    private bool isSoundOn = true;

    private void Start()
    {
        isSoundOn = PlayerPrefs.GetInt("SoundState", 1) == 1;
        ApplySoundSettings();
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        PlayerPrefs.SetInt("SoundState", isSoundOn ? 1 : 0);
        ApplySoundSettings();
    }

    private void ApplySoundSettings()
    {
        if (audioSource != null)
        {
            audioSource.mute = !isSoundOn;
        }

        if (toggleButtonImage != null)
        {
            toggleButtonImage.sprite = isSoundOn ? soundOnSprite : soundOffSprite;
        }
    }
}