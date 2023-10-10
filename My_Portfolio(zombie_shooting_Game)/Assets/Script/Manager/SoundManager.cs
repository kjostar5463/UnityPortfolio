using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sound
{
    Shot,
    Reload,
    Hit,
    Jump
}

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource bgmAudio;
    [SerializeField] AudioSource playerAudio;

    [SerializeField] AudioClip[] playerClip;

    public static SoundManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    public void PlayerSound(Sound sound)
    {
        playerAudio.PlayOneShot(playerClip[(int)sound]);
    }
}