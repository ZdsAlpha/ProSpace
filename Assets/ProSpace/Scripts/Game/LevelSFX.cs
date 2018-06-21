using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
public class LevelSFX : MonoBehaviour
{
    public AudioClip[] SFXs;

    private AudioSource Source = null;

    private void Start()
    {
        gameObject.AddComponent<AudioListener>();
        Source = gameObject.AddComponent<AudioSource>();
    }
    public void Play(int index)
    {
        PlaySound(index);
    }
    private void PlaySound(int index, float volume = 1f, float pitch = 1f)
    {
        Source.pitch = pitch;
        Source.PlayOneShot(SFXs[index], volume);
    }
}
